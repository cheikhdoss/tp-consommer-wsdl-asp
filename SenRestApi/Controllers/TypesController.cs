using System.ServiceModel;
using Microsoft.AspNetCore.Mvc;
using SenRestApi.Models;
using SenRestApi.SoapClients;

namespace SenRestApi.Controllers;

[ApiController]
[Route("api/v1/types")]
public class TypesController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var client = SoapClientFactory.CreateTypeClient();
            var types = client.getAllTypes();
            return Ok(types);
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
            var client = SoapClientFactory.CreateTypeClient();
            var type = client.getType(id);
            return Ok(type);
        }
        catch (FaultException)
        {
            return NotFound(new { message = "Type non trouvé : " + id });
        }
        catch (Exception ex)
        {
            return StatusCode(502, new { message = "Erreur SOAP : " + ex.Message });
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] TypeProduit type)
    {
        try
        {
            var client = SoapClientFactory.CreateTypeClient();
            var created = client.createType(type);
            return StatusCode(201, created);
        }
        catch (Exception ex)
        {
            return StatusCode(502, new { message = "Erreur SOAP : " + ex.Message });
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(long id, [FromBody] TypeProduit type)
    {
        try
        {
            var client = SoapClientFactory.CreateTypeClient();
            var updated = client.updateType(id, type);
            return Ok(updated);
        }
        catch (FaultException)
        {
            return NotFound(new { message = "Type non trouvé : " + id });
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
            var client = SoapClientFactory.CreateTypeClient();
            client.deleteType(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(502, new { message = "Erreur SOAP : " + ex.Message });
        }
    }
}
