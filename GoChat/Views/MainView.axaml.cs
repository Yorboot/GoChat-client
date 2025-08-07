using System;
using Avalonia.Controls;
using Avalonia.Input;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace GoChat.Views;


public partial class MainView : UserControl
{
    static string host = "http://127.0.0.1:8080"; // Change to your host:port
    static string endpoint = "/";         // Optional endpoint
    string url = host + endpoint;
    
    static string _message = string.Empty;
    private static string json = string.Empty;
    public MainView()
    {
        InitializeComponent();
        
    }

    private void SetText()
    {
        if (this.Message.Text != null || _message != string.Empty)
        {
            _message = Message.Text;
            json = @"{
            ""from"": ""user1"",
            ""message"": ""$" + _message + @""",
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

            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response:");
            Console.WriteLine(responseBody);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending request:");
            Console.WriteLine(ex.Message);
        }
    }
}