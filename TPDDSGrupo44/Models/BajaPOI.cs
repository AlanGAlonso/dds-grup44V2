using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;
using TPDDSGrupo44.DataModels;
using TPDDSGrupo44.ViewModels;

namespace TPDDSGrupo44.Models
{
    public class BajaPOI : ActualizacionAsincronica
    {
        public BajaPOI() { }

        public override void actualizar()
        {
            using (var db = new BuscAR())
            {
                LogAction log = new LogAction("Baja POIs Asinc", BaseViewModel.usuario.nombre);

                try
                {
                    string url = "http://demo3537367.mockable.io/trash/pois";
                    var jsonString = string.Empty;

                    var client = new WebClient();
                    jsonString = client.DownloadString(url);

                    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

                    List<JsonBajaPOI> listbp = (List<JsonBajaPOI>)javaScriptSerializer.Deserialize(jsonString, typeof(List<JsonBajaPOI>));

                    foreach (JsonBajaPOI p in listbp)
                    {
                        SearchViewModel modeloVista = MotorDeBusqueda.buscar(p.id.ToString());

                        if (modeloVista.paradasEncontradas.Count > 0)
                        {
                            ParadaDeColectivo.eliminarParada(modeloVista.paradasEncontradas.Single().id);

                        }
                        else if (modeloVista.paradasEncontradasCerca.Count > 0)
                        {
                            ParadaDeColectivo.eliminarParada(modeloVista.paradasEncontradasCerca.Single().id);
                        }

                        else if (modeloVista.bancosEncontrados.Count > 0)
                        {
                            Banco.eliminarBanco(modeloVista.bancosEncontrados.Single().id);
                        }
                        else if (modeloVista.bancosEncontradosCerca.Count > 0)
                        {
                            Banco.eliminarBanco(modeloVista.bancosEncontradosCerca.Single().id);
                        }

                        else if (modeloVista.cgpsEncontrados.Count > 0)
                        {
                            CGP.eliminarCGP(modeloVista.cgpsEncontrados.Single().id);
                        }
                        else if (modeloVista.cgpsEncontradosCerca.Count > 0)
                        {
                            CGP.eliminarCGP(modeloVista.cgpsEncontradosCerca.Single().id);
                        }
                        else if (modeloVista.localesEncontrados.Count > 0)
                        {
                            LocalComercial.eliminarLocComercial(modeloVista.localesEncontrados.Single().id);
                        }
                        else if (modeloVista.localesEncontradosCerca.Count > 0)
                        {
                            LocalComercial.eliminarLocComercial(modeloVista.localesEncontradosCerca.Single().id);
                        }
                    }
                }
                catch
                {
                    log.finalizarProceso("Error", "Hubo un problema inesperado en la ejecución del proceso, y el mismo no se pudo completar.");
                    db.LogProcesosAsincronicos.Add(log);
                
            }
                log.finalizarProceso("Exito");
                db.LogProcesosAsincronicos.Add(log);

                db.SaveChanges();

            }
        }
        
        }
    }
