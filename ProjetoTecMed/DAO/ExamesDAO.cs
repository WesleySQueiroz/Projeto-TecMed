using MySql.Data.MySqlClient;
using ProjetoTecMed.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoTecMed.DAO
{
    public class ExamesDAO
    {
        ConexaoDAO dao;

        public void Dispose()
        {
            this.dao.Dispose();
        }

        public List<Exame> BuscarExames(string cpf)
        {

            dao = new ConexaoDAO();

            List<Exame> exames = new List<Exame>();

            try
            {
                string readCommand = "SELECT * FROM Exames where Id_Paciente = @cpf";

                MySqlCommand m = new MySqlCommand(readCommand, dao.conexao);
                m.Parameters.AddWithValue("@cpf", cpf);

                var resultado = m.ExecuteReader();

                Exame exame = new Exame();

                while (resultado.Read())
                {
                    exame.id_exame = Convert.ToString(resultado["Id_Exame"]);
                    exame.cpf = Convert.ToString(resultado["Id_Paciente"]);
                    exame.crm = Convert.ToString(resultado["Id_Medico"]);
                    exame.descricao = Convert.ToString(resultado["Descricao"]);

                    exames.Add(exame);
                }

                return exames;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }





        }




    }
}
