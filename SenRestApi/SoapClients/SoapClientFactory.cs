using System.ServiceModel;
using System.ServiceModel.Description;

namespace SenRestApi.SoapClients;

public static class SoapClientFactory
{
    private static readonly BasicHttpBinding Binding = new()
    {
        SendTimeout = TimeSpan.FromSeconds(10),
        MaxReceivedMessageSize = 65536
    };

    private static readonly EndpointAddress ProduitEndpoint =
        new("http://localhost:9090/services/produits");

    private static readonly EndpointAddress TypeEndpoint =
        new("http://localhost:9090/services/types");

    public static IProduitService CreateProduitClient()
    {
        var factory = new ChannelFactory<IProduitService>(Binding, ProduitEndpoint);
        return factory.CreateChannel();
    }

    public static ITypeService CreateTypeClient()
    {
        var factory = new ChannelFactory<ITypeService>(Binding, TypeEndpoint);
        return factory.CreateChannel();
    }
}
