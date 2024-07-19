using System.Collections.Generic;
using System.Threading.Tasks;
using PoolMemberships.Models;

namespace PoolMemberships.Services;

public interface IPaymentService
{
    public Task<Payment> AddAsync(Payment payment);
    public Task<IEnumerable<Payment>> GetAllAsync();
}