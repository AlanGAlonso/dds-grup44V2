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
                DispositivoTactil dispositivoTactil = BaseViewModel.terminal;
                
                List<string> palabrasClave = palabraBusqueda.Split(new char[] { ',' }).ToList();
                foreach (string p in palabrasClave)
                {
                    if (p != "") { 
                    modeloVista = buscarParada(modeloVista, p, dispositivoTactil, db);
                    modeloVista = buscarLocal(modeloVista, p, dispositivoTactil, db);
                    modeloVista = buscarBanco(modeloVista, p, dispositivoTactil, db);
                    modeloVista = buscarCGP(modeloVista, p, dispositivoTactil, db);
                    }
                }

                contador.Stop();

                modeloVista.resultados = modeloVista.bancosEncontrados.Count() + modeloVista.bancosEncontradosCerca.Count() + modeloVista.cgpsEncontrados.Count() + modeloVista.localesEncontrados.Count() + modeloVista.localesEncontradosCerca.Count() + modeloVista.paradasEncontradas.Count() + modeloVista.paradasEncontradasCerca.Count();

                if (dispositivoTactil.funcionalidades.Where(f => f.nombre == "Loggear Búsquedas").ToList().Count() != 0) {
                    Busqueda busqueda = new Busqueda(palabraBusqueda, modeloVista.resultados, DateTime.Today, dispositivoTactil, contador.Elapsed);
                    db.Busquedas.Add(busqueda);
                    db.SaveChanges();
                }


                if (contador.Elapsed.Seconds > BaseViewModel.configuracion.duracionMaximaBusquedas)
                {
                    EnvioDeMails mailer = new EnvioDeMails();
                    mailer.enviarMail(contador.Elapsed, 0);
                }

                return modeloVista;
            }
        }


        private static SearchViewModel buscarParada(SearchViewModel modeloVista, string palabraBusqueda, 
            DispositivoTactil dispositivoTactil, BuscAR db) {

            List<FuncionalidadDispositivoTactil> funcionalidadesTerminal = dispositivoTactil.funcionalidades.ToList();
            if (funcionalidadesTerminal.Any(f => f.nombre == "Parada"))
            {
                //Si la persona ingresó un número, asumo que busca una parada de bondi

                List<ParadaDeColectivo> resultadosBusqueda = db.Paradas.Include("palabrasClave").Include("horarioAbierto").Include("horarioFeriado").Where(b => b.palabrasClave.Where(p => p.palabraClave.ToLower().Contains(palabraBusqueda.ToLower())).Count() != 0).ToList();
                foreach (ParadaDeColectivo punto in resultadosBusqueda)
                {
                    if (modeloVista.paradasEncontradas.Find(p => p == punto) == null &&
                        modeloVista.paradasEncontradasCerca.Find(p => p == punto) == null) { 
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

            }
            return modeloVista;
        }

        private static SearchViewModel buscarLocal(SearchViewModel modeloVista, string palabraBusqueda, 
            DispositivoTactil dispositivoTactil, BuscAR db)
        {
            List<FuncionalidadDispositivoTactil> funcionalidadesTerminal = dispositivoTactil.funcionalidades.ToList();
            if (funcionalidadesTerminal.Any(f => f.nombre == "Locales"))
            {
                //Si la persona ingresó una palabra, me fijo si es un rubro
                if (db.Rubros.Where(b => b.nombre.Contains(palabraBusqueda.ToLower())).ToList().Count() > 0)
                {

                    List<LocalComercial> resultadosBusqueda = db.Locales.Include("horarioAbierto").Include("horarioFeriado").Where(b => b.rubro.nombre.ToLower().Contains(palabraBusqueda.ToLower())).ToList();
                    foreach (LocalComercial punto in resultadosBusqueda)
                    {
                        if (modeloVista.localesEncontrados.Find(p => p == punto) == null &&
                        modeloVista.localesEncontradosCerca.Find(p => p == punto) == null)
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

                    // Si la palabra ingresada no era parada ni rubro, la busco como local
                }

                List<LocalComercial> resultadosBusquedaLocales = db.Locales.Include("horarioAbierto").Include("horarioFeriado").Include("rubro").Include("palabrasClave").Where(b => b.palabrasClave.Where(p => p.palabraClave.ToLower().Contains(palabraBusqueda.ToLower())).Count() != 0).ToList();
                if (resultadosBusquedaLocales.Count() > 0)
                {
                    foreach (LocalComercial punto in resultadosBusquedaLocales)
                    {
                        if (modeloVista.localesEncontrados.Find(p => p == punto) == null &&
                           modeloVista.localesEncontradosCerca.Find(p => p == punto) == null)
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
                }
            }
            return modeloVista;
        }

        private static SearchViewModel buscarBanco(SearchViewModel modeloVista, string palabraBusqueda, 
            DispositivoTactil dispositivoTactil, BuscAR db)
        {
            List<FuncionalidadDispositivoTactil> funcionalidadesTerminal = dispositivoTactil.funcionalidades.ToList();
            if (funcionalidadesTerminal.Any(f => f.nombre == "Banco"))
            {
                List<Banco> resultadosBusquedaBancos = db.Bancos
                    .Include("horarioAbierto").Include("horarioFeriado").Include("servicios").Include("servicios.horarioAbierto").Include("servicios.horarioFeriados").Include("palabrasClave")
                    .Where(b => b.palabrasClave.Where(p => p.palabraClave.ToLower().Contains(palabraBusqueda.ToLower())).Count() != 0).ToList();


                List<Banco> resultadoBusquedaJSONBancos = GetJsonBanks.getJsonData().FindAll(b => b.palabrasClave.Where(p => p.palabraClave.ToLower().Contains(palabraBusqueda.ToLower())).ToList().Count() > 0);
                resultadosBusquedaBancos.AddRange(resultadoBusquedaJSONBancos);

                if (resultadosBusquedaBancos.Count() > 0)
                {
                    foreach (Banco punto in resultadosBusquedaBancos)
                    {
                        if (modeloVista.bancosEncontrados.Find(p => p == punto) == null &&
                        modeloVista.bancosEncontradosCerca.Find(p => p == punto) == null)
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
                }
            }
            return modeloVista;
        }

        private static SearchViewModel buscarCGP(SearchViewModel modeloVista, string palabraBusqueda, 
            DispositivoTactil dispositivoTactil, BuscAR db)
        {
            List<FuncionalidadDispositivoTactil> funcionalidadesTerminal = dispositivoTactil.funcionalidades.ToList();
            if (funcionalidadesTerminal.Any(f => f.nombre == "CGP"))
            {
                List<CGP> resultadosBusquedaCGP = db.CGPs.Include("horarioAbierto").Include("horarioFeriado").Include("servicios").Include("servicios.horarioAbierto").Include("servicios.horarioFeriados").Include("palabrasClave").Where(b => b.palabrasClave.Where(p => p.palabraClave.ToLower().Contains(palabraBusqueda.ToLower())).Count() != 0).ToList();

                List<CGP> resultadoBusquedaJSONCGP = GetJsonCGP.getJsonData().FindAll(c => c.palabrasClave.Where(p => p.palabraClave.ToLower().Contains(palabraBusqueda.ToLower())).ToList().Count() > 0);
                resultadosBusquedaCGP.AddRange(resultadoBusquedaJSONCGP);

                if (resultadosBusquedaCGP.Count() > 0)
                {
                    foreach (CGP punto in resultadosBusquedaCGP)
                    {
                        if (modeloVista.cgpsEncontrados.Find(p => p == punto) == null &&
                        modeloVista.cgpsEncontradosCerca.Find(p => p == punto) == null)
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
                }
            }
            return modeloVista;
        }


    }

    
}