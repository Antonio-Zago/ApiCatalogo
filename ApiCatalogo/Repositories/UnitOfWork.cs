using ApiCatalogo.Context;

namespace ApiCatalogo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProdutoRepository _produtoRepository;

        private ICategoriaRepository _categoriaRepository;

        public AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IProdutoRepository ProdutoRepository
        {
            get { 
                return _produtoRepository = _produtoRepository ?? new ProdutoRepository(_appDbContext); 
            }
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _categoriaRepository = _categoriaRepository ?? new CategoriaRepository(_appDbContext);
            }
        }

        public void Commit()
        {
            _appDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}
