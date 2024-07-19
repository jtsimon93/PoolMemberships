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
    
}