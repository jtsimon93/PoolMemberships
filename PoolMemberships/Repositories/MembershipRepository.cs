using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PoolMemberships.Data;
using PoolMemberships.Models;

namespace PoolMemberships.Repositories;

public class MembershipRepository : IMembershipRepository
{
    private readonly PoolMembershipDbContext _context;

    public MembershipRepository(PoolMembershipDbContext context)
    {
        _context = context;
    }

    public async Task<Membership> AddAsync(Membership membership)
    {
        await _context.Memberships.AddAsync(membership);
        await _context.SaveChangesAsync();
        return membership;
    }

    public async Task<IEnumerable<Membership>> GetAllAsync()
    {
        return await _context.Memberships.ToListAsync();
    }

    public async Task<IEnumerable<Membership>> GetAllWithPersonAsync()
    {
        return await _context.Memberships
            .Include(m => m.Person)
            .ToListAsync();
    }

    public async Task<Membership?> GetWithPersonAsync(int id)
    {
        return await _context.Memberships.FindAsync(id);
    }

    public async Task<Membership?> GetAsync(int id)
    {
        return await _context.Memberships.FindAsync(id);
    }

    public async Task<Membership> UpdateAsync(Membership membership)
    {
        _context.Memberships.Update(membership);
        await _context.SaveChangesAsync();
        return membership;
    }
}