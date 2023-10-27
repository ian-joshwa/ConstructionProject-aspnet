using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Construction.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Construction.DataAccessLayer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Service> Services { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<StockType> StockTypes { get; set; }

        public DbSet<StockUpdates> StockUpdates { get; set; }

        public DbSet<TaskList> TaskList { get; set; }

        public DbSet<SiteUpdate> SiteUpdates { get; set; }

    }
}
