using System.Web.Mvc;
using SistemaLoja.Models.ViewModels;
using SistemaLoja.Models;
using System.Collections.Generic;

namespace SistemaLoja.Controllers
{
    public class OrdensController : Controller
    {
        // GET: Ordens
        public ActionResult NovaOrdem()
        {
            var OrdemView = new OrdemView();
            OrdemView.Customizar = new Customizar();
            OrdemView.ProdutoOrdem = new List<ProdutoOrdem>();
            return View(OrdemView);
        }
    }
}