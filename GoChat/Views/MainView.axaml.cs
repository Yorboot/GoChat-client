using System;
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
    static string host = "http://127.0.0.1:8080"; // Change to your host:port
    static string endpoint = "/";         // Optional endpoint
    string url = host + endpoint;
    
    static string _message = string.Empty;
    private static string json = string.Empty;
    public MainView()
    {
        InitializeComponent();
        database.InitDb();
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