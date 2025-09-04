using System;
using CommunityToolkit.Mvvm.ComponentModel;
using ReactiveUI;

namespace GoChat.ViewModels;

public class MainViewModel:PageViewModelBase
{
    private static readonly Lazy<MainViewModel> _instance = new(() => new MainViewModel());
    public static MainViewModel Instance => _instance.Value;
    public MainViewModel()
    {
    }
}