using Microsoft.Extensions.DependencyInjection;
using PoolMemberships.ViewModels;

namespace PoolMemberships;

public static class ServiceCollectionExtensions
{
   public static void AddCommonServices(this IServiceCollection collection)
   {
      collection.AddTransient<MainWindowViewModel>();
   } 
}