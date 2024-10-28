using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LMS.Models;

namespace LMS.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Activity> Activities { get; set; }
    }
}
