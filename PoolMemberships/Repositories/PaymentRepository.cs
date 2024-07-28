using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PoolMemberships.Data;
using PoolMemberships.Models;

namespace PoolMemberships.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly PoolMembershipDbContext _context;

    public PaymentRepository(PoolMembershipDbContext context)
    {
        _context = context;
    }

    public async Task<Payment> AddAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();
        return payment;
    }

    public async Task<IEnumerable<Payment>> GetAllAsync()
    {
        return await _context.Payments.ToListAsync();
    }
}