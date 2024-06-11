using Bus.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bus.Infrastructure.Data
{
	public class ApplicationDbContext:DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {
            
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}


		public DbSet<User> Users { get; set; }
        public DbSet<Traveller> Travellers { get; set; }

        public DbSet<Request> Requests { get; set; }
        public DbSet<Destination> Destinations { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<TravellerHistorySearch> TravellerHistorySearchs { get; set; }
      
    }
}
