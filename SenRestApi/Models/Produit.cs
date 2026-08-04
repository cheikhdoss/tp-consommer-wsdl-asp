using System.Xml.Serialization;

namespace SenRestApi.Models;

[XmlType(Namespace = "http://service.soap.exemple.com/")]
public class Produit
{
    [XmlElement]
    public long Id { get; set; }

    [XmlElement]
    public string? Nom { get; set; }

    [XmlElement]
    public double Prix { get; set; }

    [XmlElement]
    public int Quantite { get; set; }

    [XmlElement]
    public long TypeId { get; set; }
}
