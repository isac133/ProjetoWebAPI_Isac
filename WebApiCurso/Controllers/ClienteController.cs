using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiCurso.Model;
using WebApiCurso.Repositorio;

namespace WebApiCurso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteRepositorio _clienteRepositorio;

        public ClienteController(ClienteRepositorio clienteRepo)
        {
            _clienteRepositorio = clienteRepo;
        }

        // GET: api/Funcionario/{id}/foto
        [HttpGet("{id}/foto")]
        public IActionResult GetDoc(int id)
        {
            // Busca o funcionário pelo ID
            var cliente = _clienteRepositorio.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (cliente == null || cliente.DocDocumentacao == null)
            {
                return NotFound(new { Mensagem = "Documentação não encontrada." });
            }

            // Retorna a foto como um arquivo de imagem
            return File(cliente.DocDocumentacao, "image/jpeg"); // Ou "image/png" dependendo do formato
        }

        // GET: api/Funcionario
        [HttpGet]
        public ActionResult<List<Cliente>> GetAll()
        {
            // Chama o repositório para obter todos os funcionários
            var clientes = _clienteRepositorio.GetAll();

            // Verifica se a lista de funcionários está vazia
            if (clientes == null || !clientes.Any())
            {
                return NotFound(new { Mensagem = "Nenhum cliente encontrado." });
            }

            // Mapeia a lista de funcionários para incluir a URL da foto
            var listaComUrl = clientes.Select(cliente => new Cliente
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                UrlDocDocumentacao = $"{Request.Scheme}://{Request.Host}/api/Cliente/{cliente.Id}/documento" // Define a URL completa para a imagem
            }).ToList();

            // Retorna a lista de funcionários com status 200 OK
            return Ok(listaComUrl);
        }

        // GET: api/Funcionario/{id}
        [HttpGet("{id}")]
        public ActionResult<Cliente> GetById(int id)
        {
            // Chama o repositório para obter o funcionário pelo ID
            var cliente = _clienteRepositorio.GetById(id);

            // Se o funcionário não for encontrado, retorna uma resposta 404
            if (cliente == null)
            {
                return NotFound(new { Mensagem = "Funcionário não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o funcionário encontrado para incluir a URL da foto
            var clienteComUrl = new Cliente
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                UrlDocDocumentacao = $"{Request.Scheme}://{Request.Host}/api/Cliente/{cliente.Id}/documento" // Define a URL completa para a imagem
            };

            // Retorna o funcionário com status 200 OK
            return Ok(clienteComUrl);
        }


        // POST api/<FuncionarioController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] ClienteDto novoCliente)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var cliente = new Cliente
            {
                Nome = novoCliente.Nome,
                Telefone = novoCliente.Telefone,
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _clienteRepositorio.Add(cliente, novoCliente.DocDocumentacao);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário cadastrado com sucesso!",
                Nome = cliente.Nome,
                Telefone = cliente.Telefone
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<FuncionarioController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] ClienteDto clienteAtualizado)
        {
            // Busca o funcionário existente pelo Id
            var clienteExistente = _clienteRepositorio.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (clienteExistente == null)
            {
                return NotFound(new { Mensagem = "Cliente não encontrado." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            clienteExistente.Nome = clienteAtualizado.Nome;
            clienteExistente.Telefone = clienteAtualizado.Telefone;

            // Chama o método de atualização do repositório, passando a nova foto
            _clienteRepositorio.Update(clienteExistente, clienteAtualizado.DocDocumentacao);

            // Cria a URL da foto
            var urlDocDocumentacao = $"{Request.Scheme}://{Request.Host}/api/Cliente/{clienteExistente.Id}/documento";

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário atualizado com sucesso!",
                Nome = clienteExistente.Nome,
                Telefone = clienteExistente.Telefone,
                UrlFoto = urlDocDocumentacao // Inclui a URL da foto na resposta
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }


        // DELETE api/<FuncionarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o funcionário existente pelo Id
            var clienteExistente = _clienteRepositorio.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (clienteExistente == null)
            {
                return NotFound(new { Mensagem = "Cliente não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _clienteRepositorio.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário excluído com sucesso!",
                Nome = clienteExistente.Nome,
                Telefone = clienteExistente.Telefone,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
