using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TPDDSGrupo44.Models;

namespace TPDDSGrupo44.DataModels
{
    public class ServicioBanco : Servicio
    {
        [Key]
        public new int Id { get; set; }

        public new string nombre { get; set; }
        public new virtual List<HorarioAbierto> horarioAbierto { get; set; }
        public new virtual List<HorarioAbierto> horarioFeriados { get; set; }

        public ServicioBanco()
        {
            horarioAbierto = new List<HorarioAbierto>();
            horarioFeriados = new List<HorarioAbierto>();
        }

        public ServicioBanco(string nombreDelServicio)
        {
            nombre = nombreDelServicio;
            horarioAbierto = new List<HorarioAbierto>();
            horarioFeriados = new List<HorarioAbierto>();
        }
    }
}