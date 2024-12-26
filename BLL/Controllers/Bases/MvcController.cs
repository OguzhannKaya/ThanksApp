using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace BLL.Controllers.Bases
{
    public abstract class MvcController : Controller //Bölgesel ayarı yönetmek için oluşturdum.
    {
        protected MvcController()
        {
            CultureInfo cultureinfo = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = cultureinfo;
            Thread.CurrentThread.CurrentUICulture = cultureinfo;
        }
    }
}
