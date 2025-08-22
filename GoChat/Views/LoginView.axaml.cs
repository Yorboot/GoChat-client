using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GoChat.Views;

public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent();
    }

    public static bool LoginSuccess()
    {
        return true;
    }
}