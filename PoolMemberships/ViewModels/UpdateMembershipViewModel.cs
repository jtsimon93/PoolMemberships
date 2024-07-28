using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using PoolMemberships.Dtos;
using PoolMemberships.Services;
using PoolMemberships.Views;

namespace PoolMemberships.ViewModels;

public partial class UpdateMembershipViewModel : ViewModelBase
{
    private readonly IMembershipService _membershipService;
    [ObservableProperty] private bool _active;
    [ObservableProperty] private DateTimeOffset _endDate;
    [ObservableProperty] private string _keyFobId;
    private int _membershipId;
    [ObservableProperty] private string _notes;

    [ObservableProperty] private DateTimeOffset _startDate;

    public UpdateMembershipViewModel(IMembershipService membershipService)
    {
        _membershipService = membershipService;
        SaveCommand = new RelayCommand(UpdateMembership);
        CancelCommand = new RelayCommand(OnFinishUpdate);
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public async void PopulateData(int membershipId)
    {
        _membershipId = membershipId;
        var membership = await _membershipService.GetWithPersonAsync(_membershipId);

        if (membership == null) return;

        KeyFobId = membership.KeyFobId;
        Active = membership.Active;
        Notes = membership.Notes;

        // membership.StartDate and membership.EndDate are DateOnly
        // StartDate and EndDate are DateTimeOffset, so we need to convert them
        StartDate = new DateTimeOffset(membership.StartDate.Year, membership.StartDate.Month, membership.StartDate.Day,
            0, 0, 0, TimeSpan.Zero);
        EndDate = new DateTimeOffset(membership.EndDate.Year, membership.EndDate.Month, membership.EndDate.Day, 0, 0, 0,
            TimeSpan.Zero);
    }

    private async void UpdateMembership()
    {
        var dto = new UpdateMembershipDto
        {
            StartDate = new DateOnly(StartDate.Year, StartDate.Month, StartDate.Day),
            EndDate = new DateOnly(EndDate.Year, EndDate.Month, EndDate.Day),
            KeyFobId = KeyFobId,
            Active = Active,
            Notes = Notes
        };

        await _membershipService.UpdateAsync(_membershipId, dto);
        OnFinishUpdate();
    }

    private void OnFinishUpdate()
    {
        var viewMembershipViewModel = App.Services.GetRequiredService<ViewMembershipViewModel>();
        viewMembershipViewModel.PopulateData(_membershipId);
        var view = new ViewMembershipView
        {
            DataContext = viewMembershipViewModel
        };

        var mainWindowViewModel = App.Services.GetRequiredService<MainWindowViewModel>();
        mainWindowViewModel.CurrentView = view;
    }
}