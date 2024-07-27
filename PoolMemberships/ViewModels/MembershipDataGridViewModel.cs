using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using PoolMemberships.Dtos;
using PoolMemberships.Services;
using PoolMemberships.Views;

namespace PoolMemberships.ViewModels;

public partial class MembershipDataGridViewModel : ViewModelBase
{
    private readonly IMembershipService _membershipService;

    [ObservableProperty] private ObservableCollection<MembershipWithPersonDto> _memberships;


    public MembershipDataGridViewModel(IMembershipService membershipService)
    {
        _membershipService = membershipService;
        RowDoubleTappedCommand = new RelayCommand<MembershipWithPersonDto>(OnRowDoubleClicked);
        InitializeData();
    }

    public ICommand RowDoubleTappedCommand { get; }

    private async void InitializeData()
    {
        await LoadMemberships();
    }

    private async Task LoadMemberships()
    {
        var memberships = await Task.Run(() => _membershipService.GetAllWithPersonAsync());
        Memberships = new ObservableCollection<MembershipWithPersonDto>(memberships);
    }

    private void OnRowDoubleClicked(MembershipWithPersonDto membership)
    {
        // Use membership ID
        Console.WriteLine("Membership: " + membership.MembershipId);

        var viewMembershipViewModel = App.Services.GetRequiredService<ViewMembershipViewModel>();
        viewMembershipViewModel.PopulateData(membership.MembershipId);

        var view = new ViewMembershipView
        {
            DataContext = viewMembershipViewModel
        };

        var mainWindowViewModel = App.Services.GetRequiredService<MainWindowViewModel>();
        mainWindowViewModel.CurrentView = view;
    }
}