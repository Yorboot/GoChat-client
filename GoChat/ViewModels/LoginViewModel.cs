using System;
using System.ComponentModel.DataAnnotations;
using ReactiveUI;
using GoChat.Views;
namespace GoChat.ViewModels;

public class LoginViewModel: PageViewModelBase
{
    private string? _Username;
    private string? _Password;
    public LoginViewModel()
    {
        this.WhenAnyValue(x => x.Username, x => x.Password)
    }

    [Required]
    public string? Username
    {
        //change this when login logic is correctly implemented
        get { return _Username; }
        set { this.RaiseAndSetIfChanged(ref _Username, value); }
    }

    [Required]
    public string? Password
    {
        get { return _Password; }
        set {this.RaiseAndSetIfChanged(ref _Password, value); }
    }
    public string Title => "Login";
    private bool canNavigateNext;
    public override bool CanNavigateNext
    {
        get => CanNavigateNext;
        protected set
        {
            throw new NotSupportedException(); 
        }
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