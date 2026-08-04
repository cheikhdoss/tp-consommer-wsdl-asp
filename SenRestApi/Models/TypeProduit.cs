using System.Xml.Serialization;

namespace SenRestApi.Models;

[XmlType(Namespace = "http://service.soap.exemple.com/")]
public class TypeProduit
{
    [XmlElement]
    public long Id { get; set; }

    [XmlElement]
    public string? Libelle { get; set; }

    [XmlElement]
    public string? Description { get; set; }
}
