using ApiCatalogo.Context;
using ApiCatalogo.Filters;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public CategoriasController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }


        //Lendo arquivo de configuração appsettings.development.json
        [HttpGet("lerArquivoConfiguracao")]
        public string GetValores()
        {

            var chave1 = _configuration["chave1"];
            var chave2 = _configuration["chave2"];
            return chave1 + chave2;
        }   

        [HttpGet]
        //Filtro aplicado ao controlador, sendo possível definir código antes e depois da ação ser executada
        [ServiceFilter(typeof(ApiLoggiingFilter))]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            //AsNoTracking é uma boa prática porque melhora o desempenho visto que a entidade não será rastreada
            //Importante lembra que deve-se utilizar isso quando temos certeza que não precisamos alterar essa entidade da consulta
            //var categorias = _appDbContext.Categorias.AsNoTracking().ToList();

            var categorias = _unitOfWork.CategoriaRepository.GetAll();

            return Ok(categorias);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _unitOfWork.CategoriaRepository.Get(c => c.CategoriaId == id);

            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            _unitOfWork.CategoriaRepository.Post(categoria);
            _unitOfWork.Commit();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            _unitOfWork.CategoriaRepository.Put(categoria);
            _unitOfWork.Commit();

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _unitOfWork.CategoriaRepository.Get(c => c.CategoriaId == id);

            if (categoria == null)
            {
                return NotFound();
            }

            _unitOfWork.CategoriaRepository.Delete(categoria);
            _unitOfWork.Commit();

            return Ok(categoria);
        }
    }
}
