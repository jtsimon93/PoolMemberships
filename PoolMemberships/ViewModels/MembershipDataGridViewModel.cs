using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using PoolMemberships.Dtos;
using PoolMemberships.Services;

namespace PoolMemberships.ViewModels;

public partial class MembershipDataGridViewModel : ViewModelBase
{
    private readonly IMembershipService _membershipService;

    [ObservableProperty]
    private ObservableCollection<MembershipWithPersonDto> _memberships;

    
    public MembershipDataGridViewModel(IMembershipService membershipService)
    {
        _membershipService = membershipService;
        InitializeData();
    }

    private async void InitializeData()
    {
        await LoadMemberships();
    }

    private async Task LoadMemberships()
    {
        var memberships = await Task.Run(() => _membershipService.GetAllWithPersonAsync());
        Memberships = new ObservableCollection<MembershipWithPersonDto>(memberships);
    }

    
}