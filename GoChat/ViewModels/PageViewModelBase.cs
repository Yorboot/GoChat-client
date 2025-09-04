using System.Runtime.CompilerServices;

namespace GoChat.ViewModels;

public abstract class PageViewModelBase: ViewModelBase
{
    //Gets if the user can navigate forward or back
    public abstract bool CanNavigateForward { get; protected set; }
    public abstract bool CanNavigateBack { get; protected set; }
}