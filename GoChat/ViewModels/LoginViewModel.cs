using System;

namespace GoChat.ViewModels;

public class LoginViewModel:PageViewModelBase
{
    public string Title => "Login";

    public override bool CanNavigateNext
    {
        get => true;
        protected set
        {
            throw new NotSupportedException(); 
        }
    }

    public override bool CanNavigatePrevious
    {
        get => false;
        protected set => throw new NotSupportedException();
    }
}