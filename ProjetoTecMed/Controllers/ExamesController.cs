using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoTecMed.DAO;
using ProjetoTecMed.Model;

namespace ProjetoTecMed.Controllers
{
    public class ExamesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BuscarExames()
        {
            if (HttpContext.Session.GetString("usuarioLogado") == "logado")
            {

                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                return View();
            }

            return View("NovoLogin");
    
        }

        public IActionResult ListaExames(string cpf)
        {
            if (HttpContext.Session.GetString("usuarioLogado") == "logado")
            {

                ExamesDAO dao = new ExamesDAO();

                List<Exame> exames = new List<Exame>();

                exames = dao.BuscarExames(cpf);

                ViewBag.Exames = exames;

                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                return View();
            }

            return View("NovoLogin");



        }

    }
}