using WebApiCurso.ORM;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApiCurso.Model // Mude para o namespace correto
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public string Telefone { get; set; } = null!;
        [JsonIgnore]
        public byte[]? DocDocumentacao { get; set; }

        [JsonIgnore] // Ignora a serialização deste campo
        public string? DocDocumentacaoBase64 => DocDocumentacao != null ? Convert.ToBase64String(DocDocumentacao) : null;

        public string UrlDocDocumentacao { get; set; } // Certifique-se de que esta propriedade esteja visível
    }
}
