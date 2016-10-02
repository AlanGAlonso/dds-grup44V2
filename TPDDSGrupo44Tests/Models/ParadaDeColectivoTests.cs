using NUnit.Framework;
using System;
using System.Data.Entity.Spatial;
using System.Collections.Generic;

namespace TPDDSGrupo44.Models.Tests
{
    [TestFixture()]
    public class ParadaDeColectivoTests
    {
        [Test()]
        public void estaDisponibleTest()
        {
            // Agrego parada 114
            Models.ParadaDeColectivo parada = new Models.ParadaDeColectivo("Mozart 2389", DbGeography.FromText("POINT(-34.659690 -58.468764)"));

            parada.palabraClave = "114";

            Assert.IsTrue(parada.estaDisponible(DateTime.Now));
        }


        [Test()]
        public void agregarPoidParadaTest()
        {
            DispositivoTactil dispositivo = new DispositivoTactil();
            List<PuntoDeInteres> lista=new List<PuntoDeInteres> ();
            dispositivo.ListaPuntosDeInteres = lista;
            PuntoDeInteres poid = new ParadaDeColectivo("Mozart 2389", DbGeography.FromText("POINT(-34.659690 -58.468764)"));
            poid.palabraClave = "114";
            dispositivo.agregarPoid(poid);

            Assert.IsNotEmpty(lista);

        }

        [Test()]
        public void eliminarPoidParadaTest()
        {
            DispositivoTactil dispositivo = new DispositivoTactil();
            List<PuntoDeInteres> lista = new List<PuntoDeInteres>();
            dispositivo.ListaPuntosDeInteres = lista;
            PuntoDeInteres poid = new ParadaDeColectivo("Mozart 2389", DbGeography.FromText("POINT(-34.659690 -58.468764)"));
            poid.palabraClave = "114";
            dispositivo.agregarPoid(poid);
            dispositivo.eliminarPoid(poid);

            Assert.IsEmpty(lista);
        }
        

        
    }
}