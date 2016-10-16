using System.Collections.Generic;
using TPDDSGrupo44.Models;

namespace TPDDSGrupo44.ViewModels
{
    public class SearchViewModel
    {
        public List<ParadaDeColectivo> paradasEncontradasCerca { get; set; }
        public List<ParadaDeColectivo> paradasEncontradas { get; set; }
        public List<Banco> bancosEncontrados { get; set; }
        public List<Banco> bancosEncontradosCerca { get; set; }
        public List<CGP> cgpsEncontrados { get; set; }
        public List<CGP> cgpsEncontradosCerca { get; set; }
        public List<LocalComercial> localesEncontrados { get; set; }
        public List<LocalComercial> localesEncontradosCerca { get; set; }

        public SearchViewModel()
        {
            paradasEncontradas = new List<ParadaDeColectivo>();
            paradasEncontradasCerca = new List<ParadaDeColectivo>();
            bancosEncontrados = new List<Banco>();
            bancosEncontradosCerca = new List<Banco>();
            cgpsEncontrados = new List<CGP>();
            cgpsEncontradosCerca = new List<CGP>();
            localesEncontrados = new List<LocalComercial>();
            localesEncontradosCerca = new List<LocalComercial>();
        }

    }
}