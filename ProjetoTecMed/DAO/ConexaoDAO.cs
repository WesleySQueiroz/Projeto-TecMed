using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoTecMed.DAO
{ 
    public class ConexaoDAO
    {

        public MySqlConnection conexao;


        public ConexaoDAO()
        {
            this.conexao = new MySqlConnection("Server=35.194.14.243;Port=3306;Database=TECMEDBD;UID=admin;PWD=;");

            this.conexao.Open();
        }
        public void Dispose()
        {
            this.conexao.Close();
        }

    }
}
