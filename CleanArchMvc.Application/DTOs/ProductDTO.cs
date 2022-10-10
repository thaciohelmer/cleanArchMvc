using CleanArchMvc.Domain.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CleanArchMvc.Application.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [MinLength(3)]
        [MaxLength(100)]
        [DisplayName("Name")]
        [Required(ErrorMessage = "The Name is Required")]
        public string Name { get; set; }

        [MinLength(5)]
        [MaxLength(200)]
        [DisplayName("Description")]
        [Required(ErrorMessage = "The Description is Required")]
        public string Description { get; set; }

        [DisplayName("Price")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "The Price is Required")]
        public decimal Price { get; set; }

        [Range(1, 9999)]
        [DisplayName("Stock")]
        [Required(ErrorMessage = "The Stock is Required")]
        public int Stock { get; set; }

        [MaxLength(250)]
        [DisplayName("Product Image")]
        public string Image { get; set; }

        [DisplayName("Categories")]
        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }
    }
}
