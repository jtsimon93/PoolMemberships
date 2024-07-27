using System.Collections.Generic;
using System.Threading.Tasks;
using PoolMemberships.Models;
using PoolMemberships.Repositories;

namespace PoolMemberships.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Person> AddAsync(Person person)
    {
        return await _personRepository.AddAsync(person);
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _personRepository.GetAllAsync();
    }
}