using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PoolMemberships.Dtos;
using PoolMemberships.Models;
using PoolMemberships.Repositories;

namespace PoolMemberships.Services;

public class PersonService : IPersonService
{
    private readonly IMapper _mapper;
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<Person> AddAsync(Person person)
    {
        return await _personRepository.AddAsync(person);
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _personRepository.GetAllAsync();
    }

    public async Task<Person?> GetAsync(int personId)
    {
        return await _personRepository.GetAsync(personId);
    }

    public async Task<Person> UpdateAsync(int personId, UpdatePersonDto personDto)
    {
        var person = await _personRepository.GetAsync(personId);

        if (person == null) throw new KeyNotFoundException($"Person with id {personId} not found");

        _mapper.Map(personDto, person);
        return await _personRepository.UpdateAsync(person);
    }
}