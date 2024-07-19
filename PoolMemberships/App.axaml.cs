using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PoolMemberships.Data;
using PoolMemberships.ViewModels;
using PoolMemberships.Views;

namespace PoolMemberships;

public partial class App : Application
{
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

            var serviceProvider = collection.BuildServiceProvider();
            
            // Ensure DB is created and migrated
            var dbContext = serviceProvider.GetRequiredService<PoolMembershipDbContext>();
            dbContext.Database.Migrate();
            
            
            var vm = serviceProvider.GetRequiredService<MainWindowViewModel>();
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);
            desktop.MainWindow = new MainWindow
            {
                DataContext = vm,
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}