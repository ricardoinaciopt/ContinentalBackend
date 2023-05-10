using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContinentalBackend.Data;
using ContinentalBackend.Models;
using ContinentalBackend.Broker;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContinentalBackend.Controllers
{

    [Route("Alert")]
    [ApiController]
    public class AlertasController : ControllerBase
    {
        private readonly ContinentalBackendContext _context;
        private readonly AlertBroker _broker;

        public AlertasController(ContinentalBackendContext context, AlertBroker alertBroker)
        {
            _context = context;
            _broker = alertBroker;
        }

        [HttpGet("GetMaintenanceMessages")]
        public async Task<ActionResult<IEnumerable<Alerta>>> GetMaintenanceMessages( bool? all)
        {
            if (_context.Alerta == null)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
               
                if (all != null && all == true)
                {
                    return await _context.Alerta.ToListAsync();
                }
                else
                {
                    return await _context.Alerta.Where(a => a.Estado.Equals(true)).ToListAsync();
                }
            }
            else 
            {
                return Unauthorized();
            }

          
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Alerta>> GetAlerta(long id)
        {
            if (_context.Alerta == null)
            {
                return NotFound();
            }
            var alerta = await _context.Alerta.FindAsync(id);

            if (alerta == null)
            {
                return NotFound();
            }

            return alerta;
        }


       
        [HttpPut("AcknowledgeMaintenanceMessage")]
        public async Task<ActionResult<Alerta>> AcknowledgeMaintenanceMessage(long id)
        {
            if (_context.Alerta == null)
            {
                return NotFound();
            }
            var alerta = await _context.Alerta.FindAsync(id);

            if (alerta == null)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
                alerta.Estado = false;
                _context.Update(alerta);
                await _context.SaveChangesAsync();

                return alerta;
            }

            return  Unauthorized();
        }


        [HttpPost("SendAlert")]
        public async Task<ActionResult<Alerta>> SendAlert(Alerta alerta)
        {
            if (_context.Alerta == null)
            {
                return Problem("Entity set 'AlertasContext.Alerta'  is null.");
            }

            if (User.Identity.IsAuthenticated)
            {
                var email = User.Claims.SingleOrDefault(c => c.Type == "email");   //email do utilizador autenticado

                alerta.FuncionarioId = email.Value.ToString(); //gurada no estado do modelo

                _context.Alerta.Add(alerta);
                await _context.SaveChangesAsync();

                string bindingKey = "alerts."; 
                if(alerta.Tipo != null && alerta.Tipo == "Avaria")
                {
                    bindingKey += "maintenance.new";
                }
                else if(alerta.Tipo != null && alerta.Tipo == "Material")
                {
                    bindingKey += "material.new";
                }

                string alertajson = JsonSerializer.Serialize(alerta);

                _broker.SendAlerta(bindingKey, alertajson);

                return CreatedAtAction("GetAlerta", new { id = alerta.Id }, alerta);
            }

            return Unauthorized();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlerta(long id)
        {
            if (_context.Alerta == null)
            {
                return NotFound();
            }
            var alerta = await _context.Alerta.FindAsync(id);
            if (alerta == null)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
                _context.Alerta.Remove(alerta);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            return Unauthorized();

        }

        private bool AlertaExists(long id)
        {
            return (_context.Alerta?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
