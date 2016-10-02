using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using TPDDSGrupo44.Models;
using System.Data.Entity.Spatial;

namespace TPDDSGrupo44.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View(recuperarBusquedas());
        }

        public ActionResult SearchPerDay()
        {

            return View(recuperarBusquedas());
        }


        public ActionResult ResultsPerSearch()
        {
            return View(recuperarBusquedas());
        }


        private List<Busqueda> recuperarBusquedas()
        {
            List<Busqueda> busquedas;
            using (var db = new BuscAR())
            {
                busquedas = (from b in db.Busquedas
                             orderby b.Id
                             select b).ToList();
            }

            return busquedas;
        }

 // ---------------------------------------------------------------------------------------
 //                             A B M   P A R A D A
 //----------------------------------------------------------------------------------------

        public ActionResult ABMParada()
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


        public ActionResult CreateParada()
        {
            return View();
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult CreateParada(FormCollection collection)
        {
            try
            {

                DbGeography coordenada = DbGeography.FromText("POINT(" + collection["coordenada.Latitude"] + " " + collection["coordenada.Longitude"] + ")");

                //ParadaDeColectivo parada = new ParadaDeColectivo(coordenada, collection["calle"], Convert.ToInt32(collection["numeroAltura"]),
                //    Convert.ToInt32(collection["piso"]), Convert.ToInt32(collection["unidad"]), Convert.ToInt32(collection["codigoPostal"]),
                //    collection["localidad"], collection["barrio"], collection["provincia"], collection["pais"], collection["entreCalles"],
                //    collection["palabrasClave"],collection["nombreDePOI"], collection["tipoDePOI"]);

                //parada.agregarParada(parada);

                return RedirectToAction("ABMParada");
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult DeleteParada(int id)
        {
            ParadaDeColectivo parada;
            using (var db = new BuscAR())
            {
                parada = db.Paradas.Where(p => p.id == id).Single();
            }
                return View(parada);
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult DeleteParada(int id, FormCollection collection)
        {
            try
            {
                ParadaDeColectivo parada;
                using (var db = new BuscAR())
                {
                    parada = db.Paradas.Where(p => p.id == id).Single();
                }

                parada.eliminarParada(id);

                return RedirectToAction("ABMParada");
            }
            catch
            {
                return View();
            }
        }



        public ActionResult EditParada(int id)
        {
            ParadaDeColectivo parada;
            using (var db = new BuscAR())
            {
                parada = db.Paradas.Where(p => p.id == id).Single();
            }
            return View(parada);
        }

        // POST: Default/Edit
        [HttpPost]
        public ActionResult EditParada(FormCollection collection)
        {
            try
            {
                ParadaDeColectivo parada;
                using (var db = new BuscAR())
                {
                    int id = Convert.ToInt16(collection["id"]);
                    parada = db.Paradas.Where(p => p.id == id).Single();

                    parada.actualizar(collection["nombreDePOI"]);

                    db.SaveChanges();
                }

                
              
                return RedirectToAction("ABMParada");
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
            List<Banco> bancos;
            using (var db = new BuscAR())
            {
                bancos = (from p in db.Bancos
                           orderby p.nombreDePOI
                           select p).ToList();
            }

            return View(bancos);
        }


        public ActionResult CreateBanco()
        {
            return View();
        }

        // POST: Default/Create
          [HttpPost]
          public ActionResult CreateBanco(FormCollection collection)
          {
              try
              {


                DbGeography coordenada = DbGeography.FromText("POINT(" + collection["coordenada.Latitude"] + " " + collection["coordenada.Longitude"] + ")");

                List<string> palabrasClave = collection["palabrasClave"].Split(new char[] { ',' }).ToList();

                List<HorarioAbierto> horariosAbierto = new List<HorarioAbierto>();


                HorarioAbierto horarios = new HorarioAbierto(DayOfWeek.Monday, Convert.ToInt32(collection["abreLunes"]), Convert.ToInt32(collection["cierraLunes"]));


                horariosAbierto.Add(horarios);

                List<HorarioAbierto> horariosFeriado = new List<HorarioAbierto>();

                List<ServicioBanco> servicios = new List<ServicioBanco>();

                /* convert list to string -- 
                 * var result = string.Join(",", list.ToArray()); */

                Banco banco = new Banco(coordenada, collection["calle"], Convert.ToInt32(collection["numeroAltura"]),
                      Convert.ToInt32(collection["piso"]), Convert.ToInt32(collection["unidad"]), Convert.ToInt32(collection["codigoPostal"]),
                      collection["localidad"], collection["barrio"], collection["provincia"], collection["pais"], collection["entreCalles"],
                      collection["palabraClave"], collection["tipoDePOI"], horariosAbierto, horariosFeriado, servicios);

                banco.agregarBanco(banco);

                  return RedirectToAction("ABMBanco");
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
            List<CGP> cgp;
            using (var db = new BuscAR())
            {
                cgp = (from p in db.CGPs
                          orderby p.nombreDePOI
                          select p).ToList();
            }

            return View(cgp);
        }


        public ActionResult CreateCGP()
        {
            return View();
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult CreateCGP(FormCollection collection)
        {
            try
            {
                
                DbGeography coordenada = DbGeography.FromText("POINT(" + collection["coordenada.Latitude"] + " " + collection["coordenada.Longitude"] + ")");
       
                List<string> palabrasClave = collection["palabrasClave"].Split(new char[] { ',' }).ToList();

                List<HorarioAbierto> horariosAbierto = new List<HorarioAbierto>();
                

                HorarioAbierto horarios = new HorarioAbierto(DayOfWeek.Monday, Convert.ToInt32(collection["abreLunes"]), Convert.ToInt32(collection["cierraLunes"]));
                

                horariosAbierto.Add(horarios);

                List<HorarioAbierto> horariosFeriado = new List<HorarioAbierto>();

                List<ServicioCGP> servicios = new List<ServicioCGP>();



                /* convert list to string -- 
                 * var result = string.Join(",", list.ToArray()); */

                CGP cgp = new CGP(coordenada, collection["calle"], Convert.ToInt32(collection["numeroAltura"]),
                      Convert.ToInt32(collection["piso"]), Convert.ToInt32(collection["unidad"]), Convert.ToInt32(collection["codigoPostal"]),
                      collection["localidad"], collection["barrio"], collection["provincia"], collection["pais"], collection["entreCalles"],
                      palabrasClave, collection["nombreDePOI"], collection["tipoDePOI"], Convert.ToInt32(collection["numeroDeComuna"]),
                          servicios, Convert.ToInt32(collection["zonaDelimitadaPorLaComuna"]),horariosAbierto, horariosFeriado);

                //collection["horarioAbierto"], collection["horarioFeriado"], collection["servicios"], collection["zonaDelimitadaPorLaComuna"]
                cgp.agregarCGP(cgp);

                return RedirectToAction("ABMCGP");
            }
            catch
            {
                return View();
            }
        }




        // ---------------------------------------------------------------------------------------
        //                             A B M   L O C A L 
        //----------------------------------------------------------------------------------------
        public ActionResult CreateLocal()
        {
            return View();
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult CreateLocal(FormCollection collection)
        {
            try
            {

                DbGeography coordenada = DbGeography.FromText("POINT(" + collection["coordenada.Latitude"] + " " + collection["coordenada.Longitude"] + ")");
                List<string> palabrasClave = collection["palabrasClave"].Split(new char[] { ',' }).ToList();

                List<HorarioAbierto> horariosAbierto = new List<HorarioAbierto>();
                horariosAbierto.Add(new HorarioAbierto(DayOfWeek.Monday, Convert.ToInt32(collection["abreLunes"]), Convert.ToInt32(collection["cierraLunes"])));

                List<HorarioAbierto> horariosFeriado = new List<HorarioAbierto>();

                Rubro rubro = new Rubro();



                LocalComercial localC = new LocalComercial(coordenada, collection["calle"], Convert.ToInt32(collection["numeroAltura"]),
                      Convert.ToInt32(collection["piso"]), Convert.ToInt32(collection["unidad"]), Convert.ToInt32(collection["codigoPostal"]),
                      collection["localidad"], collection["barrio"], collection["provincia"], collection["pais"], collection["entreCalles"], palabrasClave,
                      collection["nombreDePOI"], collection["tipoDePOI"], horariosAbierto, horariosFeriado,rubro);
                /* convert list to string -- 
                 * var result = string.Join(",", list.ToArray()); */


                ////collection["horarioAbierto"], collection["horarioFeriado"], collection["servicios"], collection["zonaDelimitadaPorLaComuna"]
                localC.agregarLocComercial(localC);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }
}