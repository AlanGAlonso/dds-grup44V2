using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}