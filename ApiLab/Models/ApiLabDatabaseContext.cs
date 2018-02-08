using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiLab.Models;

namespace ApiLab.Models
{
    /// <summary>
    /// The ApiLab database context.
    /// </summary>
    public class ApiLabDatabaseContext : DbContext
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"></param>
        public ApiLabDatabaseContext(DbContextOptions<ApiLabDatabaseContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Get apps
        /// </summary>
        public DbSet<App> Apps { get; set; }

        /// <summary>
        /// Get messages
        /// </summary>
        public DbSet<Message> Messages { get; set; }

        /// <summary>
        /// Get resource activities
        /// </summary>
        public DbSet<ResourceActivity> ResourceActivities { get; set; }
    }
}
