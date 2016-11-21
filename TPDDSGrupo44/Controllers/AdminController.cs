using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using TPDDSGrupo44.Models;
using System.Data.Entity.Spatial;
using TPDDSGrupo44.DataModels;
using TPDDSGrupo44.ViewModels;

namespace TPDDSGrupo44.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Reportes").ToList().Count() > 0)
            {
                List<Busqueda> busquedas;
                using (var db = new BuscAR())
                {
                    busquedas = db.Busquedas.Include("terminal").ToList();
                }
                return View(busquedas);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult SearchPerDay()
        {
            if (ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Reportes").ToList().Count() > 0)
            {
                return View(new SearchsPerDayViewModel());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ResultsPerSearch()
        {
            if (ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Reportes").ToList().Count() > 0)
            {
                return View(new SearchsPerResultViewModel());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }



        // PROCESOS ASINCRÓNICOS
        public ActionResult AsynchronusProcedures()
        {
            if (ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Actualizar Local Comercial Asinc"
                                                                             || f.nombre == "Agregar Acciones Asinc"
                                                                             || f.nombre == "Proceso Múltiple Asinc"
                                                                             || f.nombre == "Baja POIs Asinc").ToList().Count() > 0)
            {
                List<ActualizacionAsincronica> actualizacion;
                using (var db = new BuscAR())
                {
                    actualizacion = db.FuncionalidadesUsuarios.OfType<ActualizacionAsincronica>().GroupBy(f => f.nombre).Select(f => f.FirstOrDefault()).ToList();
                }
                return View(actualizacion);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }



        // TERMINALES

        public ActionResult ABMTerminal()
        {
            if (ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Terminales").ToList().Count() > 0)
            {
                List<DispositivoTactil> terminales;
                using (var db = new BuscAR())
                {
                    terminales = db.Terminales.Include("funcionalidades").ToList();
                }
                return View(terminales);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        public ActionResult EditTerminal(int id)
        {
            if (ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Terminales").ToList().Count() > 0)
            {
                DispositivoTactil terminal;
                List<FuncionalidadDispositivoTactil> func = new List<FuncionalidadDispositivoTactil>();
                using (var db = new BuscAR())
                {
                    terminal = db.Terminales.Include("funcionalidades").Where(t => t.Id == id).Single();
                    func = db.FuncionalidadesTerminales.GroupBy(f => f.nombre).Select(f => f.FirstOrDefault()).ToList();
                }
                EditTerminalViewModel viewModel = new EditTerminalViewModel();
                viewModel.terminal = terminal;
                viewModel.funcionalidades = func;

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }



        [HttpPost]
        public ActionResult EditTerminal(FormCollection form)
        {
            if (ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Terminales").ToList().Count() > 0)
            {
                DispositivoTactil terminal;
                int id = Convert.ToInt32(form["terminal.Id"]);
                using (var db = new BuscAR())
                {
                    terminal = db.Terminales.Include("funcionalidades").Where(t => t.Id == id).Single();
                    DbGeography coordenada = DbGeography.FromText("POINT(" + form["terminal.coordenada.Latitude"] + " " + form["terminal.coordenada.Longitude"] + ")");

                    terminal.coordenada = coordenada;
                    terminal.nombre = form["terminal.nombre"];
                    var func = form["funcionalidades"];
                    List<string> funcFront = func.Split(new char[] { ',' }).ToList();
                    List<FuncionalidadDispositivoTactil> funcionalidades = new List<FuncionalidadDispositivoTactil>();
                    foreach (string p in funcFront)
                    {
                        funcionalidades.Add(new FuncionalidadDispositivoTactil(p));
                    }
                    terminal.funcionalidades.RemoveAll(f => f.nombre != "");
                    terminal.funcionalidades = funcionalidades;

                    db.SaveChanges();

                }

                return RedirectToAction("ABMTerminal", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // CONFIGURACIONES
        public ActionResult Settings()
        {
            if (BaseViewModel.usuario != null && ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Configuracion").ToList().Count() > 0)
            {
                if (BaseViewModel.configuracion == null)
                {
                    using (var db = new BuscAR())
                    {
                        BaseViewModel.configuracion = db.Configuraciones.Single();
                    }
                }
                return View(BaseViewModel.configuracion);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST
        [HttpPost]
        public ActionResult Settings(int id, FormCollection collection)
        {
            if (BaseViewModel.usuario != null && ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Configuracion").ToList().Count() > 0)
            {
                using (var db = new BuscAR())
                {
                    Configuracion config = db.Configuraciones.Single();
                    config.duracionMaximaBusquedas = Convert.ToInt32(collection["duracionMaximaBusquedas"]);
                    config.generarLog = Convert.ToBoolean(collection["generarLog"]);
                    BaseViewModel.configuracion = config;

                    db.SaveChanges();
                }

                return View(BaseViewModel.configuracion);
            }
            else
            {
                return View(BaseViewModel.configuracion);
            }
        }

        // ---------------------------------------------------------------------------------------
        //                             A B M   P A R A D A
        //----------------------------------------------------------------------------------------

        public ActionResult ABMParada()
        {
            if (ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Consultar POI").ToList().Count() > 0)
            {
                List<ParadaDeColectivo> paradas = ParadaDeColectivo.MostrarTodasLasParadas();
                return View(paradas);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        public ActionResult CreateParada()
        {
            if (ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Alta POI").ToList().Count() > 0)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ABMParada", "Admin");
            }
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult CreateParada(FormCollection collection)
        {
            try
            {
                if (ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Alta POI").ToList().Count() > 0)
                {
                    DbGeography coordenada = DbGeography.FromText("POINT(" + collection["coordenada.Longitude"] + " " + collection["coordenada.Latitude"] + ")");
                    List<PalabraClave> palabrasClave = parsearListaDePalabras(collection["palabrasClave"]);



                    ParadaDeColectivo parada = new ParadaDeColectivo(coordenada, collection["calle"], Convert.ToInt32(collection["numeroAltura"]),
                        Convert.ToInt32(collection["codigoPostal"]), collection["localidad"], collection["barrio"], collection["provincia"],
                        collection["pais"], collection["entreCalles"], palabrasClave, collection["nombreDePOI"]);

                    ParadaDeColectivo.agregarParada(parada);

                    return RedirectToAction("ABMParada");
                }
                else
                {
                    return RedirectToAction("ABMParada", "Admin");
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteParada(int id)
        {
            if (ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Baja POI").ToList().Count() > 0)
            {
                ParadaDeColectivo parada = ParadaDeColectivo.buscarParadaPorId(id);
                return View(parada);
            }
            else
            {
                return RedirectToAction("ABMParada", "Admin");
            }
        }

        // POST: Default/Delete
        [HttpPost]
        public ActionResult DeleteParada(int id, FormCollection collection)
        {
            try
            {
                if (ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Baja POI").ToList().Count() > 0)
                {

                    ParadaDeColectivo.eliminarParada(id);


                    return RedirectToAction("ABMParada");
                }
                else
                {
                    return RedirectToAction("ABMParada", "Admin");
                }
            }
            catch
            {
                return View();
            }
        }


        public ActionResult EditParada(int id)
        {
            if (ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Editar POI").ToList().Count() > 0)
            {
                ParadaDeColectivo parada = ParadaDeColectivo.buscarParadaPorId(id);
                return View(parada);
            }
            else
            {
                return RedirectToAction("ABMParada", "Admin");
            }
        }

        // POST: Default/Edit
        [HttpPost]
        public ActionResult EditParada(FormCollection collection)
        {
            try
            {
                if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Editar POI").ToList().Count() > 0)
                {

                    int id = Convert.ToInt16(collection["id"]);
                    ParadaDeColectivo.eliminarPalabrasClaves(id);
                    List<PalabraClave> palabrasClave = parsearListaDePalabras(collection["palabrasClave"]);
                    DbGeography coordenada = actualizarCoordenada(collection["coordenada.Latitude"], collection["coordenada.Longitude"]);



                    ParadaDeColectivo.actualizar(id, coordenada, collection["calle"], Convert.ToInt32(collection["numeroAltura"]),
                            Convert.ToInt32(collection["codigoPostal"]), collection["localidad"], collection["barrio"], collection["provincia"],
                            collection["pais"], collection["entreCalles"], palabrasClave, collection["nombreDePOI"]);



                    return RedirectToAction("ABMParada");
                }
                else
                {
                    return RedirectToAction("ABMParada", "Admin");
                }
            }
            catch
            {
                return View();
            }
        }



        // ---------------------------------------------------------------------------------------
        //                             A B M   B A N C O
        //----------------------------------------------------------------------------------------

        public ActionResult ABMBanco()
        {
            if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Consultar POI").ToList().Count() > 0)
            {
                List<Banco> bancos = Banco.MostrarTodosLosBancos();
                return View(bancos);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult CreateBanco()
        {
            if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Alta POI").ToList().Count() > 0)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Default/Create"
        [HttpPost]
        public ActionResult CreateBanco(FormCollection collection)
        {
            try
            {
                if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Alta POI").ToList().Count() > 0)
                {
                    DbGeography coordenada = DbGeography.FromText("POINT(" + collection["coordenada.Longitude"] + " " + collection["coordenada.Latitude"] + ")");
                    List<PalabraClave> palabrasClave = parsearListaDePalabras(collection["palabrasClave"]);
                    List<HorarioAbierto> horariosAbierto = new List<HorarioAbierto>();
                    horariosAbierto = funcParce(collection["abreLunes"], collection["cierraLunes"], horariosAbierto, DayOfWeek.Monday);
                    horariosAbierto = funcParce(collection["abreMartes"], collection["cierraMartes"], horariosAbierto, DayOfWeek.Tuesday);
                    horariosAbierto = funcParce(collection["abreMiercoles"], collection["cierraMiercoles"], horariosAbierto, DayOfWeek.Wednesday);
                    horariosAbierto = funcParce(collection["abreJueves"], collection["cierraJueves"], horariosAbierto, DayOfWeek.Thursday);
                    horariosAbierto = funcParce(collection["abreViernes"], collection["cierraViernes"], horariosAbierto, DayOfWeek.Friday);
                    horariosAbierto = funcParce(collection["abreSabado"], collection["cierraSabado"], horariosAbierto, DayOfWeek.Saturday);
                    horariosAbierto = funcParce(collection["abreDomingo"], collection["cierraDomingo"], horariosAbierto, DayOfWeek.Sunday);
                    List<HorarioAbierto> horariosFeriado = new List<HorarioAbierto>();
                    List<ServicioBanco> servicios = new List<ServicioBanco>();
                    Banco banco = new Banco(coordenada, collection["calle"], Convert.ToInt32(collection["numeroAltura"]),
                          Convert.ToInt32(collection["piso"]), Convert.ToInt32(collection["codigoPostal"]), collection["localidad"],
                          collection["barrio"], collection["provincia"], collection["pais"], collection["entreCalles"],
                          palabrasClave, collection["nombreDePOI"], horariosAbierto, horariosFeriado, servicios);

                    Banco.agregarBanco(banco);

                    return RedirectToAction("CreateServBancos", banco);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return View();
            }
        }


        public ActionResult DeleteBanco(int id)
        {
            if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Baja POI").ToList().Count() > 0)
            {
                Banco banco = Banco.buscarBanco2(id);
                return View(banco);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult DeleteBanco(int id, FormCollection collection)
        {
            try
            {
                if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Baja POI").ToList().Count() > 0)
                {
                    Banco.eliminarBanco(id);

                    return RedirectToAction("ABMBanco");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EditBanco(int id)
        {
            if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Editar POI").ToList().Count() > 0)
            {
                Banco banco= Banco.buscarBancoPorId(id);

                return View(banco);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Default/Edit
        [HttpPost]
        public ActionResult EditBanco(FormCollection collection)
        {
            try
            {
                if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Editar POI").ToList().Count() > 0)
                {
                    int id = Convert.ToInt32(collection["id"]);
                    Banco banco = Banco.buscarBancoPorId(id);
                    if (Request.Form["eliminarServicios"] != null)
                    {
                      Banco.eliminarServicios(id);
                      return View(banco);
                    }
                    Banco.eliminarPalabrasClaves(id);
                    List<PalabraClave> palabrasClave = parsearListaDePalabras(collection["palabrasClave"]);
                    DbGeography coordenada = actualizarCoordenada(collection["coordenada.Latitude"], collection["coordenada.Longitude"]);

                    Banco.eliminarHorarios(id);
                    List<HorarioAbierto> horariosAbierto = new List<HorarioAbierto>();
                    horariosAbierto = funcParce(collection["abreLunes"], collection["cierraLunes"], horariosAbierto, DayOfWeek.Monday);
                    horariosAbierto = funcParce(collection["abreMartes"], collection["cierraMartes"], horariosAbierto, DayOfWeek.Tuesday);
                    horariosAbierto = funcParce(collection["abreMiercoles"], collection["cierraMiercoles"], horariosAbierto, DayOfWeek.Wednesday);
                    horariosAbierto = funcParce(collection["abreJueves"], collection["cierraJueves"], horariosAbierto, DayOfWeek.Thursday);
                    horariosAbierto = funcParce(collection["abreViernes"], collection["cierraViernes"], horariosAbierto, DayOfWeek.Friday);
                    horariosAbierto = funcParce(collection["abreSabado"], collection["cierraSabado"], horariosAbierto, DayOfWeek.Saturday);
                    horariosAbierto = funcParce(collection["abreDomingo"], collection["cierraDomingo"], horariosAbierto, DayOfWeek.Sunday);
                    List<HorarioAbierto> horariosFeriado = new List<HorarioAbierto>();
                    List<ServicioBanco> servicios = new List<ServicioBanco>();
        
                    Banco.actualizar(id, coordenada, collection["calle"], Convert.ToInt32(collection["numeroAltura"]), Convert.ToInt32(collection["piso"]),
                      Convert.ToInt32(collection["codigoPostal"]), collection["localidad"], collection["barrio"], collection["provincia"],
                      collection["pais"], collection["entreCalles"], collection["nombreDePOI"], palabrasClave, horariosAbierto, horariosFeriado, servicios);
                     

                    if (Request.Form["siguiente"] != null)
                    {
                        return RedirectToAction("CreateServBancos", banco);
                    }





                    return RedirectToAction("ABMBanco");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return View();
            }
}

        
        public ActionResult CreateServBancos(Banco banco)
        {
            if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Alta POI").ToList().Count() > 0)
            {
                ServBViewModel viewModel = new ServBViewModel(banco.id);

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult CreateServBancos(FormCollection collection, int id)
        {
            try
            {

                if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Alta POI").ToList().Count() > 0)
                {
                    if (Request.Form["terminar"] != null && (collection["nombre"] == null))
                    {
                        return RedirectToAction("ABMBanco", "Admin");
                    }

                    Banco banco = Banco.agregarHorariosAServicios(collection, id);

                    if (Request.Form["terminar"] != null)
                    {
                        return RedirectToAction("ABMBanco", "Admin");
                    }
                    else
                    {
                        ServBViewModel viewModel = new ServBViewModel(banco.id);
                        return RedirectToAction("CreateServBancos", "Admin", viewModel);
                    }

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {
                return View();
            }
        }


        // ---------------------------------------------------------------------------------------
        //                             A B M   C G P
        //----------------------------------------------------------------------------------------

        public ActionResult ABMCGP()
        {
            if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Consultar POI").ToList().Count() > 0)
            {
                List<CGP> cgp = CGP.buscarCGPs();
                return View(cgp);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        public ActionResult CreateCGP()
        {
            if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Alta POI").ToList().Count() > 0)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult CreateCGP(FormCollection collection)
        {
            try
            {
                if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Alta POI").ToList().Count() > 0)
                {
                    DbGeography coordenada = DbGeography.FromText("POINT(" + collection["coordenada.Longitude"] + " " + collection["coordenada.Latitude"] + ")");
                    List<PalabraClave> palabrasClave = parsearListaDePalabras(collection["palabrasClave"]);

                    List<HorarioAbierto> horariosAbierto = new List<HorarioAbierto>();
                    horariosAbierto = funcParce(collection["abreLunes"], collection["cierraLunes"], horariosAbierto, DayOfWeek.Monday);
                    horariosAbierto = funcParce(collection["abreMartes"], collection["cierraMartes"], horariosAbierto, DayOfWeek.Tuesday);
                    horariosAbierto = funcParce(collection["abreMiercoles"], collection["cierraMiercoles"], horariosAbierto, DayOfWeek.Wednesday);
                    horariosAbierto = funcParce(collection["abreJueves"], collection["cierraJueves"], horariosAbierto, DayOfWeek.Thursday);
                    horariosAbierto = funcParce(collection["abreViernes"], collection["cierraViernes"], horariosAbierto, DayOfWeek.Friday);
                    horariosAbierto = funcParce(collection["abreSabado"], collection["cierraSabado"], horariosAbierto, DayOfWeek.Saturday);
                    horariosAbierto = funcParce(collection["abreDomingo"], collection["cierraDomingo"], horariosAbierto, DayOfWeek.Sunday);
                    List<HorarioAbierto> horariosFeriado = new List<HorarioAbierto>();
                    List<ServicioCGP> servicios = new List<ServicioCGP>();

                    CGP cgp = new CGP(coordenada, collection["calle"], Convert.ToInt32(collection["numeroAltura"]),
                          Convert.ToInt32(collection["piso"]), Convert.ToInt32(collection["unidad"]), Convert.ToInt32(collection["codigoPostal"]),
                          collection["localidad"], collection["barrio"], collection["provincia"], collection["pais"], collection["entreCalles"],
                          palabrasClave, collection["nombreDePOI"], "CGP", Convert.ToInt32(collection["numeroDeComuna"]),
                              servicios, Convert.ToInt32(collection["zonaDelimitadaPorLaComuna"]), horariosAbierto, horariosFeriado);

                    CGP.agregarCGP(cgp);

                    return RedirectToAction("CreateServCGP",cgp);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteCGP(int id)
        {
            if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Baja POI").ToList().Count() > 0)
            {
                CGP cgp = CGP.buscarCGPDelete(id);
                return View(cgp);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult DeleteCGP(int id, FormCollection collection)
        {
            try
            {
                if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Baja POI").ToList().Count() > 0)
                {
                    CGP.eliminarCGP(id);

                    return RedirectToAction("ABMCGP");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return View();
            }
        }


        public ActionResult EditCGP(int id)
        {
            if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Editar POI").ToList().Count() > 0)
            {
                CGP cgp = CGP.buscarCGPEdit(id);
                return View(cgp);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Default/Edit
        [HttpPost]
        public ActionResult EditCGP(FormCollection collection)
        {
            try
            {
                if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Editar POI").ToList().Count() > 0)
                {

                    int id = Convert.ToInt16(collection["id"]);
                    CGP cgp =   CGP.buscarCGPEdit(id);

                    if (Request.Form["eliminarServicios"] != null)
                    {
                        CGP.eliminarServicios(id);
                        return View(cgp);
                    }

                    CGP.eliminarPalabrasClaves(id);

                    List<PalabraClave> palabrasClave = parsearListaDePalabras(collection["palabrasClave"]);
                    DbGeography coordenada = actualizarCoordenada(collection["coordenada.Latitude"], collection["coordenada.Longitude"]);

                    CGP.eliminarHorarios(id);
                    List<HorarioAbierto> horariosAbierto = new List<HorarioAbierto>();
                    horariosAbierto = funcParce(collection["abreLunes"], collection["cierraLunes"], horariosAbierto, DayOfWeek.Monday);
                    horariosAbierto = funcParce(collection["abreMartes"], collection["cierraMartes"], horariosAbierto, DayOfWeek.Tuesday);
                    horariosAbierto = funcParce(collection["abreMiercoles"], collection["cierraMiercoles"], horariosAbierto, DayOfWeek.Wednesday);
                    horariosAbierto = funcParce(collection["abreJueves"], collection["cierraJueves"], horariosAbierto, DayOfWeek.Thursday);
                    horariosAbierto = funcParce(collection["abreViernes"], collection["cierraViernes"], horariosAbierto, DayOfWeek.Friday);
                    horariosAbierto = funcParce(collection["abreSabado"], collection["cierraSabado"], horariosAbierto, DayOfWeek.Saturday);
                    horariosAbierto = funcParce(collection["abreDomingo"], collection["cierraDomingo"], horariosAbierto, DayOfWeek.Sunday);
                    List<HorarioAbierto> horariosFeriado = new List<HorarioAbierto>();
                    List<ServicioCGP> servicios = new List<ServicioCGP>();

                    CGP.actualizar(id, coordenada, collection["calle"], Convert.ToInt32(collection["numeroAltura"]),
                      Convert.ToInt32(collection["piso"]), Convert.ToInt32(collection["unidad"]), Convert.ToInt32(collection["codigoPostal"]),
                      collection["localidad"], collection["barrio"], collection["provincia"], collection["pais"], collection["entreCalles"],
                      palabrasClave, collection["nombreDePOI"], Convert.ToInt32(collection["numeroDeComuna"]),
                          servicios, Convert.ToInt32(collection["zonaDelimitadaPorLaComuna"]), horariosAbierto, horariosFeriado);


                    if (Request.Form["siguiente"] != null)
                    {
                        return RedirectToAction("CreateServCGP", cgp);
                    }

                    return RedirectToAction("ABMCGP");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return View();
            }
        }


        public ActionResult CreateServCGP(CGP cgp)
        {
            if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Alta POI").ToList().Count() > 0)
            {
                ServCGPViewModel viewModel = new ServCGPViewModel(cgp.id);

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult CreateServCGP(FormCollection collection, int id)
        {
            try
            {

                if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Alta POI").ToList().Count() > 0)
                {
                    if (Request.Form["terminar"] != null && (collection["nombre"] == null))
                    {
                        return RedirectToAction("ABMCGP", "Admin");
                    }

                    CGP cgp = CGP.agregarHorariosAServicios(collection, id);

                    if (Request.Form["terminar"] != null)
                    {
                        return RedirectToAction("ABMCGP", "Admin");
                    }
                    else
                    {
                        ServCGPViewModel viewModel = new ServCGPViewModel(cgp.id);
                        return RedirectToAction("CreateServCGP", "Admin", viewModel);
                    }

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {
                return View();
            }
        }

        // ---------------------------------------------------------------------------------------
        //                             A B M   L O C A L   C O M E R C I A L
        //----------------------------------------------------------------------------------------


        public ActionResult ABMLocalComercial()
        {
            if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Consultar POI").ToList().Count() > 0)
            {
                List<LocalComercial> localComercial =LocalComercial.buscarTodosLosLocales();
                return View(localComercial);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult CreateLocalComercial()
        {
            if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Alta POI").ToList().Count() > 0)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult CreateLocalComercial(FormCollection collection)
        {
            try
            {
                if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Alta POI").ToList().Count() > 0)
                {
                    DbGeography coordenada = DbGeography.FromText("POINT(" + collection["coordenada.Longitude"] + " " + collection["coordenada.Latitude"] + ")");
                    List<PalabraClave> palabrasClave = parsearListaDePalabras(collection["palabrasClave"]);

                    List<HorarioAbierto> horariosAbierto = new List<HorarioAbierto>();
                    horariosAbierto = funcParce(collection["abreLunes"], collection["cierraLunes"], horariosAbierto, DayOfWeek.Monday);
                    horariosAbierto = funcParce(collection["abreMartes"], collection["cierraMartes"], horariosAbierto, DayOfWeek.Tuesday);
                    horariosAbierto = funcParce(collection["abreMiercoles"], collection["cierraMiercoles"], horariosAbierto, DayOfWeek.Wednesday);
                    horariosAbierto = funcParce(collection["abreJueves"], collection["cierraJueves"], horariosAbierto, DayOfWeek.Thursday);
                    horariosAbierto = funcParce(collection["abreViernes"], collection["cierraViernes"], horariosAbierto, DayOfWeek.Friday);
                    horariosAbierto = funcParce(collection["abreSabado"], collection["cierraSabado"], horariosAbierto, DayOfWeek.Saturday);
                    horariosAbierto = funcParce(collection["abreDomingo"], collection["cierraDomingo"], horariosAbierto, DayOfWeek.Sunday);
                    List<HorarioAbierto> horariosFeriado = new List<HorarioAbierto>();

                    Rubro rubro = new Rubro();
                    rubro.nombre = collection["rubro"];
                    rubro.radioDeCercania = Convert.ToInt32(collection["radioDeCercania"]);

                    LocalComercial localComercial = new LocalComercial(coordenada, collection["calle"], Convert.ToInt32(collection["numeroAltura"]),
                      Convert.ToInt32(collection["piso"]), Convert.ToInt32(collection["unidad"]), Convert.ToInt32(collection["codigoPostal"]),
                      collection["localidad"], collection["barrio"], collection["provincia"], collection["pais"], collection["entreCalles"],
                      palabrasClave, collection["nombreDePOI"], horariosAbierto, horariosFeriado, rubro);

                    LocalComercial.agregarLocComercial(localComercial);

                    return RedirectToAction("ABMLocalComercial");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteLocalComercial(int id)
        {
            if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Baja POI").ToList().Count() > 0)
            {
                LocalComercial localComercial;
                using (var db = new BuscAR())
                {
                    localComercial = db.Locales.Include("palabrasClave").Include("horarioAbierto").Where(p => p.id == id).Single();
                }
                return View(localComercial);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult DeleteLocalComercial(int id, FormCollection collection)
        {
            try
            {
                if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Baja POI").ToList().Count() > 0)
                {
                    LocalComercial.eliminarLocComercial(id);

                    return RedirectToAction("ABMLocalComercial");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EditLocalComercial(int id)
        {
            if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Editar POI").ToList().Count() > 0)
            {
                LocalComercial localComercial = LocalComercial.busquedaParaEdit (id);   
                return View(localComercial);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Default/Edit
        [HttpPost]
        public ActionResult EditLocalComercial(FormCollection collection)
        {
            try
            {
                if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Editar POI").ToList().Count() > 0)
                {
                    int id = Convert.ToInt16(collection["id"]);
                    LocalComercial.eliminarPalabrasClaves(id);
                    List<PalabraClave> palabrasClave = parsearListaDePalabras(collection["palabrasClave"]);
                    DbGeography coordenada = actualizarCoordenada(collection["coordenada.Latitude"], collection["coordenada.Longitude"]);

                    LocalComercial.eliminarHorarios(id);
                    List<HorarioAbierto> horariosAbierto = new List<HorarioAbierto>();
                    horariosAbierto = funcParce(collection["abreLunes"], collection["cierraLunes"], horariosAbierto, DayOfWeek.Monday);
                    horariosAbierto = funcParce(collection["abreMartes"], collection["cierraMartes"], horariosAbierto, DayOfWeek.Tuesday);
                    horariosAbierto = funcParce(collection["abreMiercoles"], collection["cierraMiercoles"], horariosAbierto, DayOfWeek.Wednesday);
                    horariosAbierto = funcParce(collection["abreJueves"], collection["cierraJueves"], horariosAbierto, DayOfWeek.Thursday);
                    horariosAbierto = funcParce(collection["abreViernes"], collection["cierraViernes"], horariosAbierto, DayOfWeek.Friday);
                    horariosAbierto = funcParce(collection["abreSabado"], collection["cierraSabado"], horariosAbierto, DayOfWeek.Saturday);
                    horariosAbierto = funcParce(collection["abreDomingo"], collection["cierraDomingo"], horariosAbierto, DayOfWeek.Sunday);
                    List<HorarioAbierto> horariosFeriado = new List<HorarioAbierto>();

                    LocalComercial.modificarRubro(id, collection["rubro.nombre"], collection["rubro.radioDeCercania"]);


                    LocalComercial.actualizar(id, coordenada, collection["calle"], Convert.ToInt32(collection["numeroAltura"]), Convert.ToInt32(collection["piso"]),
                      Convert.ToInt32(collection["unidad"]), Convert.ToInt32(collection["codigoPostal"]), collection["localidad"], collection["barrio"],
                      collection["provincia"], collection["pais"], collection["entreCalles"], collection["nombreDePOI"],
                      palabrasClave, horariosAbierto, horariosFeriado);


                    return RedirectToAction("ABMLocalComercial");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return View();
            }
        }



       



        private List<HorarioAbierto> funcParce(string horarioApertura, string horarioCierre, List<HorarioAbierto> listHorarios, DayOfWeek dia)
        {
            TimeSpan apertura;
            TimeSpan.TryParse(horarioApertura, out apertura);
            TimeSpan cierre;
            TimeSpan.TryParse(horarioCierre, out cierre);

            HorarioAbierto horarios = new HorarioAbierto(dia, apertura.Hours, cierre.Hours);

            listHorarios.Add(horarios);
            return listHorarios;
        }





        private List<PalabraClave> parsearListaDePalabras(string listaSeparadaConComas)
        {
            if (listaSeparadaConComas != "")
            {
                List<string> palabrasClaveFront = listaSeparadaConComas.Split(new char[] { ',' }).ToList();
                List<PalabraClave> palabrasClave = new List<PalabraClave>();
                foreach (string p in palabrasClaveFront)
                {
                    if (p != "")
                    {
                        palabrasClave.Add(new PalabraClave(p));
                    }
                }
                return palabrasClave;
            }
            else
            {
                List<PalabraClave> palabrasClave = new List<PalabraClave>();
                return palabrasClave;
            }
        }

        private DbGeography actualizarCoordenada(string coorLat, string coorLong)
        {
            string coordenadaLatitude = coorLat.Replace(",", ".");
            string coordenadaLongitude = coorLong.Replace(",", ".");
            DbGeography coordenada = DbGeography.FromText("POINT(" + coordenadaLongitude + " " + coordenadaLatitude + ")");
            return coordenada;
        }

    }


}