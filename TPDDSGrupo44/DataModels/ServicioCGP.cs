using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TPDDSGrupo44.Models;

namespace TPDDSGrupo44.DataModels
{

    public class ServicioCGP : Servicio
        {
            [Key]
            public new int Id { get; set; }

            public new string nombre { get; set; }
            public new virtual List<HorarioAbierto> horarioAbierto { get; set; }
            public new virtual List<HorarioAbierto> horarioFeriados { get; set; }

            public ServicioCGP()
            {
                horarioAbierto = new List<HorarioAbierto>();
                horarioFeriados = new List<HorarioAbierto>();

            }

            public ServicioCGP(string nombreDelServicio)
            {
                nombre = nombreDelServicio;
                horarioAbierto = new List<HorarioAbierto>();
                horarioFeriados = new List<HorarioAbierto>();
            }
        }
    }
