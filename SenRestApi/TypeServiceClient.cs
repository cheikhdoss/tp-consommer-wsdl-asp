using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;
using SenRestApi.Models;

namespace SenRestApi.SoapClients;

public class TypeServiceClient : ITypeService
{
    private static readonly HttpClient Http = new() { Timeout = TimeSpan.FromSeconds(10) };
    private static readonly XNamespace SoapEnv = "http://schemas.xmlsoap.org/soap/envelope/";
    private static readonly XNamespace Ser = "http://service.soap.exemple.com/";
    private const string Endpoint = "http://localhost:9090/services/types";

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

    private static TypeProduit? ParseType(XElement? el)
    {
        if (el is null) return null;
        return new TypeProduit
        {
            Id = (long?)el.Element("id") ?? 0,
            Libelle = (string?)el.Element("libelle"),
            Description = (string?)el.Element("description")
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

    public TypeProduit getType(long id)
    {
        var doc = Send("getType", new XElement("id", id));
        return ParseType(FirstReturn(doc)) ?? throw new FaultException($"Type non trouvé : {id}");
    }

    public TypeProduit createType(TypeProduit type)
    {
        var doc = Send("createType", new XElement("type",
            new XElement("libelle", type.Libelle),
            new XElement("description", type.Description)));
        return ParseType(FirstReturn(doc)) ?? throw new FaultException("Création impossible");
    }

    public TypeProduit[] getAllTypes()
    {
        var doc = Send("getAllTypes");
        return AllReturns(doc).Select(ParseType).Where(t => t is not null).Cast<TypeProduit>().ToArray();
    }

    public TypeProduit updateType(long id, TypeProduit type)
    {
        var doc = Send("updateType",
            new XElement("id", id),
            new XElement("type",
                new XElement("libelle", type.Libelle),
                new XElement("description", type.Description)));
        return ParseType(FirstReturn(doc)) ?? throw new FaultException($"Type non trouvé : {id}");
    }

    public bool deleteType(long id)
    {
        var doc = Send("deleteType", new XElement("id", id));
        return (bool?)FirstReturn(doc) ?? false;
    }
}
