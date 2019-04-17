using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoTecMed.DAO;
using ProjetoTecMed.Model;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System.IO.Ports;

namespace ProjetoTecMed.Controllers
{
    public class ConsultaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListaConsulta()
        {
            if (HttpContext.Session.GetString("usuarioLogado") == "logado" && HttpContext.Session.GetString("usuarioTipo") != "paciente")
            {
                ConsultaDAO dao = new ConsultaDAO();

                List<Consulta> consultas = new List<Consulta>();

                consultas = dao.ListaConsulta();

                ViewBag.ListaConsulta = consultas;

                dao.Dispose();

                ViewBag.Mensagem = HttpContext.Session.GetString("erro");

                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                return View();
            }
            else
            {
                ConsultaDAO dao = new ConsultaDAO();

                List<Consulta> consultas = new List<Consulta>();

                consultas = dao.BuscaConsultasPorCPF(HttpContext.Session.GetString("idUsuario"));

                ViewBag.ListaConsulta = consultas;

                dao.Dispose();

                ViewBag.Mensagem = HttpContext.Session.GetString("erro");

                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                return View();

            }

        }

        public IActionResult MarcarConsulta(string idConsulta)
        {
            if (HttpContext.Session.GetString("usuarioLogado") == "logado")
            {
                ConsultaDAO dao = new ConsultaDAO();

                List<string> tipos = new List<string>();

                tipos = dao.ListaTipos();

                dao.Dispose();

                ViewBag.ListaTipos = tipos;

                ViewBag.IdConsulta = idConsulta;

                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                return View();

            }

            return View("NovoLogin");

        }

        public IActionResult CadastrarConsulta(string cpf, string nome, string id, string tipo)
        {
            ConsultaDAO DAO = new ConsultaDAO();

            string ntipo = DAO.BuscarTipoNome(tipo);

            DAO.Dispose();

            ConsultaDAO dao = new ConsultaDAO();

            bool value = dao.MarcarConsulta(cpf, nome, id, ntipo);

            dao.Dispose();

            if (value)
            {
                return RedirectToAction("ListaConsulta");
            }
            else
            {
                return RedirectToAction("MarcarConsulta");
            }
        
        }

        public IActionResult AgendarConsulta()
        {
            return View();
        }

        public IActionResult Teste()
        {
            return View();
        }

        public IActionResult FormDadosPaciente(string cpfPaciente)
        {

            if (HttpContext.Session.GetString("usuarioLogado") == "logado")
            {
                PacienteDAO dao = new PacienteDAO();

                Paciente paciente = new Paciente();

                paciente = dao.BuscaPacientePorCPF(cpfPaciente);

                ViewBag.Cpf = cpfPaciente;
                ViewBag.Nome = paciente.nome;
                ViewBag.DtNasc = paciente.dt_nasc;
                ViewBag.Rua = paciente.rua;
                ViewBag.Num = paciente.numero;
                ViewBag.Cidade = paciente.cidade;
                ViewBag.Estado = paciente.estado;
                ViewBag.Telefone = paciente.telefone;

                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                dao.Dispose();

                return View();

            }

            return View("NovoLogin");
            
        }

        public IActionResult FormDadosMedico(string crmMedico)
        {

            if (HttpContext.Session.GetString("usuarioLogado") == "logado")
            {
                UsuarioDAO dao = new UsuarioDAO();

                Medico medico = new Medico();

                medico = dao.BuscaMedico(crmMedico);

                ViewBag.CRM = crmMedico;
                ViewBag.Nome = medico.nome;
                ViewBag.Id = medico.id_user;
                ViewBag.Data = medico.dt_nasc;
                ViewBag.Especialidade = medico.especialidade;

                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                dao.Dispose();

                return View();

            }

            return View("NovoLogin");

        }

        public IActionResult BuscarConsulta()
        {
            if (HttpContext.Session.GetString("usuarioLogado") == "logado")
            {
                ConsultaDAO dao = new ConsultaDAO();

                ConsultaDAO dao2 = new ConsultaDAO();

                List<string> listaTipos = new List<string>();

                List<string> listaStatus = new List<string>();

                listaTipos = dao.ListaTipos();
                listaStatus = dao2.ListaStatus();

                ViewBag.ListaTipos = listaTipos;

                ViewBag.ListaStatus = listaStatus;

                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                dao.Dispose();

                dao2.Dispose();

                return View();

            }

            return View("NovoLogin");

        }

        public IActionResult RealizaBusca(string crm, string cpf, string tipo, string status, string data, string check)
        {
            if (HttpContext.Session.GetString("usuarioLogado") == "logado")
            {

                ConsultaDAO dao = new ConsultaDAO();

                List<Consulta> lista = new List<Consulta>();

                if(check == "crm")
                {
                    if ((string.IsNullOrEmpty(crm) || crm == ""))
                    {
                        return View("BuscaConsulta");
                    }

                    lista = dao.ListaResultadoBuscaCRM(crm, tipo, status);
                    ViewBag.ListaConsulta = lista;

                    dao.Dispose();

                }
                else if(check == "cpf")
                {
                    if ((string.IsNullOrEmpty(cpf) || cpf == ""))
                    {
                        return View("BuscaConsulta");
                    }


                    lista = dao.ListaResultadoBuscaCPF(cpf, tipo, status);
                    ViewBag.ListaConsulta = lista;

                    dao.Dispose();

                }
                else if (check == "data")
                {
                    if ((string.IsNullOrEmpty(data) || data == ""))
                    {
                        return View("BuscaConsulta");
                    }


                    lista = dao.ListaResultadoBuscaData(data, tipo, status);
                    ViewBag.ListaConsulta = lista;

                    dao.Dispose();

                }
                else
                {
                    lista = dao.ListaResultadoBusca(tipo, status);
                    ViewBag.ListaConsulta = lista;

                    dao.Dispose();

                }
             
                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                return View();

            }

            return View("NovoLogin");


        }

        public IActionResult MarcarComCartao(string idConsulta)
        {
            PacienteDAO dao = new PacienteDAO();

            string cartao = dao.BuscaCartaoTemporario();

            dao.Dispose();

            Paciente paciente = new Paciente();

            ConsultaDAO DAO = new ConsultaDAO();

            paciente = DAO.BuscarPacienteCartao(cartao);

            DAO.Dispose();

            ViewBag.Nome = paciente.nome;

            ViewBag.CPF = paciente.cpf;

            ViewBag.IdConsulta = idConsulta;

            ConsultaDAO daot = new ConsultaDAO();

            List<string> tipos = new List<string>();

            tipos = daot.ListaTipos();

            dao.Dispose();

            ViewBag.ListaTipos = tipos;

            ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

            return View("ConsultaComCartao");
    
        }

        public IActionResult ConsultaComCartao()
        {

            return View();
        }

        //Função de verificar o Cartão
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

                //5 segundos 
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
            catch (TimeoutException)
            {
                porta.Close();

                return resultado = "tempo expirado";
            }

        }

        public IActionResult MetodoConfirmacao(string id)
        {
            if (HttpContext.Session.GetString("usuarioLogado") == "logado")
            {
                ViewBag.IdConsulta = id;

                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                return View();
            }

            return View("NovoLogin");

        }

        public IActionResult CadastrarAgenda()
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

        public IActionResult AdicionarAgenda(string data, string hora_inicio, string hora_fim, string crm)
        {
            //verificar antes se ja existe esta hora nesta data

            ConsultaDAO dao = new ConsultaDAO();

            bool value = dao.CadastrarAgenda(data, hora_inicio, hora_fim, crm);

            dao.Dispose();

            if (value)
            {

                HttpContext.Session.SetString("erro", "Agendamento marcado com sucesso!");

                return RedirectToAction("CadastrarAgenda");
            }
            else
            {

                HttpContext.Session.SetString("erro", "Houve um erro ao marcar a agenda!");

                return RedirectToAction("CadastrarAgenda");
            }
        }

        public IActionResult ConfirmacaoDadosCartao(string idconsulta, Paciente paciente)
        {
            if (HttpContext.Session.GetString("usuarioLogado") == "logado")
            {
                ConsultaDAO dao = new ConsultaDAO();

                List<string> tipos = new List<string>();

                tipos = dao.ListaTipos();

                dao.Dispose();

                ViewBag.IdConsulta = idconsulta;

                ViewBag.ListaTipos = tipos;

                ViewBag.CPf = paciente.cpf;

                ViewBag.Nome = paciente.nome;

                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                return View();
            }

            return View("NovoLogin");


            
        }

        public IActionResult RealizarCheckIn(string idconsulta)
        {

            if (HttpContext.Session.GetString("usuarioLogado") == "logado")
            {
                ConsultaDAO dao = new ConsultaDAO();

                string tipo = dao.BuscaTipoConsulta(idconsulta);

                dao.Dispose();

                ViewBag.Tipo = tipo;
             
                ViewBag.IdConsulta = idconsulta;

                ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

                return View();

            }

            return View("NovoLogin");

        }

        public IActionResult CadastrarConsultaCheckIn(string cpf, string nome, string id, string tipo)
        {        
            ConsultaDAO dao = new ConsultaDAO();

            bool value = dao.MarcarConsultaCheckIn(cpf, nome, id, tipo);

            dao.Dispose();

            if (value)
            {

                HttpContext.Session.SetString("erro", "Check-In realizado com sucesso!");

                return RedirectToAction("ListaConsulta");
            }
            else
            {
                HttpContext.Session.SetString("erro", "Não foi possivel realizar o check-in");

                return RedirectToAction("MarcarConsulta");
            }

        }

        public IActionResult MetodoConfirmacaoCheckIn(string id)
        {
            ViewBag.IdConsulta = id;

            ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

            return View();
        }

        public IActionResult MarcarCheckInComCartao(string idConsulta)
        {

            PacienteDAO dao = new PacienteDAO();

            string cartao = dao.BuscaCartaoTemporario();

            dao.Dispose();

            Paciente paciente = new Paciente();

            ConsultaDAO DAO = new ConsultaDAO();

            paciente = DAO.BuscarPacienteCartao(cartao);

            DAO.Dispose();

            ViewBag.Nome = paciente.nome;

            ViewBag.CPF = paciente.cpf;

            ViewBag.IdConsulta = idConsulta;

            ConsultaDAO daoTipo = new ConsultaDAO();

            string tipo = daoTipo.BuscaTipoConsulta(idConsulta);

            daoTipo.Dispose();

            dao.Dispose();

            ViewBag.ListaTipo = tipo;

            ViewBag.Layout = HttpContext.Session.GetString("usuarioLayout");

            return View("CheckInComCartao");

        }

        public IActionResult CheckInComCartao()
        {

            return View();

        }


    }

    
}