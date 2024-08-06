using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using PoolMemberships.Services;
using PoolMemberships.Views;

namespace PoolMemberships.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private UserControl _currentView;
    private readonly IMembershipService _membershipService;

    public MainWindowViewModel(IMembershipService membershipService)
    {
        _membershipService = membershipService;
        ExitCommand = new RelayCommand(ExitApplication);
        AboutCommand = new RelayCommand(ShowAboutWindow);
        AddNewMemberCommand = new RelayCommand(ShowAddNewMemberControl);
        SearchMembershipsCommand = new RelayCommand(ShowSearchMembershipsWindow);
        ShowAllMembersCommand = new RelayCommand(ShowAllMembers);
        ExcelExportCommand = new RelayCommand(() => Task.Run(() => ExcelExport()));

        var membershipDataGrid = new MembershipDataGrid
        {
            DataContext = App.Services.GetRequiredService<MembershipDataGridViewModel>()
        };

        CurrentView = membershipDataGrid;
    }

    public ICommand ExitCommand { get; }
    public ICommand AboutCommand { get; }

    public ICommand AddNewMemberCommand { get; }
    
    public ICommand SearchMembershipsCommand { get; }
    
    public ICommand ShowAllMembersCommand { get; }
    
    public ICommand ExcelExportCommand { get; }

    private void ExitApplication()
    {
        Environment.Exit(0);
    }

    private void ShowAboutWindow()
    {
        var aboutWindow = new AboutWindow();
        aboutWindow.Show();
    }

    private void ShowAddNewMemberControl()
    {
        var addNewMemberView = new AddMemberView
        {
            DataContext = App.Services.GetRequiredService<AddMemberViewModel>()
        };
        CurrentView = addNewMemberView;
    }

    private void ShowSearchMembershipsWindow()
    {
        var searchMembershipsWindowViewModel = App.Services.GetRequiredService<SearchMembershipsWindowViewModel>();
        var searchMembershipsWindow = new SearchMembershipsWindow(searchMembershipsWindowViewModel);
        
        searchMembershipsWindow.Show();
    }

    private void ShowAllMembers()
    {
        var membershipDataGrid = new MembershipDataGrid
        {
            DataContext = App.Services.GetRequiredService<MembershipDataGridViewModel>()
        };
        CurrentView = membershipDataGrid;
    }

    public async Task ExcelExport()
    {
        var excelDocument = await _membershipService.GenerateExcelFile();
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filters.Add(new FileDialogFilter { Name = "Excel Files", Extensions = { "xlsx" } });

        var mainWindow = App.Services.GetRequiredService<MainWindow>();
    
        var filePath = await saveFileDialog.ShowAsync(mainWindow);
    
        if (string.IsNullOrEmpty(filePath)) return;
    
        await System.IO.File.WriteAllBytesAsync(filePath, excelDocument);
    }
}