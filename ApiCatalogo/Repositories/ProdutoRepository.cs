using ApiCatalogo.Context;
using ApiCatalogo.Models;

namespace ApiCatalogo.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context)
        {
        }

        //Método especifico de IProdutoRepository
        public IEnumerable<Produto> GetPorCategorias(int idCategoria)
        {
            return GetAll().Where(p => p.CategoriaId == idCategoria);
        }
    }
}
