using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TPDDSGrupo44.ViewModels;

namespace TPDDSGrupo44.DataModels
{
    public class Busqueda
    {
        [Key]
        ////////////////Atributos////////////////
        public int Id { get; set; }
        public string textoBuscado { get; set; }
        public int cantidadDeResultados { get; set; }
        public DateTime fecha { get; set; }
        public string usuario { get; set; }
        public DispositivoTactil terminal { get; set; }
        public TimeSpan duracionDeBusqueda { get; set; }

        ////////////////Constructor vacio////////////////
        public Busqueda () { }




        public Busqueda(string texto, int resultados, DateTime fechaBusqueda, TimeSpan duracion)
        {
            textoBuscado = texto;
            cantidadDeResultados = resultados;
            fecha = fechaBusqueda;
            duracionDeBusqueda = duracion;
            if (BaseViewModel.usuario != null )
            {
                usuario = BaseViewModel.usuario.nombre;
            }
        }

        public static void registrarBusqueda(string palabraBusqueda, int resultados, DispositivoTactil dispositivoTactil, TimeSpan duracion)
        {
            using (var db = new BuscAR())
            {
                DateTime today = DateTime.Today;
                Busqueda busqueda = new Busqueda(palabraBusqueda, resultados, today, duracion);
                db.Busquedas.Add(busqueda);
                db.SaveChanges();

                busqueda = db.Busquedas.Where(b=>b.Id == busqueda.Id).Single();
                DispositivoTactil terminal = db.Terminales.Where(t => t.Id == dispositivoTactil.Id).Single();
                busqueda.terminal = terminal;
                db.SaveChanges();
            }
        }
    }
}