

using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoboShopv1.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [DisplayName("Title")]
        public string? Title { get; set; }

        [DisplayName("Short Description")]
        public string? ShortDescription { get; set; }

        [DisplayName("Long Description")]
        public string? LongDescription { get; set; }

        public string? ImageURL1 { get; set; }
        public string? ImageURL2 { get; set; }

        [DisplayName("Price")]
        public double Price { get; set; }

        [DisplayName("Category")]
        public ProductType? Category { get; set; }

        [NotMapped]
        public IFormFile? ImageFile1 { get; set; }
        [NotMapped]
        public IFormFile? ImageFile2 { get; set; }
    }
}
