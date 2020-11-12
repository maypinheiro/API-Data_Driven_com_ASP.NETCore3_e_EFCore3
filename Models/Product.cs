using System.ComponentModel.DataAnnotations;

namespace Shoop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este Campo é Obrigatório")]
        [MaxLength(60, ErrorMessage = "Este Campo deve conter de 3 a 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este Campo deve conter de 3 a 60 caracteres")]
        public string Title { get; set; }

        [MaxLength(1024, ErrorMessage = "Este campo deve conter no Maximo 1024 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "O preço deve ser maior que 0")]
        [Range(1, int.MaxValue, ErrorMessage = "")]
        public decimal Price { get; set; }  

        [Required(ErrorMessage = "Este Campo e Obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Categoria Invalida")]  //faixa
        public int CategoryId { get; set; }
        public Category Category { get; set; }


    }
}