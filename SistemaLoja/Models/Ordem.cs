using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaLoja.Models
{
    public class Ordem
    {
        [Key]
        public int OrdemId { get; set; }

        public DateTime OrdemData { get; set; }

        public int CustomizarId { get; set; }

        public OrdemStatus OrdemStatus { get; set; }

        //Annotacion para permitir o uso do Json em relacionamentos.
        [JsonIgnore]
        public virtual Customizar Customizar { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrdemDetalhe> OrdensDetalhes { get; set; }

    }
}