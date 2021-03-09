using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1_kasperski.Rest.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1_kasperski.Rest.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Person> People { get; set; }
    }
}
