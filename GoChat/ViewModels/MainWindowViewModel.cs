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
}