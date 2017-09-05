using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServiceLoja
    {

        [OperationContract]
        Produtoes GetProduto(int produtoId);

        [OperationContract]
        List<Produtoes> GetProdutos();

        // TODO: Add your service operations here
    }
}
