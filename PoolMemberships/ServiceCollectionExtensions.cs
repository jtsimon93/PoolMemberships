using Microsoft.Extensions.DependencyInjection;
using PoolMemberships.Data;
using PoolMemberships.ViewModels;

namespace PoolMemberships;

public static class ServiceCollectionExtensions
{
   public static void AddCommonServices(this IServiceCollection collection)
   {
      // Database
      collection.AddDbContext<PoolMembershipDbContext>();
      
      // View Models
      collection.AddTransient<MainWindowViewModel>();
   } 
}