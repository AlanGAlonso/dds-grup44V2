using System.ComponentModel.DataAnnotations;

namespace TPDDSGrupo44.Models
{
    public class Rubro
    {
        [Key]
        ////////////////Atributos////////////////
        public int Id { get; set; }

        public string nombre { get; set; }
        public int radioDeCercania { get; set; }

        ////////////////Constructor vacio////////////////
        public Rubro() { }

        ////////////////Creo Constructor////////////////
        public Rubro(string nombreR, int radio)
        {
            nombre = nombreR;
            radioDeCercania = radio;
        }
    }


   

}