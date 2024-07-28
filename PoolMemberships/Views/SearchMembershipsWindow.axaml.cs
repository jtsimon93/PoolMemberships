using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PoolMemberships.ViewModels;

namespace PoolMemberships.Views;

public partial class SearchMembershipsWindow : Window
{
    public SearchMembershipsWindow(SearchMembershipsWindowViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
        viewModel.RequestClose += CloseWindow;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void CloseWindow()
    {
        Close();
    }
}