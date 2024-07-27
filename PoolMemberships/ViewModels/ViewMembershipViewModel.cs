﻿using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using PoolMemberships.Services;
using PoolMemberships.Views;

namespace PoolMemberships.ViewModels;

public partial class ViewMembershipViewModel : ViewModelBase
{
    private readonly IMembershipService _membershipService;

    [ObservableProperty] private string _city;

    [ObservableProperty] private string _email;

    [ObservableProperty] private DateTimeOffset _endDate = DateTimeOffset.Now.AddYears(1);

    [ObservableProperty] private string _firstName;

    [ObservableProperty] private string _keyFobId;

    [ObservableProperty] private string _lastName;

    [ObservableProperty] private string _phoneNumber;

    [ObservableProperty] private DateTimeOffset _startDate = DateTimeOffset.Now;

    [ObservableProperty] private string _state;

    [ObservableProperty] private string _streetAddress;

    [ObservableProperty] private string _zipCode;

    public ViewMembershipViewModel(IMembershipService membershipService)
    {
        _membershipService = membershipService;
        ReturnToListCommand = new RelayCommand(OnReturnToList);
    }

    public ICommand ReturnToListCommand { get; }

    public async void PopulateData(int membershipId)
    {
        var membership = await _membershipService.GetWithPersonAsync(membershipId);

        if (membership == null) return;

        FirstName = membership.PersonFirstName;
        LastName = membership.PersonLastName;
        Email = membership.PersonEmail == null ? string.Empty : membership.PersonEmail;
        PhoneNumber = membership.PersonPhoneNumber == null ? string.Empty : membership.PersonPhoneNumber;
        StreetAddress = membership.PersonAddress == null ? string.Empty : membership.PersonAddress;
        City = membership.PersonCity == null ? string.Empty : membership.PersonCity;
        State = membership.PersonState == null ? string.Empty : membership.PersonState;
        ZipCode = membership.PersonZip == null ? string.Empty : membership.PersonZip;
        KeyFobId = membership.KeyFobId == null ? string.Empty : membership.KeyFobId;

        // membership.StartDate and membership.EndDate are DateOnly
        // StartDate and EndDate are DateTimeOffset, so we need to convert them
        StartDate = new DateTimeOffset(membership.StartDate.Year, membership.StartDate.Month, membership.StartDate.Day,
            0, 0, 0, TimeSpan.Zero);
        EndDate = new DateTimeOffset(membership.EndDate.Year, membership.EndDate.Month, membership.EndDate.Day, 0, 0, 0,
            TimeSpan.Zero);
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