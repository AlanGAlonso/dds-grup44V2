using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using TPDDSGrupo44.Models;
using System.Data.Entity.Spatial;
using TPDDSGrupo44.ViewModels;

namespace TPDDSGrupo44.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection search)
        {

            ViewBag.Message = "Buscá puntos de interés, descubrí cuáles están cerca.";


            string palabraBusqueda = search["palabraClave"];

            try
            {
                using (var db = new BuscAR())
                {

                    SearchViewModel modeloVista = new SearchViewModel();

                    //Defino ubicación actual (UTN/CAMPUS)
                    DispositivoTactil dispositivoTactil = db.Terminales.Where(i => i.nombre == "UTN FRBA Lugano").Single();

                    //Si la persona ingresó un número, asumo que busca una parada de bondi
                    int linea = 0;
                    if (int.TryParse(palabraBusqueda, out linea) && linea > 0)
                    {

                        List<ParadaDeColectivo> resultadosBusqueda = db.Paradas.Where(b => b.nombreDePOI == palabraBusqueda).ToList();
                        foreach (ParadaDeColectivo punto in resultadosBusqueda)
                        {

                            if (punto.estaCerca(dispositivoTactil.coordenada))
                            {
                                modeloVista.paradasEncontradasCerca.Add(punto);
                                ViewBag.Latitud = punto.coordenada.Latitude.ToString();
                                ViewBag.Longitud = punto.coordenada.Longitude.ToString();
                                ViewBag.TextoLugar = "Parada del " + punto.nombreDePOI;

                                ViewBag.Search = "ok";
                            }
                            else
                            {
                                modeloVista.paradasEncontradas.Add(punto);
                                ViewBag.Search = "ok";
                            }

                        }

                    }

                    //Si la persona ingresó una palabra, me fijo si es un rubro
                    if (db.Rubros.Where(b => b.nombre.Contains(palabraBusqueda.ToLower())).ToList().Count() > 0)
                    {

                        List<LocalComercial> resultadosBusqueda = db.Locales.Include("horarioAbierto").Include("horarioFeriado").Where(b => b.rubro.nombre.ToLower().Contains(palabraBusqueda.ToLower())).ToList();
                        foreach (LocalComercial punto in resultadosBusqueda)
                        {
                            if (punto.estaCerca(dispositivoTactil.coordenada))
                            {
                                modeloVista.localesEncontradosCerca.Add(punto);
                                ViewBag.Latitud = punto.coordenada.Latitude.ToString();
                                ViewBag.Longitud = punto.coordenada.Longitude.ToString();
                                ViewBag.Search = "ok";
                            }
                            else
                            {
                                modeloVista.localesEncontrados.Add(punto);
                                ViewBag.Search = "ok";
                            }
                        }

                        // Si la palabra ingresada no era parada ni rubro, la busco como local
                    }

                    List<LocalComercial> resultadosBusquedaLocales = db.Locales.Include("horarioAbierto").Include("horarioFeriado").Include("rubro").Where(b => b.nombreDePOI.ToLower().Contains(palabraBusqueda.ToLower())).ToList();
                    if (resultadosBusquedaLocales.Count() > 0)
                    {
                        foreach (LocalComercial punto in resultadosBusquedaLocales)
                        {
                            if (punto.estaCerca(dispositivoTactil.coordenada))
                            {
                                modeloVista.localesEncontradosCerca.Add(punto);
                                ViewBag.Latitud = punto.coordenada.Latitude.ToString();
                                ViewBag.Longitud = punto.coordenada.Longitude.ToString();
                                ViewBag.TextoLugar = punto.nombreDePOI;
                                ViewBag.Search = "ok";
                            }
                            else
                            {
                                modeloVista.localesEncontrados.Add(punto);
                                ViewBag.Search = "ok";
                            }
                        }
                    }

                    List<Banco> resultadosBusquedaBancos = db.Bancos.Include("horarioAbierto").Include("horarioFeriado").Include("servicios").Include("servicios.horarioAbierto").Include("servicios.horarioFeriados").Where(b => b.nombreDePOI.ToLower().Contains(palabraBusqueda.ToLower())).ToList();

                    GetJsonBanks buscadorDeBancosJSON = new GetJsonBanks();
                    List<Banco> resultadoBusquedaJSONBancos = buscadorDeBancosJSON.getJsonData().FindAll(b => b.nombreDePOI.ToLower().Contains(palabraBusqueda.ToLower()));
                    resultadosBusquedaBancos.AddRange(resultadoBusquedaJSONBancos);

                    if (resultadosBusquedaBancos.Count() > 0)
                    {
                        foreach (Banco punto in resultadosBusquedaBancos)
                        {
                            if (punto.estaCerca(dispositivoTactil.coordenada))
                            {
                                modeloVista.bancosEncontradosCerca.Add(punto);
                                ViewBag.Latitud = punto.coordenada.Latitude.ToString();
                                ViewBag.Longitud = punto.coordenada.Longitude.ToString();
                                ViewBag.TextoLugar = punto.nombreDePOI;
                                ViewBag.Search = "ok";
                            }
                            else
                            {
                                modeloVista.bancosEncontrados.Add(punto);
                                ViewBag.Search = "ok";
                            }
                        }
                    }



                    List<CGP> resultadosBusquedaCGP = db.CGPs.Include("horarioAbierto").Include("horarioFeriado").Include("servicios").Include("servicios.horarioAbierto").Include("servicios.horarioFeriados").Where(b => b.nombreDePOI.ToLower().Contains(palabraBusqueda.ToLower())).ToList();

                 /*   GetJsonCGP buscadorDeCGPJSON = new GetJsonCGP();
                    List<Banco> resultadoBusquedaJSONCGP = buscadorDeCGPJSON.getJsonData().FindAll(b => b.nombreDePOI.ToLower().Contains(palabraBusqueda.ToLower()));
                    resultadoBusquedaJSONCGP.AddRange(resultadoBusquedaJSONCGP);*/


                    if (resultadosBusquedaCGP.Count() > 0)
                    {
                        foreach (CGP punto in resultadosBusquedaCGP)
                        {
                            if (punto.estaCerca(dispositivoTactil.coordenada))
                            {
                                modeloVista.cgpsEncontradosCerca.Add(punto);
                                ViewBag.Latitud = punto.coordenada.Latitude.ToString();
                                ViewBag.Longitud = punto.coordenada.Longitude.ToString();
                                ViewBag.TextoLugar = punto.nombreDePOI;
                                ViewBag.Search = "ok";
                            }
                            else
                            {
                                modeloVista.cgpsEncontrados.Add(punto);
                                ViewBag.Search = "ok";
                            }
                        }
                    }

                    int resultados = modeloVista.bancosEncontrados.Count() + modeloVista.bancosEncontradosCerca.Count() + modeloVista.cgpsEncontrados.Count() + modeloVista.localesEncontrados.Count() + modeloVista.localesEncontradosCerca.Count() + modeloVista.paradasEncontradas.Count() + modeloVista.paradasEncontradasCerca.Count();
                    if (resultados == 0)
                    {
                        ViewBag.Search = "error";
                        ViewBag.SearchText = "Disculpa, pero no encontramos ningún punto con esa palabra clave.";

                    }
                    else
                    {
                        Busqueda busqueda = new Busqueda(palabraBusqueda, resultados, DateTime.Today, dispositivoTactil);
                        db.Busquedas.Add(busqueda);
                        db.SaveChanges();
                    }

                    return View(modeloVista);
                }

            }
            catch
            {
                return View();
            }
        }
    }
}
