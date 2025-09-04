using ReactiveUI;

namespace GoChat.ViewModels;

public class MainWindowViewModel: PageViewModelBase
{
    public MainWindowViewModel()
    {
        
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
    
}