using System.ComponentModel.DataAnnotations;

namespace SistemaLoja.Models
{
    public class FornecedorProduto
    {
        [Key]

        public int FornecedorProdutoId { get; set; }

        public int FornecedorId { get; set; }

        public int ProdutoId { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }

        public virtual Produto Produto { get; set; }
    }
}