using System.Linq.Expressions;

namespace ApiCatalogo.Repositories
{
    //Repositório genérico que será utilizado por todos os repositórios especficos
    public interface IRepository<T> 
    {
        IEnumerable<T> GetAll();

        //Get(c => c.categoriaId == id)
        T? Get(Expression<Func<T,bool>> predicate);

        T Post(T entity);

        T Put(T entity);

        T Delete(T entity);
    }
}
