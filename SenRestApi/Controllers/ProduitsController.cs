using System.ServiceModel;
using Microsoft.AspNetCore.Mvc;
using SenRestApi.Models;
using SenRestApi.SoapClients;

namespace SenRestApi.Controllers;

[ApiController]
[Route("api/v1/produits")]
public class ProduitsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var client = SoapClientFactory.CreateProduitClient();
            var produits = client.getAllProduits();
            return Ok(produits);
        }
        catch (Exception ex)
        {
            return StatusCode(502, new { message = "Erreur SOAP : " + ex.Message });
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(long id)
    {
        try
        {
            var client = SoapClientFactory.CreateProduitClient();
            var produit = client.getProduit(id);
            return Ok(produit);
        }
        catch (FaultException)
        {
            return NotFound(new { message = "Produit non trouvé : " + id });
        }
        catch (Exception ex)
        {
            return StatusCode(502, new { message = "Erreur SOAP : " + ex.Message });
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] Produit produit)
    {
        try
        {
            var client = SoapClientFactory.CreateProduitClient();
            var created = client.createProduit(produit);
            return StatusCode(201, created);
        }
        catch (Exception ex)
        {
            return StatusCode(502, new { message = "Erreur SOAP : " + ex.Message });
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(long id, [FromBody] Produit produit)
    {
        try
        {
            var client = SoapClientFactory.CreateProduitClient();
            var updated = client.updateProduit(id, produit);
            return Ok(updated);
        }
        catch (FaultException)
        {
            return NotFound(new { message = "Produit non trouvé : " + id });
        }
        catch (Exception ex)
        {
            return StatusCode(502, new { message = "Erreur SOAP : " + ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        try
        {
            var client = SoapClientFactory.CreateProduitClient();
            client.deleteProduit(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(502, new { message = "Erreur SOAP : " + ex.Message });
        }
    }
}
