using System.Runtime.ConstrainedExecution;
using WebApiCurso.Model;
using WebApiCurso.ORM;

namespace WebApiCurso.Repositorio
{
    public class EnderecoRepositorio
    {
        private readonly CursosContext _context;

        public EnderecoRepositorio(CursosContext context)
        {
            _context = context;
        }

        // Adiciona um novo cliente
        public void Add(Endereco endereco)
        {
            var tbEndereco = new TbEndereco()
            {
                Id = endereco.Id,
                Logradouro = endereco.Logradouro,
                Cep = endereco.Cep,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                IdNavigation = endereco.IdNavigation,
                Nº = endereco.Nº,
                Pontodereferencia = endereco.Pontodereferencia,
            };

            // Adiciona a entidade ao contexto
            _context.TbEnderecos.Add(tbEndereco);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbEndereco = _context.TbEnderecos.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbEndereco != null)
            {
                // Remove a entidade do contexto
                _context.TbEnderecos.Remove(tbEndereco);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Endereço não encontrado.");
            }
        }

        public List<Endereco> GetAll()
        {
            List<Endereco> listFun = new List<Endereco>();

            var listTb = _context.TbEnderecos.ToList();

            foreach (var item in listTb)
            {
                var endereco = new Endereco
                {
                    Id = item.Id,
                    Logradouro = item.Logradouro,
                    Cep = item.Cep,
                    Cidade = item.Cidade,
                    Estado = item.Estado,
                    IdNavigation = item.IdNavigation,
                    Nº = item.Nº,
                    Pontodereferencia = item.Pontodereferencia,
                };

                listFun.Add(endereco);
            }

            return listFun;
        }

        public Endereco GetById(int id)
        {
            // Busca o funcionário pelo ID no banco de dados
            var item = _context.TbEnderecos.FirstOrDefault(f => f.Id == id);

            // Verifica se o funcionário foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var endereco = new Endereco
            {
                Id = item.Id,
                Logradouro = item.Logradouro,
                Cep = item.Cep,
                Cidade = item.Cidade,
                Estado = item.Estado,
                IdNavigation = item.IdNavigation,
                Nº = item.Nº,
                Pontodereferencia = item.Pontodereferencia,
            };

            return endereco; // Retorna o funcionário encontrado
        }

        public void Update(Endereco endereco)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbEndereco = _context.TbEnderecos.FirstOrDefault(f => f.Id == endereco.Id);

            // Verifica se a entidade foi encontrada
            if (tbEndereco != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbEndereco.Id = endereco.Id;
                tbEndereco.Logradouro = endereco.Logradouro;
                tbEndereco.Cep = endereco.Cep;
                tbEndereco.Cidade = endereco.Cidade;
                tbEndereco.Estado = endereco.Estado;
                tbEndereco.IdNavigation = endereco.IdNavigation;
                tbEndereco.Nº = endereco.Nº;
                tbEndereco.Pontodereferencia = endereco.Pontodereferencia;



                // Atualiza as informações no contexto
                _context.TbEnderecos.Update(tbEndereco);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Endereço não encontrado.");
            }
        }
    }
}
