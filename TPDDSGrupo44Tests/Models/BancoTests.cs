using NUnit.Framework;
using TPDDSGrupo44.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace TPDDSGrupo44.Models.Tests
{
    [TestFixture()]
    public class BancoTests
    {
        [Test()]
        public void estaDisponibleBancoTest()
        {
            Banco banco = new Banco("Banco Provincia", new GeoCoordinate(-34.6571851, -58.4776738));
            
            banco.horarioAbierto.Add(new HorarioAbierto(DayOfWeek.Monday, 10, 15));

            banco.horarioAbierto.Add(new HorarioAbierto(DayOfWeek.Tuesday, 10, 15));

            banco.horarioAbierto.Add(new HorarioAbierto(DayOfWeek.Wednesday, 10, 15));

            banco.horarioAbierto.Add(new HorarioAbierto(DayOfWeek.Thursday, 10, 15));

            banco.horarioAbierto.Add(new HorarioAbierto(DayOfWeek.Friday, 10, 15));

            banco.horarioAbierto.Add(new HorarioAbierto(DayOfWeek.Saturday, 0, 0));

            banco.horarioAbierto.Add(new HorarioAbierto(DayOfWeek.Sunday, 0, 0));


            Assert.IsTrue(banco.estaDisponible(new DateTime(2016, 06, 15, 12, 00, 00)));

            Assert.IsFalse(banco.estaDisponible(new DateTime(2016, 06, 19, 00, 00, 00)));

        }
    }
}