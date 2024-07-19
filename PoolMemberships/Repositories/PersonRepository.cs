﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PoolMemberships.Data;
using PoolMemberships.Models;

namespace PoolMemberships.Repositories;

public class PersonRepository : IPersonRepository
{
   private readonly PoolMembershipDbContext _context;
   
   public PersonRepository(PoolMembershipDbContext context)
   {
      _context = context;
   }
   
   public async Task<Person> AddAsync(Person person)
   {
      await _context.People.AddAsync(person);
      await _context.SaveChangesAsync();
      return person;
   }

   public async Task<IEnumerable<Person>> GetAllAsync()
   {
      return await _context.People.ToListAsync();
   }
}