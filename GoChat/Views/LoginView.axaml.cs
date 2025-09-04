using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GoChat.ViewModels;
using ReactiveUI;

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