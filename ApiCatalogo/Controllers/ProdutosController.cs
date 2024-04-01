using ApiCatalogo.Context;
using ApiCatalogo.Dtos;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProdutosController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //Transformei o método em assincrono, porque possui uma operação de acessar o banco de dados
        //sendo assim, é um processo que não depende da minha aplicação
        [HttpGet]
        public  ActionResult<IEnumerable<ProdutoDto>> Get()
        {
            var produtos = _unitOfWork.ProdutoRepository.GetAll();

            var produtosDto = _mapper.Map<IEnumerable<ProdutoDto>>(produtos);

            return Ok(produtosDto);
        }

        //Restrição no parametro para não aceitar valores menores que 1
        //O BindRequired torna o parametro da QueryString obrigatorio
        [HttpGet("{id:int:min(1)}", Name ="ObterProduto")]
        public ActionResult<Produto> Get(int id, [BindRequired]string nome)
        {

            var produto = _unitOfWork.ProdutoRepository.Get(p => p.ProdutoId == id);

            var produtoDto = _mapper.Map<ProdutoDto>(produto);

            return Ok(produto);
        }

        [HttpPost]
        public ActionResult<Produto> Post(Produto produto)
        {

            //var produto = _mapper.Map<Produto>(produtoDto);

            _unitOfWork.ProdutoRepository.Post(produto);
            _unitOfWork.Commit();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId}, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ProdutoDto> Put(int id, ProdutoDto produtoDto) 
        {
            var produto = _mapper.Map<Produto>(produtoDto);

            _unitOfWork.ProdutoRepository.Put(produto);
            _unitOfWork.Commit();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ProdutoDto> Delete(int id) 
        {
            var produto = _unitOfWork.ProdutoRepository.Get(p => p.ProdutoId == id);

            _unitOfWork.ProdutoRepository.Delete(produto);
            _unitOfWork.Commit();

            var produtoDto = _mapper.Map<ProdutoDto>(produto);

            return Ok(produtoDto);
        }

        [HttpGet("categorias/{id}")]
        public ActionResult<IEnumerable<ProdutoDto>> GetProdutosPorCategoria(int id)
        {
            var produtos = _unitOfWork.ProdutoRepository.GetPorCategorias(id);

            var produtosDto = _mapper.Map<IEnumerable<ProdutoDto>>(produtos);

            return Ok(produtosDto);
        }


    }
}
