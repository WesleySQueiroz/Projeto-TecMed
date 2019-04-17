using MySql.Data.MySqlClient;
using ProjetoTecMed.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoTecMed.DAO
{
    public class UsuarioDAO
    {
        ConexaoDAO dao;

        public void Dispose()
        {
            this.dao.Dispose();
        }

        public bool Verificalogin(string login, string senha)
        {
            dao = new ConexaoDAO();

            try
            {
     
                string readCommand = "SELECT * FROM Usuario WHERE login = @login AND senha = @senha;";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@login", login);
                m.Parameters.AddWithValue("@senha", senha);

                var resultado = m.ExecuteReader();

                if (resultado.HasRows)
                {
                    return true;
                }

                else
                {
                    return false;
                }

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);

                //return retorno = false;
            }

            
        }

        public Usuario BuscaUsuario(string login, string senha)
        {

            dao = new ConexaoDAO();
        
            Usuario user = new Usuario();

            try
            {

                string readCommand = "SELECT * FROM Usuario WHERE login = @login AND senha = @senha;";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@login", login);
                m.Parameters.AddWithValue("@senha", senha);

                var resultado = m.ExecuteReader();

                if (resultado.Read())
                {
                    user.nome = Convert.ToString(resultado["Nome"]);
                    user.id_user = Convert.ToString(resultado["Id_Funcionario"]);                   
                    user.dt_nasc = Convert.ToString(resultado["Dt_Nasc"]);
                    user.login = Convert.ToString(resultado["Login"]);
                    user.tipo = Convert.ToString(resultado["Tipo_Usuario"]);
                }

                return user;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

                //return retorno = false;
            }

           
        }

        public Medico BuscaMedico(string crm)
        {
            dao = new ConexaoDAO();

            Medico medico = new Medico();

            try
            {
                string readCommand = "SELECT * FROM Usuario as F join Medico as M on F.Id_Funcionario = M.Id_Funcionario where crm = @crm";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@crm", crm);

                var resultado = m.ExecuteReader();

                if (resultado.Read())
                {
                    medico.nome = Convert.ToString(resultado["Nome"]);
                    medico.id_user = Convert.ToString(resultado["Id_Funcionario"]);
                    medico.dt_nasc = Convert.ToString(resultado["Dt_Nasc"]);
                    medico.especialidade = Convert.ToString(resultado["Especialidade"]);
                }

                return medico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public bool AdicionaCadastro(Usuario usuario)
        {
            dao = new ConexaoDAO();

            string tipo_numero = "";

            if (usuario.tipo == "Atendente")
            {
                tipo_numero = "2";
            }
            else if(usuario.tipo == "Administrador")
            {
                tipo_numero = "3";
            }

            try
            {
                MySqlCommand m = new MySqlCommand("sp_adiciona_funcionario", dao.conexao);

                m.CommandType = CommandType.StoredProcedure;

                m.Parameters.AddWithValue("@nome", usuario.nome);
                m.Parameters.AddWithValue("@dt_nasc", usuario.dt_nasc);
                m.Parameters.AddWithValue("@login", usuario.login);
                m.Parameters.AddWithValue("@senha", usuario.senha);
                m.Parameters.AddWithValue("@tipo", tipo_numero);

                m.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool AdicionaCadastroMedico(Usuario usuario, string crm, string especialidade)
        {
            dao = new ConexaoDAO();

            try
            {
                MySqlCommand m = new MySqlCommand("sp_adiciona_funcionario", dao.conexao);

                m.CommandType = CommandType.StoredProcedure;

                m.Parameters.AddWithValue("@nome", usuario.nome);
                m.Parameters.AddWithValue("@dt_nasc", usuario.dt_nasc);
                m.Parameters.AddWithValue("@login", usuario.login);
                m.Parameters.AddWithValue("@senha", usuario.senha);
               

                m.ExecuteNonQuery();

                return true;

            }
            catch (Exception e)
            {
                return false;

            }


        }

        public bool VerificaCRM(string crm)
        {
            dao = new ConexaoDAO();

            try
            {

                string readCommand = "SELECT * FROM Medico WHERE CRM = @crm;";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@crm", crm);
            
                var resultado = m.ExecuteReader();

                if (resultado.HasRows)
                {
                    return true;
                }

                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

                //return retorno = false;
            }
        }

        public List<Usuario> ListaUsuario()
        {
            dao = new ConexaoDAO();

            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                string readCommand = "SELECT * FROM Usuario";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                var resultado = m.ExecuteReader();

                while (resultado.Read())
                {

                    Usuario user = new Usuario();

                    user.nome = Convert.ToString(resultado["Nome"]);
                    user.id_user = Convert.ToString(resultado["Id_Funcionario"]);
                    user.dt_nasc = Convert.ToString(resultado["Dt_Nasc"]);
                    user.login = Convert.ToString(resultado["Login"]);

                    if(Convert.ToString(resultado["Tipo_Usuario"]) == "1")
                    {
                        user.tipo = "Medico";
                    }
                    else if(Convert.ToString(resultado["Tipo_Usuario"]) == "2")
                    {
                        user.tipo = "Atendente";
                    }
                    else if(Convert.ToString(resultado["Tipo_Usuario"]) == "3")
                    {
                        user.tipo = "Administrador";
                    }

                    usuarios.Add(user);
                    
                }

                return usuarios;

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);

            }



        }

    }
}
