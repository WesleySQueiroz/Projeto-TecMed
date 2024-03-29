﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;


namespace ProjetoTecMed.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

         public IActionResult Index()
        {
            // Sends a message to configured loggers, including the Stackdriver logger.
            // The Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker logger will log all controller actions with
            // log level information. This log is for additional information.
            _logger.LogInformation("Home page hit!");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }

        public IActionResult Error()
        {
            // Log messages with different log levels.
            _logger.LogError("Error page hit!");
            return View();
        }

        public IActionResult Principal()
        {

            ViewBag.Nome = HttpContext.Session.GetString("nomeUsuario");
            
            return View();
        }

        public IActionResult BemVindo()
        {
            if (HttpContext.Session.GetString("usuarioLogado") == "logado")
            {
                ViewBag.Nome = HttpContext.Session.GetString("nomeUsuario");

             
                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");


                //ViewBag.Layout = "_LayoutMenu";

                return View();
            }

            return View("NovoLogin");

        }


    }
}
