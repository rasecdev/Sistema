using System.ComponentModel.DataAnnotations;

namespace SistemaLoja.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Você precisa selecionar o  {0}")]
        public string Descricao { get; set; }
    }
}