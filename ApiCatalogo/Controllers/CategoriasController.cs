using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CategoriasController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            var categorias = _appDbContext.Categorias.Include(p => p.Produtos).ToList();

            if (categorias == null)
            {
                return NotFound("Categorias não encontradas");
            }
            return categorias;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            //AsNoTracking é uma boa prática porque melhora o desempenho visto que a entidade não será rastreada
            //Importante lembra que deve-se utilizar isso quando temos certeza que não precisamos alterar essa entidade da consulta
            var categorias = _appDbContext.Categorias.AsNoTracking().ToList();

            if (categorias == null)
            {
                return NotFound("Categorias não encontradas");
            }
            return categorias;
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _appDbContext.Categorias.AsNoTracking().Where(p => p.CategoriaId == id).FirstOrDefault();

            if (categoria == null)
            {
                return NotFound("Categoria não encontrado");
            }
            return categoria;
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            _appDbContext.Categorias.Add(categoria);

            _appDbContext.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }

            _appDbContext.Entry(categoria).State = EntityState.Modified;
            _appDbContext.SaveChanges();

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _appDbContext.Categorias.FirstOrDefault(a => a.CategoriaId == id);

            if (categoria == null)
            {
                return NotFound();
            }

            _appDbContext.Categorias.Remove(categoria);
            _appDbContext.SaveChanges();

            return Ok(categoria);
        }
    }
}
