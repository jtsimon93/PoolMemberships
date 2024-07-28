using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using PoolMemberships.Dtos;
using PoolMemberships.ViewModels;

namespace PoolMemberships.Views;

public partial class MembershipDataGridSearchResults : UserControl
{
    public MembershipDataGridSearchResults()
    {
        InitializeComponent();
    }

    private void MembershipDataGrid_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (e.InitialPressMouseButton == MouseButton.Left)
            if (DataContext is MembershipDataGridSearchResultsViewModel viewModel)
            {
                var selectedMembership = (MembershipWithPersonDto)MDataGrid.SelectedItem;
                viewModel.RowDoubleTappedCommand.Execute(selectedMembership);
            } 
    }
}