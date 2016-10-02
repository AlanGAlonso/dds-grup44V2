namespace TPDDSGrupo44.Migrations
{
    using Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Spatial;

    internal sealed class Configuration : DbMigrationsConfiguration<TPDDSGrupo44.Models.BuscAR>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "TPDDSGrupo44.Models.BuscAR";
        }

        protected override void Seed(TPDDSGrupo44.Models.BuscAR context)
        {
            //  This method will be called after migrating to the latest version.

            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% Agrego terminales
            context.Terminales.AddOrUpdate(d => d.nombre,
            new DispositivoTactil
            {
                nombre = "UTN FRBA Lugano",
                coordenada = DbGeography.FromText("POINT(-34.6597047 -58.4688947)")
            },
new DispositivoTactil
{
    nombre = "Teatro Gran Rivadavia",
    coordenada = DbGeography.FromText("POINT(-34.6349293 -58.4853798)")
});

            context.SaveChanges();


            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% Agrego listas de POIs

            //Paradas
            context.Paradas.AddOrUpdate(
            p => p.nombreDePOI,
            new ParadaDeColectivo
            {
                nombreDePOI = "114",
                calle = "Mozart",
                numeroAltura = 2389,
                localidad = "Ciudad Autónoma de Buenos Aires",
                barrio = "Lugano",
                provincia = "Ciudad Autónoma de Buenos Aires",
                pais = "Argentina",
                entreCalles = "Saraza y Dellepiane Sur",
                coordenada = DbGeography.FromText("POINT(-34.659690 -58.468764)")
            },
new ParadaDeColectivo
{
    nombreDePOI = "36",
    calle = "Av Escalada",
    numeroAltura = 2680,
    localidad = "Ciudad Autónoma de Buenos Aires",
    barrio = "Lugano",
    provincia = "Ciudad Autónoma de Buenos Aires",
    pais = "Argentina",
    entreCalles = "Av Derqui y Dellepiane Norte",
    coordenada = DbGeography.FromText("POINT(-34.662325 -58.473300)")
});

            context.SaveChanges();


            //Horarios de locales
            List<HorarioAbierto> horarios = new List<HorarioAbierto>();
            horarios.Add(new HorarioAbierto(System.DayOfWeek.Monday, 8, 21));
            horarios.Add(new HorarioAbierto(System.DayOfWeek.Tuesday, 8, 21));
            horarios.Add(new HorarioAbierto(System.DayOfWeek.Wednesday, 8, 21));
            horarios.Add(new HorarioAbierto(System.DayOfWeek.Thursday, 8, 21));
            horarios.Add(new HorarioAbierto(System.DayOfWeek.Friday, 8, 21));
            horarios.Add(new HorarioAbierto(System.DayOfWeek.Saturday, 8, 21));
            horarios.Add(new HorarioAbierto(System.DayOfWeek.Sunday, 0, 0));

            List<HorarioAbierto> feriados = new List<HorarioAbierto>();
            feriados.Add(new HorarioAbierto(1, 1, 0, 0));
            feriados.Add(new HorarioAbierto(9, 7, 10, 16));

            //Locales
            context.Locales.AddOrUpdate(
            l => l.nombreDePOI,
            new LocalComercial
            {
                nombreDePOI = "Librería CEIT",
                coordenada = DbGeography.FromText("POINT(-34.659492 -58.467906)"),
                rubro = new Rubro("librería escolar", 5),
                horarioAbierto = horarios,
                //horarioFeriados = feriados
            },
new LocalComercial
{
    nombreDePOI = "Kiosco Las Flores",
    coordenada = DbGeography.FromText("POINT(-34.634015 -58.482805)"),
    rubro = new Rubro("kiosco de diarios", 5)
});

            context.SaveChanges();


            // servicios CGP Lugano
            ServicioCGP servicio1 = new ServicioCGP("Rentas");
            servicio1.horarioAbierto.Add(new HorarioAbierto(System.DayOfWeek.Monday, 8, 18));
            servicio1.horarioAbierto.Add(new HorarioAbierto(System.DayOfWeek.Tuesday, 8, 18));
            servicio1.horarioAbierto.Add(new HorarioAbierto(System.DayOfWeek.Wednesday, 8, 18));
            servicio1.horarioAbierto.Add(new HorarioAbierto(System.DayOfWeek.Thursday, 8, 18));
            servicio1.horarioAbierto.Add(new HorarioAbierto(System.DayOfWeek.Friday, 8, 18));
            servicio1.horarioAbierto.Add(new HorarioAbierto(System.DayOfWeek.Saturday, 0, 0));
            servicio1.horarioAbierto.Add(new HorarioAbierto(System.DayOfWeek.Sunday, 0, 0));
            List<ServicioCGP> servicios1 = new List<ServicioCGP>();
            servicios1.Add(servicio1);

            //servicios CGP Floresta
            ServicioCGP servicio2 = new ServicioCGP("Registro Civil");
            servicio2.horarioAbierto.Add(new HorarioAbierto(System.DayOfWeek.Monday, 8, 18));
            servicio2.horarioAbierto.Add(new HorarioAbierto(System.DayOfWeek.Tuesday, 8, 18));
            servicio2.horarioAbierto.Add(new HorarioAbierto(System.DayOfWeek.Wednesday, 8, 18));
            servicio2.horarioAbierto.Add(new HorarioAbierto(System.DayOfWeek.Thursday, 8, 18));
            servicio2.horarioAbierto.Add(new HorarioAbierto(System.DayOfWeek.Friday, 8, 18));
            servicio2.horarioAbierto.Add(new HorarioAbierto(System.DayOfWeek.Saturday, 10, 16));
            servicio2.horarioAbierto.Add(new HorarioAbierto(System.DayOfWeek.Sunday, 0, 0));
            List<ServicioCGP> servicios2 = new List<ServicioCGP>();
            servicios2.Add(servicio2);

            //CGPs
            context.CGPs.AddOrUpdate(
            c => c.nombreDePOI,
            new CGP
            {
                nombreDePOI = "Sede Comunal 8",
                calle = "Av Coronel Roca",
                numeroAltura = 5252,
                codigoPostal = 1439,
                localidad = "Ciudad Autónoma de Buenos Aires",
                barrio = "Lugano",
                provincia = "Ciudad Autónoma de Buenos Aires",
                pais = "Argentina",
                entreCalles = "Av Escalda y Av General Paz",
                coordenada = DbGeography.FromText("POINT(-34.6862397 -58.4606666)"),
                zonaDelimitadaPorLaComuna = 50,
                servicios = new List<ServicioCGP>()
                {
                     new ServicioCGP()
                    {
                         horarioAbierto = new List<HorarioAbierto>()
                                         {
                             new HorarioAbierto()
                            {
                                 dia = System.DayOfWeek.Monday,
                                horarioInicio = 8,
                                horarioFin = 18
                                             },
                             new HorarioAbierto()
                            {
                                 dia = System.DayOfWeek.Tuesday,
                                horarioInicio = 8,
                                horarioFin = 18
                                             },
                             new HorarioAbierto()
                            {
                                 dia = System.DayOfWeek.Wednesday,
                                horarioInicio = 8,
                                horarioFin = 18
                                             },
                             new HorarioAbierto()
                            {
                                 dia = System.DayOfWeek.Thursday,
                                horarioInicio = 8,
                                horarioFin = 18
                                             },
                             new HorarioAbierto()
                            {
                                 dia = System.DayOfWeek.Friday,
                                horarioInicio = 8,
                                horarioFin = 18
                                             },
                             new HorarioAbierto()
                            {
                                 dia = System.DayOfWeek.Saturday,
                                horarioInicio = 0,
                                horarioFin = 0
                                             },
                             new HorarioAbierto()
                            {
                                 dia = System.DayOfWeek.Sunday,
                                horarioInicio = 0,
                                horarioFin = 0
                                             }
                                         }
                                              }

                                    },
                horarioAbierto = new List<HorarioAbierto>()
                    {
                     new HorarioAbierto()
                    {
                         dia = System.DayOfWeek.Monday,
                        horarioInicio = 8,
                        horarioFin = 18
                                             },
 new HorarioAbierto()
{
     dia = System.DayOfWeek.Tuesday,
    horarioInicio = 8,
    horarioFin = 18
                         },
 new HorarioAbierto()
{
     dia = System.DayOfWeek.Wednesday,
    horarioInicio = 8,
    horarioFin = 18
                         },
 new HorarioAbierto()
{
     dia = System.DayOfWeek.Thursday,
    horarioInicio = 8,
    horarioFin = 18
                         },
 new HorarioAbierto()
{
     dia = System.DayOfWeek.Friday,
    horarioInicio = 8,
    horarioFin = 18
                         },
 new HorarioAbierto()
{
     dia = System.DayOfWeek.Saturday,
    horarioInicio = 0,
    horarioFin = 0
                         },
 new HorarioAbierto()
{
     dia = System.DayOfWeek.Sunday,
    horarioInicio = 0,
    horarioFin = 0
                         }
                    }
            },
new CGP
{
    nombreDePOI = "Sede Comunal 10",
    calle = "Bacacay",
    numeroAltura = 3968,
    codigoPostal = 1407,
    localidad = "Ciudad Autónoma de Buenos Aires",
    barrio = "Vélez Sarsfield",
    provincia = "Ciudad Autónoma de Buenos Aires",
    pais = "Argentina",
    entreCalles = "Mercedes y Av Chivilcoy",
    coordenada = DbGeography.FromText("POINT(-34.6318411 -58.4857468)"),
    zonaDelimitadaPorLaComuna = 10,
    servicios = new List<ServicioCGP>()
                {
                     new ServicioCGP()
                    {
                         horarioAbierto = new List<HorarioAbierto>()
                                         {
                             new HorarioAbierto()
                            {
                                 dia = System.DayOfWeek.Monday,
                                horarioInicio = 8,
                                horarioFin = 18
                                             },
                             new HorarioAbierto()
                            {
                                 dia = System.DayOfWeek.Tuesday,
                                horarioInicio = 8,
                                horarioFin = 18
                                             },
                             new HorarioAbierto()
                            {
                                 dia = System.DayOfWeek.Wednesday,
                                horarioInicio = 8,
                                horarioFin = 18
                                             },
                             new HorarioAbierto()
                            {
                                 dia = System.DayOfWeek.Thursday,
                                horarioInicio = 8,
                                horarioFin = 18
                                             },
                             new HorarioAbierto()
                            {
                                 dia = System.DayOfWeek.Friday,
                                horarioInicio = 8,
                                horarioFin = 18
                                             },
                             new HorarioAbierto()
                            {
                                 dia = System.DayOfWeek.Saturday,
                                horarioInicio = 10,
                                horarioFin = 16
                                             },
                             new HorarioAbierto()
                            {
                                 dia = System.DayOfWeek.Sunday,
                                horarioInicio = 0,
                                horarioFin = 0
                                             }
                                         }
                                              }

                                    }
});
            context.SaveChanges();


            // Horarios Banco Provincia
            ServicioBanco servicio3 = new ServicioBanco("Depósitos");
            servicio3.horarioAbierto.Add(new HorarioAbierto(System.DayOfWeek.Monday, 8, 18));
            servicio3.horarioAbierto.Add(new HorarioAbierto(System.DayOfWeek.Tuesday, 8, 18));
            servicio3.horarioAbierto.Add(new HorarioAbierto(System.DayOfWeek.Wednesday, 8, 18));
            servicio3.horarioAbierto.Add(new HorarioAbierto(System.DayOfWeek.Thursday, 8, 18));
            servicio3.horarioAbierto.Add(new HorarioAbierto(System.DayOfWeek.Friday, 8, 18));
            servicio3.horarioAbierto.Add(new HorarioAbierto(System.DayOfWeek.Saturday, 0, 0));
            servicio3.horarioAbierto.Add(new HorarioAbierto(System.DayOfWeek.Sunday, 0, 0));
            List<ServicioBanco> servicios3 = new List<ServicioBanco>();
            servicios3.Add(servicio3);

            horarios = new List<HorarioAbierto>();
            horarios.Add(new HorarioAbierto(System.DayOfWeek.Monday, 10, 15));
            horarios.Add(new HorarioAbierto(System.DayOfWeek.Tuesday, 10, 15));
            horarios.Add(new HorarioAbierto(System.DayOfWeek.Wednesday, 10, 15));
            horarios.Add(new HorarioAbierto(System.DayOfWeek.Thursday, 10, 15));
            horarios.Add(new HorarioAbierto(System.DayOfWeek.Friday, 10, 15));
            horarios.Add(new HorarioAbierto(System.DayOfWeek.Saturday, 0, 0));
            horarios.Add(new HorarioAbierto(System.DayOfWeek.Sunday, 0, 0));

            // Horarios Banco Francés

            //Bancos
            context.Bancos.AddOrUpdate(
            b => b.nombreDePOI,
            new Banco
            {
                nombreDePOI = "Banco Provincia",
                coordenada = DbGeography.FromText("POINT( 34.660979  58.469821)"),
                servicios = servicios3,
                horarioAbierto = horarios
            },
            new Banco
            {
                nombreDePOI = "Banco Francés",
                coordenada = DbGeography.FromText("POINT( 34.6579153  58.4791142)"),
                servicios = new List<ServicioBanco>()
                {
                     new ServicioBanco()
                    {
                         horarioAbierto = new List<HorarioAbierto>()
                                         {
                             new HorarioAbierto()
                            {
                                 dia = System.DayOfWeek.Monday,
                                horarioInicio = 8,
                                horarioFin = 18
                                             },
                             new HorarioAbierto()
                            {
                                 dia = System.DayOfWeek.Tuesday,
                                horarioInicio = 8,
                                horarioFin = 18
                                             },
                             new HorarioAbierto()
                            {
                                 dia = System.DayOfWeek.Wednesday,
                                horarioInicio = 8,
                                horarioFin = 18
                                             },
                             new HorarioAbierto()
                            {
                                 dia = System.DayOfWeek.Thursday,
                                horarioInicio = 8,
                                horarioFin = 18
                                             },
                             new HorarioAbierto()
                            {
                                 dia = System.DayOfWeek.Friday,
                                horarioInicio = 8,
                                horarioFin = 18
                                             },
                             new HorarioAbierto()
                            {
                                 dia = System.DayOfWeek.Saturday,
                                horarioInicio = 10,
                                horarioFin = 16
                                             },
                             new HorarioAbierto()
                            {
                                 dia = System.DayOfWeek.Sunday,
                                horarioInicio = 0,
                                horarioFin = 0
                                             }
                                         }
                                              }

                                    },
                horarioAbierto = new List<HorarioAbierto>()
                    {
                     new HorarioAbierto()
                    {
                         dia = System.DayOfWeek.Monday,
                        horarioInicio = 8,
                        horarioFin = 18
                                             },
 new HorarioAbierto()
{
     dia = System.DayOfWeek.Tuesday,
    horarioInicio = 8,
    horarioFin = 18
                         },
 new HorarioAbierto()
{
     dia = System.DayOfWeek.Wednesday,
    horarioInicio = 8,
    horarioFin = 18
                         },
 new HorarioAbierto()
{
     dia = System.DayOfWeek.Thursday,
    horarioInicio = 8,
    horarioFin = 18
                         },
 new HorarioAbierto()
{
     dia = System.DayOfWeek.Friday,
    horarioInicio = 8,
    horarioFin = 18
                         },
 new HorarioAbierto()
{
     dia = System.DayOfWeek.Saturday,
    horarioInicio = 0,
    horarioFin = 0
                         },
 new HorarioAbierto()
{
     dia = System.DayOfWeek.Sunday,
    horarioInicio = 0,
    horarioFin = 0
                         }
                    }
            });

            context.SaveChanges();


        }
    }
}