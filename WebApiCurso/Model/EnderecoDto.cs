using WebApiCurso.ORM;

namespace WebApiCurso.Model
{
    public class EnderecoDto
    {       

        public string Logradouro { get; set; } = null!;

        public string Cidade { get; set; } = null!;

        public string Estado { get; set; } = null!;

        public string Cep { get; set; } = null!;

        public string Pontodereferencia { get; set; } = null!;

        public int Nº { get; set; }

        public int FkCliente { get; set; }
        
    }
}
