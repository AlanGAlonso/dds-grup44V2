using System.Web.Mvc;
using TPDDSGrupo44.Models;

namespace TPDDSGrupo44.ViewModels
{
    public class BaseViewModel
    {
        
        public static Usuario usuario { get; set; }
        private static BaseViewModel instance;

        public BaseViewModel() { }

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