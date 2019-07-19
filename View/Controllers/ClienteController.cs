using Model;
using Repoisitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ClienteController : Controller
    {
        private ClienteRepository repository;
        public ClienteController()
        {
            repository = new ClienteRepository();
        }

        // GET: Cliente
        public ActionResult Index()
        {
            List<Cliente> clientes = repository.ObterTodos();
            ViewBag.Clientes = clientes;
            return View();
        }
        public ActionResult Cadastro()
        {
            CidadeRepository cidadeRepository = new CidadeRepository();
            List<Cidade> cidades = cidadeRepository.ObterTodos();
            ViewBag.Cidades = cidades;

            return View();
        }
        public ActionResult Store(string nome, int cidade, string cpf, DateTime dataNascimento, string complemento, string logradouro, string cep,  int numero)
        {
            Cliente cliente = new Cliente();
            cliente.Nome = nome;
            cliente.IdCidade = cidade;
            cliente.Cpf = cpf;
            cliente.DataNascimento = dataNascimento;
            cliente.Complemento = complemento;
            cliente.Logradouro = logradouro;
            cliente.Cep = cep;
            cliente.Numero = numero;
            repository.Inserir(cliente);
            return RedirectToAction("Index");
        }
        public ActionResult Editar(int id)
        {
            Cliente cliente = repository.ObterPeloId(id);
            ViewBag.Cliente = cliente;

            CidadeRepository cidadeRepository = new CidadeRepository();
            List<Cidade> cidades = cidadeRepository.ObterTodos();
            ViewBag.Cidades = cidades;

            return View();
        }
        public ActionResult Update(int id, string nome, string cpf, DateTime dataNascimento, string complemento, string logradouro, string cep, int cidade, int numero)
        {
            Cliente cliente = new Cliente();
            cliente.Id = id;
            cliente.Nome = nome;
            cliente.IdCidade = cidade;
            cliente.Cpf = cpf;
            cliente.DataNascimento = dataNascimento;
            cliente.Complemento = complemento;
            cliente.Logradouro = logradouro;
            cliente.Cep = cep;
            cliente.Numero = numero;
            repository.Alterar(cliente);
            return RedirectToAction("Index");
        }
        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }
    }
}