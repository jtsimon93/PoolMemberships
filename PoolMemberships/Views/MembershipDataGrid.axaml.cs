using Avalonia.Controls;
using Avalonia.Input;
using PoolMemberships.Dtos;
using PoolMemberships.ViewModels;

namespace PoolMemberships.Views;

public partial class MembershipDataGrid : UserControl
{
    public MembershipDataGrid()
    {
        InitializeComponent();
    }

    private void MembershipDataGrid_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (e.InitialPressMouseButton == MouseButton.Left)
            if (DataContext is MembershipDataGridViewModel viewModel)
            {
                var selectedMembership = (MembershipWithPersonDto)MDataGrid.SelectedItem;
                viewModel.RowDoubleTappedCommand.Execute(selectedMembership);
            }
    }
}