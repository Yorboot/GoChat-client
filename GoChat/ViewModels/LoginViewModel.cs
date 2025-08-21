using System;
using System.ComponentModel.DataAnnotations;
using ReactiveUI;
using GoChat.Views;
namespace GoChat.ViewModels;

public class LoginViewModel: PageViewModelBase
{
    private string? _username;
    private string? _password;
    public LoginViewModel()
    {
        this.WhenAnyValue(x => x.Username, x => x.Password).Subscribe(_ => UpdateCanNavigateNext());
    }

    [Required]
    public string? Username
    {
        //change this when login logic is correctly implemented
        get { return _username; }
        set { this.RaiseAndSetIfChanged(ref _username, value); }
    }

    [Required]
    public string? Password
    {
        get { return _password; }
        set {this.RaiseAndSetIfChanged(ref _password, value); }
    }
    public string Title => "Login";
    private bool _canNavigateNext;
    public override bool CanNavigateNext
    {
        get { return _canNavigateNext; }
        protected set { this.RaiseAndSetIfChanged(ref _canNavigateNext, value); }
    }

    public override bool CanNavigatePrevious
    {
        get => true;
        protected set => throw new NotSupportedException();
    }

    private void UpdateCanNavigateNext()
    {
        CanNavigateNext = LoginView.LoginSuccess();
    }
}