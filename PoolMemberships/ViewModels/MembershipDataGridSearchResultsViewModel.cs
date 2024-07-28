using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using PoolMemberships.Dtos;
using PoolMemberships.Views;

namespace PoolMemberships.ViewModels;

public partial class MembershipDataGridSearchResultsViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<MembershipWithPersonDto> _memberships;
    
    public ICommand RowDoubleTappedCommand { get; }

    public MembershipDataGridSearchResultsViewModel()
    {
        RowDoubleTappedCommand = new RelayCommand<MembershipWithPersonDto>(OnRowDoubleClicked);
    }

    public void SetMemberships(ObservableCollection<MembershipWithPersonDto> memberships)
    {
        Memberships = memberships;
    }

    private void OnRowDoubleClicked(MembershipWithPersonDto membership)
    {
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