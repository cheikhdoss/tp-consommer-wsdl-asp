using System.Xml.Serialization;

namespace SenRestApi.Models;

[XmlType(TypeName = "type", Namespace = "")]
public class TypeProduit
{
    [XmlElement("id")]
    public long Id { get; set; }

    [XmlElement("libelle")]
    public string? Libelle { get; set; }

    [XmlElement("description")]
    public string? Description { get; set; }
}
