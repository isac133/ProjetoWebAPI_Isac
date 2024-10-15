namespace WebApiCurso.Model
{
    public class ProdutoDto
    {

        public string Nome { get; set; } = null!;

        public decimal Preco { get; set; }

        public int Quanto { get; set; }

        public IFormFile NotaFiscal { get; set; }
    }
}
