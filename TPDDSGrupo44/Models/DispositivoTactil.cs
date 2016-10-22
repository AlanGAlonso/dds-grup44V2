using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using TPDDSGrupo44.Models;

namespace TPDDSGrupo44
{
    public class DispositivoTactil
    {
        [Key]
        ////////////////Atributos////////////////
        public int Id { get; set; }

        public DbGeography coordenada { get; set; }
        public string nombre { get; set; }
        public virtual List<FuncionalidadDispositivoTactil> funcionalidades { get; set; }

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