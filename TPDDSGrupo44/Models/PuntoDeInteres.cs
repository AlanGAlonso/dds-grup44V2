using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.ComponentModel.DataAnnotations;

namespace TPDDSGrupo44.Models
{
    public class PuntoDeInteres
    {

        ////////////////Atributos////////////////
        [Key]
        public int id { get; set; }
        public DbGeography coordenada { get; set; }
        public string calle { get; set; }
        public int numeroAltura { get; set; }
        public int piso { get; set; }
        public int unidad { get; set; }
        public int codigoPostal { get; set; }
        public string localidad { get; set; }
        public string barrio { get; set; }
        public string provincia { get; set; }
        public string pais { get; set; }
        public string entreCalles { get; set; }
        public virtual List<string> palabrasClave { get; set; }
        public string nombreDePOI { get; set; }
        public string tipoDePOI { get; set; }
        public virtual List<HorarioAbierto> horarioAbierto { get; set; }
        public virtual List<HorarioAbierto> horarioFeriado { get; set; }


        ////////////////Constructor vacio////////////////
        public PuntoDeInteres() {
        }

        ////////////////Constructor generico////////////////
        public PuntoDeInteres(DbGeography unaCoordenada, string calle, int numeroAltura, int piso, int unidad,
           int codigoPostal, string localidad, string barrio, string provincia, string pais, string entreCalles, List<string> palabrasClave,
           string nombreDePOI,string tipoDePOI, List<HorarioAbierto> horarioAbierto, List<HorarioAbierto> horarioFeriados)
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
            this.tipoDePOI = tipoDePOI;
            this.horarioAbierto = horarioAbierto;
            this.horarioFeriado = horarioFeriados;
        }
        
        ////////////////Constructor Viejo(Usado en controlador////////////////
        public PuntoDeInteres(string nombre, DbGeography unaCordenada)
        {
            this.nombreDePOI = nombre;
            this.coordenada = unaCordenada;
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
        public virtual bool estaCerca(DbGeography coordenadaDeDispositivoTactil)
        {
            return (functionManhattan(coordenada, coordenadaDeDispositivoTactil) / 100) < 5;
        }
        

        ////////////////Cálculo de Disponibilidad Horaria genérico////////////////
        public virtual bool estaDisponible(DateTime searchTime)
        {
            HorarioAbierto todaysHours = new HorarioAbierto();
            //busco entre los feriados a ver si hoy es feriado
            if (horarioFeriado != null)
            {
                todaysHours = horarioFeriado.Find(x => x.numeroDeDia == searchTime.Day && x.numeroDeMes == searchTime.Month);
            }

            //si es feriado, verificar si está abierto (pueden hacer horarios diferenciales)
            if (todaysHours != null)
            {
                return todaysHours.horarioValido(searchTime);
            } else
            {
                // si no era feriado, busco si está abierto por día de la semana
                todaysHours = horarioAbierto.Find(x => x.dia == searchTime.DayOfWeek );
                return todaysHours.horarioValido(searchTime);
            }
        }




    }
}