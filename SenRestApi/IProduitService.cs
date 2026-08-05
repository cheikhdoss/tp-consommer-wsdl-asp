using System.ServiceModel;
using System.ServiceModel.Description;
using SenRestApi.Models;

namespace SenRestApi.SoapClients;

[ServiceContract(Namespace = "http://service.soap.exemple.com/")]
[XmlSerializerFormat]
public interface IProduitService
{
    [OperationContract(Action = "")]
    Produit getProduit(long id);

    [OperationContract(Action = "")]
    Produit createProduit(Produit produit);

    [OperationContract(Action = "")]
    Produit[] getAllProduits();

    [OperationContract(Action = "")]
    Produit updateProduit(long id, Produit produit);

    [OperationContract(Action = "")]
    bool deleteProduit(long id);
}
