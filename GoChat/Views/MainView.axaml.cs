using System;
using System.Collections.Generic;
using System.Net;
using Avalonia.Controls;
using Avalonia.Input;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GoChat.Database;

namespace GoChat.Views;


public partial class MainView : UserControl
{   
    static Database.Database database = new Database.Database();
    MySql.Data.MySqlClient.MySqlConnection conn;
    static string host = "http://127.0.0.1:8080";
    static string endpoint = "/";
    string url = host + endpoint;
    private HashSet<string> _messages = new();
    static string _message = string.Empty;
    private static string json = string.Empty;
    public MainView()
    {
        InitializeComponent();
        conn = database.InitDb();
        PrintAllMessages();
    }

    private void SetText()
    {
        if (this.Message.Text != null || _message != string.Empty)
        {
            _message = Message.Text;
            json = @"{
            ""userId"": ""1"",
            ""content"": """ + _message + @"""
        }";
        }
    }
    void sendData(object sender, KeyEventArgs e)
    {

        _ = SendData(sender,e);
    }
    private async Task SendData(object? sender, KeyEventArgs e)
    {
        SetText();
        if (e.Key == Key.Enter)
        {
            await SendRequest();
        }
    }

    private async Task SendRequest()
    {
        Console.WriteLine($"Message sent {_message}");
        using var httpClient = new HttpClient();
        try
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);

            Console.WriteLine($"Status: {response.StatusCode}");

            var responseStatus = (response.StatusCode == HttpStatusCode.OK);
            if (responseStatus)
            {
                PrintMessagesAfter();
            }
            Console.WriteLine("Response:");
            Console.WriteLine(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending request:");
            Console.WriteLine(ex.Message);
        }
    }
    private void PrintMessagesAfter()
    {
        Dictionary<int,string> messages = database.getMessagesAfter(conn);
        if (messages == null || messages.Count == 0)
        {
            return;
        }

        int maxId = database.LastMessageId;
        foreach (KeyValuePair<int,string> pair in messages)
        {
            if (pair.Value != string.Empty)
            {
                Console.WriteLine($"{pair.Value}");
                var tb = new TextBlock
                {
                    Text = pair.Value,
                    FontSize = 16,
                };
                MessageContainer.Children.Add(tb);
                if(pair.Key > maxId)
                    maxId = pair.Key;
            }
            
        }
        database.LastMessageId = maxId;

    }

    private void PrintAllMessages()
    {
        Dictionary<int, string> messages = database.getAllMessages(conn);
        int index = 0;
        foreach (KeyValuePair<int, string> pair in messages)
        {
            index += 1;
            if (pair.Value != string.Empty)
            {
                if (_messages.Add(pair.Value))
                {
                    var tb = new TextBlock
                    {
                        Text = pair.Value,
                        FontSize = 16,
                    };
                    MessageContainer.Children.Add(tb);
                }
            }

            if (index == messages.Count)
            {
                Console.WriteLine($"set last id to{pair.Key}");
                database.LastMessageId = pair.Key;
            }
        }
    }

    private void PreventDuplicateMessages(string message)
    {
        foreach (var child in MessageContainer.Children)
        {
            if (child is TextBlock textBlock && textBlock.Text == message)
            {
                return;
            }
        }
    }
}