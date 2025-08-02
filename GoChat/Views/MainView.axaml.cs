using Avalonia.Controls;

namespace GoChat.Views;

public partial class MainView : UserControl
{
    string _message = string.Empty;
    public MainView()
    {
        InitializeComponent();
        if (this.Message.Text != null || _message != string.Empty)
        {
            _message = Message.Text;
        }
        
    }

    public void SendData()
    {
        
    }
}