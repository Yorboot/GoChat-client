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
    
    static string _message = string.Empty;
    private static string json = string.Empty;
    public MainView()
    {
        InitializeComponent();
        conn = database.InitDb();
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
                List<string> messages = database.getMessages(conn);
                foreach (string message in messages)
                {
                    if (_message != string.Empty)
                    {
                        Console.WriteLine($"{message}");
                    }
                }
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
}