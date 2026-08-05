using System.Xml.Serialization;

namespace SenRestApi.Models;

[XmlType(TypeName = "produit", Namespace = "")]
public class Produit
{
    [XmlElement("id")]
    public long Id { get; set; }

    [XmlElement("nom")]
    public string? Nom { get; set; }

    [XmlElement("prix")]
    public double Prix { get; set; }

    [XmlElement("quantite")]
    public int Quantite { get; set; }

    [XmlElement("typeId")]
    public long TypeId { get; set; }
}
