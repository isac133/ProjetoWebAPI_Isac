using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApiCurso.Model;
using WebApiCurso.ORM;

namespace WebApiCurso.Repositorio
{
    public class ClienteRepositorio
    {
        private readonly CursosContext _context;

        public ClienteRepositorio(CursosContext context)
        {
            _context = context;
        }

        // Adiciona um novo cliente
        public void Add(Cliente cliente, IFormFile DocDocumentacao)
        {
            // Verifica se uma foto foi enviada
            byte[] DocDocumentacaoBytes = null;
            if (DocDocumentacao != null && DocDocumentacao.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    DocDocumentacao.CopyTo(memoryStream);
                    DocDocumentacaoBytes = memoryStream.ToArray();
                }
            }

            // Cria uma nova entidade do tipo TbFuncionario a partir do objeto Funcionario recebido
            var tbCliente = new TbCliente()
            {
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                DocDocumentacao = DocDocumentacaoBytes // Armazena a foto na entidade
            };

            // Adiciona a entidade ao contexto
            _context.TbClientes.Add(tbCliente);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbCliente = _context.TbClientes.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbCliente != null)
            {
                // Remove a entidade do contexto
                _context.TbClientes.Remove(tbCliente);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Funcionário não encontrado.");
            }
        }

        public List<Cliente> GetAll()
        {
            List<Cliente> listFun = new List<Cliente>();

            var listTb = _context.TbClientes.ToList();

            foreach (var item in listTb)
            {
                var funcionario = new Cliente
                {
                    Nome = item.Nome,
                    Telefone = item.Telefone,
                    DocDocumentacao = item.DocDocumentacao,
                };

                listFun.Add(funcionario);
            }

            return listFun;
        }

        public Cliente GetById(int id)
        {
            // Busca o funcionário pelo ID no banco de dados
            var item = _context.TbClientes.FirstOrDefault(f => f.Id == id);

            // Verifica se o funcionário foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var funcionario = new Cliente
            {
                Id = item.Id,
                Nome = item.Nome,
                Telefone = item.Telefone,
                DocDocumentacao = item.DocDocumentacao,// Mantém o campo Foto como byte[]
            };

            return funcionario; // Retorna o funcionário encontrado
        }

        public void Update(Cliente cliente, IFormFile DocDocumentacao)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbCliente = _context.TbClientes.FirstOrDefault(f => f.Id == cliente.Id);

            // Verifica se a entidade foi encontrada
            if (tbCliente != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbCliente.Nome = cliente.Nome;
                tbCliente.Telefone = cliente.Telefone;

                // Verifica se uma nova foto foi enviada
                if (DocDocumentacao != null && DocDocumentacao.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        DocDocumentacao.CopyTo(memoryStream);
                        tbCliente.DocDocumentacao = memoryStream.ToArray(); // Atualiza a foto na entidade
                    }
                }

                // Atualiza as informações no contexto
                _context.TbClientes.Update(tbCliente);

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
