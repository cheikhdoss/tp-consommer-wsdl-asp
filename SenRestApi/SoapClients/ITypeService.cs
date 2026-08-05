using System.ServiceModel;
using System.ServiceModel.Description;
using SenRestApi.Models;

namespace SenRestApi.SoapClients;

[ServiceContract(Namespace = "http://service.soap.exemple.com/")]
[XmlSerializerFormat]
public interface ITypeService
{
    [OperationContract(Action = "")]
    TypeProduit getType(long id);

    [OperationContract(Action = "")]
    TypeProduit createType(TypeProduit type);

    [OperationContract(Action = "")]
    TypeProduit[] getAllTypes();

    [OperationContract(Action = "")]
    TypeProduit updateType(long id, TypeProduit type);

    [OperationContract(Action = "")]
    bool deleteType(long id);
}
