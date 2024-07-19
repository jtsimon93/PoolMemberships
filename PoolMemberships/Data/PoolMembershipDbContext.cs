using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using PoolMemberships.Models;

namespace PoolMemberships.Data;

public class PoolMembershipDbContext : DbContext
{
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "PMM",
            "PoolMembershipDatabase.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    } 
    
}