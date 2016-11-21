using System;
using System.Collections.Generic;
using System.Linq;
using TPDDSGrupo44.DataModels;

namespace TPDDSGrupo44.ViewModels
{
    public class SearchsPerResultViewModel : BaseViewModel
    {
        public Dictionary<DispositivoTactil, List<KeyValuePair<DateTime, int>>> busquedasPorResultados {get; set ;}

        public SearchsPerResultViewModel():base() {
            List<Busqueda> busquedas;
            using (var db = new BuscAR())
            {
               terminal = db.Terminales.Include("funcionalidades").Where(i => i.nombre == "UTN FRBA Lugano").Single();
               configuracion = db.Configuraciones.Single();
                
                busquedas = db.Busquedas.Include("terminal").ToList();
            }

            busquedasPorResultados = busquedas.GroupBy(b => b.terminal,
                (key, g) => new 
                 {
                Terminal = key,
                Detail = g.GroupBy(b => b.fecha, 
                    (fecha, busqueda) => new {
                        Fecha = fecha,
                        Resultados = busqueda.Sum(b => b.cantidadDeResultados)
                    }).ToDictionary(d => d.Fecha, d => d.Resultados)
            }).ToDictionary(g => g.Terminal, g => g.Detail.ToList());

        }
    }
}