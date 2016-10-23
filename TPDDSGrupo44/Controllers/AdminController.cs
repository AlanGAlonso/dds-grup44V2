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
                return View(recuperarBusquedas());
            } else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult SearchPerDay()
        {
            if (ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Reportes").ToList().Count() > 0)
            {
                return View(recuperarBusquedas());
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
                return View(recuperarBusquedas());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private List<Busqueda> recuperarBusquedas()
        {
            List<Busqueda> busquedas;
            using (var db = new BuscAR())
            {
                busquedas = db.Busquedas.Include("terminal").ToList();
            }

            return busquedas;
        }

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
                List<ParadaDeColectivo> paradas;
                using (var db = new BuscAR())
                {
                    paradas = (from p in db.Paradas
                               orderby p.nombreDePOI
                               select p).ToList();
                }

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
                return RedirectToAction("Index", "Home");
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
                    DbGeography coordenada = DbGeography.FromText("POINT(" + collection["coordenada.Latitude"] + " " + collection["coordenada.Longitude"] + ")");
                    List<PalabraClave> palabrasClave = parsearListaDePalabras(collection["palabrasClave"]);



                    ParadaDeColectivo parada = new ParadaDeColectivo(coordenada, collection["calle"], Convert.ToInt32(collection["numeroAltura"]),
                        Convert.ToInt32(collection["codigoPostal"]), collection["localidad"], collection["barrio"], collection["provincia"],
                        collection["pais"], collection["entreCalles"], palabrasClave, collection["nombreDePOI"]);

                    parada.agregarParada(parada);

                    return RedirectToAction("ABMParada");
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

        public ActionResult DeleteParada(int id)
        {
            if (ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Baja POI").ToList().Count() > 0)
            {
                ParadaDeColectivo parada;
                using (var db = new BuscAR())
                {
                    parada = db.Paradas.Include("palabrasClave").Where(p => p.id == id).Single();
                }
                return View(parada);
            }
            else
            {
                return RedirectToAction("Index", "Home");
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
                    ParadaDeColectivo parada;

                    using (var db = new BuscAR())
                    {
                        parada = db.Paradas.Where(p => p.id == id).Single();
                        parada.palabrasClave.Clear();
                        db.SaveChanges();
                    }

                    if (parada.palabrasClave.Count() < 1)
                    {
                        parada.eliminarParada(id);
                    }

                    return RedirectToAction("ABMParada");
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


        public ActionResult EditParada(int id)
        {
            if (ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Editar POI").ToList().Count() > 0)
            {
                ParadaDeColectivo parada;
                using (var db = new BuscAR())
                {
                    parada = db.Paradas.Include("palabrasClave").Where(p => p.id == id).Single();
                }
                return View(parada);
            }
            else
            {
                return RedirectToAction("Index", "Home");
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
                    ParadaDeColectivo parada;
                    using (var db = new BuscAR())
                    {
                        int id = Convert.ToInt16(collection["id"]);
                        parada = db.Paradas.Where(p => p.id == id).Single();

                        parada.palabrasClave.Clear();
                        db.SaveChanges();
                        List<PalabraClave> palabrasClave = parsearListaDePalabras(collection["palabrasClave"]);
                        DbGeography coordenada = actualizarCoordenada(collection["coordenada.Latitude"], collection["coordenada.Longitude"]);



                        parada.actualizar(coordenada, collection["calle"], Convert.ToInt32(collection["numeroAltura"]),
                            Convert.ToInt32(collection["codigoPostal"]), collection["localidad"], collection["barrio"], collection["provincia"],
                            collection["pais"], collection["entreCalles"], palabrasClave, collection["nombreDePOI"]);


                        db.SaveChanges();
                    }

                    return RedirectToAction("ABMParada");
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
        //                             A B M   B A N C O
        //----------------------------------------------------------------------------------------

        public ActionResult ABMBanco()
        {
            if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Consultar POI").ToList().Count() > 0)
            {
                List<Banco> bancos;
                using (var db = new BuscAR())
                {
                    bancos = (from p in db.Bancos
                              orderby p.nombreDePOI
                              select p).ToList();
                }

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

        // POST: Default/Create
        [HttpPost]
        public ActionResult CreateBanco(FormCollection collection)
        {
            try
            {

                if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Alta POI").ToList().Count() > 0)
                {
                    DbGeography coordenada = DbGeography.FromText("POINT(" + collection["coordenada.Latitude"] + " " + collection["coordenada.Longitude"] + ")");
                    List<PalabraClave> palabrasClave = parsearListaDePalabras(collection["palabrasClave"]);

                    List<HorarioAbierto> horariosAbierto = new List<HorarioAbierto>();
                    //         HorarioAbierto horarios = new HorarioAbierto(DayOfWeek.Monday, Convert.ToInt32(collection["abreLunes"]), Convert.ToInt32(collection["cierraLunes"]));
                    //         horariosAbierto.Add(horarios);
                    List<HorarioAbierto> horariosFeriado = new List<HorarioAbierto>();

                    List<ServicioBanco> servicios = new List<ServicioBanco>();

                    Banco banco = new Banco(coordenada, collection["calle"], Convert.ToInt32(collection["numeroAltura"]),
                          Convert.ToInt32(collection["piso"]), Convert.ToInt32(collection["codigoPostal"]), collection["localidad"],
                          collection["barrio"], collection["provincia"], collection["pais"], collection["entreCalles"],
                          palabrasClave, collection["nombreDePOI"], horariosAbierto, horariosFeriado, servicios);

                    banco.agregarBanco(banco);

                    return RedirectToAction("CreateServBancos",banco);
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
        public ActionResult CreateServBancos(FormCollection collection,int id)
        {
            try
            {

                if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Alta POI").ToList().Count() > 0)
                {

                    Banco banco;
                    using (var db = new BuscAR())
                    {
                        banco = db.Bancos.Where(p => p.id == id).Single();
                        ServicioBanco servicioBanco = new ServicioBanco();
                        servicioBanco.nombre = collection["nombre"];
                        banco.servicios.Add(servicioBanco);
                        db.SaveChanges();
                    }

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


        public ActionResult DeleteBanco(int id)
        {
            if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Baja POI").ToList().Count() > 0)
            {
                Banco banco;
                using (var db = new BuscAR())
                {
                    banco = db.Bancos.Include("palabrasClave").Include("servicios").Where(p => p.id == id).Single();
                }
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
                    Banco banco;
                    using (var db = new BuscAR())
                    {
                        banco = db.Bancos.Where(p => p.id == id).Single();
                        banco.servicios.Clear();
                        banco.palabrasClave.Clear();
                        db.SaveChanges();
                    }

                    banco.eliminarBanco(id);

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
                Banco banco;
                using (var db = new BuscAR())
                {
                    banco = db.Bancos.Include("palabrasClave").Where(p => p.id == id).Single();
                }
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
                    Banco banco;
                    using (var db = new BuscAR())
                    {
                        int id = Convert.ToInt16(collection["id"]);
                        banco = db.Bancos.Where(p => p.id == id).Single();

                        banco.palabrasClave.Clear();
                        db.SaveChanges();
                        List<PalabraClave> palabrasClave = parsearListaDePalabras(collection["palabrasClave"]);

                        DbGeography coordenada = actualizarCoordenada(collection["coordenada.Latitude"], collection["coordenada.Longitude"]);

                        List<HorarioAbierto> horariosAbierto = new List<HorarioAbierto>();
                        //         HorarioAbierto horarios = new HorarioAbierto(DayOfWeek.Monday, Convert.ToInt32(collection["abreLunes"]), Convert.ToInt32(collection["cierraLunes"]));
                        //         horariosAbierto.Add(horarios);
                        List<HorarioAbierto> horariosFeriado = new List<HorarioAbierto>();

                        List<ServicioBanco> servicios = new List<ServicioBanco>();

                        banco.actualizar(coordenada,collection["calle"], Convert.ToInt32(collection["numeroAltura"]), Convert.ToInt32(collection["piso"]),
                          Convert.ToInt32(collection["codigoPostal"]), collection["localidad"], collection["barrio"], collection["provincia"],
                          collection["pais"], collection["entreCalles"], collection["nombreDePOI"], palabrasClave, horariosAbierto, horariosFeriado, servicios);

                        db.SaveChanges();
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

        // ---------------------------------------------------------------------------------------
        //                             A B M   C G P
        //----------------------------------------------------------------------------------------

        public ActionResult ABMCGP()
        {
            if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Consultar POI").ToList().Count() > 0)
            {
                List<CGP> cgp;
                using (var db = new BuscAR())
                {
                    cgp = (from p in db.CGPs
                           orderby p.nombreDePOI
                           select p).ToList();
                }

                return View(cgp);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        public ActionResult CreateCGP()
        {
            if(TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Alta POI").ToList().Count() > 0)
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
                    DbGeography coordenada = DbGeography.FromText("POINT(" + collection["coordenada.Latitude"] + " " + collection["coordenada.Longitude"] + ")");
                    List<PalabraClave> palabrasClave = parsearListaDePalabras(collection["palabrasClave"]);

                    List<HorarioAbierto> horariosAbierto = new List<HorarioAbierto>();
                    //HorarioAbierto horarios = new HorarioAbierto(DayOfWeek.Monday, Convert.ToInt32(collection["abreLunes"]), Convert.ToInt32(collection["cierraLunes"]));
                    //horariosAbierto.Add(horarios);
                    List<HorarioAbierto> horariosFeriado = new List<HorarioAbierto>();
                    List<ServicioCGP> servicios = new List<ServicioCGP>();

                    CGP cgp = new CGP(coordenada, collection["calle"], Convert.ToInt32(collection["numeroAltura"]),
                          Convert.ToInt32(collection["piso"]), Convert.ToInt32(collection["unidad"]), Convert.ToInt32(collection["codigoPostal"]),
                          collection["localidad"], collection["barrio"], collection["provincia"], collection["pais"], collection["entreCalles"],
                          palabrasClave, collection["nombreDePOI"], "CGP", Convert.ToInt32(collection["numeroDeComuna"]),
                              servicios, Convert.ToInt32(collection["zonaDelimitadaPorLaComuna"]), horariosAbierto, horariosFeriado);

                    cgp.agregarCGP(cgp);

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

        public ActionResult DeleteCGP(int id)
        {
            if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Baja POI").ToList().Count() > 0)
            {
                CGP cgp;
                using (var db = new BuscAR())
                {
                    cgp = db.CGPs.Include("palabrasClave").Where(p => p.id == id).Single();
                }
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
                    CGP cgp;
                    using (var db = new BuscAR())
                    {
                        cgp = db.CGPs.Where(p => p.id == id).Single();
                        cgp.palabrasClave.Clear();
                        db.SaveChanges();

                    }

                    cgp.eliminarCGP(id);

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
                CGP cgp;
                using (var db = new BuscAR())
                {
                    cgp = db.CGPs.Include("palabrasClave").Where(p => p.id == id).Single();
                }
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
                    CGP cgp;
                    using (var db = new BuscAR())
                    {
                        int id = Convert.ToInt16(collection["id"]);
                        cgp = db.CGPs.Where(p => p.id == id).Single();

                        cgp.palabrasClave.Clear();
                        db.SaveChanges();

                        DbGeography coordenada = actualizarCoordenada(collection["coordenada.Latitude"], collection["coordenada.Longitude"]);
                        List<PalabraClave> palabrasClave = parsearListaDePalabras(collection["palabrasClave"]);

                        List<HorarioAbierto> horariosAbierto = new List<HorarioAbierto>();
                        //         HorarioAbierto horarios = new HorarioAbierto(DayOfWeek.Monday, Convert.ToInt32(collection["abreLunes"]), Convert.ToInt32(collection["cierraLunes"]));
                        //         horariosAbierto.Add(horarios);
                        List<HorarioAbierto> horariosFeriado = new List<HorarioAbierto>();

                        List<ServicioCGP> servicios = new List<ServicioCGP>();

                        cgp.actualizar(coordenada,collection["calle"], Convert.ToInt32(collection["numeroAltura"]),
                          Convert.ToInt32(collection["piso"]), Convert.ToInt32(collection["unidad"]), Convert.ToInt32(collection["codigoPostal"]),
                          collection["localidad"], collection["barrio"], collection["provincia"], collection["pais"], collection["entreCalles"],
                          palabrasClave, collection["nombreDePOI"], Convert.ToInt32(collection["numeroDeComuna"]),
                              servicios, Convert.ToInt32(collection["zonaDelimitadaPorLaComuna"]), horariosAbierto, horariosFeriado);


                        db.SaveChanges();
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

        // ---------------------------------------------------------------------------------------
        //                             A B M   L O C A L   C O M E R C I A L
        //----------------------------------------------------------------------------------------


        public ActionResult ABMLocalComercial()
        {
            if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Consultar POI").ToList().Count() > 0)
            {
                List<LocalComercial> localComercial;
                using (var db = new BuscAR())
                {
                    localComercial = (from p in db.Locales
                           orderby p.nombreDePOI
                           select p).ToList();
                }

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
                    DbGeography coordenada = DbGeography.FromText("POINT(" + collection["coordenada.Latitude"] + " " + collection["coordenada.Longitude"] + ")");
                List<PalabraClave> palabrasClave = parsearListaDePalabras(collection["palabrasClave"]);

                List<HorarioAbierto> horariosAbierto = new List<HorarioAbierto>();
                //         HorarioAbierto horarios = new HorarioAbierto(DayOfWeek.Monday, Convert.ToInt32(collection["abreLunes"]), Convert.ToInt32(collection["cierraLunes"]));
                //         horariosAbierto.Add(horarios);
                List<HorarioAbierto> horariosFeriado = new List<HorarioAbierto>();

                Rubro rubro = new Rubro();

                LocalComercial localComercial = new LocalComercial(coordenada, collection["calle"], Convert.ToInt32(collection["numeroAltura"]),
                      Convert.ToInt32(collection["piso"]), Convert.ToInt32(collection["unidad"]), Convert.ToInt32(collection["codigoPostal"]), 
                      collection["localidad"],collection["barrio"], collection["provincia"], collection["pais"], collection["entreCalles"],
                      palabrasClave, collection["nombreDePOI"], horariosAbierto, horariosFeriado,rubro);

                localComercial.agregarLocComercial(localComercial);

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

        public ActionResult DeleteLocalComercial(int id)
        {
            if (TPDDSGrupo44.ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Baja POI").ToList().Count() > 0)
            {
                LocalComercial localComercial;
                using (var db = new BuscAR())
                {
                    localComercial = db.Locales.Include("palabrasClave").Where(p => p.id == id).Single();
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
                    LocalComercial localComercial;
                using (var db = new BuscAR())
                {
                    localComercial = db.Locales.Where(p => p.id == id).Single();
                    localComercial.palabrasClave.Clear();
                }

                localComercial.eliminarLocComercial(id);

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
                LocalComercial localComercial;
            using (var db = new BuscAR())
            {
                localComercial = db.Locales.Include("palabrasClave").Where(p => p.id == id).Single();
            }
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
                    LocalComercial localComercial;
                using (var db = new BuscAR())
                {
                    int id = Convert.ToInt16(collection["id"]);
                    localComercial = db.Locales.Where(p => p.id == id).Single();

                    localComercial.palabrasClave.Clear();
                    db.SaveChanges();
                    List<PalabraClave> palabrasClave = parsearListaDePalabras(collection["palabrasClave"]);
                    DbGeography coordenada = actualizarCoordenada(collection["coordenada.Latitude"], collection["coordenada.Longitude"]);


                    List<HorarioAbierto> horariosAbierto = new List<HorarioAbierto>();
                    //         HorarioAbierto horarios = new HorarioAbierto(DayOfWeek.Monday, Convert.ToInt32(collection["abreLunes"]), Convert.ToInt32(collection["cierraLunes"]));
                    //         horariosAbierto.Add(horarios);
                    List<HorarioAbierto> horariosFeriado = new List<HorarioAbierto>();

                    Rubro rubro = new Rubro();

                    localComercial.actualizar(coordenada,collection["calle"], Convert.ToInt32(collection["numeroAltura"]), Convert.ToInt32(collection["piso"]),
                      Convert.ToInt32(collection["unidad"]),Convert.ToInt32(collection["codigoPostal"]), collection["localidad"], collection["barrio"], 
                      collection["provincia"], collection["pais"], collection["entreCalles"], collection["nombreDePOI"], 
                      palabrasClave, horariosAbierto, horariosFeriado, rubro);

                    db.SaveChanges();
                }

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



















        private List<PalabraClave> parsearListaDePalabras(string listaSeparadaConComas)
        {
            if (listaSeparadaConComas != "")
            {
                List<string> palabrasClaveFront = listaSeparadaConComas.Split(new char[] { ',' }).ToList();
                List<PalabraClave> palabrasClave = new List<PalabraClave>();
                foreach (string p in palabrasClaveFront)
                {
                    palabrasClave.Add(new PalabraClave(p));
                }
                return palabrasClave;
            }
            else {
                List<PalabraClave> palabrasClave = new List<PalabraClave>();
                return palabrasClave;
            }
        }

        private DbGeography actualizarCoordenada(string coorLat, string coorLong)
        {
            string coordenadaLatitude = coorLat.Replace(",", ".");
            string coordenadaLongitude = coorLong.Replace(",", ".");
            DbGeography coordenada = DbGeography.FromText("POINT(" + coordenadaLatitude + " " + coordenadaLongitude + ")");
            return coordenada;
        }

    }

    
}