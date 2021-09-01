using Microsoft.EntityFrameworkCore;
using RefactorThis.Models;

namespace RefactorThis.Repositories
{
    /// <summary>
    /// AppDbContext
    /// </summary>
    public sealed class AppDbContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {            
        }
        /// <summary>
        /// Override Model creation
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Apply configurations for entity
            modelBuilder.ApplyConfiguration(new Product());
            modelBuilder.ApplyConfiguration(new ProductOption());
        }
    }
}