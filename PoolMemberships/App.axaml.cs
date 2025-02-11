using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PoolMemberships.Data;
using PoolMemberships.ViewModels;
using PoolMemberships.Views;

namespace PoolMemberships;

public class App : Application
{
    public static IServiceProvider Services { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var collection = new ServiceCollection();
            collection.AddCommonServices();

            Services = collection.BuildServiceProvider();

            // Ensure DB is created and migrated
            var dbContext = Services.GetRequiredService<PoolMembershipDbContext>();
            dbContext.Database.Migrate();


            var vm = Services.GetRequiredService<MainWindowViewModel>();
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);

            var mainWindow = Services.GetRequiredService<MainWindow>();
            mainWindow.DataContext = vm;
            desktop.MainWindow = mainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
}