using Microsoft.Extensions.DependencyInjection;
using PoolMemberships.Data;
using PoolMemberships.Models;
using PoolMemberships.Repositories;
using PoolMemberships.Services;
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
      
      // Repositories
      collection.AddTransient<IMembershipRepository, MembershipRepository>();
      collection.AddTransient<IPersonRepository, PersonRepository>();
      collection.AddTransient<IPaymentRepository, PaymentRepository>();
      
      // Services
      collection.AddTransient<IMembershipService, MembershipService>();
      collection.AddTransient<IPersonService, PersonService>();
      collection.AddTransient<IPaymentService, PaymentService>();
   } 
}