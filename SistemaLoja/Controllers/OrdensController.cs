using System.Web.Mvc;
using SistemaLoja.Models.ViewModels;
using SistemaLoja.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SistemaLoja.Controllers
{
    public class OrdensController : Controller
    {
        private SistemaLojaContext db = new SistemaLojaContext();

        // GET: Ordens
        public ActionResult NovaOrdem()
        {
            var ordemView = new OrdemView();
            ordemView.Customizar = new Customizar();
            ordemView.Produtos = new List<ProdutoOrdem>();
            Session["OrdemView"] = ordemView;

            //Adionar uma informação no Dropbox.
            var list = db.Customizars.ToList();
            list.Add(new Customizar { CustomizarId = 0, Nome = "[Selecione um cliente]" });
            list = list.OrderBy(x => x.NomeCompleto).ToList();
            ViewBag.CustomizarId = new SelectList(list, "CustomizarId", "NomeCompleto");

            return View(ordemView);
        }

        [HttpPost]
        public ActionResult NovaOrdem(OrdemView ordemView)
        {
            ordemView = Session["OrdemView"] as OrdemView;
            var customizarId = int.Parse(Request["CustomizarId"]);
            //Adionar uma informação no Dropbox.
            var list = db.Customizars.ToList();

            if (customizarId == 0)
            {
                list.Add(new Customizar { CustomizarId = 0, Nome = "[Selecione um cliente]" });
                list = list.OrderBy(x => x.NomeCompleto).ToList();
                ViewBag.CustomizarId = new SelectList(list, "CustomizarId", "NomeCompleto");
                ViewBag.Error = "Selecione um cliente";

                return View(ordemView);
            }

            var cliente = db.Customizars.Find(customizarId);
            if (cliente == null)
            {
                list = db.Customizars.ToList();
                list.Add(new Customizar { CustomizarId = 0, Nome = "[Selecione um cliente]" });
                list = list.OrderBy(c => c.NomeCompleto).ToList();
                ViewBag.CustomizarId = new SelectList(list, "CustomizarId", "NomeCompleto");
                ViewBag.Error = "O cliente não existe";

                return View(ordemView);
            }

            if (ordemView.Produtos.Count == 0)
            {
                list = db.Customizars.ToList();
                list.Add(new Customizar { CustomizarId = 0, Nome = "[Selecione um cliente]" });
                list = list.OrderBy(c => c.NomeCompleto).ToList();
                ViewBag.CustomizarId = new SelectList(list, "CustomizarId", "NomeCompleto");
                ViewBag.Error = "Seleione um produto";

                return View(ordemView);
            }

            //Trabalhar com trasação para que as informações salvadas sejam recuperadas na página.
            var ordemId = 0;
            using(var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var ordem = new Ordem
                    {
                        CustomizarId = customizarId,
                        OrdemData = DateTime.Now,
                        OrdemStatus = OrdemStatus.Criada
                    };

                    db.Ordem.Add(ordem);
                    db.SaveChanges();

                    //Recupera a última ordem salva.
                    ordemId = db.Ordem.ToList().Select(o => o.OrdemId).Max();

                    foreach (var item in ordemView.Produtos)
                    {
                        var ordemDetalhe = new OrdemDetalhe
                        {
                            ProdutoId = item.ProdutoId,
                            Descricao = item.Descricao,
                            Preco = item.Preco,
                            Quantidade = item.Quantidade,
                            OrdemId = ordemId
                        };

                        db.OrdemDetalhe.Add(ordemDetalhe);
                        db.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    ViewBag.Error = "Error " + ex.Message;
                    return View(ordemView);
                }
            }

            ViewBag.Mensagem = string.Format("Ordem: {0}, foi salva com sucesso!", ordemId);

            list = db.Customizars.ToList();
            list.Add(new Customizar { CustomizarId = 0, Nome = "[Selecione um cliente]" });
            list = list.OrderBy(c => c.NomeCompleto).ToList();
            ViewBag.CustomizarId = new SelectList(list, "CustomizarId", "NomeCompleto");
            
            ordemView = new OrdemView();
            ordemView.Customizar = new Customizar();
            ordemView.Produtos = new List<ProdutoOrdem>();
            Session["OrdemView"] = ordemView;

            //Retorna na mesma view.
            return View(ordemView);

        }

        public ActionResult AddProduto()
        {
            
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
            var ordemView = Session["OrdemView"] as OrdemView;

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

            //Verifica se o produto adicionado já existe na ordem.
            produtoOrdem = ordemView.Produtos.Find(p => p.ProdutoId == produtoId);

            float quantidade = 0;
            //TryParse para não ter o erro de formatação na cadeia de caracteres.
            float.TryParse(Request["Quantidade"], out quantidade);
            if (quantidade == 0)
            {
                //list.Add(new ProdutoOrdem { ProdutoId = 0, Descricao = "[Selecione um produto]" });
                list = list.OrderBy(x => x.Descricao).ToList();
                ViewBag.ProdutoId = new SelectList(list, "ProdutoId", "Descricao");
                ViewBag.Error = "Digite a quantidade maior que (0)";

                return View(produtoOrdem);
            }

            if (produtoOrdem == null)
            {
                produtoOrdem = new ProdutoOrdem
                {
                    Descricao = produto.Descricao,
                    Preco = produto.Preco,
                    ProdutoId = produto.ProdutoId,
                    Quantidade = float.Parse(Request["Quantidade"])
                };

                ordemView.Produtos.Add(produtoOrdem);
            }else
            {
                produtoOrdem.Quantidade += float.Parse(Request["Quantidade"]);
            }

            //Adionar uma informação no DropDown.
            var listCliente = db.Customizars.ToList();
            //Para a página não ser atualizada sem o cliente selecionado anteriormente.
            //listCliente.Add(new Customizar { CustomizarId = 0, Nome = "[Selecione um cliente]" });
            listCliente = listCliente.OrderBy(x => x.NomeCompleto).ToList();
            ViewBag.CustomizarId = new SelectList(listCliente, "CustomizarId", "NomeCompleto");

            return View("NovaOrdem", ordemView);
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