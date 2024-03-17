using ApiCatalogo.Context;
using ApiCatalogo.Models;
using System.Linq.Expressions;

namespace ApiCatalogo.Repositories
{
    //Classe concreta herda da classe concreta generiaca de repository e implementa interfacce de ICategoriaRepository
    //Nesse caso não precidei implementar os métodos de ICategoriaRepository porque já estão implementados na classe herdada
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context)
        {
        }

        
    }
}
