using LCode.NETCore.Base._5._0.Entidades;
using LCode.NETCore.Base._5._0.Logs;
using LCode.RegistroEventos.BD;
using MassTransit;
using Microsoft.EntityFrameworkCore;
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
                var ApliComp = await _context.AplicacionesComponentes.FirstOrDefaultAsync(m => m.NombreComponente == data.NombreComponente && m.NombreComponenteCompleto == data.NombreComponenteCompleto);
                if (ApliComp == null)
                {
                    data.FechaHoraRegistro = DateTime.Now;
                    foreach (EventoOrigen Origen in data.ListaOrigen)
                    {

                        Origen.FechaHoraRegistro = DateTime.Now;
                        foreach (EventoEntidad Evnt in Origen.ListaEventos)
                        {
                            Evnt.FechaHoraEvento = DateTime.Now;
                            foreach (RastroEntidad Rastro in Evnt.ListaRastros)
                            {
                                Rastro.FechaHoraRastro = DateTime.Now;
                            }
                        }
                    }
                    _context.Add(data);
                    await _context.SaveChangesAsync();
                }
                else
                { 
                    foreach (EventoOrigen Origen in data.ListaOrigen)
                    {
                        var OrigenVar = await _context.EventosOrigenes.FirstOrDefaultAsync(m => m.IdAplicativoComponente == ApliComp.IdAplicativoComponente && m.IPOrigen == Origen.IPOrigen && m.NombreHost == Origen.NombreHost && m.Version == Origen.Version && m.EsDocker == Origen.EsDocker);
                        if (OrigenVar==null)
                        { 
                            Origen.FechaHoraRegistro = DateTime.Now;
                            Origen.IdAplicativoComponente = ApliComp.IdAplicativoComponente;
                            foreach (EventoEntidad Evnt in Origen.ListaEventos)
                            {
                                Evnt.FechaHoraEvento = DateTime.Now;
                                foreach (RastroEntidad Rastro in Evnt.ListaRastros)
                                {
                                    Rastro.FechaHoraRastro = DateTime.Now;
                                }
                            }
                            _context.Add(Origen);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            foreach (EventoEntidad Evnt in Origen.ListaEventos)
                            {
                                Evnt.IdEventoOrigen = OrigenVar.IdEventoOrigen;
                                Evnt.FechaHoraEvento = DateTime.Now;
                                foreach (RastroEntidad Rastro in Evnt.ListaRastros)
                                {
                                    Rastro.FechaHoraRastro = DateTime.Now;
                                }
                                _context.Add(Evnt);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }

                //var ApliComp = await _context.AplicacionesComponentes.FirstOrDefaultAsync(m => m.NombreComponente == data.NombreComponente && m.NombreComponenteCompleto == data.NombreComponenteCompleto);
                //if (ApliComp!=null)
                //{ 
                //    data.IdAplicativoComponente= ApliComp.IdAplicativoComponente;
                //    data.FechaHoraRegistro = ApliComp.FechaHoraRegistro;
                //}
                //else { 
                //data.FechaHoraRegistro = DateTime.Now;
                //}
                //foreach (EventoOrigen Origen in data.ListaOrigen)
                //{
                //    var OrigenVar = await _context.EventosOrigenes.FirstOrDefaultAsync(m => m.IdAplicativoComponente == data.IdAplicativoComponente && m.IPOrigen == Origen.IPOrigen && m.NombreHost==Origen.NombreHost&&m.Version==Origen.Version&&m.EsDocker==Origen.EsDocker);
                //    if (OrigenVar != null)
                //    {
                //        Origen.IdEventoOrigen= OrigenVar.IdEventoOrigen;
                //        Origen.IdAplicativoComponente = OrigenVar.IdAplicativoComponente;
                //        Origen.FechaHoraRegistro = OrigenVar.FechaHoraRegistro;
                //    }
                //    else
                //    {
                //        Origen.FechaHoraRegistro = DateTime.Now;
                //    }
                //    foreach (EventoEntidad Evnt in Origen.ListaEventos)
                //    {
                //        Evnt.FechaHoraEvento = DateTime.Now;
                //        foreach(RastroEntidad Rastro in Evnt.ListaRastros)
                //        {
                //            Rastro.FechaHoraRastro = DateTime.Now;
                //        }
                //    }
                //}

            }
            catch (Exception Ex)
            {
                await Evento.ErrorAsync(Ex);
            }
        }
    }
}
