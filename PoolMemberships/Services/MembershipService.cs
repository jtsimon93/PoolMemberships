using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PoolMemberships.Dtos;
using PoolMemberships.Models;
using PoolMemberships.Repositories;

namespace PoolMemberships.Services;

public class MembershipService : IMembershipService
{
    private readonly IMembershipRepository _membershipRepository;
    private readonly IMapper _mapper;
    
    public MembershipService(IMembershipRepository membershipRepository, IMapper mapper)
    {
        _membershipRepository = membershipRepository;
        _mapper = mapper;
    }
    
    public async Task<Membership> AddAsync(Membership membership)
    {
        return await _membershipRepository.AddAsync(membership);
    }
    
    public async Task<IEnumerable<Membership>> GetAllAsync()
    {
        return await _membershipRepository.GetAllAsync();
    }

    public async Task<IEnumerable<MembershipWithPersonDto>> GetAllWithPersonAsync()
    {
        var memberships = await _membershipRepository.GetAllWithPersonAsync();
        return _mapper.Map<IEnumerable<MembershipWithPersonDto>>(memberships);
    }

    public async Task<MembershipWithPersonDto?> GetWithPersonAsync(int id)
    {
        var membership = await _membershipRepository.GetWithPersonAsync(id);
        
        return membership == null ? null : _mapper.Map<MembershipWithPersonDto>(membership);
    }
    

}