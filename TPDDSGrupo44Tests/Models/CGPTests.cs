using NUnit.Framework;
using System;
using System.Data.Entity.Spatial;
using System.Collections.Generic;




namespace TPDDSGrupo44.Models.Tests
{
    [TestFixture()]
     public class CGPTests
     {
         [Test()]
         public void estaDisponibleCGPTest()
        {
             CGP CGP = new CGP("Sede Comunal 8", DbGeography.FromText("POINT(-34.6862397 -58.4606666)"), 50);
             ServicioCGP servicio = new ServicioCGP("Rentas");
             CGP.horarioAbierto.Add(new HorarioAbierto(DayOfWeek.Monday, 8, 18));
             CGP.horarioAbierto.Add(new HorarioAbierto(DayOfWeek.Tuesday, 8, 18));
             CGP.horarioAbierto.Add(new HorarioAbierto(DayOfWeek.Wednesday, 8, 18));
             CGP.horarioAbierto.Add(new HorarioAbierto(DayOfWeek.Thursday, 8, 18));
             CGP.horarioAbierto.Add(new HorarioAbierto(DayOfWeek.Friday, 8, 18));
             CGP.horarioAbierto.Add(new HorarioAbierto(DayOfWeek.Saturday, 0, 0));
             CGP.horarioAbierto.Add(new HorarioAbierto(DayOfWeek.Sunday, 0, 0));
             CGP.servicios.Add(servicio);
 
             Assert.IsTrue(CGP.estaDisponible(new DateTime(2016, 06, 15, 12, 00, 00)));
 
             Assert.IsFalse(CGP.estaDisponible(new DateTime(2016, 06, 19, 00, 00, 00)));
         }

        [Test()]
        public void agregarPoidCGPTest()
        {
            DispositivoTactil dispositivo = new DispositivoTactil();
            List<PuntoDeInteres> lista = new List<PuntoDeInteres>();
            dispositivo.ListaPuntosDeInteres = lista;
            // Agrego CGP Lugano
            PuntoDeInteres poid1 = new CGP("Sede Comunal 8", DbGeography.FromText("POINT(-34.6862397 -58.4606666)"), 50);
            // Agrego CGP Floresta
            PuntoDeInteres poid2 = new CGP("Sede Comunal 10", DbGeography.FromText("POINT(-34.6318411 -58.4857468)"), 10);

            dispositivo.agregarPoid(poid1);
            dispositivo.agregarPoid(poid2);

            Assert.IsTrue(lista.Count == 2);

        }

        [Test()]
        public void eliminarPoidCGPTest()
        {
            DispositivoTactil dispositivo = new DispositivoTactil();
            List<PuntoDeInteres> lista = new List<PuntoDeInteres>();
            dispositivo.ListaPuntosDeInteres = lista;

            // Agrego CGP Lugano
            PuntoDeInteres poid1 = new CGP("Sede Comunal 8", DbGeography.FromText("POINT(-34.6862397 -58.4606666)"), 50);
            // Agrego CGP Floresta
            PuntoDeInteres poid2 = new CGP("Sede Comunal 10", DbGeography.FromText("POINT(-34.6318411 -58.4857468)"), 10);

            dispositivo.agregarPoid(poid1);
            dispositivo.agregarPoid(poid2);
            dispositivo.eliminarPoid(poid1);
            dispositivo.eliminarPoid(poid2);

            Assert.IsEmpty(lista);
        }




    }
 } 