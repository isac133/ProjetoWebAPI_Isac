using WebApiCurso.Model;
using WebApiCurso.ORM;

namespace WebApiCurso.Repositorio
{
    public class VendaRepositorio
    {
        private readonly CursosContext _context;

        public VendaRepositorio(CursosContext context)
        {
            _context = context;
        }

        // Adiciona um novo cliente
        public void Add(Venda venda, IFormFile NotaFiscal)
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
            var tbVenda = new TbVenda()
            {
                Valor = venda.Valor,
                NotaFiscal = NotaFiscalBytes // Armazena a foto na entidade
            };

            // Adiciona a entidade ao contexto
            _context.TbVendas.Add(tbVenda);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbVenda = _context.TbVendas.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbVenda != null)
            {
                // Remove a entidade do contexto
                _context.TbVendas.Remove(tbVenda);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Venda não encontrada.");
            }
        }

        public List<Venda> GetAll()
        {
            List<Venda> listFun = new List<Venda>();

            var listTb = _context.TbVendas.ToList();

            foreach (var item in listTb)
            {
                var vendas = new Venda
                {
                    Valor = item.Valor,
                    NotaFiscal = item.NotaFiscal,
                };

                listFun.Add(vendas);
            }

            return listFun;
        }

        public Venda GetById(int id)
        {
            // Busca o funcionário pelo ID no banco de dados
            var venda = _context.TbVendas.FirstOrDefault(f => f.Id == id);

            // Verifica se o funcionário foi encontrado
            if (venda == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var vendas = new Venda
            {
                Id = venda.Id,
                NotaFiscal = venda.NotaFiscal,// Mantém o campo Foto como byte[]
            };

            return vendas; // Retorna o funcionário encontrado
        }

        public void Update(Venda venda, IFormFile NotaFiscal)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbVenda = _context.TbVendas.FirstOrDefault(f => f.Id == venda.Id);

            // Verifica se a entidade foi encontrada
            if (tbVenda != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbVenda.Valor = tbVenda.Valor;

                // Verifica se uma nova foto foi enviada
                if (NotaFiscal != null && NotaFiscal.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        NotaFiscal.CopyTo(memoryStream);
                        tbVenda.NotaFiscal = memoryStream.ToArray(); // Atualiza a foto na entidade
                    }
                }

                // Atualiza as informações no contexto
                _context.TbVendas.Update(tbVenda);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Venda não encontrado.");
            }
        }
    }
}
