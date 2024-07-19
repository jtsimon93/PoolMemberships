using System.Collections.Generic;
using System.Threading.Tasks;
using PoolMemberships.Models;

namespace PoolMemberships.Services;

public interface IPersonService
{
    Task<Person> AddAsync(Person person);
    Task<IEnumerable<Person>> GetAllAsync();
}