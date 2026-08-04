using System.ServiceModel;
using System.ServiceModel.Description;
using SenRestApi.Models;

namespace SenRestApi.SoapClients;

[ServiceContract(Namespace = "http://service.soap.exemple.com/")]
[XmlSerializerFormat]
public interface IProduitService
{
    [OperationContract(Action = "", ReplyAction = "*")]
    Produit getProduit(long id);

    [OperationContract(Action = "", ReplyAction = "*")]
    Produit createProduit(Produit produit);

    [OperationContract(Action = "", ReplyAction = "*")]
    Produit[] getAllProduits();

    [OperationContract(Action = "", ReplyAction = "*")]
    Produit updateProduit(long id, Produit produit);

    [OperationContract(Action = "", ReplyAction = "*")]
    bool deleteProduit(long id);
}
