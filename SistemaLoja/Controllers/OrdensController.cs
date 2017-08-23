using System.Web.Mvc;
using SistemaLoja.Models.ViewModels;
using SistemaLoja.Models;
using System.Collections.Generic;
using System.Linq;

namespace SistemaLoja.Controllers
{
    public class OrdensController : Controller
    {
        private SistemaLojaContext db = new SistemaLojaContext();

        // GET: Ordens
        public ActionResult NovaOrdem()
        {
            var OrdemView = new OrdemView();
            OrdemView.Customizar = new Customizar();
            OrdemView.Produtos = new List<ProdutoOrdem>();
            Session["OrdemView"] = OrdemView;

            //Adionar uma informação no Dropbox.
            var list = db.Customizars.ToList();
            list.Add(new Customizar { CustomizarId = 0, Nome = "[Selecione um cliente]" });
            list = list.OrderBy(x => x.NomeCompleto).ToList();
            ViewBag.CustomizarId = new SelectList(list, "CustomizarId", "NomeCompleto");

            return View(OrdemView);
        }

        [HttpPost]
        public ActionResult NovaOrdem(OrdemView OrdemView)
        {
            //Adionar uma informação no Dropbox.
            var list = db.Customizars.ToList();            
            list = list.OrderBy(x => x.NomeCompleto).ToList();
            ViewBag.CustomizarId = new SelectList(list, "CustomizarId", "NomeCompleto");

            return View(OrdemView);
        }

        public ActionResult AddProduto()
        {
            var OrdermView = Session["OrdemView"] as OrdemView;
            //Adionar uma informação no Dropbox.
            var list = db.Produtos.ToList();
            list.Add(new ProdutoOrdem { ProdutoId = 0, Descricao = "[Selecione um produto]" });
            list = list.OrderBy(x => x.Descricao).ToList();
            ViewBag.ProdutoId = new SelectList(list, "ProdutoId", "Descricao");

            return View();
        }

        [HttpPost]
        public ActionResult AddProduto(ProdutoOrdem produtoOrdem)
        {
            //Adionar uma informação no Dropbox.
            var list = db.Produtos.ToList();
            //Recuperando dados da base de dados.
            var produtoId = int.Parse(Request["ProdutoId"]);

            //Se for 0 (nada selecionado) simplesmente cria a lista e mostra na tela.
            if (produtoId == 0)
            {
                list.Add(new ProdutoOrdem { ProdutoId = 0, Descricao = "[Selecione um produto]" });
                list = list.OrderBy(x => x.Descricao).ToList();
                ViewBag.ProdutoId = new SelectList(list, "ProdutoId", "Descricao");
                ViewBag.Error = "Seleione um produto";

                return View(produtoOrdem);
            }

            //Encontrar o produto para ser enviado para a tela de Nova Ordem.
            var produto = db.Produtos.Find(produtoId);

            produtoOrdem = new ProdutoOrdem
            {
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                ProdutoId = produto.ProdutoId,
                Quantidade = float.Parse(Request["Quantidade"])
            };

            //Adionar uma informação no Dropbox.
            var listCliente = db.Customizars.ToList();
            listCliente.Add(new Customizar { CustomizarId = 0, Nome = "[Selecione um cliente]" });
            listCliente = listCliente.OrderBy(x => x.NomeCompleto).ToList();
            ViewBag.CustomizarId = new SelectList(list, "CustomizarId", "NomeCompleto");

            return View("NovaOrdem", produtoOrdem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}