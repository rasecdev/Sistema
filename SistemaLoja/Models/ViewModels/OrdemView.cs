using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaLoja.Models.ViewModels
{
    public class OrdemView
    {
        public Customizar Customizar { get; set; }

       // public ProdutoOrdem ProdutoOrdem { get; set; }

        public List<ProdutoOrdem> ProdutoOrdem { get; set; }

    }
}