﻿using System;
using CommunityToolkit.Mvvm.ComponentModel;
using PoolMemberships.Services;

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

    public ViewMembershipViewModel(IMembershipService membershipService)
    {
        _membershipService = membershipService;
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

    }

}