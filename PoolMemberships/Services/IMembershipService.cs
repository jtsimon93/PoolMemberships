using System.Collections.Generic;
using System.Threading.Tasks;
using PoolMemberships.Dtos;
using PoolMemberships.Models;

namespace PoolMemberships.Services;

public interface IMembershipService
{
    Task<Membership> AddAsync(Membership membership);
    Task<IEnumerable<Membership>> GetAllAsync();
    Task<IEnumerable<MembershipWithPersonDto>> GetAllWithPersonAsync();
}