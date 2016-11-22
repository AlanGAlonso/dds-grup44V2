namespace TPDDSGrupo44.Migrations
{
    using DataModels;
    using Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<TPDDSGrupo44.DataModels.BuscAR>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "TPDDSGrupo44.DataModels.BuscAR";
        }

        protected override void Seed(TPDDSGrupo44.DataModels.BuscAR context)
        {
            // DATOS INSTANCIADOS POR DEFECTO
            context.Configuraciones.AddOrUpdate(c => c.duracionMaximaBusquedas,
            new Configuracion
            {
                duracionMaximaBusquedas = 30,
                generarLog = true
            });



            // FUNCIONALIDADES DE TERMINALES
            /*
              context.FuncionalidadesTerminales.AddOrUpdate(f => f.nombre,
                           new FuncionalidadDispositivoTactil
                           {
                               nombre = "Parada"
                           },
                           new FuncionalidadDispositivoTactil
                           {
                               nombre = "Banco"
                           },
                           new FuncionalidadDispositivoTactil {
                               nombre = "CGP"
                           },
                           new FuncionalidadDispositivoTactil
                           {
                               nombre = "Locales"
                           }
                           ,
                           new FuncionalidadDispositivoTactil
                           {
                               nombre = "Loggear Búsquedas"
                           }
                       );
                   context.SaveChanges();


            context.FuncionalidadesUsuarios.AddOrUpdate(fu => fu.nombre,
                new ActualizacionLocalComercial
                    {
                        nombre = "Actualizar Local Comercial Asinc",
                        lote = 1
                    },
                    new AgregarAcciones
                    {
                        nombre = "Agregar Acciones Asinc",
                        lote = 1
                    },
                    new ProcesoMultiple
                    {
                        nombre = "Proceso Múltiple Asinc",
                        lote = 1
                    },
                    new BajaPOI
                    {
                        nombre = "Baja POIs Asinc",
                        lote = 1
                    },
                    new FuncionalidadUsuario
                    {
                        nombre = "Alta POI",
                        lote = 1
                    },
                    new FuncionalidadUsuario
                    {
                        nombre = "Baja POI",
                        lote = 1
                    },
                    new FuncionalidadUsuario
                    {
                        nombre = "Editar POI",
                        lote = 1
                    },
                    new FuncionalidadUsuario
                    {
                        nombre = "Consultar POI",
                        lote = 1
                    },
                    new FuncionalidadUsuario
                    {
                        nombre = "Reportes",
                        lote = 1
                    },
                    new FuncionalidadUsuario
                    {
                        nombre = "Configuracion",
                        lote = 1
                    },
                    new FuncionalidadUsuario
                    {
                        nombre = "Terminales",
                        lote = 1
                    },new FuncionalidadUsuario
                    {
                        nombre = "Trámite",
                        lote = 1
                    }
                );
                context.SaveChanges();
            */

            //ROLES

            context.Roles.AddOrUpdate(r => r.nombre,
        new Rol
        {
            nombre = "Administrador",
            funcionalidades = new List<FuncionalidadUsuario>()
            {
                    new ActualizacionLocalComercial
                    {
                        nombre = "Actualizar Local Comercial Asinc",
                        lote = 1
                    },
                    new AgregarAcciones
                    {
                        nombre = "Agregar Acciones Asinc",
                        lote = 1
                    },
                    new ProcesoMultiple
                    {
                        nombre = "Proceso Múltiple Asinc",
                        lote = 1
                    },
                    new BajaPOI
                    {
                        nombre = "Baja POIs Asinc",
                        lote = 1
                    },
                    new FuncionalidadUsuario
                    {
                        nombre = "Alta POI",
                        lote = 1
                    },
                    new FuncionalidadUsuario
                    {
                        nombre = "Baja POI",
                        lote = 1
                    },
                    new FuncionalidadUsuario
                    {
                        nombre = "Editar POI",
                        lote = 1
                    },
                    new FuncionalidadUsuario
                    {
                        nombre = "Consultar POI",
                        lote = 1
                    },
                    new FuncionalidadUsuario
                    {
                        nombre = "Reportes",
                        lote = 1
                    },
                    new FuncionalidadUsuario
                    {
                        nombre = "Configuracion",
                        lote = 1
                    },
                    new FuncionalidadUsuario
                    {
                        nombre = "Terminales",
                        lote = 1
                    }
            }
        },
        new Rol
        {
            nombre = "Terminal",
            funcionalidades = new List<FuncionalidadUsuario>()
            {
                    new FuncionalidadUsuario
                    {
                        nombre = "Trámite",
                        lote = 1
                    }
            }
        });

            context.SaveChanges();


            //USUARIOS
            var provider = new SHA256CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            byte[] passAdmin = provider.ComputeHash(encoding.GetBytes("1234admin"));
            byte[] passTrans = provider.ComputeHash(encoding.GetBytes("1234term"));
            Rol admin = context.Roles.Where(r => r.nombre == "Administrador").Single();
            Rol trans = context.Roles.Where(r => r.nombre == "Terminal").Single();

            context.Usuarios.AddOrUpdate(r => r.nombre,
            new Usuario
            {
                nombre = "admin",
                dni = 37025888,
                contrasenia = passAdmin,
                rol = admin,
                email = "dds44utnviernes@gmail.com"
            },
            new Usuario
            {
                nombre = "terminal",
                dni = 12605907,
                contrasenia = passTrans,
                rol = trans,
                email = "jmlanghe@gmail.com"
            });

            context.SaveChanges();


            

            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% Agrego terminales
            context.Terminales.AddOrUpdate(d => d.nombre,
            new DispositivoTactil
            {
                nombre = "UTN FRBA Lugano",
                coordenada = DbGeography.FromText("POINT(-58.4688947 -34.6597047)"),
                funcionalidades = new List<FuncionalidadDispositivoTactil>
                {
                    new FuncionalidadDispositivoTactil("Parada"),
                    new FuncionalidadDispositivoTactil("Banco"),
                    new FuncionalidadDispositivoTactil("CGP"),
                    new FuncionalidadDispositivoTactil("Locales"),
                           new FuncionalidadDispositivoTactil("Loggear Búsquedas")
                }
            },
new DispositivoTactil
{
    nombre = "Teatro Gran Rivadavia",
    coordenada = DbGeography.FromText("POINT(-58.4853798 -34.6349293)"),
    funcionalidades = new List<FuncionalidadDispositivoTactil>
                {
                    new FuncionalidadDispositivoTactil("Parada"),
                    new FuncionalidadDispositivoTactil("Banco"),
                    new FuncionalidadDispositivoTactil("CGP"),
                    new FuncionalidadDispositivoTactil("Locales")
                }
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
                codigoPostal = 1428,
                localidad = "Ciudad Autónoma de Buenos Aires",
                barrio = "Lugano",
                provincia = "Ciudad Autónoma de Buenos Aires",
                pais = "Argentina",
                entreCalles = "Saraza y Dellepiane Sur",
                palabrasClave = new List<PalabraClave> {
                    new PalabraClave("Colectivo"),
                    new PalabraClave("114"),
                    new PalabraClave("Bondi") },
                coordenada = DbGeography.FromText("POINT(-58.468764 -34.659690)"),

                horarioAbierto = new List<HorarioAbierto>
    {
        new HorarioAbierto(System.DayOfWeek.Monday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Tuesday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Wednesday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Thursday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Friday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Saturday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Sunday, 0, 24)
    },
                horarioFeriado = new List<HorarioAbierto>()
            },
new ParadaDeColectivo
{
    nombreDePOI = "36",
    calle = "Av Escalada",
    numeroAltura = 2680,
    codigoPostal = 1428,
    localidad = "Ciudad Autónoma de Buenos Aires",
    barrio = "Lugano",
    provincia = "Ciudad Autónoma de Buenos Aires",
    pais = "Argentina",
    entreCalles = "Av Derqui y Dellepiane Norte",
    palabrasClave = new List<PalabraClave> {
                    new PalabraClave("Colectivo"),
                    new PalabraClave("36"),
                    new PalabraClave("Bondi") },
    coordenada = DbGeography.FromText("POINT(-58.473300 -34.662325)"),
    horarioAbierto = new List<HorarioAbierto>
    {
        new HorarioAbierto(System.DayOfWeek.Monday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Tuesday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Wednesday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Thursday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Friday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Saturday, 0, 24),
        new HorarioAbierto(System.DayOfWeek.Sunday, 0, 24)
    },
    horarioFeriado = new List<HorarioAbierto>()
});

            context.SaveChanges();


            List<HorarioAbierto> feriados = new List<HorarioAbierto>();
            feriados.Add(new HorarioAbierto(1, 1, 0, 0));
            feriados.Add(new HorarioAbierto(9, 7, 10, 16));









            //Locales
            context.Locales.AddOrUpdate(
            l => l.nombreDePOI,
            new LocalComercial
            {

                nombreDePOI = "Librería CEIT",
                coordenada = DbGeography.FromText("POINT(-58.467906 -34.659492)"),
                calle = "Cramer",
                numeroAltura = 2701,
                piso = 2,
                unidad = 3,
                codigoPostal = 1428,
                localidad = "Ciudad Autónoma de Buenos Aires",
                barrio = "Lugano",
                provincia = "Ciudad Autónoma de Buenos Aires",
                pais = "Argentina",
                entreCalles = "Av Derqui y Dellepiane Norte",
                rubro = new Rubro("librería escolar", 5),
                palabrasClave = new List<PalabraClave> {
                    new PalabraClave("CEIT"),
                    new PalabraClave("Librería") },
                horarioAbierto = new List<HorarioAbierto>
                {
                 new HorarioAbierto(System.DayOfWeek.Monday, 1, 1),
                 new HorarioAbierto(System.DayOfWeek.Tuesday, 2, 2),
                 new HorarioAbierto(System.DayOfWeek.Wednesday, 3, 3),
                 new HorarioAbierto(System.DayOfWeek.Thursday, 4, 4),
                 new HorarioAbierto(System.DayOfWeek.Friday, 5, 5),
                 new HorarioAbierto(System.DayOfWeek.Saturday, 6, 6),
                 new HorarioAbierto(System.DayOfWeek.Sunday, 7,7)
                }
                //horarioFeriados = feriados
            },

                new LocalComercial
                {
                    nombreDePOI = "Kiosco Las Flores",
                    coordenada = DbGeography.FromText("POINT(-58.482805 -34.634015)"),
                    calle = "Cramer",
                    numeroAltura = 2710,
                    piso = 1,
                    unidad = 4,
                    codigoPostal = 1428,
                    localidad = "Ciudad Autónoma de Buenos Aires",
                    barrio = "Lugano",
                    provincia = "Ciudad Autónoma de Buenos Aires",
                    pais = "Argentina",
                    entreCalles = "Av Derqui y Dellepiane Norte",
                    rubro = new Rubro("Kiosco", 5),
                    palabrasClave = new List<PalabraClave> {
                    new PalabraClave("Kiosco"),
                    new PalabraClave("Flores") },
                    horarioAbierto = new List<HorarioAbierto>
                {
                 new HorarioAbierto(System.DayOfWeek.Monday, 8, 8),
                 new HorarioAbierto(System.DayOfWeek.Tuesday, 9, 9),
                 new HorarioAbierto(System.DayOfWeek.Wednesday, 10, 1),
                 new HorarioAbierto(System.DayOfWeek.Thursday, 10, 2),
                 new HorarioAbierto(System.DayOfWeek.Friday, 10, 3),
                 new HorarioAbierto(System.DayOfWeek.Saturday, 10, 4),
                 new HorarioAbierto(System.DayOfWeek.Sunday, 10, 5)
                }
                });

            context.SaveChanges();









            //CGPs
            context.CGPs.AddOrUpdate(
                c => c.nombreDePOI,
                new CGP
                {
                    nombreDePOI = "Sede Comunal 8",
                    calle = "Av Coronel Roca",
                    numeroAltura = 5252,
                    codigoPostal = 1439,
                    piso = 2,
                    unidad = 3,
                    numeroDeComuna = 8,
                    localidad = "Ciudad Autónoma de Buenos Aires",
                    barrio = "Lugano",
                    provincia = "Ciudad Autónoma de Buenos Aires",
                    pais = "Argentina",
                    entreCalles = "Av Escalda y Av General Paz",
                    coordenada = DbGeography.FromText("POINT(-58.4606666 -34.6862397)"),
                    horarioAbierto = new List<HorarioAbierto>
                    {
                 new HorarioAbierto(System.DayOfWeek.Monday, 7, 16),
                 new HorarioAbierto(System.DayOfWeek.Tuesday, 7, 16),
                 new HorarioAbierto(System.DayOfWeek.Wednesday, 7, 16),
                 new HorarioAbierto(System.DayOfWeek.Thursday, 7, 16),
                 new HorarioAbierto(System.DayOfWeek.Friday, 7, 15),
                 new HorarioAbierto(System.DayOfWeek.Saturday, 7, 12),
                 new HorarioAbierto(System.DayOfWeek.Sunday, 7, 12)
                    },

                    palabrasClave = new List<PalabraClave> {
                    new PalabraClave("Sede Comunal 8"),
                    new PalabraClave("CGP") },
                    zonaDelimitadaPorLaComuna = 50,
                    servicios = new List<ServicioCGP>()
                {
                     new ServicioCGP()
                    {
                         nombre = "Registro CUIL",
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

                },
new CGP
{
    nombreDePOI = "Sede Comunal 10",
    calle = "Bacacay",
    numeroAltura = 3968,
    codigoPostal = 1407,
    piso = 2,
    unidad = 3,
    numeroDeComuna = 10,
    localidad = "Ciudad Autónoma de Buenos Aires",
    barrio = "Vélez Sarsfield",
    provincia = "Ciudad Autónoma de Buenos Aires",
    pais = "Argentina",
    entreCalles = "Mercedes y Av Chivilcoy",
    horarioAbierto = new List<HorarioAbierto>
                    {
                 new HorarioAbierto(System.DayOfWeek.Monday, 7, 16),
                 new HorarioAbierto(System.DayOfWeek.Tuesday, 7, 16),
                 new HorarioAbierto(System.DayOfWeek.Wednesday, 7, 16),
                 new HorarioAbierto(System.DayOfWeek.Thursday, 7, 16),
                 new HorarioAbierto(System.DayOfWeek.Friday, 7, 15),
                 new HorarioAbierto(System.DayOfWeek.Saturday, 7, 12),
                 new HorarioAbierto(System.DayOfWeek.Sunday, 7, 12)
                    },

    palabrasClave = new List<PalabraClave> {
        new PalabraClave("Sede Comunal 10"),
        new PalabraClave("CGP") },
    coordenada = DbGeography.FromText("POINT(-58.4857468 -34.6318411)"),
    zonaDelimitadaPorLaComuna = 10,
    servicios = new List<ServicioCGP>()
                {
                     new ServicioCGP()
                    {
                         nombre = "Casamiento",
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





            //Bancos
            context.Bancos.AddOrUpdate(
                b => b.nombreDePOI,
                new Banco
                {
                    nombreDePOI = "Banco Provincia",
                    coordenada = DbGeography.FromText("POINT(-58.469821 -34.660979)"),

                    calle = "Cramer",
                    numeroAltura = 2701,
                    piso = 9,
                    codigoPostal = 1428,
                    localidad = "Ciudad Autónoma de Buenos Aires",
                    barrio = "Vélez Sarsfield",
                    provincia = "Ciudad Autónoma de Buenos Aires",
                    pais = "Argentina",
                    entreCalles = "Mercedes y Av Chivilcoy",
                    horarioAbierto = new List<HorarioAbierto>
                        {
                 new HorarioAbierto(System.DayOfWeek.Monday, 0, 24),
                 new HorarioAbierto(System.DayOfWeek.Tuesday, 0, 24),
                 new HorarioAbierto(System.DayOfWeek.Wednesday, 0, 24),
                 new HorarioAbierto(System.DayOfWeek.Thursday, 0, 24),
                 new HorarioAbierto(System.DayOfWeek.Friday, 0, 24),
                 new HorarioAbierto(System.DayOfWeek.Saturday, 0, 24),
                 new HorarioAbierto(System.DayOfWeek.Sunday, 0, 24)
                        },



                    palabrasClave = new List<PalabraClave> {
                    new PalabraClave("Banco"),
                    new PalabraClave("Provincia") },
                    servicios = new List<ServicioBanco>()
                    {
                     new ServicioBanco()
                    {
                         nombre = "Extracción",
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
                },
                new Banco
                {
                    nombreDePOI = "Banco Francés",
                    coordenada = DbGeography.FromText("POINT(-58.4791142 -34.6579153)"),

                    calle = "Cramer",
                    numeroAltura = 2701,
                    piso = 9,
                    codigoPostal = 1428,
                    localidad = "Ciudad Autónoma de Buenos Aires",
                    barrio = "Vélez Sarsfield",
                    provincia = "Ciudad Autónoma de Buenos Aires",
                    pais = "Argentina",
                    entreCalles = "Mercedes y Av Chivilcoy",
                    horarioAbierto = new List<HorarioAbierto>
                        {
                 new HorarioAbierto(System.DayOfWeek.Monday, 0, 24),
                 new HorarioAbierto(System.DayOfWeek.Tuesday, 0, 24),
                 new HorarioAbierto(System.DayOfWeek.Wednesday, 0, 24),
                 new HorarioAbierto(System.DayOfWeek.Thursday, 0, 24),
                 new HorarioAbierto(System.DayOfWeek.Friday, 0, 24),
                 new HorarioAbierto(System.DayOfWeek.Saturday, 0, 24),
                 new HorarioAbierto(System.DayOfWeek.Sunday, 0, 24)
                        },


                    palabrasClave = new List<PalabraClave> {
                    new PalabraClave("Banco"),
                    new PalabraClave("Francés") },
                    servicios = new List<ServicioBanco>()
                    {
                     new ServicioBanco()
                    {
                         nombre = "Depósito",
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
                });

            context.SaveChanges();


        }
    }
}