using System.ComponentModel.DataAnnotations;

namespace ProjetoTecMed.Model
{
    public class Usuario
    {
        [Required]
        public string login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string senha { get; set; }

        public string nome { get; set; }

        public string telefone { get; set; }

        public string dt_nasc { get; set; }

        public string id_user { get; set; }

        public string tipo { get; set; }


    }
}
