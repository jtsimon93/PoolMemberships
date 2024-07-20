using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using PoolMemberships.Models;
using PoolMemberships.Services;
using PoolMemberships.Views;

namespace PoolMemberships.ViewModels;

public class AddMemberViewModel : ViewModelBase
{
    private readonly IPersonService _personService;
    private readonly IMembershipService _membershipService;
    
    public ICommand CancelCommand { get; private set; }
    public ICommand AddPersonCommand { get; private set; }
    
    public AddMemberViewModel(IPersonService personService, IMembershipService membershipService)
    {
        _personService = personService;
        _membershipService = membershipService;
        InitializeCommands();
    }
    
    private void InitializeCommands()
    {
        CancelCommand = new RelayCommand(Cancel);
        AddPersonCommand = new RelayCommand(AddPerson);
    }

    public void Cancel()
    {
        var mainWindowViewModel = App.Services.GetRequiredService<MainWindowViewModel>();
        var membershipDataGrid = new MembershipDataGrid
        {
            DataContext = App.Services.GetRequiredService<MembershipDataGridViewModel>() 
        };
        
        mainWindowViewModel.CurrentView = membershipDataGrid;
    }

    public void AddPerson()
    {
        
    }

    private async Task AddPerson(Person person)
    {   
        await Task.Run(() => _personService.AddAsync(person));
    }

    private async Task AddMembership(Membership membership)
    {
        await Task.Run(() => _membershipService.AddAsync(membership));
    }
    
}