namespace WebApiCurso.Model
{
    public class ClienteDto
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public IFormFile DocDocumentacao { get; set; } // Campo para receber a foto
    }
}
