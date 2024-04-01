using ApiCatalogo.Context;
using ApiCatalogo.Dtos;
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
        public ActionResult<IEnumerable<CategoriaDto>> Get()
        {
            //AsNoTracking é uma boa prática porque melhora o desempenho visto que a entidade não será rastreada
            //Importante lembra que deve-se utilizar isso quando temos certeza que não precisamos alterar essa entidade da consulta
            //var categorias = _appDbContext.Categorias.AsNoTracking().ToList();

            var categorias = _unitOfWork.CategoriaRepository.GetAll();

            return Ok(categorias);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<CategoriaDto> Get(int id)
        {
            var categoria = _unitOfWork.CategoriaRepository.Get(c => c.CategoriaId == id);

            var categoriaDto = new CategoriaDto()
            {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                ImagemUrl = categoria.ImagemUrl
            };

            return Ok(categoriaDto);
        }

        [HttpPost]
        public ActionResult<CategoriaDto> Post(CategoriaDto categoriaDto)
        {
            var categoria = new Categoria() {
                CategoriaId = categoriaDto.CategoriaId,
                Nome = categoriaDto.Nome,
                ImagemUrl = categoriaDto.ImagemUrl
        };
            

            _unitOfWork.CategoriaRepository.Post(categoria);
            _unitOfWork.Commit();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaDto.CategoriaId }, categoriaDto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Categoria> Put(int id, Categoria categoria)
        {
            _unitOfWork.CategoriaRepository.Put(categoria);
            _unitOfWork.Commit();

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> Delete(int id)
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
