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

            using (var db = new BuscAR())
            {
                LogAction log = new LogAction("Agregar Acciones Asinc", BaseViewModel.usuario.nombre);
                try
                {

                    string[] values = funcionalidades.Split(',').Select(sValue => sValue.Trim()).ToArray();
            List<FuncionalidadUsuario> func = new List<FuncionalidadUsuario>();
            foreach (string v in values)
            {
                        if (v != "") { 
                            func.Add(new FuncionalidadUsuario(v));
                        }
                    }
            

                

                    List<Rol> roles = db.Roles.ToList();

                    foreach (Rol r in roles)
                    {
                        int lote = r.funcionalidades.Aggregate((i1, i2) => i1.lote > i2.lote ? i1 : i2).lote + 1;
                        
                            foreach (FuncionalidadUsuario f in func)
                            {
                                if (r.funcionalidades.Find(i => i.nombre == f.nombre) == null)
                                {
                                    FuncionalidadUsuario funcio = new FuncionalidadUsuario(f.nombre);
                                    funcio.lote = lote;
                                    r.funcionalidades.Add(funcio);
                                }
                            }
                        
                    }

                    log.finalizarProceso("Exito");
                    db.LogProcesosAsincronicos.Add(log);
                }
                catch
                {
                    log.finalizarProceso("Error", "Hubo un problema inesperado en la ejecución del proceso, y el mismo no se pudo completar.");
                    db.LogProcesosAsincronicos.Add(log);
                }

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
                    if (lote > 1) { 
                        List<FuncionalidadUsuario> ultimasFuncAgregadas = r.funcionalidades.Where(f => f.lote == lote).ToList();
                        r.funcionalidades = r.funcionalidades.Except(ultimasFuncAgregadas).ToList();
                    }
                }

                db.SaveChanges();
            }
        }

    }
}