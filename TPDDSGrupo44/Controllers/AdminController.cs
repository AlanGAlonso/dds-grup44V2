using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using TPDDSGrupo44.Models;
using System.Data.Entity.Spatial;
using TPDDSGrupo44.DataModels;

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
                return RedirectToAction("NoAccess", "Shared");
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
                return RedirectToAction("NoAccess", "Shared");
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
                return RedirectToAction("NoAccess", "Shared");
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




        public ActionResult ABMHome()
        {
            if (ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == "Alta POI" || f.nombre == "Baja POI" || f.nombre == "Editar POI" || f.nombre == "Consultar POI").ToList().Count() > 0)
            {
                return View();
            }
            else
            {
                return RedirectToAction("NoAccess", "Shared");
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
                return RedirectToAction("NoAccess", "Shared");
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
                return RedirectToAction("NoAccess", "Shared");
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
                    return RedirectToAction("NoAccess", "Shared");
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
                    parada = db.Paradas.Where(p => p.id == id).Single();
                }
                return View(parada);
            }
            else
            {
                return RedirectToAction("NoAccess", "Shared");
            }
        }

        // POST: Default/Create
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
                        parada = db.Paradas.Include("palabrasClave").Where(p => p.id == id).Single();

                        parada.palabrasClave.RemoveAll(p => p.palabraClave != "");
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
                    return RedirectToAction("NoAccess", "Shared");
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
                return RedirectToAction("NoAccess", "Shared");
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


                        parada.palabrasClave.RemoveAll(p => p.palabraClave != "");
                        db.SaveChanges();

                        List<PalabraClave> palabrasClave = parsearListaDePalabras(collection["palabrasClave"]);



                        string coordenadaLatitude = (collection["coordenada.Latitude"]).Replace(",", ".");
                        DbGeography coordenada = DbGeography.FromText("POINT(" + coordenadaLatitude + " " + coordenadaLatitude + ")");



                        parada.actualizar(coordenada, collection["calle"], Convert.ToInt32(collection["numeroAltura"]),
                            Convert.ToInt32(collection["codigoPostal"]), collection["localidad"], collection["barrio"], collection["provincia"],
                            collection["pais"], collection["entreCalles"], palabrasClave, collection["nombreDePOI"]);


                        db.SaveChanges();
                    }

                    return RedirectToAction("ABMParada");
                }
                else
                {
                    return RedirectToAction("NoAccess", "Shared");
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
                return RedirectToAction("NoAccess", "Shared");
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
                return RedirectToAction("NoAccess", "Shared");
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

                    return RedirectToAction("ABMBanco");
                }
                else
                {
                    return RedirectToAction("NoAccess", "Shared");
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
                    banco = db.Bancos.Where(p => p.id == id).Single();
                }
                return View(banco);
            }
            else
            {
                return RedirectToAction("NoAccess", "Shared");
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
                    }

                    banco.eliminarBanco(id);

                    return RedirectToAction("ABMBanco");
                }
                else
                {
                    return RedirectToAction("NoAccess", "Shared");
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
                    banco = db.Bancos.Where(p => p.id == id).Single();
                }
                return View(banco);
            }
            else
            {
                return RedirectToAction("NoAccess", "Shared");
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


                        DbGeography coordenada = DbGeography.FromText("POINT(" + collection["coordenada.Latitude"] + " " + collection["coordenada.Longitude"] + ")");

                        List<PalabraClave> palabrasClave = parsearListaDePalabras(collection["palabrasClave"]);

                        List<HorarioAbierto> horariosAbierto = new List<HorarioAbierto>();

                        //         HorarioAbierto horarios = new HorarioAbierto(DayOfWeek.Monday, Convert.ToInt32(collection["abreLunes"]), Convert.ToInt32(collection["cierraLunes"]));
                        //         horariosAbierto.Add(horarios);

                        List<HorarioAbierto> horariosFeriado = new List<HorarioAbierto>();

                        List<ServicioBanco> servicios = new List<ServicioBanco>();

                        banco.actualizar(collection["calle"], Convert.ToInt32(collection["numeroAltura"]), Convert.ToInt32(collection["piso"]),
                          Convert.ToInt32(collection["codigoPostal"]), collection["localidad"], collection["barrio"], collection["provincia"],
                          collection["pais"], collection["entreCalles"], collection["nombreDePOI"], palabrasClave, horariosAbierto, horariosFeriado, servicios);



                        db.SaveChanges();
                    }

                    return RedirectToAction("ABMBanco");
                }
                else
                {
                    return RedirectToAction("NoAccess", "Shared");
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
                return RedirectToAction("NoAccess", "Shared");
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
                return RedirectToAction("NoAccess", "Shared");
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


                    /* convert list to string -- 
                     * var result = string.Join(",", list.ToArray()); */

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
                    return RedirectToAction("NoAccess", "Shared");
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
                    cgp = db.CGPs.Where(p => p.id == id).Single();
                }
                return View(cgp);
            }
            else
            {
                return RedirectToAction("NoAccess", "Shared");
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
                    }

                    cgp.eliminarCGP(id);

                    return RedirectToAction("ABMCGP");
                }
                else
                {
                    return RedirectToAction("NoAccess", "Shared");
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
                    cgp = db.CGPs.Where(p => p.id == id).Single();
                }
                return View(cgp);
            }
            else
            {
                return RedirectToAction("NoAccess", "Shared");
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


                        DbGeography coordenada = DbGeography.FromText("POINT(" + collection["coordenada.Latitude"] + " " + collection["coordenada.Longitude"] + ")");

                        List<PalabraClave> palabrasClave = parsearListaDePalabras(collection["palabrasClave"]);

                        List<HorarioAbierto> horariosAbierto = new List<HorarioAbierto>();

                        //         HorarioAbierto horarios = new HorarioAbierto(DayOfWeek.Monday, Convert.ToInt32(collection["abreLunes"]), Convert.ToInt32(collection["cierraLunes"]));
                        //         horariosAbierto.Add(horarios);

                        List<HorarioAbierto> horariosFeriado = new List<HorarioAbierto>();

                        List<ServicioCGP> servicios = new List<ServicioCGP>();



                        cgp.actualizar(collection["calle"], Convert.ToInt32(collection["numeroAltura"]),
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
                    return RedirectToAction("NoAccess", "Shared");
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
                return RedirectToAction("NoAccess", "Shared");
            }
        }

        //public ActionResult CreateLocalComercial()
        //{
        //
        //    return View();
        //}

        //// POST: Default/Create
        //[HttpPost]
        //public ActionResult CreateLocalComercial(FormCollection collection)
        //{
        //    try
        //    {


        //        DbGeography coordenada = DbGeography.FromText("POINT(" + collection["coordenada.Latitude"] + " " + collection["coordenada.Longitude"] + ")");
        //        List<string> palabrasClaveFront = collection["palabrasClave"].Split(new char[] { ',' }).ToList();

        //    List<PalabraClave> palabrasClave = parsearListaDePalabras(collection["palabrasClave"]);

        //        List<HorarioAbierto> horariosAbierto = new List<HorarioAbierto>();

        //        //         HorarioAbierto horarios = new HorarioAbierto(DayOfWeek.Monday, Convert.ToInt32(collection["abreLunes"]), Convert.ToInt32(collection["cierraLunes"]));
        //        //         horariosAbierto.Add(horarios);

        //        List<HorarioAbierto> horariosFeriado = new List<HorarioAbierto>();

        //        List<ServicioBanco> servicios = new List<ServicioBanco>();



        //        LocalComercial localComercial = new LocalComercial(coordenada, collection["calle"], Convert.ToInt32(collection["numeroAltura"]),
        //              Convert.ToInt32(collection["piso"]), Convert.ToInt32(collection["codigoPostal"]), collection["localidad"],
        //              collection["barrio"], collection["provincia"], collection["pais"], collection["entreCalles"],
        //              palabrasClave, collection["nombreDePOI"], horariosAbierto, horariosFeriado, servicios);

        //        localComercial.agregarBanco(localComercial);

        //        return RedirectToAction("ABMBanco");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //public ActionResult DeleteLocalComercial(int id)
        //{
        //    LocalComercial localComercial;
        //    using (var db = new BuscAR())
        //    {
        //        localComercial = db.Locales.Where(p => p.id == id).Single();
        //    }
        //    return View(localComercial);
        //}

        //// POST: Default/Create
        //[HttpPost]
        //public ActionResult DeleteLocalComercial(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        LocalComercial localComercial;
        //        using (var db = new BuscAR())
        //        {
        //            localComercial = db.Locales.Where(p => p.id == id).Single();
        //        }

        //        localComercial.eliminarLocalComercial(id);

        //        return RedirectToAction("ABMLocalComercial");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}



        //public ActionResult EditLocalComercial(int id)
        //{
        //    LocalComercial localComercial;
        //    using (var db = new BuscAR())
        //    {
        //        localComercial = db.Locales.Where(p => p.id == id).Single();
        //    }
        //    return View(localComercial);
        //}

        //// POST: Default/Edit
        //[HttpPost]
        //public ActionResult EditLocalComercial(FormCollection collection)
        //{
        //    try
        //    {
        //        LocalComercial localComercial;
        //        using (var db = new BuscAR())
        //        {
        //            int id = Convert.ToInt16(collection["id"]);
        //            localComercial = db.Locales.Where(p => p.id == id).Single();


        //            DbGeography coordenada = DbGeography.FromText("POINT(" + collection["coordenada.Latitude"] + " " + collection["coordenada.Longitude"] + ")");
        //    List<PalabraClave> palabrasClave = parsearListaDePalabras(collection["palabrasClave"]);

        //            List<HorarioAbierto> horariosAbierto = new List<HorarioAbierto>();

        //            //         HorarioAbierto horarios = new HorarioAbierto(DayOfWeek.Monday, Convert.ToInt32(collection["abreLunes"]), Convert.ToInt32(collection["cierraLunes"]));
        //            //         horariosAbierto.Add(horarios);

        //            List<HorarioAbierto> horariosFeriado = new List<HorarioAbierto>();

        //            List<ServicioBanco> servicios = new List<ServicioBanco>();

        //            localComercial.actualizar(collection["calle"], Convert.ToInt32(collection["numeroAltura"]), Convert.ToInt32(collection["piso"]),
        //              Convert.ToInt32(collection["codigoPostal"]), collection["localidad"], collection["barrio"], collection["provincia"],
        //              collection["pais"], collection["entreCalles"], collection["nombreDePOI"], palabrasClave, horariosAbierto, horariosFeriado, servicios);



        //            db.SaveChanges();
        //        }

        //        return RedirectToAction("ABMLocalComercial");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}



        private List<PalabraClave> parsearListaDePalabras(string listaSeparadaConComas)
        {
            List<string> palabrasClaveFront = listaSeparadaConComas.Split(new char[] { ',' }).ToList();
            List<PalabraClave> palabrasClave = new List<PalabraClave>();
            foreach (string p in palabrasClaveFront)
            {
                palabrasClave.Add(new PalabraClave(p));
            }
            return palabrasClave;
        }

    }

    
}