using System.Collections.Generic;
using System.Threading.Tasks;
using PoolMemberships.Dtos;
using PoolMemberships.Models;

namespace PoolMemberships.Services;

public interface IPersonService
{
    Task<Person> AddAsync(Person person);
    Task<IEnumerable<Person>> GetAllAsync();
    Task<Person?> GetAsync(int personId);
    Task<Person> UpdateAsync(int personId, UpdatePersonDto personDto);
}