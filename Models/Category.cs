
using System.ComponentModel.DataAnnotations;   
using System.ComponentModel.DataAnnotations.Schema; 

namespace Shoop.Models
{
    public class Category
    {
        // [Table("Categoria")] // para gerar uma tabela com nome diferente 

        [Key]     
        [Column("Cat_ID")] //nome da coluna (caso vc não queira o padrão)
        public int Id { get; set; }  

        [Required(ErrorMessage = " Campo Obrigatório")]
        [MaxLength(60, ErrorMessage = "Este Campo deve conter entre 3 e 60 carateres ")]
        [MinLength(3, ErrorMessage = "Este Campo deve conter entre 3 e 60 carateres ")]
        [DataType("nvarchar")]
        public string Title { get; set; }
    }
}