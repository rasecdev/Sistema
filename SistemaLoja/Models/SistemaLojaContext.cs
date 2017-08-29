﻿using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SistemaLoja.Models
{
    public class SistemaLojaContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public SistemaLojaContext() : base("name=SistemaLojaContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            //Resolve o problema de formatação de caracters para o WEB API.
            this.Configuration.ProxyCreationEnabled = false;
        }

        // Não excluir em cascata
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //   base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<SistemaLoja.Models.Produto> Produtos { get; set; }

        public System.Data.Entity.DbSet<SistemaLoja.Models.TipoDocumento> TipoDocumentoes { get; set; }

        public System.Data.Entity.DbSet<SistemaLoja.Models.Funcionario> Funcionarios { get; set; }

        public System.Data.Entity.DbSet<SistemaLoja.Models.Fornecedor> Fornecedors { get; set; }

        public System.Data.Entity.DbSet<SistemaLoja.Models.Customizar> Customizars { get; set; }

        public System.Data.Entity.DbSet<SistemaLoja.Models.Ordem> Ordem { get; set; }

        public System.Data.Entity.DbSet<SistemaLoja.Models.OrdemDetalhe> OrdemDetalhe { get; set; }

        public System.Data.Entity.DbSet<SistemaLoja.Models.Categoria> Categorias { get; set; }

        public System.Data.Entity.DbSet<SistemaLoja.Models.OrdensAPI> OrdensAPIs { get; set; }
    }
}
