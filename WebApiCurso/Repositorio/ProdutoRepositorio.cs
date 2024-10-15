using Microsoft.EntityFrameworkCore;
using WebApiCurso.Model;
using WebApiCurso.ORM;

namespace WebApiCurso.Repositorio
{
    public class ProdutoRepositorio
    {
        private readonly CursosContext _context;

        public ProdutoRepositorio(CursosContext context)
        {
            _context = context;
        }

        // Adiciona um novo cliente
        public void Add(Produto produto, IFormFile NotaFiscal)
        {
            // Verifica se uma foto foi enviada
            byte[] NotaFiscalBytes = null;
            if (NotaFiscal != null && NotaFiscal.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    NotaFiscal.CopyTo(memoryStream);
                    NotaFiscalBytes = memoryStream.ToArray();
                }
            }

            // Cria uma nova entidade do tipo TbFuncionario a partir do objeto Funcionario recebido
            var tbProduto = new TbProduto()
            {
                Nome = produto.Nome,
                Preco = produto.Preco,
                Quanto = produto.Quanto,
                NotaFiscal = NotaFiscalBytes // Armazena a foto na entidade
            };

            // Adiciona a entidade ao contexto
            _context.TbProdutos.Add(tbProduto);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbProduto = _context.TbProdutos.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbProduto != null)
            {
                // Remove a entidade do contexto
                _context.TbProdutos.Remove(tbProduto);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Funcionário não encontrado.");
            }
        }

        public List<Produto> GetAll()
        {
            List<Produto> listFun = new List<Produto>();

            var listTb = _context.TbProdutos.ToList();

            foreach (var item in listTb)
            {
                var produtos = new Produto
                {
                    Nome = item.Nome,
                    Preco = item.Preco,
                    NotaFiscal = item.NotaFiscal,
                };

                listFun.Add(produtos);
            }

            return listFun;
        }

        public Produto GetById(int id)
        {
            // Busca o funcionário pelo ID no banco de dados
            var produto = _context.TbProdutos.FirstOrDefault(f => f.Id == id);

            // Verifica se o funcionário foi encontrado
            if (produto == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var produtos = new Produto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Quanto = produto.Quanto,
                NotaFiscal = produto.NotaFiscal,// Mantém o campo Foto como byte[]
            };

            return produtos; // Retorna o funcionário encontrado
        }

        public void Update(Produto produto, IFormFile NotaFiscal)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbProduto = _context.TbProdutos.FirstOrDefault(f => f.Id == produto.Id);

            // Verifica se a entidade foi encontrada
            if (tbProduto != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbProduto.Nome = produto.Nome;
                tbProduto.Preco = produto.Preco;

                // Verifica se uma nova foto foi enviada
                if (NotaFiscal != null && NotaFiscal.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        NotaFiscal.CopyTo(memoryStream);
                        tbProduto.NotaFiscal = memoryStream.ToArray(); // Atualiza a foto na entidade
                    }
                }

                // Atualiza as informações no contexto
                _context.TbProdutos.Update(tbProduto);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Funcionário não encontrado.");
            }
        }
    }

}

