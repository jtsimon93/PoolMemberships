using System.Collections.Generic;
using System.Threading.Tasks;
using PoolMemberships.Models;

namespace PoolMemberships.Repositories;

public interface IPersonRepository
{
    Task<Person> AddAsync(Person person);
    Task<IEnumerable<Person>> GetAllAsync();
    Task<Person> UpdateAsync(Person person);
}