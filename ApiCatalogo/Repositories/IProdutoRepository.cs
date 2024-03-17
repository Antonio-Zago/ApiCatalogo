using ApiCatalogo.Models;

namespace ApiCatalogo.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        IEnumerable<Produto> GetPorCategorias(int idCategoria);
    }
}
