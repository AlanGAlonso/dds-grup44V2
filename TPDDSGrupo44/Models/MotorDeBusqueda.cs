using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TPDDSGrupo44.DataModels;
using TPDDSGrupo44.Helpers;
using TPDDSGrupo44.ViewModels;

namespace TPDDSGrupo44.Models
{
    public class MotorDeBusqueda
    {

        public MotorDeBusqueda() { }

        public static SearchViewModel buscar(string palabraBusqueda)
        {
            Stopwatch contador = new Stopwatch();
            contador.Start();
            using (var db = new BuscAR())
            {

                SearchViewModel modeloVista = new SearchViewModel();

                //Defino ubicación actual (UTN/CAMPUS)
                DispositivoTactil dispositivoTactil = db.Terminales.Where(i => i.nombre == "UTN FRBA Lugano").Single();

                //Si la persona ingresó un número, asumo que busca una parada de bondi
                int linea = 0;
                if (int.TryParse(palabraBusqueda, out linea) && linea > 0)
                {

                    List<ParadaDeColectivo> resultadosBusqueda = db.Paradas.Include("palabrasClave").Where(b => b.palabrasClave.Where(p => p.palabraClave.ToLower().Contains(palabraBusqueda.ToLower())).Count() != 0).ToList();
                    foreach (ParadaDeColectivo punto in resultadosBusqueda)
                    {

                        if (punto.estaCerca(dispositivoTactil.coordenada))
                        {
                            modeloVista.paradasEncontradasCerca.Add(punto);
                        }
                        else
                        {
                            modeloVista.paradasEncontradas.Add(punto);
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
                        }
                        else
                        {
                            modeloVista.localesEncontrados.Add(punto);
                        }
                    }

                    // Si la palabra ingresada no era parada ni rubro, la busco como local
                }

                List<LocalComercial> resultadosBusquedaLocales = db.Locales.Include("horarioAbierto").Include("horarioFeriado").Include("rubro").Include("palabrasClave").Where(b => b.palabrasClave.Where(p => p.palabraClave.ToLower().Contains(palabraBusqueda.ToLower())).Count() != 0).ToList();
                if (resultadosBusquedaLocales.Count() > 0)
                {
                    foreach (LocalComercial punto in resultadosBusquedaLocales)
                    {
                        if (punto.estaCerca(dispositivoTactil.coordenada))
                        {
                            modeloVista.localesEncontradosCerca.Add(punto);
                        }
                        else
                        {
                            modeloVista.localesEncontrados.Add(punto);
                        }
                    }
                }

                List<Banco> resultadosBusquedaBancos = db.Bancos.Include("horarioAbierto").Include("horarioFeriado").Include("servicios").Include("servicios.horarioAbierto").Include("servicios.horarioFeriados").Include("palabrasClave").Where(b => b.palabrasClave.Where(p => p.palabraClave.ToLower().Contains(palabraBusqueda.ToLower())).Count() != 0).ToList();
                
                List<Banco> resultadoBusquedaJSONBancos = GetJsonBanks.getJsonData().FindAll(b => b.palabrasClave.Where(p => p.palabraClave.ToLower().Contains(palabraBusqueda.ToLower())).ToList().Count() >0);
                resultadosBusquedaBancos.AddRange(resultadoBusquedaJSONBancos);

                if (resultadosBusquedaBancos.Count() > 0)
                {
                    foreach (Banco punto in resultadosBusquedaBancos)
                    {
                        if (punto.estaCerca(dispositivoTactil.coordenada))
                        {
                            modeloVista.bancosEncontradosCerca.Add(punto);
                        }
                        else
                        {
                            modeloVista.bancosEncontrados.Add(punto);
                        }
                    }
                }



                List<CGP> resultadosBusquedaCGP = db.CGPs.Include("horarioAbierto").Include("horarioFeriado").Include("servicios").Include("servicios.horarioAbierto").Include("servicios.horarioFeriados").Include("palabrasClave").Where(b => b.palabrasClave.Where(p => p.palabraClave.ToLower().Contains(palabraBusqueda.ToLower())).Count() != 0).ToList();

                List<CGP> resultadoBusquedaJSONCGP = GetJsonCGP.getJsonData().FindAll(c => c.palabrasClave.Where(p => p.palabraClave.ToLower().Contains(palabraBusqueda.ToLower())).ToList().Count() > 0);
                resultadosBusquedaCGP.AddRange(resultadoBusquedaJSONCGP);

                if (resultadosBusquedaCGP.Count() > 0)
                {
                    foreach (CGP punto in resultadosBusquedaCGP)
                    {
                        if (punto.estaCerca(dispositivoTactil.coordenada))
                        {
                            modeloVista.cgpsEncontradosCerca.Add(punto);
                        }
                        else
                        {
                            modeloVista.cgpsEncontrados.Add(punto);
                        }
                    }
                }

                contador.Stop();

                modeloVista.resultados = modeloVista.bancosEncontrados.Count() + modeloVista.bancosEncontradosCerca.Count() + modeloVista.cgpsEncontrados.Count() + modeloVista.localesEncontrados.Count() + modeloVista.localesEncontradosCerca.Count() + modeloVista.paradasEncontradas.Count() + modeloVista.paradasEncontradasCerca.Count();
                
                Busqueda busqueda = new Busqueda(palabraBusqueda, modeloVista.resultados, DateTime.Today, dispositivoTactil, contador.Elapsed);
                db.Busquedas.Add(busqueda);
                db.SaveChanges();


                if (contador.Elapsed.Seconds > BaseViewModel.configuracion.duracionMaximaBusquedas)
                {
                    EnvioDeMails mailer = new EnvioDeMails();
                    mailer.enviarMail(contador.Elapsed, 0);
                }

                return modeloVista;
            }
        }

    }
}