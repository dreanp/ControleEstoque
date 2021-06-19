using ControleEstoque.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.WEB.Controllers
{
    public class CadastroController : Controller
    {
        private const int _quantidadeMaxLinhasPorPagina = 5;

        private const string _senhaPadrao = "{$127;$188}";

        #region Usuários

        [Authorize]
        public ActionResult Usuario()
        {
            ViewBag.SenhaPadrao = _senhaPadrao;
            return View(UsuarioModel.RecuperarLista());
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult RecuperarUsuario(int id)
        {
            return Json(UsuarioModel.RecuperarPeloId(id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirUsuario(int id)
        {           
            return Json(UsuarioModel.ExcluirPeloId(id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarUsuario(UsuarioModel model)
        {
            var resultado = "Ok";
            var mensagens = new List<String>();
            var idSalvo = string.Empty;

            if (!ModelState.IsValid)
            {
                resultado = "Aviso";
                mensagens = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            }
            else
            {

                try
                {
                    if(model.Senha == _senhaPadrao)
                    {
                        model.Senha = "";
                    }

                    var id = model.Salvar();
                    if (id > 0)
                    {
                        idSalvo = id.ToString();
                    }
                    else
                    {
                        resultado = "Erro";
                    }

                }
                catch (Exception ex)
                {
                    resultado = "Erro";
                }

                
            }
            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo });
        }

        #endregion



        #region Grupos de Produtos

        [Authorize]
        public ActionResult GrupoProduto()
        {
            ViewBag.QuantidadeMaxLinhasPorPagina = _quantidadeMaxLinhasPorPagina;
            ViewBag.PaginaAtual = 1;

            var lista = GrupoProdutoModel.RecuperarLista(ViewBag.PaginaAtual, _quantidadeMaxLinhasPorPagina);
            var quantidade = GrupoProdutoModel.RecuperarQuantidade();



            var difQuantidadePaginas = (quantidade % ViewBag.QuantidadeMaxLinhasPorPagina) > 0 ? 1 : 0;
            ViewBag.QuantidadePaginas = (quantidade / ViewBag.QuantidadeMaxLinhasPorPagina) + difQuantidadePaginas;


            return View(lista);
        }

        [Authorize]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult GrupoProdutoPagina(int pagina)
        {
            var lista = GrupoProdutoModel.RecuperarLista(pagina, _quantidadeMaxLinhasPorPagina);
           

            return Json(lista);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult RecuperarGrupoProduto(int id)
        {
            return Json(GrupoProdutoModel.RecuperarPeloId(id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult ExcluirGrupoProduto(int id)
        {           
            return Json(GrupoProdutoModel.ExcluirPeloId(id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult SalvarGrupoProduto(GrupoProdutoModel model)
        {
            var resultado = "Ok";
            var mensagens = new List<String>();
            var idSalvo = string.Empty;

            if (!ModelState.IsValid)
            {
                resultado = "Aviso";
                mensagens = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            }
            else
            {

                try
                {
                    var id = model.Salvar();
                    if (id > 0)
                    {
                        idSalvo = id.ToString();
                    }
                    else
                    {
                        resultado = "Erro";
                    }

                }
                catch (Exception ex)
                {
                    resultado = "Erro";
                }

                
            }
            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo });
        }

        #endregion

        [Authorize]
        public ActionResult MarcaProduto()
        {
            return View();
        }
        [Authorize]
        public ActionResult LocalProduto()
        {
            return View();
        }
        [Authorize]
        public ActionResult UnidadeMedida()
        {
            return View();
        }
        [Authorize]
        public ActionResult Produto()
        {
            return View();
        }
        [Authorize]
        public ActionResult Pais()
        {
            return View();
        }
        [Authorize]
        public ActionResult Estado()
        {
            return View();
        }
        [Authorize]
        public ActionResult Cidade()
        {
            return View();
        }
        [Authorize]
        public ActionResult Fornecedor()
        {
            return View();
        }
        [Authorize]
        public ActionResult PerfilUsuario()
        {
            return View();
        }

    }
}