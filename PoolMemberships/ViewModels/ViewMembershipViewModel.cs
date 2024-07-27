using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using PoolMemberships.Services;
using PoolMemberships.Views;

namespace PoolMemberships.ViewModels;

public partial class ViewMembershipViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _firstName;
    [ObservableProperty]
    private string _lastName;
    [ObservableProperty]
    private string _email;
    [ObservableProperty]
    private string _phoneNumber;
    [ObservableProperty]
    private string _streetAddress;
    [ObservableProperty]
    private string _city;
    [ObservableProperty]
    private string _state;
    [ObservableProperty]
    private string _zipCode;
    [ObservableProperty]
    private DateTimeOffset _startDate = DateTimeOffset.Now;
    [ObservableProperty]
    private DateTimeOffset _endDate = DateTimeOffset.Now.AddYears(1);
    [ObservableProperty]
    private string _keyFobId;
    
    private readonly IMembershipService _membershipService;
    
    public ICommand ReturnToListCommand { get; }

    public ViewMembershipViewModel(IMembershipService membershipService)
    {
        _membershipService = membershipService;
        ReturnToListCommand = new RelayCommand(OnReturnToList);
    }

    public async void PopulateData(int membershipId)
    {
        var membership = await _membershipService.GetWithPersonAsync(membershipId);
        
        if (membership == null)
        {
            return;
        }

        FirstName = membership.PersonFirstName;
        LastName = membership.PersonLastName;
        Email = membership.PersonEmail == null ? String.Empty : membership.PersonEmail;
        PhoneNumber = membership.PersonPhoneNumber == null ? String.Empty : membership.PersonPhoneNumber;
        StreetAddress = membership.PersonAddress == null ? String.Empty : membership.PersonAddress;
        City = membership.PersonCity == null ? String.Empty : membership.PersonCity;
        State = membership.PersonState == null ? String.Empty : membership.PersonState;
        ZipCode = membership.PersonZip == null ? String.Empty : membership.PersonZip;
        KeyFobId = membership.KeyFobId == null ? String.Empty : membership.KeyFobId;
        
        // membership.StartDate and membership.EndDate are DateOnly
        // StartDate and EndDate are DateTimeOffset, so we need to convert them
        StartDate = new DateTimeOffset(membership.StartDate.Year, membership.StartDate.Month, membership.StartDate.Day, 0, 0, 0, TimeSpan.Zero);
        EndDate = new DateTimeOffset(membership.EndDate.Year, membership.EndDate.Month, membership.EndDate.Day, 0, 0, 0, TimeSpan.Zero);

    }

    private void OnReturnToList()
    {
       var mainWindowViewModel = App.Services.GetRequiredService<MainWindowViewModel>();
       var membershipDataGridViewModel = App.Services.GetRequiredService<MembershipDataGridViewModel>();
       var membershipDataGridView = new MembershipDataGrid
       {
           DataContext = membershipDataGridViewModel
       };
       
       mainWindowViewModel.CurrentView = membershipDataGridView;
    }

}