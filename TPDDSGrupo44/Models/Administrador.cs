using System;
namespace TPDDSGrupo44.Models
{
    public class Administrador
    {
        public int dni { get; set; }
        public string  contrasenia { get; set; }

        public TimeSpan cantSegDemoraA { get; private set; }

        //Creo Constructor
        public Administrador(int dni, string contrasenia, TimeSpan cantSegDemora)
        {
            this.dni = dni;
            this.contrasenia = contrasenia;
            cantSegDemoraA = cantSegDemora;
        }

        //CLASE Administrador
        public String parametrizarDemoraEnSeg(TimeSpan cantSegDemora)
        {
            TimeSpan intervalo = new TimeSpan(2, 14, 18); // Displays "02:14:18".

            return intervalo.ToString();
        }


    }
}