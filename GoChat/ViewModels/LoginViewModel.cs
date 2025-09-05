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
        this.WhenAnyValue(x => x.Username, x => !string.IsNullOrWhiteSpace(x.))
            .Subscribe(x => CanNavigateForward = x);
    }

    private string? _username;

    [Required]
    public string? Username
    {
        get
        {
            if(_username == null) throw new NullReferenceException("Username is not set");
            return _username;
        }
        set {this.RaiseAndSetIfChanged(ref _username, value);}
    }

}