using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using DynamicData;
using ReactiveUI;
using GoChat.Views;
namespace GoChat.ViewModels;

public class LoginViewModel: PageViewModelBase
{
    private string? _username;
    private string? _password;
    public LoginViewModel()
    {
        Console.WriteLine("In LoginViewModel constructor");
        // var test = CurrentPage.CanNavigateNext;
        // var canNavNext = this.WhenAnyValue(x => CurrentPage.CanNavigateNext);
        // Console.WriteLine(canNavNext);
        // var canNavigatePrevious = this.WhenAnyValue(x => CurrentPage.CanNavigatePrevious);
        // NavigateNextCommand = ReactiveCommand.Create(NavigateNext, canNavNext);
        // NavigatePreviousCommand = ReactiveCommand.Create(NavigatePrevious, canNavigatePrevious);
        // this.WhenAnyValue(x => x.Username, x => x.Password).Subscribe(_ => UpdateCanNavigateNext());
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
    private bool _canNavigateNext = true;
    public override bool CanNavigateNext
    {
        get { return _canNavigateNext; }
        protected set { this.RaiseAndSetIfChanged(ref _canNavigateNext, value); }
    }

    private readonly List<PageViewModelBase> Pages;
    private static PageViewModelBase _currentPage;

    public static PageViewModelBase CurrentPage
    {
        get => _currentPage;
        set => _currentPage = value;
    }
    public ICommand NavigateNextCommand { get; }

    private void NavigateNext()
    {
        var index = Pages.IndexOf(CurrentPage) +1;
        if (index >= Pages.Count)
        {
            index = 0;
        }
        CurrentPage = Pages[index];
    }
    public ICommand NavigatePreviousCommand { get; }

    private void NavigatePrevious()
    {
        var index = Pages.IndexOf(CurrentPage) -1;
        if (index < 0)
        {
            index = 0;
        }
        CurrentPage = Pages[index];
    }
    public override bool CanNavigatePrevious
    {
        get => true;
        protected set => throw new NotSupportedException();
    }

    private void UpdateCanNavigateNext()
    {
        if (CurrentPage is LoginViewModel loginView)
        {
            CanNavigateNext = LoginView.LoginSuccess();
            CanNavigateNext = loginView.CanNavigateNext;
            Console.WriteLine(CanNavigateNext);
        }
        else
        {
            CanNavigateNext = false;
            Console.WriteLine("The window is not a LoginViewModel");
        }
        
        
    }
}