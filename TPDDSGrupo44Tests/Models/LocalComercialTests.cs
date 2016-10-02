//using NUnit.Framework;
//using System;
//using System.Data.Entity.Spatial;
//using System.Collections.Generic;


//namespace TPDDSGrupo44.Models.Tests
//{
//    [TestFixture()]
//    public class LocalComercialTests
//    {
//        [Test()]
//        public void estaDisponibleLocalTest()
//        {
//            // Agrego librería ceit
//            LocalComercial local = new LocalComercial("Librería CEIT", DbGeography.FromText("POINT(-34.659492 -58.467906)"), new Rubro("librería escolar", 5));

//            local.horarioAbierto.Add(new HorarioAbierto(DayOfWeek.Monday, 8, 21));

//            local.horarioAbierto.Add(new HorarioAbierto(DayOfWeek.Tuesday, 8, 21));

//            local.horarioAbierto.Add(new HorarioAbierto(DayOfWeek.Wednesday, 8, 21));

//            local.horarioAbierto.Add(new HorarioAbierto(DayOfWeek.Thursday, 8, 21));

//            local.horarioAbierto.Add(new HorarioAbierto(DayOfWeek.Friday, 8, 21));

//            local.horarioAbierto.Add(new HorarioAbierto(DayOfWeek.Saturday, 8, 21));

//            local.horarioAbierto.Add(new HorarioAbierto(DayOfWeek.Sunday, 0, 0));

//            local.horarioFeriado.Add(new HorarioAbierto(1, 1, 0, 0));

//            local.horarioFeriado.Add(new HorarioAbierto(9, 7, 10, 16));

//            Assert.IsTrue(local.estaDisponible(new DateTime(2016, 4, 18, 17, 00, 00)));

//            Assert.IsFalse(local.estaDisponible(new DateTime(2016, 6, 19, 00, 00, 00)));
//        }

//        [Test()]
//        public void agregarPoidLocalesTest()
//        {
//            DispositivoTactil dispositivo = new DispositivoTactil();
//            List<PuntoDeInteres> lista = new List<PuntoDeInteres>();
//            dispositivo.ListaPuntosDeInteres = lista;
//            // Agrego librería ceit
//            PuntoDeInteres poid1 = new LocalComercial("Librería CEIT", DbGeography.FromText("POINT(-34.659492 -58.467906)"), new Rubro("librería escolar", 5));
//            // agrego puesto de diarios 
//            PuntoDeInteres poid2 = new LocalComercial("Kiosco Las Flores", DbGeography.FromText("POINT(-34.634015 -58.482805)"), new Rubro("kiosco de diarios", 5));

//            dispositivo.agregarPoid(poid1);


//            Assert.IsNotEmpty(lista);

//        }

//        [Test()]
//        public void eliminarPoidLocalesTest()
//        {
//            DispositivoTactil dispositivo = new DispositivoTactil();
//            List<PuntoDeInteres> lista = new List<PuntoDeInteres>();
//            dispositivo.ListaPuntosDeInteres = lista;
//            // Agrego librería ceit
//            PuntoDeInteres poid1 = new LocalComercial("Librería CEIT", DbGeography.FromText("POINT(-34.659492 -58.467906)"), new Rubro("librería escolar", 5));
//            // agrego puesto de diarios 
//            PuntoDeInteres poid2 = new LocalComercial("Kiosco Las Flores", DbGeography.FromText("POINT(-34.634015 -58.482805)"), new Rubro("kiosco de diarios", 5));

//            dispositivo.agregarPoid(poid1);
//            dispositivo.agregarPoid(poid2);
//            dispositivo.eliminarPoid(poid1);
 

//            Assert.IsTrue(lista.Count == 1);
//        }


//    }
//}