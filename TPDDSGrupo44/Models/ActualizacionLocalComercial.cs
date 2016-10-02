using System.Collections.Generic;
using System.Linq;

namespace TPDDSGrupo44.Models
{
    public class ActualizacionLocalComercial : ActualizacionAsincronica
    {

        public ActualizacionLocalComercial() { }

        public override void actualizar() {


            //abrir archivo. tendría que recibirlo por parámetro... corregir!
            System.IO.StreamReader reader = System.IO.File.OpenText("filename.txt");
            string line;
            //traigo linea por linea, leyendo y parseando
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(';');
                string nombre = items[0];

                List<string> palabrasClave = items[1].Split(' ').ToList();

                using (var db = new BuscAR())
                {
                    LocalComercial local = (from l in db.Locales
                                            where l.nombreDePOI == nombre
                                            select l).Single();
                    // si el local ya existe, lo actualizo
                    if (local != null)
                    {
                        local.palabrasClave = palabrasClave;
                        db.SaveChanges();
                    } else
                    {
                        //si el local no existe, lo agrego
                        local = new LocalComercial(nombre, palabrasClave);
                        db.Locales.Add(local);
                        db.SaveChanges();
                    }

                }
                
            }
        }
    }
}