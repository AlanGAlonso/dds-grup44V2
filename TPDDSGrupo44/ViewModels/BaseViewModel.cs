using System.Linq;
using System.Web.Mvc;
using TPDDSGrupo44.DataModels;
using TPDDSGrupo44.Models;

namespace TPDDSGrupo44.ViewModels
{
    public class BaseViewModel
    {
        
        public static Usuario usuario { get; set; }
        public static Configuracion configuracion { get; set; }
        private static BaseViewModel instance;

        public BaseViewModel() {

            using (var db = new BuscAR())
            {
                configuracion = db.Configuraciones.Single();
            }    

        }

        public static BaseViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BaseViewModel();
                }
                return instance;
            }
        }

    }
}