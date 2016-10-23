using System.Collections.Generic;
using TPDDSGrupo44.Models;

namespace TPDDSGrupo44.ViewModels
{
    public class ServBViewModel : BaseViewModel
    {
        public int idBanco { get; set; }
        public string nombre { get; set; }
        public virtual List<HorarioAbierto> horarioAbierto { get; set; }
        public virtual List<HorarioAbierto> horarioFeriados { get; set; }


        public ServBViewModel(int idBanco) : base()
        {
            this.idBanco = idBanco;
        }

    }
}