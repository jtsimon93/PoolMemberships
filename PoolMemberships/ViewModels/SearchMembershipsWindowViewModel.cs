using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PoolMemberships.Services;

namespace PoolMemberships.ViewModels;

public partial class SearchMembershipsWindowViewModel : ViewModelBase
{
    private readonly IMembershipService _membershipService;
    public event Action RequestClose;

    [ObservableProperty] private string _keyFobId;
    [ObservableProperty] private string _firstName;
    [ObservableProperty] private string _lastName;
    [ObservableProperty] private bool _active;
    
    public ICommand CloseWindowCommand { get; }
    public ICommand SearchCommand { get; }
    
    public SearchMembershipsWindowViewModel(IMembershipService membershipService)
    {
        _membershipService = membershipService;
        CloseWindowCommand = new RelayCommand(CloseWindow);
        SearchCommand = new RelayCommand(Search);
    }

    public void CloseWindow()
    {
        RequestClose?.Invoke();
    }

    private async void Search()
    {
        

    }

}