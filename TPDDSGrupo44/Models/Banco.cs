using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;

namespace TPDDSGrupo44.Models
{
    public class Banco : PuntoDeInteres
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
        public new string nombreDePOI { get; set; }
        public virtual new List<string> palabrasClave { get; set; }
        public new string tipoDePOI { get; set; }
        public virtual new List<HorarioAbierto> horarioAbierto { get; set; }
        public virtual new List<HorarioAbierto> horarioFeriado { get; set; }
        public virtual List<ServicioBanco> servicios { get; set; }

        ////////////////Constructor vacio///////////////
        public Banco()
        {
            servicios = new List<ServicioBanco>();
        }

        //public Banco():base() { }

        ////////////////Constructor JSON (usado para generar bancos a partir del JSON que tiene poca data)////////////////
        public Banco(string nombre, DbGeography unaCoordenada, List<string> serviciosJSON) :base(nombre, unaCoordenada)
        {
            nombreDePOI = nombre;
            coordenada = unaCoordenada;
            servicios = new List<ServicioBanco>();
            horarioAbierto = new List<HorarioAbierto>();
            horarioFeriado = new List<HorarioAbierto>();

            foreach (string servicio in serviciosJSON)
            {
                ServicioBanco serv = new ServicioBanco(servicio);
                servicios.Add(serv);
            }

        }
        ////////////////Constructor generico////////////////
        public Banco(DbGeography unaCoordenada, string calle, int numeroAltura, int piso, int unidad,
           int codigoPostal, string localidad, string barrio, string provincia, string pais, string entreCalles, string palabraClave,
           string tipoDePOI, List<HorarioAbierto> horarioAbierto, List<HorarioAbierto> horarioFeriado, List<ServicioBanco> servicios)

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
            this.nombreDePOI = palabraClave;
            this.tipoDePOI = tipoDePOI;
            this.horarioAbierto = horarioAbierto;
            this.horarioFeriado = horarioFeriado;
            this.servicios = servicios;
        
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

        ////////////////Cálculo de Cercanía genérico - distancia menor a 5 cuadras////////////////
        public override bool estaCerca(DbGeography coordenadaDeDispositivoTactil)
        {
            return (functionManhattan(coordenada, coordenadaDeDispositivoTactil) / 100) < 5;
        }



        //--------------- ABM BANCO --------------------
        public void agregarBanco(Banco banco)
        {
            using (var db = new BuscAR())
            {
                db.Bancos.Add(banco);
                db.SaveChanges();
            }
        }

        public void eliminarBanco(int id)
        {
            using (var db = new BuscAR())
            {

                Banco banco = db.Bancos.Where(p => p.id == id).Single();

                db.Bancos.Remove(banco);
                db.SaveChanges();
            }
        }


    }
}
