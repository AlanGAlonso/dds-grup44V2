using System.Collections.Generic;
using System.Linq;
using TPDDSGrupo44.DataModels;
using TPDDSGrupo44.ViewModels;

namespace TPDDSGrupo44.Models
{
    public class AgregarAcciones : ActualizacionAsincronica
    {

        public AgregarAcciones () :base (){ }

        public override void actualizar(string funcionalidades) {

            string[] values = funcionalidades.Split(',').Select(sValue => sValue.Trim()).ToArray();
            List<FuncionalidadUsuario> func = new List<FuncionalidadUsuario>();
            foreach (string v in values)
            {
                func.Add(new FuncionalidadUsuario(v));
            }

            using (var db = new BuscAR())
            {
                LogAction log = new LogAction("Agregar Acciones Asinc", BaseViewModel.usuario.nombre);

                List<Rol> roles = db.Roles.ToList();

                foreach (Rol r in roles)
                {
                    int lote = r.funcionalidades.Aggregate((i1, i2) => i1.lote > i2.lote ? i1 : i2).lote + 1;
                    foreach (FuncionalidadUsuario f in func)
                    {
                        if (r.funcionalidades.Find(i => i.nombre == f.nombre) == null)
                        {
                            f.lote = lote;
                            r.funcionalidades.Add(f);
                        }
                    }
                }

                log.finalizarProceso("Exito");
                db.LogProcesosAsincronicos.Add(log);

                db.SaveChanges();

            }
        }

        public void deshacer()
        {
            using (var db = new BuscAR())
            {
                List<Rol> roles = db.Roles.ToList();

                foreach (Rol r in roles)
                {
                    int lote = r.funcionalidades.Aggregate((i1, i2) => i1.lote > i2.lote ? i1 : i2).lote;
                    List<FuncionalidadUsuario> ultimasFuncAgregadas = r.funcionalidades.Where(f => f.lote == lote).ToList();
                    r.funcionalidades = r.funcionalidades.Except(ultimasFuncAgregadas).ToList();
                }

                db.SaveChanges();
            }
        }

    }
}