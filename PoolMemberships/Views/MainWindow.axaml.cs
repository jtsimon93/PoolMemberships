using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using PoolMemberships.ViewModels;

namespace PoolMemberships.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        SetupMembershipDataGrid();
    }

    private void SetupMembershipDataGrid()
    {
        var membershipDataGrid = new MembershipDataGrid
        {
            DataContext = App.Services.GetRequiredService<MembershipDataGridViewModel>()
        };

        this.FindControl<ContentControl>("MainContent").Content = membershipDataGrid;
    }
}