using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using RefactorThis.Filters;

namespace RefactorThis.Models
{
    /// <summary>
    /// Product Model
    /// </summary>
    public class Product : IEntityTypeConfiguration<Product>
    {
        /// <summary>
        /// Get or Sets the Product  Id
        /// </summary>
        [ValidateGuid]
        public Guid Id { get; set; }

        /// <summary>
        /// Get or Sets the Product Name
        /// </summary>

        [Required(ErrorMessage = "The Name is mandatory")]
        [StringLength(32, MinimumLength = 2)]
        [RegularExpression(@"[^a-zA-Z0-9_ ]*$")]
        public string Name { get; set; }

        /// <summary>
        /// Get or Sets the Product Description
        /// </summary>
        [Required(ErrorMessage = "The Description is mandatory")]
        [StringLength(50, MinimumLength = 3)]
        [RegularExpression(@"[^a-zA-Z0-9_ ]*$")]
        public string Description { get; set; }

        /// <summary>
        /// Get or Sets the Product Price
        /// </summary>
        [Range(0, 9999)]
        public decimal Price { get; set; }

        /// <summary>
        /// Get or Sets the Product Delivery Price
        /// </summary>
        [Range(0, 9999)]
        public decimal DeliveryPrice { get; set; }

        [JsonIgnore] public bool IsNew { get; }
        
        [JsonProperty("ProductOptions")]
        public IEnumerable<ProductOption> ProductOptions{ get; set; }
        
        /// <summary>
        /// Configure the product model
        /// </summary>
        /// <param name="builder"></param>

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            // Set key for entity
            builder.HasKey(p => p.Id);
            // Set configuration for columns            
            builder.Property(p => p.Name);
            builder.Property(p => p.Description);
            builder.Property(p => p.Price);
            builder.Property(p => p.DeliveryPrice);            
        }
    }
}
