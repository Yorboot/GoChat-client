using System;
using System.Windows.Input;
using DynamicData;
using ReactiveUI;

namespace GoChat.ViewModels;

public class MainWindowViewModel: PageViewModelBase
{
    public MainWindowViewModel()
    {
        CurrentPage = _pages[0];
        var canNavForward = this.WhenAnyValue(x => x.CurrentPage.CanNavigateForward);
        var canNavBack = this.WhenAnyValue(x => x.CurrentPage.CanNavigateBack);
    }

    private readonly PageViewModelBase[] _pages = 
    {
        LoginViewModel.Instance,
        MainViewModel.Instance
    };
    private PageViewModelBase _CurrentPage;

    public PageViewModelBase CurrentPage
    {
        get => _CurrentPage; 
        private set => this.RaiseAndSetIfChanged(ref _CurrentPage, value);
    }
    public ICommand NavigateForwardCommand { get; }

    private void NavigateForward()
    {
        var index = _pages.IndexOf(CurrentPage)+1;
        if (index > _pages.Length)
        {
            throw new IndexOutOfRangeException("Cannot navigate forward, already at the last page");
        }
        CurrentPage = _pages[index];
    }
    public ICommand NavigateBackCommand { get; }
    private void NavigateBack()
    {
        var index = _pages.IndexOf(CurrentPage)-1;
        if (index < 0)
        {
            throw new IndexOutOfRangeException("Cannot navigate back, already at the first page");
        }
        CurrentPage = _pages[index];
    }
    
}