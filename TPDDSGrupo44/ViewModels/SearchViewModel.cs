using System.Collections.Generic;
using System.Linq;
using TPDDSGrupo44.DataModels;
using TPDDSGrupo44.Models;

namespace TPDDSGrupo44.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        public List<ParadaDeColectivo> paradasEncontradasCerca { get; set; }
        public List<ParadaDeColectivo> paradasEncontradas { get; set; }
        public List<Banco> bancosEncontrados { get; set; }
        public List<Banco> bancosEncontradosCerca { get; set; }
        public List<CGP> cgpsEncontrados { get; set; }
        public List<CGP> cgpsEncontradosCerca { get; set; }
        public List<LocalComercial> localesEncontrados { get; set; }
        public List<LocalComercial> localesEncontradosCerca { get; set; }
        public int resultados { get; set; }

        private static SearchViewModel instance;

        public SearchViewModel() : base ()
        {

            using (var db = new BuscAR())
            {
                terminal = db.Terminales.Include("funcionalidades").Where(i => i.nombre == "UTN FRBA Lugano").Single();
            }
            paradasEncontradas = new List<ParadaDeColectivo>();
            paradasEncontradasCerca = new List<ParadaDeColectivo>();
            bancosEncontrados = new List<Banco>();
            bancosEncontradosCerca = new List<Banco>();
            cgpsEncontrados = new List<CGP>();
            cgpsEncontradosCerca = new List<CGP>();
            localesEncontrados = new List<LocalComercial>();
            localesEncontradosCerca = new List<LocalComercial>();
            resultados = 0;
        }

        public static new SearchViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SearchViewModel();
                }
                instance.paradasEncontradas = new List<ParadaDeColectivo>();
                instance.paradasEncontradasCerca = new List<ParadaDeColectivo>();
                instance.bancosEncontrados = new List<Banco>();
                instance.bancosEncontradosCerca = new List<Banco>();
                instance.cgpsEncontrados = new List<CGP>();
                instance.cgpsEncontradosCerca = new List<CGP>();
                instance.localesEncontrados = new List<LocalComercial>();
                instance.localesEncontradosCerca = new List<LocalComercial>();
                instance.resultados = 0;
                return instance;
            }
        }
    }
}