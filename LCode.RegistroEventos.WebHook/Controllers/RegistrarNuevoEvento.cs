using LCode.NETCore.Base._5._0.Configuracion;
using LCode.NETCore.Base._5._0.Logs;
using LCode.RegistroEventos.BD.Modelos;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LCode.RegistroEventos.WebHook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrarNuevoEvento : ControllerBase
    {
        private readonly IBus _bus;
        public RegistrarNuevoEvento(IBus bus)
        {
            _bus = bus;
        }
        // POST api/<RegistrarNuevoEvento>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] NuevoEvento value)
        {
            try
            {
                if (value != null)
                {
                    var IP=Request.HttpContext.Connection.RemoteIpAddress;
                    if (IP!=null)
                    {
                        value.IPOrigen = IP.ToString();
                    }
                    string ServidorMQ = BaseConfiguracion.ObtenerValor("ConfigMQ:RabbitMQ:Servidor");
                    string MQ = BaseConfiguracion.ObtenerValor("ConfigMQ:RabbitMQ:Cola");
                    Uri uri = new Uri("rabbitmq://" + ServidorMQ + "/" + MQ + "");
                    var endPoint = await _bus.GetSendEndpoint(uri);
                    await endPoint.Send<NuevoEvento>(value);
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                await Evento.Registrar(NETCore.Base._5._0.Entidades.TipoEvento.Error, ex);
                return StatusCode(500);
            }
        }
    }
}
