using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using TPDDSGrupo44.DataModels;

namespace TPDDSGrupo44.Models
{

    public class ParadaDeColectivo : PuntoDeInteres
    {

        ////////////////Atributos////////////////
        public new int id { get; set; }
        public new DbGeography coordenada { get; set; }
        public new string calle { get; set; }
        public new int numeroAltura { get; set; }
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


        ////////////////Constructor vacio////////////////
        public ParadaDeColectivo() :base () {
            horarioAbierto = new List<HorarioAbierto>
    {
        new HorarioAbierto(System.DayOfWeek.Monday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Tuesday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Wednesday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Thursday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Friday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Saturday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Sunday, 0, 24)
    };
            horarioFeriado = new List<HorarioAbierto>();
        }

        ////////////////Constructor generico////////////////
        public ParadaDeColectivo(DbGeography unaCoordenada, string calle, int numeroAltura, int codigoPostal,
           string localidad, string barrio, string provincia, string pais, string entreCalles, List<PalabraClave> palabrasClave,
           string nombreDePOI)
            : base(nombreDePOI, unaCoordenada)
        {
            this.coordenada = unaCoordenada;
            this.calle = calle;
            this.numeroAltura = numeroAltura;
            this.codigoPostal = codigoPostal;
            this.localidad = localidad;
            this.barrio = barrio;
            this.provincia = provincia;
            this.pais = pais;
            this.entreCalles = entreCalles;
            this.palabrasClave = palabrasClave;
            this.nombreDePOI = nombreDePOI;
            horarioAbierto = new List<HorarioAbierto>
    {
        new HorarioAbierto(System.DayOfWeek.Monday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Tuesday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Wednesday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Thursday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Friday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Saturday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Sunday, 0, 24)
    };
            horarioFeriado = new List<HorarioAbierto>();
        }




        

        ////////////////Funcion manhattan////////////////
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

        ////////////////Cálculo de Cercanía genérico - distancia menor a 1 cuadras////////////////
        public override bool estaCerca(DbGeography coordenadaDeDispositivoTactil)
        {
            return (functionManhattan(coordenada, coordenadaDeDispositivoTactil) / 100) < 1;
        }

        ////////////////Cálculo de Disponibilidad Horaria - Siempre está disponible////////////////
        public override bool estaDisponible(DateTime searchTime)
        {
            return true;
        }



        public static void agregarParada(ParadaDeColectivo parada)
        {
            using (var db = new BuscAR())
            {
                db.Paradas.Add(parada);
                db.SaveChanges();
            }
        }

        public static void eliminarParada(int id)
        {
            using (var db = new BuscAR())
            {

                ParadaDeColectivo parada = db.Paradas.Where(p => p.id == id).Single();
                parada.palabrasClave.Clear();
                parada.horarioAbierto.Clear();
                parada.horarioFeriado.Clear();
                db.Paradas.Remove(parada);
                db.SaveChanges();
            }


        }


        public static void actualizar(DbGeography unaCoordenada,string calle, int numeroAltura, int codigoPostal, string localidad, string barrio, string provincia, string pais,
            string entreCalles, List<PalabraClave> palabrasClave, string nombreDePOI)
        {
            using (var db = new BuscAR())
            {

                ParadaDeColectivo parada = db.Paradas.Where(p => p.nombreDePOI == nombreDePOI).Single();

                parada.coordenada = unaCoordenada;
                parada.calle = calle;
                parada.numeroAltura = numeroAltura;
                parada.codigoPostal = codigoPostal;
                parada.localidad = localidad;
                parada.barrio = barrio;
                parada.provincia = provincia;
                parada.pais = pais;
                parada.entreCalles = entreCalles;
                parada.palabrasClave = palabrasClave;
                parada.nombreDePOI = nombreDePOI;

                db.SaveChanges();
            }
        }

    }
    }
