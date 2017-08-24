using System.Collections.Generic;

namespace SistemaLoja.Models.ViewModels
{
    public class OrdemView
    {
        public Customizar Customizar { get; set; }

        public ProdutoOrdem Produto { get; set; }

        public List<ProdutoOrdem> Produtos { get; set; }

    }
}