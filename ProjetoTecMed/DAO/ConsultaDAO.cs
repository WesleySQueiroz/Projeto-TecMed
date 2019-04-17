using MySql.Data.MySqlClient;
using ProjetoTecMed.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoTecMed.DAO
{
    public class ConsultaDAO
    {
        ConexaoDAO dao;

        public void Dispose()
        {
            this.dao.Dispose();
        }

        public List<Consulta> ListaConsulta()
        {

            dao = new ConexaoDAO();

            List<Consulta> consultas = new List<Consulta>();

            try
            {
                string readCommand = "SELECT distinct(A.Id_Consulta), A.Id_Funcionario, A.Dt_Consulta, A.Hr_Inicio, A.Hr_Fim," +
                    "C.Tipo, C.Descricao, C.Status, C.CPF_Paciente FROM Agenda as A join Consulta as C on A.Id_Consulta = C.Id_Consulta order by Id_Consulta";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                var resultado = m.ExecuteReader();

                //if ou while
                while(resultado.Read())
                {
                    //refazer
                    Consulta consulta = new Consulta();

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
                    else if (Convert.ToString(resultado["Tipo"]) == "2")
                    {
                        consulta.tipo = "Primeira Consulta";
                    }
                    else if (Convert.ToString(resultado["Tipo"]) == "3")
                    {
                        consulta.tipo = "Realizada";
                    }
                    else if (Convert.ToString(resultado["Tipo"]) == "4")
                    {
                        consulta.tipo = "Exames";
                    }
                    else if (Convert.ToString(resultado["Tipo"]) == "5")
                    {
                        consulta.tipo = "Marcação";
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

        public List<string> ListaTipos()
        {
            dao = new ConexaoDAO();

            try
            {
                List<string> lista = new List<string>();

                string readCommand = "SELECT * FROM Tipo_Consulta";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                var resultado = m.ExecuteReader();

                while(resultado.Read())
                {
                    string item = Convert.ToString(resultado["Nome_Tipo_Consulta"]);

                    lista.Add(item);
                }

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        public List<string> ListaStatus()
        {

            dao = new ConexaoDAO();

            try
            {
                List<string> lista = new List<string>();

                string readCommand = "SELECT * FROM Status_Consulta";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                var resultado = m.ExecuteReader();

                while(resultado.Read())
                {
                    string item = Convert.ToString(resultado["Nome_Status_Consulta"]);

                    lista.Add(item);
                }

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        public List<Consulta> ListaResultadoBuscaCPF(string cpf, string tipo, string status)
        {

            dao = new ConexaoDAO();

            List<Consulta> lista = new List<Consulta>();

            try
            {
                string readCommand = "SELECT * FROM Agenda as A join Consulta as C on A.Id_Consulta = C.Id_Consulta join Tipo_Consulta as T on C.Tipo = T.Id_Tipo_Consulta join Status_Consulta as S on C.Status = S.Id_Status_Consulta where CPF_Paciente = @cpf and Nome_Tipo_Consulta = @tipo and Nome_Status_Consulta = @status";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@cpf", cpf);
                m.Parameters.AddWithValue("@tipo", tipo);
                m.Parameters.AddWithValue("@status", status);

                var resultado = m.ExecuteReader();

                Consulta consulta = new Consulta();

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

                    if(Convert.ToString(resultado["Tipo"]) == "1")
                    {
                        consulta.tipo = "Rotina";
                    }
                    else if (Convert.ToString(resultado["Tipo"]) == "2")
                    {
                        consulta.tipo = "Primeira Consulta";
                    }
                    else if (Convert.ToString(resultado["Tipo"]) == "3")
                    {
                        consulta.tipo = "Realizada";
                    }
                    else if (Convert.ToString(resultado["Tipo"]) == "4")
                    {
                        consulta.tipo = "Exames";
                    }
                    else if (Convert.ToString(resultado["Tipo"]) == "5")
                    {
                        consulta.tipo = "Marcação";
                    }

                    consulta.dt_consulta = Convert.ToString(resultado["Dt_Consulta"]);
                    consulta.hr_inicio = Convert.ToString(resultado["Hr_Inicio"]);
                    consulta.hr_fim = Convert.ToString(resultado["Hr_Fim"]);

                    lista.Add(consulta);

                }

                return lista;
               
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public List<Consulta> ListaResultadoBuscaData(string data, string tipo, string status)
        {

            dao = new ConexaoDAO();

            List<Consulta> lista = new List<Consulta>();

            try
            {

                string readCommand = "SELECT * FROM Agenda as A join Consulta as C on A.Id_Consulta = C.Id_Consulta join Tipo_Consulta as T on C.Tipo = T.Id_Tipo_Consulta join Status_Consulta as S on C.Status = S.Id_Status_Consulta where Dt_Consulta = @data and Nome_Tipo_Consulta = @tipo and Nome_Status_Consulta = @status";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@data", data);
                m.Parameters.AddWithValue("@tipo", tipo);
                m.Parameters.AddWithValue("@status", status);

                var resultado = m.ExecuteReader();

                Consulta consulta = new Consulta();

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
                    else if (Convert.ToString(resultado["Tipo"]) == "2")
                    {
                        consulta.tipo = "Primeira Consulta";
                    }
                    else if (Convert.ToString(resultado["Tipo"]) == "3")
                    {
                        consulta.tipo = "Realizada";
                    }
                    else if (Convert.ToString(resultado["Tipo"]) == "4")
                    {
                        consulta.tipo = "Exames";
                    }
                    else if (Convert.ToString(resultado["Tipo"]) == "5")
                    {
                        consulta.tipo = "Marcação";
                    }

                    consulta.dt_consulta = Convert.ToString(resultado["Dt_Consulta"]);
                    consulta.hr_inicio = Convert.ToString(resultado["Hr_Inicio"]);
                    consulta.hr_fim = Convert.ToString(resultado["Hr_Fim"]);

                    lista.Add(consulta);

                }

                return lista;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        //refazer isso daqui
        public bool MarcarConsultaCartao(string cartao, string idConsulta)
        {

            dao = new ConexaoDAO();

            Paciente paciente = new Paciente();

            try
            {
                string readCommand = "Insert into Paciente values (@cpf, @nome, @dtnasc, @rua, @num, @estado, @cidade)";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@cpf", paciente.cpf);
                m.Parameters.AddWithValue("@nome", paciente.nome);
                m.Parameters.AddWithValue("@dtnasc", paciente.dt_nasc);
                m.Parameters.AddWithValue("@rua", paciente.rua);
                m.Parameters.AddWithValue("@num", paciente.numero);
                m.Parameters.AddWithValue("@estado", paciente.estado);
                m.Parameters.AddWithValue("@cidade", paciente.cidade);

                var resultado = m.ExecuteNonQuery();

                return true;

            }
            catch (Exception e)
            {
                return false;
            }

        }

        public List<Consulta> ListaResultadoBuscaCRM(string crm, string tipo, string status)
        {

            dao = new ConexaoDAO();

            List<Consulta> lista = new List<Consulta>();

            try
            {
                string readCommand = "SELECT * FROM Agenda as A join Consulta as C on A.Id_Consulta = C.Id_Consulta join Tipo_Consulta as T on " +
                    "C.Tipo = T.Id_Tipo_Consulta join Status_Consulta as S on C.Status = S.Id_Status_Consulta where Id_Funcionario = @crm and " +
                    "Nome_Tipo_Consulta = @tipo and Nome_Status_Consulta = @status";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@crm", crm);
                m.Parameters.AddWithValue("@tipo", tipo);
                m.Parameters.AddWithValue("@status", status);

                var resultado = m.ExecuteReader();

                Consulta consulta = new Consulta();

                while (resultado.Read())
                {
                    consulta.cpf = Convert.ToString(resultado["CPF_Paciente"]);
                    consulta.descricao = Convert.ToString(resultado["Descricao"]);
                    consulta.id_consulta = Convert.ToString(resultado["Id_Consulta"]);
                    consulta.id_medico = Convert.ToString(resultado["Id_Funcionario"]);

                    if (Convert.ToString(resultado["Status"]) == "1")
                    {
                        consulta.status = "Aberto";
                        consulta.tipo = " ";
                    }
                    else if (Convert.ToString(resultado["Status"]) == "2")
                    {
                        consulta.status = "Aguardando";
                    }
                    else if (Convert.ToString(resultado["Status"]) == "3")
                    {
                        consulta.status = "Realizada";
                    }

                    if (Convert.ToString(resultado["Tipo"]) == "2" && Convert.ToString(resultado["Status"]) != "1")
                    {
                        consulta.tipo = "Primeira Consulta";
                    }
                    else if (Convert.ToString(resultado["Tipo"]) == "3" && Convert.ToString(resultado["Status"]) != "1")
                    {
                        consulta.tipo = "Realizada";
                    }

                    consulta.dt_consulta = Convert.ToString(resultado["Dt_Consulta"]);
                    consulta.hr_inicio = Convert.ToString(resultado["Hr_Inicio"]);
                    consulta.hr_fim = Convert.ToString(resultado["Hr_Fim"]);

                    lista.Add(consulta);
                }


                return lista;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Consulta> ListaResultadoBusca(string tipo, string status)
        {

            dao = new ConexaoDAO();

            List<Consulta> lista = new List<Consulta>();

            try
            {
                string readCommand = "SELECT * FROM Agenda as A join Consulta as C on A.Id_Consulta = C.Id_Consulta join Tipo_Consulta as T on C.Tipo = T.Id_Tipo_Consulta join Status_Consulta as S on C.Status = S.Id_Status_Consulta where Nome_Tipo_Consulta = @tipo and Nome_Status_Consulta = @status";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@tipo", tipo);
                m.Parameters.AddWithValue("@status", status);

                var resultado = m.ExecuteReader();

                Consulta consulta = new Consulta();

                while (resultado.Read())
                {
                    consulta.cpf = Convert.ToString(resultado["CPF_Paciente"]);
                    consulta.descricao = Convert.ToString(resultado["Descricao"]);
                    consulta.id_consulta = Convert.ToString(resultado["Id_Consulta"]);
                    consulta.id_medico = Convert.ToString(resultado["Id_Funcionario"]);

                    if (Convert.ToString(resultado["Status"]) == "1")
                    {
                        consulta.status = "Aberto";
                        consulta.tipo = " ";
                    }
                    else if (Convert.ToString(resultado["Status"]) == "2")
                    {
                        consulta.status = "Aguardando";
                    }
                    else if (Convert.ToString(resultado["Status"]) == "3")
                    {
                        consulta.status = "Realizada";
                    }

                    if (Convert.ToString(resultado["Tipo"]) == "2" && Convert.ToString(resultado["Status"]) != "1")
                    {
                        consulta.tipo = "Primeira Consulta";
                    }
                    else if (Convert.ToString(resultado["Tipo"]) == "3" && Convert.ToString(resultado["Status"]) != "1")
                    {
                        consulta.tipo = "Realizada";
                    }



                    consulta.dt_consulta = Convert.ToString(resultado["Dt_Consulta"]);
                    consulta.hr_inicio = Convert.ToString(resultado["Hr_Inicio"]);
                    consulta.hr_fim = Convert.ToString(resultado["Hr_Fim"]);

                    lista.Add(consulta);
                }



                return lista;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool CadastrarAgenda(string data, string hora_inicio, string hora_fim, string crm)
        {

            dao = new ConexaoDAO();

            bool valor = false;

            try
            {
                //string readCommand = "Insert into Agenda values (@data, @hr_inicio, @hr_fim, @crm)";

                MySqlCommand m = new MySqlCommand("spagenda", dao.conexao);

                m.CommandType = CommandType.StoredProcedure;

                m.Parameters.AddWithValue("@data_agenda", data);
                m.Parameters.AddWithValue("@hora_inicio", data + " " + hora_inicio);
                m.Parameters.AddWithValue("@hora_fim", data + " " + hora_fim);
                m.Parameters.AddWithValue("@crm", crm);

                m.ExecuteNonQuery();

                return valor = true;

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);

                return valor = false;
            } 
            
        }

        public bool MarcarConsulta(string cpf, string nome, string id, string tipo)
        {

            dao = new ConexaoDAO();

            bool valor = false;

            try
            {
                string readCommand = "Update Consulta set Tipo = @tipo, Status = 2, CPF_Paciente = @cpf where Id_Consulta = @id;";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@cpf", cpf);
                m.Parameters.AddWithValue("@tipo", tipo);
                m.Parameters.AddWithValue("@id", id );
           
                var resultado = m.ExecuteNonQuery();

                return true;

            }
            catch (Exception e)
            {
                return false;
            }



        }

        public string BuscarTipoNome(string nome)
        {

            dao = new ConexaoDAO();

            string tipo = "";

            try
            {
                string readCommand = "select Id_Tipo_Consulta from Tipo_Consulta where Nome_Tipo_Consulta = @nome;";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@nome", nome);

                var resultado = m.ExecuteReader();

                while (resultado.Read())
                {
                    tipo = Convert.ToString(resultado["Id_Tipo_Consulta"]);
                }

                return tipo;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        public Paciente BuscarPacienteCartao(string cartao)
        {
            dao = new ConexaoDAO();

            string nome = "";

            Paciente paciente = new Paciente();

            try
            {
                string readCommand = "select * from Paciente where cartao = @cartao;";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);
                m.Parameters.AddWithValue("@cartao", cartao);

                var resultado = m.ExecuteReader();

                while (resultado.Read())
                {
                    paciente.cpf = Convert.ToString(resultado["CPF"]);
                    paciente.nome = Convert.ToString(resultado["Nome"]);

                }

                return paciente;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public bool MarcarConsultaCheckIn(string cpf, string nome, string id, string tipo)
        {

            dao = new ConexaoDAO();

            bool valor = false;

            try
            {
                string readCommand = "Update Consulta set Tipo = @tipo, Status = 3, CPF_Paciente = @cpf where Id_Consulta = @id;";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@cpf", cpf);
                m.Parameters.AddWithValue("@tipo", tipo);
                m.Parameters.AddWithValue("@id", id );
           
                var resultado = m.ExecuteNonQuery();

                return true;

            }
            catch (Exception e)
            {
                return false;
            }



        }

        public string BuscaTipoConsulta(string id)
        {
            dao = new ConexaoDAO();

            string tipo = "";

            try
            {
                string readCommand = "select Tipo from Consulta where Id_Consulta = @id;";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@id", id);

                var resultado = m.ExecuteReader();

                while (resultado.Read())
                {
                    tipo = Convert.ToString(resultado["Tipo"]);
                }

                return tipo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public List<Consulta> BuscaConsultasPorCPF(string cpf)
        {

            dao = new ConexaoDAO();

            List<Consulta> lista = new List<Consulta>();

            try
            {
                string readCommand = "SELECT * FROM Agenda as A join Consulta as C on A.Id_Consulta = C.Id_Consulta join Tipo_Consulta as T on " +
                    "C.Tipo = T.Id_Tipo_Consulta join Status_Consulta as S on C.Status = S.Id_Status_Consulta where CPF_Paciente = @cpf";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@cpf", cpf);
    
                var resultado = m.ExecuteReader();

             
                while (resultado.Read())
                {
                    Consulta consulta = new Consulta();

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
                    else if (Convert.ToString(resultado["Tipo"]) == "2")
                    {
                        consulta.tipo = "Primeira Consulta";
                    }
                    else if (Convert.ToString(resultado["Tipo"]) == "3")
                    {
                        consulta.tipo = "Realizada";
                    }
                    else if (Convert.ToString(resultado["Tipo"]) == "4")
                    {
                        consulta.tipo = "Exames";
                    }
                    else if (Convert.ToString(resultado["Tipo"]) == "5")
                    {
                        consulta.tipo = "Marcação";
                    }

                    consulta.dt_consulta = Convert.ToString(resultado["Dt_Consulta"]);
                    consulta.hr_inicio = Convert.ToString(resultado["Hr_Inicio"]);
                    consulta.hr_fim = Convert.ToString(resultado["Hr_Fim"]);

                    lista.Add(consulta);

                }

                return lista;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }
}
