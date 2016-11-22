using System;
using System.Collections.Generic;
using System.Linq;
using TPDDSGrupo44.DataModels;
using TPDDSGrupo44.Models;

namespace TPDDSGrupo44.ViewModels
{
    public class AsynchronusProceduresViewModel : BaseViewModel
    {
        public List<ActualizacionAsincronica> actualizaciones {get; set ;}
        public List<FuncionalidadUsuario> funcionalidadesUsuarios { get; set; }

        public AsynchronusProceduresViewModel():base() {
            using (var db = new BuscAR())
            {
                actualizaciones = db.FuncionalidadesUsuarios.OfType<ActualizacionAsincronica>().GroupBy(f => f.nombre).Select(f => f.FirstOrDefault()).ToList();
                funcionalidadesUsuarios = db.FuncionalidadesUsuarios.GroupBy(f => f.nombre).Select(f => f.FirstOrDefault()).ToList();
            }

        }
    }
}