using MySql.Data.MySqlClient;
using ProjetoTecMed.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoTecMed.DAO
{
    public class PacienteDAO
    {
        ConexaoDAO dao;

        public void Dispose()
        {
            this.dao.Dispose();
        }

        public Paciente BuscaPacientePorCPF(string cpf)
        {
            dao = new ConexaoDAO();

            Paciente p = new Paciente();

            try
            {
                string readCommand = "SELECT * FROM Paciente as P join Telefone as T on P.CPF = T.Id_Usuario WHERE cpf = @cpf";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@cpf", cpf);

                var resultado = m.ExecuteReader();

                while(resultado.Read())
                {
                    p.nome = Convert.ToString(resultado["Nome"]);
                    p.dt_nasc = Convert.ToString(resultado["Dt_Nasc"]);
                    p.rua = Convert.ToString(resultado["Rua"]);
                    p.numero = Convert.ToString(resultado["Num_Endereco"]);
                    p.estado = Convert.ToString(resultado["Estado"]);
                    p.cidade = Convert.ToString(resultado["Cidade"]);
                    p.telefone = Convert.ToString(resultado["Telefone"]);
                    
                }

                return p;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        public bool AdicionaPaciente(Paciente paciente)
        {
            dao = new ConexaoDAO();

            try
            {
                string readCommand = "Insert into Paciente (CPF, Nome, Dt_Nasc, Rua, Num_Endereco, Estado, Cidade, login, senha) values (@cpf, @nome, @dtnasc, @rua, @num, @estado, @cidade, @login, @senha)";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@cpf", paciente.cpf);
                m.Parameters.AddWithValue("@nome", paciente.nome);
                m.Parameters.AddWithValue("@dtnasc", paciente.dt_nasc);
                m.Parameters.AddWithValue("@rua", paciente.rua);
                m.Parameters.AddWithValue("@num", paciente.numero);
                m.Parameters.AddWithValue("@estado", paciente.estado);
                m.Parameters.AddWithValue("@cidade", paciente.cidade);
                m.Parameters.AddWithValue("@login", paciente.login);
                m.Parameters.AddWithValue("@senha", paciente.senha);

                var resultado = m.ExecuteNonQuery();

                return true;

            }
            catch(Exception e)
            {
                return false;
            }

        }

        public List<Consulta> BuscaConsultaPaciente(string cpf)
        {
            dao = new ConexaoDAO();

            List<Consulta> consultas = new List<Consulta>();

            try
            {
                string readCommand = "SELECT * FROM Agenda as A join Consulta as C on A.Id_Consulta = C.Id_Consulta where CPF_Paciente = @cpf";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);
                m.Parameters.AddWithValue("@cpf", cpf);

                var resultado = m.ExecuteReader();

                Consulta consulta = new Consulta();

                //if ou while
                while (resultado.Read())
                {
            
                    consulta.cpf = Convert.ToString(resultado["CPF_Paciente"]);
                    consulta.descricao = Convert.ToString(resultado["Descricao"]);
                    consulta.id_consulta = Convert.ToString(resultado["Id_Consulta"]);
                    consulta.id_medico = Convert.ToString(resultado["Id_Funcionario"]);

                    if (Convert.ToString(resultado["Status"]) == "1")
                    {
                        consulta.status = "Aberto";
                    }
                    else if (Convert.ToString(resultado["Status"]) == "2")
                    {
                        consulta.status = "Aguardando";
                    }
                    else if (Convert.ToString(resultado["Status"]) == "3")
                    {
                        consulta.status = "Realizada";
                    }

                    if (Convert.ToString(resultado["Tipo"]) == "1")
                    {
                        consulta.tipo = "Rotina";
                    }
                    else if (Convert.ToString(resultado["Status"]) == "2")
                    {
                        consulta.tipo = "Primeira Consulta";
                    }
                    else if (Convert.ToString(resultado["Status"]) == "3")
                    {
                        consulta.tipo = "Realizada";
                    }
                    consulta.dt_consulta = Convert.ToString(resultado["Dt_Consulta"]);
                    consulta.hr_inicio = Convert.ToString(resultado["Hr_Inicio"]);
                    consulta.hr_fim = Convert.ToString(resultado["Hr_Fim"]);
                    consultas.Add(consulta);
                }

                return consultas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool RemoverCartao(string cpf)
        {

            dao = new ConexaoDAO();

            try
            {
                string readCommand = "Update Paciente set Cartao = null where CPF = @cpf";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@cpf", cpf);
           
                var resultado = m.ExecuteNonQuery();

                return true;

            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool AdicionarCartaoPaciente(string cpf, string cartao)
        {
            dao = new ConexaoDAO();

            try
            {
                string readCommand = "Update Paciente set Cartao = @cartao where CPF = @cpf";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@cpf", cpf);
                m.Parameters.AddWithValue("@cartao", cartao);
              
                var resultado = m.ExecuteNonQuery();
     

                return true;

            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool ExcluirPacienteCPF(string cpf)
        {
            dao = new ConexaoDAO();

            try
            {
                string readCommand = "Delete from Paciente where CPF = @cpf";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@cpf", cpf);

                var resultado = m.ExecuteNonQuery();

                return true;

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public List<string> BuscarTiposUsuario()
        {

            dao = new ConexaoDAO();

            List<string> tipos = new List<string>();

            try
            {
                string readCommand = "SELECT * FROM Usuario_Tipo;";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                var resultado = m.ExecuteReader();

                while (resultado.Read())
                {
                    string valor = Convert.ToString(resultado["Nome_Tipo_Usuario"]);

                    tipos.Add(valor);
                }


                return tipos;

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public bool ExcluirFuncionario(string id, string tipo)
        {
            dao = new ConexaoDAO();

            try
            {
                MySqlCommand m = new MySqlCommand("sp_excluir_funcionario", dao.conexao);

                m.CommandType = CommandType.StoredProcedure;

                m.Parameters.AddWithValue("@idtipo", tipo);            
                m.Parameters.AddWithValue("@idfuncionario", id);
                
                m.ExecuteNonQuery();

                return true;

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public string BuscarIdFuncionario(string nome)
        {
            dao = new ConexaoDAO();

            string id = "";

            try
            {
                string readCommand = "Select Id_Funcionario from Usuario where Nome = @nome";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@nome", nome);

                var resultado = m.ExecuteReader();

                while (resultado.Read())
                {
                    id = Convert.ToString(resultado["Id_Funcionario"]);
                }

                return id;

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string BuscaCartaoTemporario()
        {

            dao = new ConexaoDAO();

            string cartao = " ";

            try
            {
                string readCommand = "Select valor_cartao from Cartao_Temp where id = 1;";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                var resultado = m.ExecuteReader();

                while (resultado.Read())
                {
                    cartao = Convert.ToString(resultado["valor_cartao"]);
                }

                return cartao;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public bool VerificaCPF(string cpf)
        {
            dao = new ConexaoDAO();

            try
            {
                string readCommand = "SELECT * FROM Paciente WHERE CPF = @cpf;";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@cpf", cpf);

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

        public List<Paciente> ListaPacientes()
        {

            dao = new ConexaoDAO();

            List<Paciente> pacientes = new List<Paciente>();

            try
            {
                string readCommand = "SELECT * FROM Paciente";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                var resultado = m.ExecuteReader();

                while (resultado.Read())
                {

                    Paciente paciente = new Paciente();

                    paciente.cpf = Convert.ToString(resultado["CPF"]);
                    paciente.nome = Convert.ToString(resultado["Nome"]);
                    paciente.dt_nasc = Convert.ToString(resultado["Dt_Nasc"]);
                    
                }

                return pacientes;

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);

            }




        }

        public Paciente BuscaPacientePorCartao(string cartao)
        {

            dao = new ConexaoDAO();

            Paciente p = new Paciente();

            try
            {
                string readCommand = "SELECT * FROM Paciente as P join Telefone as T on P.CPF = T.Id_Usuario WHERE cartao = @cartao";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@cartao", cartao);

                var resultado = m.ExecuteReader();

                while (resultado.Read())
                {
                    p.nome = Convert.ToString(resultado["Nome"]);
                    p.dt_nasc = Convert.ToString(resultado["Dt_Nasc"]);
                    p.rua = Convert.ToString(resultado["Rua"]);
                    p.numero = Convert.ToString(resultado["Num_Endereco"]);
                    p.estado = Convert.ToString(resultado["Estado"]);
                    p.cidade = Convert.ToString(resultado["Cidade"]);
                    p.telefone = Convert.ToString(resultado["Telefone"]);

                }

                return p;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public bool VerificaloginPaciente(string login, string senha)
        {
            dao = new ConexaoDAO();

            try
            {

                string readCommand = "SELECT * FROM Paciente WHERE login = @login AND senha = @senha;";

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
            catch (Exception e)
            {
                throw new Exception(e.Message);

                //return retorno = false;
            }


        }

        public Paciente BuscaPaciente(string login, string senha)
        {

            dao = new ConexaoDAO();

            Paciente user = new Paciente();

            try
            {

                string readCommand = "SELECT * FROM Paciente WHERE login = @login AND senha = @senha;";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@login", login);
                m.Parameters.AddWithValue("@senha", senha);

                var resultado = m.ExecuteReader();

                if (resultado.Read())
                {
                    user.nome = Convert.ToString(resultado["Nome"]);
                    user.cpf = Convert.ToString(resultado["CPF"]);
                    user.dt_nasc = Convert.ToString(resultado["Dt_Nasc"]);
               
                }

                return user;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

                //return retorno = false;
            }


        }

        public bool AlterarDadosPaciente(string nome, string data, string cpf)
        {
            dao = new ConexaoDAO();

            try
            {
                string readCommand = "Update Paciente set Nome = @nome, Dt_Nasc = @data where CPF = @cpf;";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@data", Convert.ToDateTime(data));

                m.Parameters.AddWithValue("@nome", nome);

                m.Parameters.AddWithValue("@cpf", cpf);

                m.ExecuteNonQuery();

                return true;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
