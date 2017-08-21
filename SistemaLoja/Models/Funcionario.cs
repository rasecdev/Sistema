using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaLoja.Models
{
    public class Funcionario
    {
        [Key]
        public int FuncionarioID { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Você precisa digitar o  {0}")]
        [StringLength(30, ErrorMessage = "Insira um nome com até 30 caracteres.")]
        public string Nome { get; set; }

        [Display(Name = "Último Nome")]
        [Required(ErrorMessage = "Você precisa digitar o  {0}")]
        public string Sobrenome { get; set; }

        [Display(Name = "Salário")]
        [Required(ErrorMessage = "Você precisa digitar o  {0}")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Salario { get; set; }

        [Display(Name = "Comissão")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = false)]
        public float Comissao { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }

        [Display(Name = "Admissão")]
        [Required(ErrorMessage = "Você precisa digitar a  {0}")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Date)]
        public DateTime Admissao { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [NotMapped]
        public int Idade { get { return DateTime.Now.Year - Nascimento.Year; }}

        [Required(ErrorMessage = "Você precisa selecionar o  {0}")]
        [Display(Name = "Tipo de Documento")]
        public int TipoDocumentoID { get; set; }

        public virtual TipoDocumento TipoDocumento { get; set; }
    }
}