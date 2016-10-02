using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPDDSGrupo44.Models
{
    public class Servicio
    {
        [Key]
        ////////////////Atributos////////////////
        public int Id { get; set;  }

        public string nombre { get; set; }
        public virtual List<HorarioAbierto> horarioAbierto { get; set; }
        public virtual List<HorarioAbierto> horarioFeriados { get; set; }

        ////////////////Constructor vacio////////////////
        public Servicio()
        {
            horarioAbierto = new List<HorarioAbierto>();
            horarioFeriados = new List<HorarioAbierto>();
        }


        ////////////////Creo Constructor////////////////
        public Servicio(string nombreDelServicio)
        {
            nombre = nombreDelServicio;
            horarioAbierto = new List<HorarioAbierto>();
            horarioFeriados = new List<HorarioAbierto>();
        }


        // Cálculo de Disponibilidad - Bancos y CGPs tienen servicios que pueden o no estar disponibles
        public bool estaDisponible(DateTime searchTime)
        {
            HorarioAbierto todaysHours = new HorarioAbierto();
            //busco entre los feriados a ver si hoy es feriado
            if (horarioFeriados != null) { 
                todaysHours = horarioFeriados.Find(x => x.numeroDeDia == searchTime.Day && x.numeroDeMes == searchTime.Month);
            }

            //si es feriado, verificar si está abierto (pueden hacer horarios diferenciales)
            if (todaysHours != null)
            {
                return todaysHours.horarioValido(searchTime);
            }
            else
            {
                // si no era feriado, busco si está abierto por día de la semana
                todaysHours = horarioAbierto.Find(x => x.dia == searchTime.DayOfWeek);
                return todaysHours.horarioValido(searchTime);
            }
        }

    }


    public class ServicioBanco : Servicio
    {
        [Key]
        public new int Id { get; set; }

        public new string nombre { get; set; }
        public new virtual List<HorarioAbierto> horarioAbierto { get; set; }
        public new virtual List<HorarioAbierto> horarioFeriados { get; set; }

        public ServicioBanco() {
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
