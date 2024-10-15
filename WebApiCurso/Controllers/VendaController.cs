using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiCurso.Model;
using WebApiCurso.Repositorio;

namespace WebApiCurso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VendaController : ControllerBase
    {
        private readonly VendaRepositorio _vendaRepositorio;

        public VendaController(VendaRepositorio vendaRepo)
        {
            _vendaRepositorio = vendaRepo;
        }

        // GET: api/Funcionario/{id}/foto
        [HttpGet("{id}/notafiscal")]
        public IActionResult GetDoc(int id)
        {
            // Busca o funcionário pelo ID
            var venda = _vendaRepositorio.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (venda == null || venda.NotaFiscal == null)
            {
                return NotFound(new { Mensagem = "Nota Fiscal não encontrada." });
            }

            // Retorna a foto como um arquivo de imagem
            return File(venda.NotaFiscal, "image/jpeg"); // Ou "image/png" dependendo do formato
        }

        // GET: api/Funcionario
        [HttpGet]
        public ActionResult<List<Produto>> GetAll()
        {
            // Chama o repositório para obter todos os funcionários
            var vendas = _vendaRepositorio.GetAll();

            // Verifica se a lista de funcionários está vazia
            if (vendas == null || !vendas.Any())
            {
                return NotFound(new { Mensagem = "Nenhum produto encontrado." });
            }

            // Mapeia a lista de funcionários para incluir a URL da foto
            var listaComUrl = vendas.Select(venda => new Venda
            {
                Id = venda.Id,
                Valor = venda.Valor,
               
                UrlNotaFiscal = $"{Request.Scheme}://{Request.Host}/api/Produto/{venda.Id}/produto" // Define a URL completa para a imagem
            }).ToList();

            // Retorna a lista de funcionários com status 200 OK
            return Ok(listaComUrl);
        }

        // GET: api/Funcionario/{id}
        [HttpGet("{id}")]
        public ActionResult<Produto> GetById(int id)
        {
            // Chama o repositório para obter o funcionário pelo ID
            var venda = _vendaRepositorio.GetById(id);

            // Se o funcionário não for encontrado, retorna uma resposta 404
            if (venda == null)
            {
                return NotFound(new { Mensagem = "Produto não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o funcionário encontrado para incluir a URL da foto
            var vendaComUrl = new Venda
            {
                Id = venda.Id,
                Valor = venda.Valor,
              
                UrlNotaFiscal = $"{Request.Scheme}://{Request.Host}/api/Produto/{venda.Id}/produto" // Define a URL completa para a imagem
            };

            // Retorna o funcionário com status 200 OK
            return Ok(vendaComUrl);
        }


        // POST api/<FuncionarioController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] VendaDto novoVenda)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var venda = new Venda
            {

                Valor = novoVenda.Valor,

               
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _vendaRepositorio.Add(venda, novoVenda.NotaFiscal);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário cadastrado com sucesso!",
               
                Valor = venda.Valor,
               
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<FuncionarioController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] VendaDto vendaAtualizado)
        {
            // Busca o funcionário existente pelo Id
            var vendaExistente = _vendaRepositorio.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (vendaExistente == null)
            {
                return NotFound(new { Mensagem = "Produto não encontrado." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            vendaExistente.Valor = vendaAtualizado.Valor;
          

            // Chama o método de atualização do repositório, passando a nova foto
            _vendaRepositorio.Update(vendaExistente, vendaAtualizado.NotaFiscal);

            // Cria a URL da foto
            var urlNotaFiscal = $"{Request.Scheme}://{Request.Host}/api/Produto/{vendaExistente.Id}/notafiscal";

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Produto atualizado com sucesso!",
                Valor = vendaExistente.Valor,
               
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
            var vendaExistente = _vendaRepositorio.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (vendaExistente == null)
            {
                return NotFound(new { Mensagem = "Produto não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _vendaRepositorio.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Produto excluído com sucesso!",
                Valor = vendaExistente.Valor,
              
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
