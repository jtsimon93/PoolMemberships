using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using PoolMemberships.Views;

namespace PoolMemberships.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ICommand ExitCommand { get; }
    public ICommand AboutCommand { get; }

    public MainWindowViewModel()
    {
        ExitCommand = new RelayCommand(ExitApplication);
        AboutCommand = new RelayCommand(ShowAboutWindow);
    }

    private void ExitApplication()
    {
        Environment.Exit(0);
    }

    private void ShowAboutWindow()
    {
        var aboutWindow = new AboutWindow();
        aboutWindow.Show();
    }
}