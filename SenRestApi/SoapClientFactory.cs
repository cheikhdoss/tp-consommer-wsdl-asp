using System.ServiceModel;

namespace SenRestApi.SoapClients;

public static class SoapClientFactory
{
    public static IProduitService CreateProduitClient()
    {
        return new ProduitServiceClient();
    }

    public static ITypeService CreateTypeClient()
    {
        return new TypeServiceClient();
    }
}
