using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoTecMed.Model
{
    public class Consulta
    {
        public string cpf { get; set; }

        public string id_consulta { get; set; }

        public string id_medico { get; set; }

        public string status { get; set; }

        public string tipo { get; set; }

        public string descricao { get; set; }

        public string hr_inicio { get; set; }

        public string hr_fim { get; set; }

        public string dt_consulta { get; set; }

    }
}
