using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using PoolMemberships.Dtos;
using PoolMemberships.Services;
using PoolMemberships.Views;

namespace PoolMemberships.ViewModels;

public partial class UpdatePersonViewModel : ViewModelBase
{
    private readonly IPersonService _personService;
    [ObservableProperty] private string _city;
    [ObservableProperty] private string _email;

    [ObservableProperty] private string _firstName;
    [ObservableProperty] private string _lastName;

    private int _personId;
    [ObservableProperty] private string _phoneNumber;
    [ObservableProperty] private string _state;
    [ObservableProperty] private string _streetAddress;
    [ObservableProperty] private string _zipCode;

    public UpdatePersonViewModel(IPersonService personService)
    {
        _personService = personService;
        SavePersonCommand = new RelayCommand(UpdatePerson);
        CancelCommand = new RelayCommand(OnFinishUpdate);
    }

    public ICommand SavePersonCommand { get; }
    public ICommand CancelCommand { get; }

    public async void PopulateData(int personId)
    {
        _personId = personId;
        var person = await _personService.GetAsync(personId);

        if (person == null) return;

        FirstName = person.FirstName;
        LastName = person.LastName;
        Email = person.Email == null ? string.Empty : person.Email;
        PhoneNumber = person.Phone == null ? string.Empty : person.Phone;
        StreetAddress = person.Address == null ? string.Empty : person.Address;
        City = person.City == null ? string.Empty : person.City;
        State = person.State == null ? string.Empty : person.State;
        ZipCode = person.Zip == null ? string.Empty : person.Zip;
    }

    private async void UpdatePerson()
    {
        var dto = new UpdatePersonDto
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

        await _personService.UpdateAsync(_personId, dto);

        OnFinishUpdate();
    }

    private void OnFinishUpdate()
    {
        var viewMembershipViewModel = App.Services.GetRequiredService<ViewMembershipViewModel>();
        viewMembershipViewModel.PopulateData(_personId);
        var viewMembershipView = new ViewMembershipView
        {
            DataContext = viewMembershipViewModel
        };

        var mainWindowViewModel = App.Services.GetRequiredService<MainWindowViewModel>();
        mainWindowViewModel.CurrentView = viewMembershipView;
    }
}