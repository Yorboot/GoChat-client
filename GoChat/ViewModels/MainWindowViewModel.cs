using System.Windows.Input;
using DynamicData;
using ReactiveUI;

namespace GoChat.ViewModels;

public class MainWindowViewModel: ViewModelBase
{
    public MainWindowViewModel()
    {
        _currentPage = Pages[0];
    }

    private readonly PageViewModelBase[] Pages =
    {
        new MainViewModel(),
        new LoginViewModel(),
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
}