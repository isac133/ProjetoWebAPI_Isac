using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiCurso.Model;
using WebApiCurso.Repositorio;

namespace WebApiCurso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoRepositorio _produtoRepositorio;

        public ProdutoController(ProdutoRepositorio produtoRepo)
        {
            _produtoRepositorio = produtoRepo;
        }

        // GET: api/Funcionario/{id}/foto
        [HttpGet("{id}/notafiscal")]
        public IActionResult GetDoc(int id)
        {
            // Busca o funcionário pelo ID
            var produto = _produtoRepositorio.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (produto == null || produto.NotaFiscal == null)
            {
                return NotFound(new { Mensagem = "Nota Fiscal não encontrada." });
            }

            // Retorna a foto como um arquivo de imagem
            return File(produto.NotaFiscal, "image/jpeg"); // Ou "image/png" dependendo do formato
        }

        // GET: api/Funcionario
        [HttpGet]
        public ActionResult<List<Produto>> GetAll()
        {
            // Chama o repositório para obter todos os funcionários
            var produtos = _produtoRepositorio.GetAll();

            // Verifica se a lista de funcionários está vazia
            if (produtos == null || !produtos.Any())
            {
                return NotFound(new { Mensagem = "Nenhum produto encontrado." });
            }

            // Mapeia a lista de funcionários para incluir a URL da foto
            var listaComUrl = produtos.Select(produto => new Produto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Quanto = produto.Quanto,
                UrlNotaFiscal = $"{Request.Scheme}://{Request.Host}/api/Produto/{produto.Id}/produto" // Define a URL completa para a imagem
            }).ToList();

            // Retorna a lista de funcionários com status 200 OK
            return Ok(listaComUrl);
        }

        // GET: api/Funcionario/{id}
        [HttpGet("{id}")]
        public ActionResult<Produto> GetById(int id)
        {
            // Chama o repositório para obter o funcionário pelo ID
            var produto = _produtoRepositorio.GetById(id);

            // Se o funcionário não for encontrado, retorna uma resposta 404
            if (produto == null)
            {
                return NotFound(new { Mensagem = "Produto não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o funcionário encontrado para incluir a URL da foto
            var produtoComUrl = new Produto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Quanto = produto.Quanto,
                UrlNotaFiscal = $"{Request.Scheme}://{Request.Host}/api/Produto/{produto.Id}/produto" // Define a URL completa para a imagem
            };

            // Retorna o funcionário com status 200 OK
            return Ok(produtoComUrl);
        }


        // POST api/<FuncionarioController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] ProdutoDto novoProduto)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var produto = new Produto
            {
                Nome = novoProduto.Nome,
                Preco = novoProduto.Preco,
                Quanto = novoProduto.Quanto,
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _produtoRepositorio.Add(produto, novoProduto.NotaFiscal);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário cadastrado com sucesso!",
                Nome = produto.Nome,
                Preco = produto.Preco,
                Quanto = produto.Quanto,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<FuncionarioController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] ProdutoDto produtoAtualizado)
        {
            // Busca o funcionário existente pelo Id
            var produtoExistente = _produtoRepositorio.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (produtoExistente == null)
            {
                return NotFound(new { Mensagem = "Produto não encontrado." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            produtoExistente.Nome = produtoAtualizado.Nome;
            produtoExistente.Preco = produtoAtualizado.Preco;
            produtoExistente.Quanto = produtoAtualizado.Quanto;

            // Chama o método de atualização do repositório, passando a nova foto
            _produtoRepositorio.Update(produtoExistente, produtoAtualizado.NotaFiscal);

            // Cria a URL da foto
            var urlNotaFiscal = $"{Request.Scheme}://{Request.Host}/api/Produto/{produtoExistente.Id}/notafiscal";

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Produto atualizado com sucesso!",
                Nome = produtoExistente.Nome,
                Preco = produtoExistente.Preco,
                Quanto = produtoExistente.Quanto,
                UrlNotaFiscal = urlNotaFiscal // Inclui a URL da foto na resposta
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }


        // DELETE api/<FuncionarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o funcionário existente pelo Id
            var produtoExistente = _produtoRepositorio.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (produtoExistente == null)
            {
                return NotFound(new { Mensagem = "Produto não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _produtoRepositorio.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Produto excluído com sucesso!",
                Nome = produtoExistente.Nome,
                Preco = produtoExistente.Preco,
                Quanto = produtoExistente.Quanto,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
