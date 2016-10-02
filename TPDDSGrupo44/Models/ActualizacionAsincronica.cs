using System.ComponentModel.DataAnnotations;

namespace TPDDSGrupo44.Models
{
    public class ActualizacionAsincronica : FuncionalidadUsuario
    {

        [Key]
        int Id { get; set; }

        public ActualizacionAsincronica() { }

        public virtual void actualizar() { }
    }
}