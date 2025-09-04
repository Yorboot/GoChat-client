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
}