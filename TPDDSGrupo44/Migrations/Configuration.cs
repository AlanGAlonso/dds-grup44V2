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
                duracionMaximaBusquedas = 30
            });

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
                        nombre = "Proceso M�ltiple Asinc",
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
                        nombre = "Tr�mite",
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


            // FUNCIONALIDADES DE TERMINALES
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
                );

            context.SaveChanges();
            
            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% Agrego terminales
            context.Terminales.AddOrUpdate(d => d.nombre,
            new DispositivoTactil
            {
                nombre = "UTN FRBA Lugano",
                coordenada = DbGeography.FromText("POINT(-34.6597047 -58.4688947)"),
                funcionalidades = new List<FuncionalidadDispositivoTactil>
                {
                    new FuncionalidadDispositivoTactil("Parada"),
                    new FuncionalidadDispositivoTactil("Banco"),
                    new FuncionalidadDispositivoTactil("CGP"),
                    new FuncionalidadDispositivoTactil("Locales")
                }
            },
new DispositivoTactil
{
    nombre = "Teatro Gran Rivadavia",
    coordenada = DbGeography.FromText("POINT(-34.6349293 -58.4853798)"),
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
                localidad = "Ciudad Aut�noma de Buenos Aires",
                barrio = "Lugano",
                provincia = "Ciudad Aut�noma de Buenos Aires",
                pais = "Argentina",
                entreCalles = "Saraza y Dellepiane Sur",
                palabrasClave = new List<PalabraClave> {
                    new PalabraClave("Colectivo"),
                    new PalabraClave("Bondi") },
                coordenada = DbGeography.FromText("POINT(-34.659690 -58.468764)"),

            },
new ParadaDeColectivo
{
    nombreDePOI = "36",
    calle = "Av Escalada",
    numeroAltura = 2680,
    localidad = "Ciudad Aut�noma de Buenos Aires",
    barrio = "Lugano",
    provincia = "Ciudad Aut�noma de Buenos Aires",
    pais = "Argentina",
    entreCalles = "Av Derqui y Dellepiane Norte",
    palabrasClave = new List<PalabraClave> {
                    new PalabraClave("Colectivo"),
                    new PalabraClave("Bondi") },
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
                nombreDePOI = "Librer�a CEIT",
                coordenada = DbGeography.FromText("POINT(-34.659492 -58.467906)"),
                rubro = new Rubro("librer�a escolar", 5),
                palabrasClave = new List<PalabraClave> {
                    new PalabraClave("CEIT"),
                    new PalabraClave("Librer�a") },
                horarioAbierto = horarios,
                //horarioFeriados = feriados
            },
new LocalComercial
{
    nombreDePOI = "Kiosco Las Flores",
    coordenada = DbGeography.FromText("POINT(-34.634015 -58.482805)"),
    palabrasClave = new List<PalabraClave> {
                    new PalabraClave("Las Flores"),
                    new PalabraClave("Kiosco") },
    rubro = new Rubro("kiosco de diarios", 5)
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
                localidad = "Ciudad Aut�noma de Buenos Aires",
                barrio = "Lugano",
                provincia = "Ciudad Aut�noma de Buenos Aires",
                pais = "Argentina",
                entreCalles = "Av Escalda y Av General Paz",
                coordenada = DbGeography.FromText("POINT(-34.6862397 -58.4606666)"),
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
    localidad = "Ciudad Aut�noma de Buenos Aires",
    barrio = "V�lez Sarsfield",
    provincia = "Ciudad Aut�noma de Buenos Aires",
    pais = "Argentina",
    entreCalles = "Mercedes y Av Chivilcoy",
    palabrasClave = new List<PalabraClave> {
        new PalabraClave("Sede Comunal 10"),
        new PalabraClave("CGP") },
    coordenada = DbGeography.FromText("POINT(-34.6318411 -58.4857468)"),
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
                coordenada = DbGeography.FromText("POINT( 34.660979  58.469821)"),
                palabrasClave = new List<PalabraClave> {
                    new PalabraClave("Banco"),
                    new PalabraClave("Provincia") },
                servicios = new List<ServicioBanco>()
                {
                     new ServicioBanco()
                    {
                         nombre = "Extracci�n",
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
                horarioAbierto = horarios
            },
            new Banco
            {
                nombreDePOI = "Banco Franc�s",
                coordenada = DbGeography.FromText("POINT( 34.6579153  58.4791142)"),
                palabrasClave = new List<PalabraClave> {
                    new PalabraClave("Banco"),
                    new PalabraClave("Franc�s") },
                servicios = new List<ServicioBanco>()
                {
                     new ServicioBanco()
                    {
                         nombre = "Dep�sito",
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