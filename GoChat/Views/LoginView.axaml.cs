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
    private static readonly LoginViewModel Vm = new LoginViewModel();
    public LoginView()
    {
        Pages = new()
        {
            Vm,
            new MainViewModel()
        };
        DataContext = Vm;
        // Pages.Add(Vm);
        // Pages.Add(new MainViewModel());
        LoginViewModel.CurrentPage = Pages[0];
        InitializeComponent();
    }

    private readonly List<PageViewModelBase> Pages;
    public void SetNextPage(object sender, RoutedEventArgs e)
    {
        Console.WriteLine(LoginSuccess());
        var index = Pages.IndexOf(LoginViewModel.CurrentPage) + 1;
        if (index < Pages.Count&& LoginSuccess())
        {
            LoginViewModel.CurrentPage = Pages[index];
           LoginViewModel.CurrentPage= LoginViewModel.CurrentPage;
        }
    }

    public static bool LoginSuccess()
    {
        return true;
    }
}