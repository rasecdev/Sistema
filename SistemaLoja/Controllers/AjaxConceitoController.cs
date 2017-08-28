using System.Web.Mvc;

namespace SistemaLoja.Controllers
{
    public class AjaxConceitoController : Controller
    {
        // GET: AjaxConceito
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult JsonFatorial(int n)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult
            {
                Data = new { Fatorial = Fatorial(n) }
            };

            return result;

        }

        private double Fatorial(int n)
        {
            double fatorial = 1;

            for (int i = 2; i < n; i++)
            {
                fatorial *= i;
            }

            return fatorial;
        }
    }
}