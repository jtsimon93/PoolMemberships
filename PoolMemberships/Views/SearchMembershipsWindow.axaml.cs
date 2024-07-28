using System;
using Avalonia;
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
        Console.WriteLine("close window triggered");
        this.Close();
    }
}