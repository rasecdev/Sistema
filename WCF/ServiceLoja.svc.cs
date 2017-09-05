using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IServiceLoja
    {
        public Produtoes GetProduto(int produtoId)
        {
            var db = new SistemaLojaEntities();

            var produto = db.Produtoes.Find(produtoId);

            db.Dispose();

            return produto;

        }

        public List<Produtoes> GetProdutos()
        {
            var db = new SistemaLojaEntities();

            var produtos = db.Produtoes.ToList();

            db.Dispose();

            return produtos;
        }
    }
}
