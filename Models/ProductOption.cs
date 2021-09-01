using System;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RefactorThis.Filters;
using System.ComponentModel.DataAnnotations;

namespace RefactorThis.Models
{
    /// <summary>
    /// ProductOption Model class
    /// </summary>
    public class ProductOption : IEntityTypeConfiguration<ProductOption>
    {
        /// <summary>
        /// Get or Sets the Product Option Id
        /// </summary>
        [ValidateGuid]
        public Guid Id { get; set; }
        /// <summary>
        /// Get or Sets the Product  Id
        /// </summary>
        [ValidateGuid] 
        public Guid ProductId { get; set; }

        /// <summary>
        /// Get or Sets the Product Option Name
        /// </summary>
        [Required(ErrorMessage = "The Name is mandatory")]
        [StringLength(32, MinimumLength = 2)]
        [RegularExpression(@"[^a-zA-Z0-9_ ]*$")]
        public string Name { get; set; }

        /// <summary>
        /// Get or Sets the Product Option Description
        /// </summary>
        [Required(ErrorMessage = "The Description is mandatory")]
        [StringLength(50, MinimumLength = 3)]
        [RegularExpression(@"[^a-zA-Z0-9_ ]*$")]
        public string Description { get; set; }
       
        public void Configure(EntityTypeBuilder<ProductOption> builder)
        {
            builder.ToTable("productoptions");            
            // Set key for entity
            builder.HasKey(p => p.Id);
            builder.Property(p => p.ProductId);
            // Set configuration for columns            
            builder.Property(p => p.Name);
            builder.Property(p => p.Description);            
        }
    }
}
