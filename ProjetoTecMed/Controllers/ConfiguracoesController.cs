using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoTecMed.DAO;
using ProjetoTecMed.Model;

namespace ProjetoTecMed.Controllers
{
    public class ConfiguracoesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Perfil()
        {
            if (HttpContext.Session.GetString("usuarioLogado") == "logado" && HttpContext.Session.GetString("usuarioTipo") != "paciente")
            {
                ConfiguracaoDAO dao = new ConfiguracaoDAO();

                Usuario usuario = new Usuario();

                usuario = dao.BuscarUsuarioDados(Convert.ToString(HttpContext.Session.GetString("idUsuario")));

                dao.Dispose();

                ViewBag.ID = usuario.id_user;

                ViewBag.Nome = usuario.nome;

                ViewBag.Dt_Nasc = usuario.dt_nasc;

                ViewBag.Tipo = usuario.tipo;

                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                return View();
            }
            else
            {
                ViewBag.ID = HttpContext.Session.GetString("idUsuario");

                ViewBag.Nome = HttpContext.Session.GetString("nomeUsuario");

                ViewBag.Dt_Nasc = HttpContext.Session.GetString("usuarioData");

                ViewBag.Tipo = "Paciente";

                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                return View();
            }

        }

        public IActionResult CadastrarNovaSenha(string senha)
        {
            ConfiguracaoDAO dao = new ConfiguracaoDAO();

            bool valor = dao.AlterarSenha(senha, Convert.ToString(HttpContext.Session.GetString("idUsuario")));

            dao.Dispose();

            if (valor)
            {
                //variavel session com msg de erro
                return RedirectToAction("Perfil");
            }
            else
            {
                //variavel session com msg de erro
                return RedirectToAction("Perfil");
            }

        }

        public IActionResult AlterarSenha()
        {
            if (HttpContext.Session.GetString("usuarioLogado") == "logado")
            {

                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                return View();
            }

            return View("NovoLogin");

        }

        public IActionResult AlterarDados(string nome, string data)
        {

            if(HttpContext.Session.GetString("usuarioTipo") == "paciente")
            {
                PacienteDAO dao = new PacienteDAO();

                bool valor = dao.AlterarDadosPaciente(nome, data, HttpContext.Session.GetString("idUsuario"));

                dao.Dispose();

                return RedirectToAction("Perfil");


            }
            else
            {

                ConfiguracaoDAO dao = new ConfiguracaoDAO();

                bool valor = dao.AlterarDadosUsuario(nome, data, HttpContext.Session.GetString("idUsuario"));

                dao.Dispose();

                return RedirectToAction("Perfil");

            }

        }

        public IActionResult ConfigurarLeitor()
        {
            try
            {
                List<string> ports = new List<string>();

                string[] portas = SerialPort.GetPortNames();

                for (int i = 0; i < portas.Length; i++)
                {
                    ports.Add(portas[i]);
                }

                ViewBag.ListaPortas = ports;

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            return View();
        }

        public IActionResult PortaSelecionada(string porta)
        {

            HttpContext.Session.SetString("PortaCartao", porta);

            return RedirectToAction("ConfigurarLeitor");
        }

        public IActionResult Teste()
        {
            return View();
        }


    }
}