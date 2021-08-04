using LCode.NETCore.Base._5._0.Entidades;
using LCode.NETCore.Base._5._0.Logs;
using LCode.RegistroEventos.BD;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace LCode.RegistroEventos.ServicioProcesador.ConsumidoresMQ
{
    public class RegistroEventosConsumidor: IConsumer<AplicativoComponente>
    {
        private readonly Contexto _context = new Contexto();
        public async Task Consume(ConsumeContext<AplicativoComponente> context)
        {
            try
            {
                var data = context.Message;
                _context.Add(data);
                await _context.SaveChangesAsync();
            }
            catch (Exception Ex)
            {
                await Evento.ErrorAsync(Ex);
            }
        }
    }
}
