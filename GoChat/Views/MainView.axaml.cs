using System;
using Avalonia.Controls;
using Avalonia.Input;
namespace GoChat.Views;

public partial class MainView : UserControl
{
    string _message = string.Empty;
    public MainView()
    {
        InitializeComponent();
        
    }

    private void SetText()
    {
        if (this.Message.Text != null || _message != string.Empty)
        {
            _message = Message.Text;
        }
    }
    private void SendData(object? sender, KeyEventArgs e)
    {
        SetText();
        if (e.Key == Key.Enter)
        {
            Console.WriteLine($"Message sent {_message}");
        }
    }
}