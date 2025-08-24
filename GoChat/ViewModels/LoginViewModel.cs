using System;
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
        var canNavNext = this.WhenAnyValue(x => CurrentPage.CanNavigateNext);
        var canNavigatePrevious = this.WhenAnyValue(x => CurrentPage.CanNavigatePrevious);
        NavigateNextCommand = ReactiveCommand.Create(NavigateNext, canNavNext);
        NavigatePreviousCommand = ReactiveCommand.Create(NavigatePrevious, canNavigatePrevious);
        _currentPage = Pages[0];
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
    private bool _canNavigateNext = true;
    public override bool CanNavigateNext
    {
        get { return _canNavigateNext; }
        protected set { this.RaiseAndSetIfChanged(ref _canNavigateNext, value); }
    }

    private readonly PageViewModelBase[] Pages =
    {
        new LoginViewModel(),
        new MainViewModel(),
    };
    private PageViewModelBase _currentPage;

    public PageViewModelBase CurrentPage
    {
        get { return _currentPage; }
        private set { this.RaiseAndSetIfChanged(ref _currentPage, value); }
    }
    public ICommand NavigateNextCommand { get; }

    private void NavigateNext()
    {
        var index = Pages.IndexOf(CurrentPage) +1;
        if (index >= Pages.Length)
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
        CanNavigateNext = LoginView.LoginSuccess();
    }
}