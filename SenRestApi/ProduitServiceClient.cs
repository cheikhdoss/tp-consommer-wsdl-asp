using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;
using SenRestApi.Models;

namespace SenRestApi.SoapClients;

public class ProduitServiceClient : IProduitService
{
    private static readonly HttpClient Http = new() { Timeout = TimeSpan.FromSeconds(10) };
    private static readonly XNamespace SoapEnv = "http://schemas.xmlsoap.org/soap/envelope/";
    private static readonly XNamespace Ser = "http://service.soap.exemple.com/";
    private const string Endpoint = "http://localhost:9090/services/produits";

    private static XDocument Send(string operation, params XElement[] body)
    {
        var envelope = new XDocument(
            new XElement(SoapEnv + "Envelope",
                new XAttribute(XNamespace.Xmlns + "soapenv", SoapEnv),
                new XAttribute(XNamespace.Xmlns + "ser", Ser),
                new XElement(SoapEnv + "Body",
                    body.Length == 0
                        ? new XElement(Ser + operation)
                        : new XElement(Ser + operation, body))));

        var content = new StringContent(envelope.ToString(), Encoding.UTF8, "text/xml");
        content.Headers.Add("SOAPAction", "\"\"");

        var response = Http.PostAsync(Endpoint, content).GetAwaiter().GetResult();
        var xml = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var doc = XDocument.Parse(xml);

        var fault = doc.Descendants(SoapEnv + "Fault").FirstOrDefault();
        if (fault is not null)
        {
            throw new FaultException(
                fault.Element(SoapEnv + "Reason")?.Value ??
                fault.Element("faultstring")?.Value ??
                "Erreur SOAP inconnue");
        }

        return doc;
    }

    private static Produit? ParseProduit(XElement? el)
    {
        if (el is null) return null;
        return new Produit
        {
            Id = (long?)el.Element("id") ?? 0,
            Nom = (string?)el.Element("nom"),
            Prix = (double?)el.Element("prix") ?? 0,
            Quantite = (int?)el.Element("quantite") ?? 0,
            TypeId = (long?)el.Element("typeId") ?? 0
        };
    }

    private static XElement? FirstReturn(XDocument doc)
    {
        return doc.Descendants("return").FirstOrDefault();
    }

    private static IEnumerable<XElement> AllReturns(XDocument doc)
    {
        return doc.Descendants("return");
    }

    public Produit getProduit(long id)
    {
        var doc = Send("getProduit", new XElement("id", id));
        return ParseProduit(FirstReturn(doc)) ?? throw new FaultException($"Produit non trouvé : {id}");
    }

    public Produit createProduit(Produit produit)
    {
        var doc = Send("createProduit", new XElement("produit",
            new XElement("nom", produit.Nom),
            new XElement("prix", produit.Prix),
            new XElement("quantite", produit.Quantite),
            new XElement("typeId", produit.TypeId)));
        return ParseProduit(FirstReturn(doc)) ?? throw new FaultException("Création impossible");
    }

    public Produit[] getAllProduits()
    {
        var doc = Send("getAllProduits");
        return AllReturns(doc).Select(ParseProduit).Where(p => p is not null).Cast<Produit>().ToArray();
    }

    public Produit updateProduit(long id, Produit produit)
    {
        var doc = Send("updateProduit",
            new XElement("id", id),
            new XElement("produit",
                new XElement("nom", produit.Nom),
                new XElement("prix", produit.Prix),
                new XElement("quantite", produit.Quantite),
                new XElement("typeId", produit.TypeId)));
        return ParseProduit(FirstReturn(doc)) ?? throw new FaultException($"Produit non trouvé : {id}");
    }

    public bool deleteProduit(long id)
    {
        var doc = Send("deleteProduit", new XElement("id", id));
        return (bool?)FirstReturn(doc) ?? false;
    }
}
