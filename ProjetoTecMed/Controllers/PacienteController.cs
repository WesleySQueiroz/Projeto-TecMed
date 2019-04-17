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
    public class PacienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CadastrarPaciente()
        {
            if (HttpContext.Session.GetString("usuarioLogado") == "logado")
            {
                ViewBag.Mensagem = HttpContext.Session.GetString("erro");

                HttpContext.Session.SetString("erro", "");

                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                return View();

            }
            return View("NovoLogin");
          
        }

        public IActionResult AdicionaPaciente(Paciente paciente)
        {
            PacienteDAO DAO = new PacienteDAO();

            if (DAO.VerificaCPF(paciente.cpf))
            {
                HttpContext.Session.SetString("erro", "Paciente já cadastrado!");

                return RedirectToAction("CadastrarPaciente");
            }
            else
            {
                PacienteDAO dao = new PacienteDAO();

                bool valor = dao.AdicionaPaciente(paciente);

                dao.Dispose();

                if (valor)
                {
                    HttpContext.Session.SetString("erro", "Paciente Cadastrado com Sucesso!");

                    return RedirectToAction("CadastrarPaciente");

                }
                else
                {
                    HttpContext.Session.SetString("erro", "Erro ao cadastrar Paciente!");

                    return RedirectToAction("CadastrarPaciente");
                }

            }

        }

        public IActionResult CartaoPaciente()
        {
            if (HttpContext.Session.GetString("usuarioLogado") == "logado")
            {
                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                return View();
            }

            return View("NovoLogin");
       
        }

        public IActionResult ResultadoBuscaPaciente()
        {
            if (HttpContext.Session.GetString("usuarioLogado") == "logado")
            {
                PacienteDAO dao = new PacienteDAO();

                string cartao = dao.BuscaCartaoTemporario();

                dao.Dispose();

                PacienteDAO b = new PacienteDAO();

                Paciente paciente = b.BuscaPacientePorCartao(cartao);

                b.Dispose();

                PacienteDAO DAO = new PacienteDAO();

                List<Consulta> lista = new List<Consulta>();

                lista = DAO.BuscaConsultaPaciente(paciente.cpf);

                ViewBag.ListaConsulta = lista;

                return View();

                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                return View("CartaoPaciente");

            }

            return View("NovoLogin");

        }

        public string ValorCartao()
        {
            string resultado;

            SerialPort porta = new SerialPort();

            try
            {

                string[] ports = SerialPort.GetPortNames();

                string[] valores = new string[8];

                int cont = 0;

                porta.BaudRate = 9600;

                porta.PortName = "COM4";

                //8 segundos 
                porta.ReadTimeout = 8000;

                porta.Open();

                while (cont < 6)
                {
                    valores[cont] = porta.ReadLine();

                    cont++;
                }

                porta.Close();

                return valores[1];

            }
            catch(TimeoutException)
            {
                porta.Close();

                return resultado = "tempo expirado";
            }

        }

        public IActionResult CadastrarCartao()
        {
            if (HttpContext.Session.GetString("usuarioLogado") == "logado")
            {
                ViewBag.Mensagem = HttpContext.Session.GetString("erro");

                HttpContext.Session.SetString("erro", "");

                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                return View();
            }

            return View("NovoLogin");


           
        }

        public IActionResult ListaPaciente(string cpfPaciente)
        {

            if (HttpContext.Session.GetString("usuarioLogado") == "logado")
            {
                PacienteDAO dao = new PacienteDAO();

                Paciente paciente = new Paciente();

                paciente = dao.BuscaPacientePorCPF(cpfPaciente);

                ViewBag.CPf = cpfPaciente;
                ViewBag.Nome = paciente.nome;
                ViewBag.Data = paciente.dt_nasc;
                ViewBag.Telefone = paciente.telefone;
                ViewBag.Bairro = paciente.bairro;
                ViewBag.Numero = paciente.numero;
                ViewBag.Cidade = paciente.cidade;
                ViewBag.Estado = paciente.estado;

                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                return View();

            }

            return View("NovoLogin");
     
        }

        public IActionResult CadastroCartaoPaciente(string cpf)
        {

            PacienteDAO dao = new PacienteDAO();

            string cartao = dao.BuscaCartaoTemporario();

            dao.Dispose();

            PacienteDAO DAO = new PacienteDAO();

            bool resultado = DAO.AdicionarCartaoPaciente(cpf, cartao);

            DAO.Dispose();

            if (resultado)
            {

                HttpContext.Session.SetString("erro", "Cartão cadastrado com sucesso");

                return RedirectToAction("CadastrarCartao");
            }
            else
            {
                HttpContext.Session.SetString("erro", "Não foi possivel cadastrar este cartão!");

                return View("CadastrarCartao");
            }

        }

        public IActionResult RemoverCartaoPaciente(string cpf)
        {

            PacienteDAO DAO = new PacienteDAO();

            bool resultado = DAO.RemoverCartao(cpf); 

            DAO.Dispose();

            if (resultado)
            {

                HttpContext.Session.SetString("erro", "Cartão removido com sucesso");

                return RedirectToAction("CadastrarCartao");
            }
            else
            {
                HttpContext.Session.SetString("erro", "Não foi possivel remover este cartão!");

                return View("CadastrarCartao");
            }

        }

        public IActionResult DescadastrarCartao()
        {
            if (HttpContext.Session.GetString("usuarioLogado") == "logado")
            {
                ViewBag.Mensagem = HttpContext.Session.GetString("erro");

                HttpContext.Session.SetString("erro", "");

                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                return View();

            }

            return View("NovoLogin");

        }

        public IActionResult FormExcluirPaciente()
        {
            if (HttpContext.Session.GetString("usuarioLogado") == "logado")
            {
                ViewBag.Mensagem = HttpContext.Session.GetString("erro");

                HttpContext.Session.SetString("erro", "");

                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                return View();

            }

            return View("NovoLogin");

        }

        public IActionResult ExcluirPaciente(string cpf)
        {
            PacienteDAO dao = new PacienteDAO();

            bool valor = dao.ExcluirPacienteCPF(cpf);

            if (valor)
            {

                HttpContext.Session.SetString("erro", "Paciente excluido com Sucesso");

                ViewBag.Layout = HttpContext.Session.GetString("usuarioTipo");

                return RedirectToAction("FormExcluirPaciente");
            }

            dao.Dispose();

            HttpContext.Session.SetString("erro", "Erro ao tentar excluir paciente Paciente");

            ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

            return RedirectToAction("CadastrarPaciente");

        }

        public IActionResult FormExcluirUsuario()
        {
            if (HttpContext.Session.GetString("usuarioLogado") == "logado")
            {
                PacienteDAO dao = new PacienteDAO();

                List<string> lista = new List<string>();

                lista = dao.BuscarTiposUsuario();

                ViewBag.ListaTipos = lista;

                dao.Dispose();

                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                return View();

            }

            return View("NovoLogin");

        }

        public IActionResult ExcluirUsuario(string nome, string id, string check, string tipo)
        {
            PacienteDAO dao = new PacienteDAO();

            string tipoid = "";

            bool valor = false;

            if (tipo == "Medico")
            {
                tipoid = "1";
            }
            else if (tipo == "Atendente")
            {
                tipoid = "2";
            }
            else
            {
                tipoid = "3";
            }

            if (!string.IsNullOrEmpty(check))
            {
                if (!string.IsNullOrEmpty(id))
                {
                    valor = dao.ExcluirFuncionario(id, tipoid);

                    dao.Dispose();
                }
                else
                {
                    ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                    return RedirectToAction("FormExcluirUsuario");
                }

            }
            else if (!string.IsNullOrEmpty(check))
            {
                if (!string.IsNullOrEmpty(nome) && string.IsNullOrEmpty(id))
                {
                    PacienteDAO DAO = new PacienteDAO();

                    id = DAO.BuscarIdFuncionario(nome);

                    DAO.Dispose();
                }

                valor = dao.ExcluirFuncionario(id, tipoid);

                dao.Dispose();

            }

            ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

            return RedirectToAction("FormExcluirUsuario");
        }

        //public IActionResult ListaPaciente()
        //{
        //    PacienteDAO dao = new PacienteDAO();

        //    List<Paciente> lista = dao.ListaPacientes();

        //    dao.Dispose();

        //    ViewBag.Lista = lista;

        //    ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

        //    return View();
        //}

    }
}