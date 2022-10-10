using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.Application.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [MinLength(3)]
        [MaxLength(100)]
        [Required(ErrorMessage = "The Name is Required")]
        public string Name { get; set; }
    }
}
