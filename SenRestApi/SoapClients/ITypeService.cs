using System.ServiceModel;
using System.ServiceModel.Description;
using SenRestApi.Models;

namespace SenRestApi.SoapClients;

[ServiceContract(Namespace = "http://service.soap.exemple.com/")]
[XmlSerializerFormat]
public interface ITypeService
{
    [OperationContract(Action = "", ReplyAction = "*")]
    TypeProduit getType(long id);

    [OperationContract(Action = "", ReplyAction = "*")]
    TypeProduit createType(TypeProduit type);

    [OperationContract(Action = "", ReplyAction = "*")]
    TypeProduit[] getAllTypes();

    [OperationContract(Action = "", ReplyAction = "*")]
    TypeProduit updateType(long id, TypeProduit type);

    [OperationContract(Action = "", ReplyAction = "*")]
    bool deleteType(long id);
}
