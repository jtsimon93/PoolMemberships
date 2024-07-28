using System;
using System.Windows.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using PoolMemberships.Views;

namespace PoolMemberships.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private UserControl _currentView;

    public MainWindowViewModel()
    {
        ExitCommand = new RelayCommand(ExitApplication);
        AboutCommand = new RelayCommand(ShowAboutWindow);
        AddNewMemberCommand = new RelayCommand(ShowAddNewMemberControl);
        SearchMembershipsCommand = new RelayCommand(ShowSearchMembershipsWindow);

        var membershipDataGrid = new MembershipDataGrid
        {
            DataContext = App.Services.GetRequiredService<MembershipDataGridViewModel>()
        };

        CurrentView = membershipDataGrid;
    }

    public ICommand ExitCommand { get; }
    public ICommand AboutCommand { get; }

    public ICommand AddNewMemberCommand { get; }
    
    public ICommand SearchMembershipsCommand { get; }

    private void ExitApplication()
    {
        Environment.Exit(0);
    }

    private void ShowAboutWindow()
    {
        var aboutWindow = new AboutWindow();
        aboutWindow.Show();
    }

    private void ShowAddNewMemberControl()
    {
        var addNewMemberView = new AddMemberView
        {
            DataContext = App.Services.GetRequiredService<AddMemberViewModel>()
        };
        CurrentView = addNewMemberView;
    }

    private void ShowSearchMembershipsWindow()
    {
        var searchMembershipsWindowViewModel = App.Services.GetRequiredService<SearchMembershipsWindowViewModel>();
        var searchMembershipsWindow = new SearchMembershipsWindow(searchMembershipsWindowViewModel);
        
        searchMembershipsWindow.Show();
        
    }
}