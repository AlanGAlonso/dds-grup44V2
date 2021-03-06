﻿using System.Collections.Generic;
using System.Linq;
using TPDDSGrupo44.DataModels;
using TPDDSGrupo44.ViewModels;

namespace TPDDSGrupo44.Models
{
    public class ActualizacionLocalComercial : ActualizacionAsincronica
    {

        public string archivo { get; set; }

        public ActualizacionLocalComercial() : base () {
        }

        public override void actualizar(string archivo) {
            using (var db = new BuscAR())
            {
                LogAction log = new LogAction("Actualizar Local Comercial Asinc", BaseViewModel.usuario.nombre);

                try { 

                    System.IO.StreamReader reader = System.IO.File.OpenText(archivo);
                    string line;
                    //traigo linea por linea, leyendo y parseando
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] items = line.Split(';');
                        string nombre = items[0];

                        List<string> palabrasClaveFront = items[1].Split(' ').ToList();
                        List<PalabraClave> palabrasClave = new List<PalabraClave>();
                        foreach (string p in palabrasClaveFront)
                        {
                            palabrasClave.Add(new PalabraClave(p));
                        }
                        LocalComercial local;
                            // si el local ya existe, lo actualizo
                            if (db.Locales.Where(l=> l.nombreDePOI == nombre).Count() > 0)
                            {
                            db.Locales.Where(l => l.nombreDePOI == nombre).Single().palabrasClave = palabrasClave;
                            } else
                            {
                                //si el local no existe, lo agrego
                                local = new LocalComercial(nombre, palabrasClave);
                                db.Locales.Add(local);
                            }

                        db.SaveChanges();
                    }

                    log.finalizarProceso("Exito");
                    db.LogProcesosAsincronicos.Add(log);
                    }
                catch
                {
                    log.finalizarProceso("Error", "Hubo un problema inesperado en la ejecución del proceso, y el mismo no se pudo completar.");
                    db.LogProcesosAsincronicos.Add(log);
                }
                db.SaveChanges();

            }
        }
    }
}