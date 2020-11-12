using System.ComponentModel.DataAnnotations;

namespace Shoop.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este Campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este Campo deve conter entre 3 e 20 caracteres")]
        [MinLength(3, ErrorMessage = "Este Campo deve conter entre 3 e 20 caracteres")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Este Campo é Obrigatório")]
        [MaxLength(20, ErrorMessage = "Este Campo deve conter entre 3 e 20 caracteres")]
        [MinLength(3, ErrorMessage = "Este Campo deve conter entre 3 e 20 caracteres")]
        public string Password { get; set; }

        public string Role { get; set; } //permissão

    }
}