using System.Collections.Generic;
using TPDDSGrupo44.Models;

namespace TPDDSGrupo44.ViewModels
{
    public class EditTerminalViewModel : BaseViewModel
    {
        public DispositivoTactil terminal { get; set; }
        public List<FuncionalidadDispositivoTactil> funcionalidades { get; set; }

        public EditTerminalViewModel() : base()
        {
            funcionalidades = new List<FuncionalidadDispositivoTactil>();
        }

    }
}