using CommunityToolkit.Mvvm.ComponentModel;

namespace GoChat.ViewModels;

public class MainViewModel: PageViewModelBase
{
    private bool _canNavigateNext = false;
    private bool _canNavigatePrevious;

    public override bool CanNavigateNext
    {
        get { return _canNavigateNext;} 
        protected set { _canNavigateNext = value; }
    }

    public override bool CanNavigatePrevious
    {
        get { return _canNavigatePrevious; }
        protected set { _canNavigatePrevious = value; }
    }
}