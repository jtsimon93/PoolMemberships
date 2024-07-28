using System.Collections.Generic;
using System.Threading.Tasks;
using PoolMemberships.Dtos;
using PoolMemberships.Models;

namespace PoolMemberships.Repositories;

public interface IMembershipRepository
{
    Task<Membership> AddAsync(Membership membership);
    Task<IEnumerable<Membership>> GetAllAsync();
    Task<IEnumerable<Membership>> GetAllWithPersonAsync();
    Task<Membership?> GetWithPersonAsync(int id);
    Task<Membership?> GetAsync(int id);
    Task<Membership> UpdateAsync(Membership membership);
    Task<IEnumerable<Membership>> SearchAsync(MembershipSearchCriteriaDto searchDto);
}