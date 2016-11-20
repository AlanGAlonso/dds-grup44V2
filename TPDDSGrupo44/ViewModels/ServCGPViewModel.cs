using System.Collections.Generic;
using TPDDSGrupo44.Models;

namespace TPDDSGrupo44.ViewModels
{
    public class ServCGPViewModel : BaseViewModel
    {

        public int idCGP { get; set; }
        public string nombre { get; set; }
        public virtual List<HorarioAbierto> horarioAbierto { get; set; }
        public virtual List<HorarioAbierto> horarioFeriados { get; set; }


        public ServCGPViewModel(int idBanco) : base()
        {
            this.idCGP = idCGP;


        }
    }
}