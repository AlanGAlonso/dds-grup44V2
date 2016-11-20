using System;
using System.Collections.Generic;
using System.Linq;
using TPDDSGrupo44.DataModels;

namespace TPDDSGrupo44.ViewModels
{
    public class SearchsPerDayViewModel : BaseViewModel
    {
        public IEnumerable<IGrouping<DateTime, Busqueda>> busquedasPorDia {get; set ;}

        public SearchsPerDayViewModel():base() {
            List<Busqueda> busquedas;
            using (var db = new BuscAR())
            {
               terminal = db.Terminales.Include("funcionalidades").Where(i => i.nombre == "UTN FRBA Lugano").Single();
               configuracion = db.Configuraciones.Single();
                
                busquedas = db.Busquedas.Include("terminal").ToList();
            }

            busquedasPorDia = busquedas.GroupBy(b => b.fecha);
        }
    }
}