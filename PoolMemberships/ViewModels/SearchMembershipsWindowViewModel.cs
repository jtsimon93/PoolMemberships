using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using PoolMemberships.Dtos;
using PoolMemberships.Services;
using PoolMemberships.Views;

namespace PoolMemberships.ViewModels;

public partial class SearchMembershipsWindowViewModel : ViewModelBase
{
    private readonly IMembershipService _membershipService;
    public event Action RequestClose;

    [ObservableProperty] private string _keyFobId;
    [ObservableProperty] private string _firstName;
    [ObservableProperty] private string _lastName;
    [ObservableProperty] private bool _active;
    [ObservableProperty] private bool _noResults;
    
    public ICommand CloseWindowCommand { get; }
    public ICommand SearchCommand { get; }
    
    public SearchMembershipsWindowViewModel(IMembershipService membershipService)
    {
        _membershipService = membershipService;
        CloseWindowCommand = new RelayCommand(CloseWindow);
        SearchCommand = new RelayCommand(Search);
        NoResults = false;
        Active = true;
    }

    public void CloseWindow()
    {
        RequestClose?.Invoke();
    }

    private async void Search()
    {
        var searchCriteria = new MembershipSearchCriteriaDto
        {
            KeyFobId = KeyFobId,
            FirstName = FirstName,
            LastName = LastName,
            Active = Active
        };
        
        var memberships = await _membershipService.SearchAsync(searchCriteria);

        var numMemberships = memberships.Count();

        if (numMemberships == 0)
        {
            NoResults = true;
            return;
        }

        var membershipDataGridSearchResultsViewModel = App.Services.GetRequiredService<MembershipDataGridSearchResultsViewModel>();
        
        // Convert memberships to an ObservableCollection
        var membershipsObservableCollection = new ObservableCollection<MembershipWithPersonDto>(memberships);
        membershipDataGridSearchResultsViewModel.SetMemberships(membershipsObservableCollection);
        
        var view = new MembershipDataGridSearchResults
        {
            DataContext = membershipDataGridSearchResultsViewModel
        };
        
        var mainWindowViewModel = App.Services.GetRequiredService<MainWindowViewModel>();
        mainWindowViewModel.CurrentView = view;
        
        CloseWindow();

    }

    private void ShowRegularDataGrid()
    {
        var membershipDataGridViewModel = App.Services.GetRequiredService<MembershipDataGridViewModel>();
        var view = new MembershipDataGrid
        {
            DataContext = membershipDataGridViewModel
        };
        
        var mainWindowViewModel = App.Services.GetRequiredService<MainWindowViewModel>();
        mainWindowViewModel.CurrentView = view;

    }

}