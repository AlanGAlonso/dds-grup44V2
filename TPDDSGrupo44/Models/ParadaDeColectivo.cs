using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;

namespace TPDDSGrupo44.Models
{

    public class ParadaDeColectivo : PuntoDeInteres
    {

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
        public new List<string> palabrasClave { get; set; }
        public new string nombreDePOI { get; set; }
        public new string tipoDePOI { get; set; }



        ////////////////Constructor vacio////////////////
        public ParadaDeColectivo() { }

        ////////////////Constructor generico////////////////
        public ParadaDeColectivo(DbGeography unaCoordenada, string calle, int numeroAltura, int piso, int unidad,
           int codigoPostal, string localidad, string barrio, string provincia, string pais, string entreCalles, List<string> palabrasClave,
           string nombreDePOI,string tipoDePOI)
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
            this.tipoDePOI = tipoDePOI;
        }





        ////////////////Constructor Viejo(Usado en controlador////////////////
        public ParadaDeColectivo(string nombre, DbGeography unaCoordenada)
        : base(nombre, unaCoordenada)
        {
            nombreDePOI = nombre;
            coordenada = unaCoordenada;
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


        public void agregarParada(ParadaDeColectivo parada)
        {
            using (var db = new BuscAR())
            {
                db.Paradas.Add(parada);
                db.SaveChanges();
            }
        }

        public void eliminarParada(int id)
        {
            using (var db = new BuscAR())
            {
                
                ParadaDeColectivo parada = db.Paradas.Where(p => p.id == id).Single();

                db.Paradas.Remove(parada);
                db.SaveChanges();
            }


        }


        public void actualizar(string nombre)
        {
            this.nombreDePOI = nombre;
        }

        
        }
    }
