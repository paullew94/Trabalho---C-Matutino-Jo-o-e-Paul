using Model;
using Repoisitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class CidadeController : Controller
    {
        private CidadeRepository repository;

        public CidadeController()
        {
            repository = new CidadeRepository();
        }
        // GET: Cidade
        public ActionResult Index()
        {
            List<Cidade> cidades = repository.ObterTodos();
            ViewBag.Cidades = cidades;
            return View();
        }
        public ActionResult Cadastro()
        {
            EstadoRepository estadoRepository = new EstadoRepository();
            List<Estado> estados = estadoRepository.ObterTodos();
            ViewBag.Estados = estados;

            return View();
        }
        public ActionResult Store(string nome, int estado, int numeroHabitantes)
        {
            Cidade cidade = new Cidade();
            cidade.Nome = nome;
            cidade.IdEstado = estado;
            cidade.NumeroHabitantes = numeroHabitantes;
            repository.Inserir(cidade);
            return RedirectToAction("Index");
        }
        public ActionResult Editar(int id)
        {
            Cidade cidade = repository.ObterPeloId(id);
            ViewBag.Cidade = cidade;

            EstadoRepository estadoRepository = new EstadoRepository();
            List<Estado> estados = estadoRepository.ObterTodos();
            ViewBag.Estados = estados;

            return View();
        }
        public ActionResult Update( int id,string nome, int estado, int numeroHabitantes)
        {
            Cidade cidade = new Cidade();
            cidade.Id = id;
            cidade.Nome = nome;
            cidade.IdEstado = estado;
            cidade.NumeroHabitantes = numeroHabitantes;
            repository.Alterar(cidade);
            return RedirectToAction("Index");
        }
        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }
    }
}