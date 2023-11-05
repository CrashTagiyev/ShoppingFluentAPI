using Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingDataAccess.Repositories.Abstracts.ProductRepositories
{
    public interface IQuerryRepository<T> where T : BaseEntity, new()
    {
        //Query
        ICollection<T> GetAllProducts();
        T GetProductById(int id);


    }
}
