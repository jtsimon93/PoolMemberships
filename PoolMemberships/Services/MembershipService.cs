using System.Collections.Generic;
using System.Threading.Tasks;
using PoolMemberships.Models;
using PoolMemberships.Repositories;

namespace PoolMemberships.Services;

public class MembershipService : IMembershipService
{
    private readonly IMembershipRepository _membershipRepository;
    
    public MembershipService(IMembershipRepository membershipRepository)
    {
        _membershipRepository = membershipRepository;
    }
    
    public async Task<Membership> AddAsync(Membership membership)
    {
        return await _membershipRepository.AddAsync(membership);
    }
    
    public async Task<IEnumerable<Membership>> GetAllAsync()
    {
        return await _membershipRepository.GetAllAsync();
    }

}