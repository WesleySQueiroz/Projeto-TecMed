using MySql.Data.MySqlClient;
using ProjetoTecMed.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoTecMed.DAO
{
    public class ConfiguracaoDAO
    {
        ConexaoDAO dao = new ConexaoDAO();

        public void Dispose()
        {
            this.dao.Dispose();
        }

        public Usuario BuscarUsuarioDados(string id)
        {

            dao = new ConexaoDAO();

            Usuario usuario = new Usuario();

            try
            {
                string readCommand = "select * from Usuario where Id_Funcionario = @id;";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@id", id);

                var resultado = m.ExecuteReader();

                while (resultado.Read())
                {

                    usuario.id_user = Convert.ToString(resultado["Id_Funcionario"]);

                    usuario.nome = Convert.ToString(resultado["Nome"]);

                    usuario.dt_nasc = Convert.ToString(resultado["Dt_Nasc"]);

                    usuario.tipo = Convert.ToString(resultado["Tipo_Usuario"]);

                }

                return usuario;

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }



        }

        public bool AlterarSenha(string senha, string idusuario)
        {
            dao = new ConexaoDAO();

            try
            {
                string readCommand = "Update Usuario set senha = @senha where Id_Funcionario = @id;";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@senha", senha);

                m.Parameters.AddWithValue("@id", idusuario);

                m.ExecuteNonQuery();

                return true;
          
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public bool AlterarDadosUsuario(string nome, string data, string id)
        {

            dao = new ConexaoDAO();

            try
            {

                string readCommand = "Update Usuario set Nome = @nome, Dt_Nasc = @data where Id_Funcionario = @id;";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);

                m.Parameters.AddWithValue("@data", Convert.ToDateTime(data));

                m.Parameters.AddWithValue("@nome", nome);

                m.Parameters.AddWithValue("@id", id);

                m.ExecuteNonQuery();

                return true;

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }







        }

    }
}
