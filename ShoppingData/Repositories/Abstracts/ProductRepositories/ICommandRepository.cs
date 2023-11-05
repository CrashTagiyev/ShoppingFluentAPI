using Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingDataAccess.Repositories.Abstracts.ProductRepositories
{
    internal interface ICommandRepository<T> where T : BaseEntity,new()
    {
        //Commands
        void AddProduct(T product);
        void UpdateProduct(T product);
        void RemoveProduct(T product);
        void RemoveProductById(int id);
        void SaveChanges();
    }
}
