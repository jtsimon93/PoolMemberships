using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using PoolMemberships.Models;
using PoolMemberships.Services;
using PoolMemberships.Views;

namespace PoolMemberships.ViewModels;

public partial class AddMemberViewModel : ViewModelBase
{
    private readonly IMembershipService _membershipService;


    private readonly IPersonService _personService;
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

    public AddMemberViewModel(IPersonService personService, IMembershipService membershipService)
    {
        _personService = personService;
        _membershipService = membershipService;
        InitializeCommands();
    }

    public ICommand CancelCommand { get; private set; }
    public ICommand AddMembershipCommand { get; private set; }

    private void InitializeCommands()
    {
        CancelCommand = new RelayCommand(Cancel);
        AddMembershipCommand = new RelayCommand(AddMembership);
    }

    private void Cancel()
    {
        ReturnToMembershipDataGrid();
    }

    private async void AddMembership()
    {
        var person = await AddPersonAsync();

        await AddMembershipAsync(person);

        ReturnToMembershipDataGrid();
    }

    private async Task<Person> AddPersonAsync()
    {
        var person = new Person
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Phone = PhoneNumber,
            Address = StreetAddress,
            City = City,
            State = State,
            Zip = ZipCode
        };

        return await Task.Run(() => _personService.AddAsync(person));
    }

    private async Task AddMembershipAsync(Person person)
    {
        var membership = new Membership
        {
            PersonId = person.PersonId,
            Person = person,
            StartDate = DateOnly.FromDateTime(StartDate.DateTime),
            EndDate = DateOnly.FromDateTime(EndDate.DateTime),
            KeyFobId = KeyFobId
        };

        await Task.Run(() => _membershipService.AddAsync(membership));
    }

    private static void ReturnToMembershipDataGrid()
    {
        var mainWindowViewModel = App.Services.GetRequiredService<MainWindowViewModel>();
        var membershipDataGrid = new MembershipDataGrid
        {
            DataContext = App.Services.GetRequiredService<MembershipDataGridViewModel>()
        };

        mainWindowViewModel.CurrentView = membershipDataGrid;
    }
}