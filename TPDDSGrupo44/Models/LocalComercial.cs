
using System.Data.Entity.Spatial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using TPDDSGrupo44.DataModels;

namespace TPDDSGrupo44.Models
{
    public class LocalComercial : PuntoDeInteres
    {
        [Key]
        ////////////////Atributos////////////////
        public new int id { get; set; }
        public new DbGeography coordenada { get; set; }
        public new string calle { get; set; }
        public new int numeroAltura { get; set; }
        public new int piso { get; set; }
        public new int unidad { get; set; }
        public new int codigoPostal { get; set; }
        public new string localidad { get; set; }
        public new string barrio { get; set; }
        public new string provincia { get; set; }
        public new string pais { get; set; }
        public new string entreCalles { get; set; }
        public new string nombreDePOI { get; set; }
        public virtual new List<PalabraClave> palabrasClave { get; set; }
        public virtual new List<HorarioAbierto> horarioAbierto { get; set; }
        public virtual new List<HorarioAbierto> horarioFeriado { get; set; }


        public virtual Rubro rubro { get; set; }




        ////////////////Constructor vacio////////////////
        public LocalComercial() : base() { }


        ////////////////Constructor Viejo(Usado en controlador////////////////
        public LocalComercial(string nombre, DbGeography unaCoordenada, Rubro rubro)
        : base(nombre, unaCoordenada)
        {
            this.rubro = rubro;
            coordenada = unaCoordenada;
            nombreDePOI = nombre;
        }

        public LocalComercial(DbGeography unaCoordenada, string calle, int numeroAltura, int piso, int unidad,
           int codigoPostal, string localidad, string barrio, string provincia, string pais, string entreCalles,
           List<PalabraClave> palabrasClave, string nombreDePOI, List<HorarioAbierto> horarioAbierto,
           List<HorarioAbierto> horarioFeriados,
           Rubro rubro)
            : base(unaCoordenada, calle, numeroAltura, piso, codigoPostal, localidad, barrio, provincia, pais, entreCalles,
                  palabrasClave, nombreDePOI, horarioAbierto, horarioFeriados, 0)
        {
            this.coordenada = unaCoordenada;
            this.calle = calle;
            this.numeroAltura = numeroAltura;
            this.piso = piso;
            this.unidad = unidad;
            this.codigoPostal = codigoPostal;
            this.localidad = localidad;
            this.barrio = barrio;
            this.provincia = provincia;
            this.pais = pais;
            this.entreCalles = entreCalles;
            this.palabrasClave = palabrasClave;
            this.nombreDePOI = nombreDePOI;
            this.horarioAbierto = horarioAbierto;
            this.horarioFeriado = horarioFeriados;
            this.rubro = rubro;

        }


        //E4 - JM - Constructor para actualización asincrónica
        public LocalComercial(string nombre, List<PalabraClave> palabras) : base()
        {
            nombreDePOI = nombre;
            palabrasClave = palabras;
        }



        ////////////////Funcion manhattan////////////
        private static double functionManhattan(DbGeography coordenadaDeDispositivoTactil, DbGeography coordenada)
        {
            double lat1InDegrees = (double)coordenadaDeDispositivoTactil.Latitude;
            double long1InDegrees = (double)coordenadaDeDispositivoTactil.Longitude;

            double lat2InDegrees = (double)coordenada.Latitude;
            double long2InDegrees = (double)coordenada.Longitude;

            double lats = (double)Math.Abs(lat1InDegrees - lat2InDegrees);
            double lngs = (double)Math.Abs(long1InDegrees - long2InDegrees);

            //grados a metros
            double latm = lats * 60 * 1852;
            double lngm = (lngs * Math.Cos((double)lat1InDegrees * Math.PI / 180)) * 60 * 1852;
            double distInMeters = Math.Sqrt(Math.Pow(latm, 2) + Math.Pow(lngm, 2));
            return distInMeters;

        }

        ////////////////Cálculo de Cercanía - Depende del radio de cercanía del rubro////////////////
        public override bool estaCerca(DbGeography coordenadaDeDispositivoTactil)
        {
            return (functionManhattan(coordenada, coordenadaDeDispositivoTactil) / 100) < rubro.radioDeCercania; //Cuadras
        }


        //////////////// ABM Local Comercial ////////////////
        public static void agregarLocComercial(LocalComercial localComercial)
        {
            using (var db = new BuscAR())
            {
                db.Locales.Add(localComercial);
                db.SaveChanges();
            }
        }

        public static void eliminarLocComercial(int id)
        {
            using (var db = new BuscAR())
            {
                LocalComercial local = db.Locales.Where(p => p.id == id).Single();
                local.palabrasClave.Clear();
                local.horarioAbierto.Clear();
                local.horarioFeriado.Clear();
                local.rubro = null;

                db.Locales.Remove(local);
                db.SaveChanges();
            }


        }

        public static void actualizar(int id, DbGeography unaCoordenada, string calle, int numeroAltura, int piso, int unidad,
           int codigoPostal, string localidad, string barrio, string provincia, string pais, string entreCalles,
           string nombreDePOI, List<PalabraClave> palabrasClave, List<HorarioAbierto> horarioAbierto,
           List<HorarioAbierto> horarioFeriados)
        {
            using (var db = new BuscAR())
            {

                LocalComercial local = db.Locales.Where(p => p.id == id).Single();
                local.coordenada = unaCoordenada;
                local.calle = calle;
                local.numeroAltura = numeroAltura;
                local.piso = piso;
                local.unidad = unidad;
                local.codigoPostal = codigoPostal;
                local.localidad = localidad;
                local.barrio = barrio;
                local.provincia = provincia;
                local.pais = pais;
                local.entreCalles = entreCalles;
                local.palabrasClave = palabrasClave;
                local.nombreDePOI = nombreDePOI;
                local.horarioAbierto = horarioAbierto;
                local.horarioFeriado = horarioFeriados;
             

                db.SaveChanges();
            }
        }

        public static void eliminarPalabrasClaves(int id)
        {
            using (var db = new BuscAR())
            {

                LocalComercial local = db.Locales.Where(p => p.id == id).Single();
                local.palabrasClave.Clear();
                db.SaveChanges();
            }
        }


        public static List<LocalComercial> buscarTodosLosLocales()
        {
            List<LocalComercial> localesComerciales;
            using (var db = new BuscAR())
            {
                localesComerciales = (from p in db.Locales
                                      orderby p.nombreDePOI
                                      select p).ToList();
            }
            return localesComerciales;
        }

        public static LocalComercial busquedaParaEdit(int id)
        {
            LocalComercial localComercial;
            using (var db = new BuscAR())
            {
                localComercial = db.Locales.Include("palabrasClave").Include("rubro").Include("horarioAbierto").Where(p => p.id == id).Single();
            }
            return localComercial;
        }



        public static void modificarRubro(int id,string nombre,string radioDeCercania)
        {
            LocalComercial localComercial = busquedaParaEdit(id);
            using (var db = new BuscAR())
            {
                Rubro rub=db.Rubros.Where(p => p.Id == localComercial.rubro.Id).Single();

                rub.nombre = nombre;
                rub.radioDeCercania = Convert.ToInt32(radioDeCercania);
                db.SaveChanges();
            }
 

        }

        public static void eliminarHorarios(int id)
        {
            using (var db = new BuscAR())
            {
                LocalComercial local = db.Locales.Where(p => p.id == id).Single();
                local.horarioAbierto.Clear();
                local.horarioFeriado.Clear();
                db.SaveChanges();
            }
        }



    }


}
