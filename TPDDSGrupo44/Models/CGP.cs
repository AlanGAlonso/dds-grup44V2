﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using TPDDSGrupo44.DataModels;

namespace TPDDSGrupo44.Models
{
    public class CGP : PuntoDeInteres
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
        public virtual new List<PalabraClave> palabrasClave { get; set; }
        public new string nombreDePOI { get; set; }
        public int numeroDeComuna { get; set; }
        public virtual new List<HorarioAbierto> horarioAbierto { get; set; }
        public virtual new List<HorarioAbierto> horarioFeriado { get; set; }

        public virtual List<ServicioCGP> servicios { get; set; }
        public int zonaDelimitadaPorLaComuna { get; set; }

        ////////////////Constructor Vacio////////////////
        public CGP() : base()
        {
            servicios = new List<ServicioCGP>();
            palabrasClave = new List<PalabraClave>();
            horarioAbierto = new List<HorarioAbierto>();
            horarioFeriado = new List<HorarioAbierto>();
        }


       ////////////////Constructor JSON (usado para generar cgp a partir del JSON que tiene poca data)////////////////
        //public CGP(int comuna, List<ServicioCGP> serviciosJSON) : base()
        ////public CGP(int comuna, List<ServiciosJSON> serviciosJSON) : base()

        //{
        //    //JsonCGP jsoncgp = new JsonCGP();
        //    nombreDePOI = "Sede Comunal " + comuna;
        //    palabrasClave = new List<PalabraClave>();
        //    palabrasClave.Add(new PalabraClave("CGP"));
        //    palabrasClave.Add(new PalabraClave(nombreDePOI));
        //    servicios = new List<ServicioCGP>();
        //    horarioAbierto = new List<HorarioAbierto>();
        //    //horarioAbierto = jsoncgp.serviciosJson  horaDesde + horaHasta
        //    //horarioFeriado = new List<HorarioAbierto>(); --> no contiene el json
        //    servicios = serviciosJSON;


        //}


      



        ////////////////Constructor generico////////////////
        public CGP(DbGeography unaCoordenada, string calle, int numeroAltura, int piso, int unidad,
           int codigoPostal, string localidad, string barrio, string provincia, string pais, string entreCalles, List<PalabraClave> palabrasClave,
           string nombreDePOI,string tipoDePOI, int numeroDeComuna, List<ServicioCGP> servicios, int zonaDelimitadaPorLaComuna,
           List<HorarioAbierto> horarioAbierto, List<HorarioAbierto> horarioFeriado)
            : base(unaCoordenada, calle, numeroAltura, piso, codigoPostal, localidad, barrio, provincia, pais, entreCalles,
                  palabrasClave, nombreDePOI, horarioAbierto, horarioFeriado, 0)
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
            this.numeroDeComuna = numeroDeComuna;
            this.horarioAbierto = horarioAbierto;
            this.horarioFeriado = horarioFeriado;
            this.servicios = servicios;
            this.zonaDelimitadaPorLaComuna = zonaDelimitadaPorLaComuna;
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

        ////////////////Cálculo de Cercanía////////////////
        public override bool estaCerca(DbGeography coordenadaDeDispositivoTactil)
        {
            if (coordenada == null)
            {
                return false;
            }
            return (functionManhattan(coordenada, coordenadaDeDispositivoTactil) / 100) < zonaDelimitadaPorLaComuna;
        }



        // -------------------- ABM CGP --------------------------
        public static void agregarCGP(CGP cgp)
        {
            using (var db = new BuscAR())
            {
                db.CGPs.Add(cgp);
                db.SaveChanges();
            }
        }

        public static void eliminarCGP(int id)
        {
            using (var db = new BuscAR())
            {

                CGP cgp = db.CGPs.Where(p => p.id == id).Single();
                cgp.palabrasClave.Clear();
                cgp.horarioAbierto.Clear();
                cgp.horarioFeriado.Clear();
                foreach (ServicioCGP s in cgp.servicios)
                {
                    s.horarioAbierto.Clear();
                    s.horarioFeriados.Clear();
                }

                cgp.servicios.Clear();

                db.CGPs.Remove(cgp);
                db.SaveChanges();
            }


        }

        public static void actualizar(int id,DbGeography unaCoordenada,string calle, int numeroAltura, int piso, int unidad,
           int codigoPostal, string localidad, string barrio, string provincia, string pais, string entreCalles, List<PalabraClave> palabrasClave,
           string nombreDePOI, int numeroDeComuna, List<ServicioCGP> servicios, int zonaDelimitadaPorLaComuna,
           List<HorarioAbierto> horarioAbierto, List<HorarioAbierto> horarioFeriado)
        {

            using (var db = new BuscAR())
            {

                CGP cgp = db.CGPs.Where(p => p.id == id).Single();
                cgp.coordenada = unaCoordenada;
                cgp.calle = calle;
                cgp.numeroAltura = numeroAltura;
                cgp.piso = piso;
                cgp.unidad = unidad;
                cgp.codigoPostal = codigoPostal;
                cgp.localidad = localidad;
                cgp.barrio = barrio;
                cgp.provincia = provincia;
                cgp.pais = pais;
                cgp.entreCalles = entreCalles;
                cgp.palabrasClave = palabrasClave;
                cgp.nombreDePOI = nombreDePOI;
                cgp.numeroDeComuna = numeroDeComuna;
                cgp.horarioAbierto = horarioAbierto;
                cgp.horarioFeriado = horarioFeriado;
                cgp.servicios = servicios;
                cgp.zonaDelimitadaPorLaComuna = zonaDelimitadaPorLaComuna;

                db.SaveChanges();
            }
        }




        public static void eliminarPalabrasClaves(int id)
        {
            using (var db = new BuscAR())
            {

                CGP cgp= db.CGPs.Where(p => p.id == id).Single();
                cgp.palabrasClave.Clear();
                db.SaveChanges();
            }
        }

    }
}