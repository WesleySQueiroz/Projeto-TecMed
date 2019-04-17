using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoTecMed.DAO;
using ProjetoTecMed.Model;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;

namespace ProjetoTecMed.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult EfetuarLogin(string login, string senha, string check)
        {


            if(check == "funcionario")
            {
                UsuarioDAO dao = new UsuarioDAO();

                Usuario user = new Usuario();

                bool valida = dao.Verificalogin(login, senha);

                dao.Dispose();

                if (valida == true)
                {
                    user = dao.BuscaUsuario(login, senha);

                    HttpContext.Session.SetString("nomeUsuario", user.nome);
                    HttpContext.Session.SetString("idUsuario", user.id_user);
                    HttpContext.Session.SetString("usuarioLogado", "logado");

                    if (user.tipo == "1")
                    {
                        HttpContext.Session.SetString("usuarioLayout", "_LayoutMedico");
                    }
                    else if (user.tipo == "2")
                    {
                        HttpContext.Session.SetString("usuarioLayout", "_LayoutAtendente");
                    }
                    else if (user.tipo == "3")
                    {
                        HttpContext.Session.SetString("usuarioLayout", "_LayoutMenu");
                    }

                    HttpContext.Session.SetString("usuarioTipo", user.tipo);

                    TempData["Nome"] = HttpContext.Session.GetString("nomeUsuario");

                    return RedirectToAction("BemVindo", "Home");
                }
                else
                {
                    HttpContext.Session.SetString("erro", "Usuário ou senha incorretos! Tente novamente.");

                    ViewBag.Erro = HttpContext.Session.GetString("erro");

                    return View("NovoLogin");

                }

            }
            else if(check == "paciente")
            {
                PacienteDAO dao = new PacienteDAO();

                Paciente paciente = new Paciente();

                bool valida = dao.VerificaloginPaciente(login, senha);

                dao.Dispose();

                if (valida == true) 
                {
                    paciente = dao.BuscaPaciente(login, senha);

                    HttpContext.Session.SetString("nomeUsuario", paciente.nome);
                    HttpContext.Session.SetString("idUsuario", paciente.cpf);
                    HttpContext.Session.SetString("usuarioLogado", "logado");
                    HttpContext.Session.SetString("usuarioData", paciente.dt_nasc);

                    HttpContext.Session.SetString("usuarioLayout", "_LayoutGeral");

                    HttpContext.Session.SetString("usuarioTipo", "paciente");

                    TempData["Nome"] = HttpContext.Session.GetString("nomeUsuario");

                    return RedirectToAction("BemVindo", "Home");

                }
                else
                {
                    HttpContext.Session.SetString("erro", "Usuário ou senha incorretos! Tente novamente.");

                    ViewBag.Erro = HttpContext.Session.GetString("erro");

                    return View("NovoLogin");

                }

            }


            return View("NovoLogin");

        }

        public IActionResult Login()
        {

            HttpContext.Session.SetString("nomeUsuario", "");
            HttpContext.Session.SetString("idUsuario", "");
            HttpContext.Session.SetString("usuarioLogado", "deslogado");

            ViewBag.Erro = HttpContext.Session.GetString("erro");

            HttpContext.Session.SetString("erro", "");

            return View();
        }

        public IActionResult NovoLogin()
        {

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("nomeUsuario", " ");
            HttpContext.Session.SetString("idUsuario", " ");
            HttpContext.Session.SetString("usuarioLogado", "deslogado");
            HttpContext.Session.SetString("usuarioLayout", " ");


            return View("NovoLogin");
        }

        public IActionResult RecuperarSenha(string email, string id)
        {

            MailMessage mail = new MailMessage();

            mail.To.Add("tecmed.sistema@gmail.com");
            mail.From = new MailAddress(email);
            mail.Subject = "Recuperar senha!!";

            object[] args = new object[] { id, email };

            string Body = string.Format("Enviar senha do usuario {0} para o email {1}", args );

            mail.Body = Body;
            mail.IsBodyHtml = true;

            //Instância smtp do servidor, neste caso o gmail.
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            ("tecmed.sistema@gmail.com", "tecmed3565");// Login e senha do e-mail.
            smtp.EnableSsl = true;
            smtp.Send(mail);

            return View("NovoLogin");

        }

        public IActionResult FormEsqueceuSenha()
        {


            return View();
        }


    }
}