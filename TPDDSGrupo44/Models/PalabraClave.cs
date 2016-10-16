using System.ComponentModel.DataAnnotations;

namespace TPDDSGrupo44.Models
{
    public class PalabraClave
    {
        ////////////////Atributos////////////////
        [Key]
        public int Id { get; set; }
        public string palabraClave { get; set; }


        ////////////////////////Constructor vacio////////////////
        public PalabraClave() { }

        ////////////////////////Constructor real////////////////
        public PalabraClave(string palabra) {
            palabraClave = palabra;
        }

        //public void eliminarPalabraClave(int id)
        //{
        //    using (var db = new BuscAR())
        //    {

        //        PalabraClave palabras = db.PalabraClaves.Where(p => p.id == id).Single();

        //        db.PalabraClaves.Remove(palabras);
        //        db.SaveChanges();
        //    }


        




    }
}