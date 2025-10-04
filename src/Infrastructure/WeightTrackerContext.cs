using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightTracker.Infrastructure
{
    public class WeightTrackerContext : DbContext
    {
        private readonly string _connectionString;
        public WeightTrackerContext(IOptions<DatabaseSettings> dbOptions)
        {
            _connectionString = dbOptions.Value.ConnectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) 
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
        public DbSet<Users> Users { get; set; }
    }
}
