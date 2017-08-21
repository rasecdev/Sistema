using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaLoja.Models
{
    public class Fornecedor
    {
        [Key]
        public int FornecedorId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Você precisa digitar o  {0}")]
        public string Nome { get; set; }

        [Display(Name = "Último Nome")]
        [Required(ErrorMessage = "Você precisa digitar o  {0}")]
        public string Sobrenome { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Você precisa digitar o  {0}")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public virtual ICollection<FornecedorProduto> FornecedorProduto { get; set; }
    }
    
}