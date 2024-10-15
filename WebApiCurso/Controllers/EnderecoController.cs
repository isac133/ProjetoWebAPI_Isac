using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using WebApiCurso.Model;
using WebApiCurso.ORM;
using WebApiCurso.Repositorio;

namespace WebApiCurso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnderecoController : ControllerBase
    {
        private readonly EnderecoRepositorio _enderecoRepositorio;

        public EnderecoController(EnderecoRepositorio enderecoRepo)
        {
            _enderecoRepositorio = enderecoRepo;
        }


        // GET: api/Funcionario
        [HttpGet]
        public ActionResult<List<Endereco>> GetAll()
        {
            // Chama o repositório para obter todos os funcionários
            var enderecos = _enderecoRepositorio.GetAll();

            // Verifica se a lista de funcionários está vazia
            if (enderecos == null || !enderecos.Any())
            {
                return NotFound(new { Mensagem = "Nenhum endereço encontrado." });
            }

            // Mapeia a lista de funcionários para incluir a URL da foto
            var listaComUrl = enderecos.Select(endereco => new Endereco
            {
                Id = endereco.Id,
                Logradouro = endereco.Logradouro,
                Cep = endereco.Cep,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                FkCliente = endereco.FkCliente,
                IdNavigation = endereco.IdNavigation,
                Nº = endereco.Nº,
                Pontodereferencia = endereco.Pontodereferencia,

            }).ToList();

            // Retorna a lista de funcionários com status 200 OK
            return Ok(listaComUrl);
        }

        // GET: api/Funcionario/{id}
        [HttpGet("{id}")]
        public ActionResult<Endereco> GetById(int id)
        {
            // Chama o repositório para obter o funcionário pelo ID
            var endereco = _enderecoRepositorio.GetById(id);

            // Se o funcionário não for encontrado, retorna uma resposta 404
            if (endereco == null)
            {
                return NotFound(new { Mensagem = "Funcionário não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o funcionário encontrado para incluir a URL da foto
            var enderecoComUrl = new Endereco
            {
                Id = endereco.Id,
                Logradouro = endereco.Logradouro,
                Cep = endereco.Cep,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                FkCliente = endereco.FkCliente,
                IdNavigation = endereco.IdNavigation,
                Nº = endereco.Nº,
                Pontodereferencia = endereco.Pontodereferencia,
            };

            // Retorna o funcionário com status 200 OK
            return Ok(enderecoComUrl);
        }


        // POST api/<FuncionarioController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] EnderecoDto novoEndereco)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var endereco = new Endereco
            {
                
                Logradouro = novoEndereco.Logradouro,
                Cep = novoEndereco.Cep,
                Cidade = novoEndereco.Cidade,
                Estado =novoEndereco.Estado,                
                Nº = novoEndereco.Nº,
                Pontodereferencia = novoEndereco.Pontodereferencia,
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            // _enderecoRepositorio.Add(cliente, novoCliente.DocDocumentacao);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Endereço cadastrado com sucesso!",
                Id = endereco.Id,
                Logradouro = endereco.Logradouro,
                Cep = endereco.Cep,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                IdNavigation = endereco.IdNavigation,
                Nº = endereco.Nº,
                Pontodereferencia = endereco.Pontodereferencia,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<FuncionarioController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] EnderecoDto enderecoAtualizado)
        {
            // Busca o funcionário existente pelo Id
            var enderecoExistente = _enderecoRepositorio.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (enderecoExistente == null)
            {
                return NotFound(new { Mensagem = "Endereço não encontrado." });
            }

            
            enderecoExistente.Logradouro = enderecoAtualizado.Logradouro;
            enderecoExistente.Cep = enderecoAtualizado.Cep;
            enderecoExistente.Cidade = enderecoAtualizado.Cidade;
            enderecoExistente.Estado = enderecoAtualizado.Estado;           
            enderecoExistente.Nº = enderecoAtualizado.Nº;
            enderecoExistente.Pontodereferencia = enderecoAtualizado.Pontodereferencia;

            // Chama o método de atualização do repositório, passando a nova foto
            // _enderecoRepositorio.Update(enderecoExistente, enderecoAtualizado.DocDocumentacao);


            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário atualizado com sucesso!",
                Id = enderecoExistente.Id,
                Logradouro = enderecoExistente.Logradouro,
                Cep = enderecoExistente.Cep,
                Cidade = enderecoExistente.Cidade,
                Estado = enderecoExistente.Estado,
                IdNavigation = enderecoExistente.IdNavigation,
                Nº = enderecoExistente.Nº,
                Pontodereferencia = enderecoExistente.Pontodereferencia,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }


        // DELETE api/<FuncionarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o funcionário existente pelo Id
            var enderecoExistente = _enderecoRepositorio.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (enderecoExistente == null)
            {
                return NotFound(new { Mensagem = "Endereco não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _enderecoRepositorio.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Endereço excluído com sucesso!",
                Id = enderecoExistente.Id,
                Logradouro = enderecoExistente.Logradouro,
                Cep = enderecoExistente.Cep,
                Cidade = enderecoExistente.Cidade,
                Estado = enderecoExistente.Estado,                
                Nº = enderecoExistente.Nº,
                Pontodereferencia = enderecoExistente.Pontodereferencia,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }

}
