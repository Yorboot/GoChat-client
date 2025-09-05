using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using DynamicData;
using ReactiveUI;
using GoChat.Views;
namespace GoChat.ViewModels;

public class LoginViewModel:PageViewModelBase
{
    private static readonly Lazy<LoginViewModel> _instance = new(() => new LoginViewModel());
    public static LoginViewModel Instance => _instance.Value;
    public LoginViewModel()
    {
    }

}