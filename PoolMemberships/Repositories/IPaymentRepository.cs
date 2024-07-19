using System.Collections.Generic;
using System.Threading.Tasks;
using PoolMemberships.Models;

namespace PoolMemberships.Repositories;

public interface IPaymentRepository
{
    Task<Payment> AddAsync(Payment payment);
    Task<IEnumerable<Payment>> GetAllAsync();
}