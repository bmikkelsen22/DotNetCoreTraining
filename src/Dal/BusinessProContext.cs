using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Dal
{
    public class BusinessProContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<PersonDepartment> PersonDepartment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./BusinessPro.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonDepartment>()
                .HasKey(t => new { t.PersonId, t.DepartmentId });
            modelBuilder.Entity<PersonDepartment>()
                .HasOne(pd => pd.Person)
                .WithMany(p => p.PersonDepartments)
                .HasForeignKey(pd => pd.PersonId);
            modelBuilder.Entity<PersonDepartment>()
                .HasOne(pd => pd.Department)
                .WithMany(d => d.PersonDepartments)
                .HasForeignKey(pd => pd.DepartmentId);
        }
    }
}
