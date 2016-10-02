using System;
using System.ComponentModel.DataAnnotations;

namespace TPDDSGrupo44.Models
{
    public class HorarioAbierto
    {
        [Key]
        ////////////////Atributos////////////////
        public int Id { get; set; }

        public DayOfWeek dia { get; set; }
        public int numeroDeDia { get; set; }
        public int numeroDeMes { get; set; }  
        public int horarioInicio { get; set; }
        public int horarioFin { get; set; }

        ////////////////Constructor vacio////////////////
        public HorarioAbierto() { }

        ////////////////Creo Constructor del HorarioAbierto para días normales////////////////
        public HorarioAbierto(DayOfWeek dia, int horarioInicio, int horarioFin)
        {
            this.dia = dia;
            this.horarioInicio = horarioInicio;
            this.horarioFin = horarioFin;
        }

        ////////////////Constructor para días feriados////////////////
        public HorarioAbierto(int numeroDeDia, int numeroDeMes, int horarioInicio, int horarioFin)
        {
            this.numeroDeDia = numeroDeDia;
            this.numeroDeMes = numeroDeMes;
        }

        ////////////////Validar horario////////////////
        public bool horarioValido(DateTime searchTime)
        {
            if ((searchTime.Hour >= horarioInicio) && (searchTime.Hour < horarioFin)) //Contemplo rango en el que horario abre y cierra
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
