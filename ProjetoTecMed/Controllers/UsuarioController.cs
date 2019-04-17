using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoTecMed.DAO;
using ProjetoTecMed.Model;
using System.IO.Ports;
using Microsoft.AspNetCore.Http;

namespace ProjetoTecMed.Controllers
{
    public class UsuarioController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CadastraUsuario(string tipo)
        {

            if(tipo == "Medico")
            {
                ViewBag.Tipo = tipo;

                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                return View("CadastrarUsuarioMedico");
            }
            else
            {
                ViewBag.Tipo = tipo;

                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                return View("CadastraUsuario");
            }

        }

        public IActionResult AdicionaUsuario(Usuario usuario)
        {
            UsuarioDAO dao = new UsuarioDAO();

            bool valor = dao.AdicionaCadastro(usuario);

            dao.Dispose();

            if (valor)
            {
                HttpContext.Session.SetString("erro", "Usuario cadastrado com sucesso");

                ViewBag.Layout = HttpContext.Session.GetString("usuarioTipo");

                return RedirectToAction("EscolhaTipoUsuario");
            }
            else
            {
                HttpContext.Session.SetString("erro", "Erro ao cadastrar usuario!");

                ViewBag.Layout = HttpContext.Session.GetString("usuarioTipo");

                return RedirectToAction("EscolhaTipoUsuario");
            }

        }

        public IActionResult AdicionaUsuarioMedico(Usuario usuario, string crm, string especialidade)
        {
            UsuarioDAO DAO = new UsuarioDAO();

            if (DAO.VerificaCRM(crm))
            {
                DAO.Dispose();

                HttpContext.Session.SetString("erro", "Usuario já cadastrado!");

                ViewBag.Layout = HttpContext.Session.GetString("usuarioTipo");

                return RedirectToAction("EscolhaTipoUsuario");
            }
            else
            {
                UsuarioDAO dao = new UsuarioDAO();

                bool valor = dao.AdicionaCadastroMedico(usuario, crm, especialidade);

                dao.Dispose();

                if (valor)
                {
                    HttpContext.Session.SetString("erro", "Usuario cadastrado com sucesso!");

                    ViewBag.Layout = HttpContext.Session.GetString("usuarioTipo");

                    return RedirectToAction("EscolhaTipoUsuario");
                }
                else
                {
                    HttpContext.Session.SetString("erro", "Erro ao cadastrar usuario!");

                    ViewBag.Layout = HttpContext.Session.GetString("usuarioTipo");

                    return RedirectToAction("EscolhaTipoUsuario");
                }
            }

        }

        [HttpGet]
        public IActionResult MinhaAction()
        {
            SerialPort porta = new SerialPort();

            string[] ports = SerialPort.GetPortNames();

            string[] valores = new string[6];

            foreach(string port in ports)
            {
                int cont = 0;

                bool valor = true;

                porta.BaudRate = 9600;

                porta.PortName = "COM4";

                //5 segundos 
                porta.ReadTimeout = 5000;

                porta.Open();

                while (cont < 6)
                {
                    valores[cont] = porta.ReadLine();
                    cont++;
                }


                string parou = "parou";
                
            }
            

            return View();
        }

        public IActionResult EscolhaTipoUsuario()
        {

            ViewBag.Mensagem = HttpContext.Session.GetString("erro");

            HttpContext.Session.SetString("erro", "");

            ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

            return View();
        }

        public IActionResult ListaUsuario()
        {

            UsuarioDAO dao = new UsuarioDAO();

            List<Usuario> lista = dao.ListaUsuario();

            dao.Dispose();

            ViewBag.Lista = lista;

            ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

            return View();

        }

        
    }
}