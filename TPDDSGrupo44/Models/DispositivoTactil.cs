using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;

namespace TPDDSGrupo44
{
    public class DispositivoTactil
    {
        [Key]
        ////////////////Atributos////////////////
        public int Id { get; set; }

        public DbGeography coordenada { get; set; }
        public string nombre { get; set; }
        public int comuna { get; set; } 

        ////////////////Constructor vacio////////////////
        public DispositivoTactil() { }

        ////////////////Creo Constructor////////////////
        public DispositivoTactil(string nombreDelPunto, DbGeography unaCoordenada)
        {
            nombre = nombreDelPunto;
            coordenada = unaCoordenada;
        }
        

    }
}