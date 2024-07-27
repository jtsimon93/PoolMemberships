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
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "PMM");

        if (!Directory.Exists(dbPath)) Directory.CreateDirectory(dbPath);

        var dbFile = Path.Combine(dbPath, "PoolMembershipDatabase.db");

        if (!File.Exists(dbFile)) File.Create(dbFile).Close();

        optionsBuilder.UseSqlite($"Data Source={dbFile}");
    }
}