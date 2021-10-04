using ControleEstoque.WEB.Models;
using ControleEstoque.WEB.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ControleEstoque.WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sobre()
        {
            return View();
        }

        //public ActionResult GetSamples()
        //{
        //    List<Sample> samples = new List<Sample>();
        //    samples.Add(new Sample() { Name = "temp1", Email = "email@email.com" });
        //    samples.Add(new Sample() { Name = "temp2", Email = "email2@email.com" });
        //    return View(samples);
        //}

        //public ActionResult GeneratePDF()
        //{
        //    return new Rotativa.ActionAsPdf("GetSamples");
        //}

    }
}