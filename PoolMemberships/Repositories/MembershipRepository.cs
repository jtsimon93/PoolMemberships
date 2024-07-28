using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PoolMemberships.Data;
using PoolMemberships.Dtos;
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
            .OrderByDescending(m => m.Active)
            .ThenBy(m => m.Person.LastName)
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

    public async Task<IEnumerable<Membership>> SearchAsync(MembershipSearchCriteriaDto searchDto)
    {   
        var query = _context.Memberships.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchDto.KeyFobId))
        {
            query = query.Where(m => m.KeyFobId == searchDto.KeyFobId);
        }

        if (!string.IsNullOrWhiteSpace(searchDto.FirstName))
        {
            query = query.Where(m => m.Person.FirstName.Contains(searchDto.FirstName));
        }

        if (!string.IsNullOrWhiteSpace(searchDto.LastName))
        {
            query = query.Where(m => m.Person.LastName.Contains(searchDto.LastName));
        }

        if (searchDto.Active.HasValue)
        {
            query = query.Where(m => m.Active == searchDto.Active);
        }

        return await query
            .OrderByDescending(m => m.Active)
            .ThenBy(m => m.Person.LastName)
            .ToListAsync();
    }
}