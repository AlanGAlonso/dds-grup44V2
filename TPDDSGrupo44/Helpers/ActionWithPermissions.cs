using System.Linq;
using System.Web.Mvc;

namespace TPDDSGrupo44.Helpers
{
    public static class ActionWithPermissions 
    {
        public static MvcHtmlString ActionPermissions(this HtmlHelper helper, string visibleText, string action, string controller, string permission)
        {
            if (ViewModels.BaseViewModel.usuario != null) { 
                if (ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == permission).ToList().Count() > 0) {
                    var li = new TagBuilder("li");
                    var a = new TagBuilder("a");
                    a.MergeAttribute("href", "/" + controller + "/" + action);
                    a.InnerHtml = visibleText;
                    li.InnerHtml = a.ToString();
                    return MvcHtmlString.Create(li.ToString());
                }
                else
                {
                    return MvcHtmlString.Create("");
                }
            } else
            {
                return MvcHtmlString.Create("");
            }
        }

        //alternativa con íconos
        public static MvcHtmlString ActionPermissions(this HtmlHelper helper, string visibleText, string action, string controller, string permission, string ionicon)
        {
            if (ViewModels.BaseViewModel.usuario != null)
            {
                if (ViewModels.BaseViewModel.usuario.rol.funcionalidades.Where(f => f.nombre == permission).ToList().Count() > 0)
                {
                    var li = new TagBuilder("li");
                    var a = new TagBuilder("a");
                    var span = new TagBuilder("span");

                    span.AddCssClass(ionicon);

                    a.MergeAttribute("href", "/" + controller + "/" + action);
                    a.InnerHtml = span.ToString() + " " + visibleText;

                    li.InnerHtml =  a.ToString();
                    return MvcHtmlString.Create(li.ToString());
                }
                else
                {
                    return MvcHtmlString.Create("");
                }
            }
            else
            {
                return MvcHtmlString.Create("");
            }
        }
    }
}