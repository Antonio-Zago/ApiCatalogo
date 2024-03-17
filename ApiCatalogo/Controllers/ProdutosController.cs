using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(IUnitOfWork unitOfWork, IProdutoRepository produtoRepository)
        {
            _unitOfWork = unitOfWork;
            _produtoRepository = produtoRepository;
        }

        //Transformei o método em assincrono, porque possui uma operação de acessar o banco de dados
        //sendo assim, é um processo que não depende da minha aplicação
        [HttpGet]
        public  ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _unitOfWork.ProdutoRepository.GetAll();

            return Ok(produtos);
        }

        //Restrição no parametro para não aceitar valores menores que 1
        //O BindRequired torna o parametro da QueryString obrigatorio
        [HttpGet("{id:int:min(1)}", Name ="ObterProduto")]
        public ActionResult<Produto> Get(int id, [BindRequired]string nome)
        {

            var produto = _unitOfWork.ProdutoRepository.Get(p => p.ProdutoId == id);
            return Ok(produto);
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            _unitOfWork.ProdutoRepository.Post(produto);
            _unitOfWork.Commit();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId}, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id,Produto produto) 
        {
            _unitOfWork.ProdutoRepository.Put(produto);
            _unitOfWork.Commit();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id) 
        {
            var produto = _unitOfWork.ProdutoRepository.Get(p => p.ProdutoId == id);

            _unitOfWork.ProdutoRepository.Delete(produto);
            _unitOfWork.Commit();

            return Ok(produto);
        }

        [HttpGet("categorias/{id}")]
        public ActionResult<IEnumerable<Produto>> GetProdutosPorCategoria(int id)
        {
            var produtos = _produtoRepository.GetPorCategorias(id);

            return Ok(produtos);
        }


    }
}
