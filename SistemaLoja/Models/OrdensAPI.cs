using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaLoja.Models
{
    public class OrdensAPI
    {
        //Mudar o nome da Key, pois o model Ordem possui a mesmo nome.
        [Key]
        public int OrdensId { get; set; }

        public DateTime OrdemData { get; set; }

        public Customizar Customizar { get; set; }

        public OrdemStatus OrdemStatus { get; set; }

        public ICollection<OrdemDetalhe> Detalhes { get; set; }
    }
}