using Entities.Concretes.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingDataAccess.Repositories.Concretes.OrdersRepos
{
    public class ProductsOrdersRepository
    {
        private Repository<ProductsOrders> productsOrdersRepo;
        public ProductsOrdersRepository()
        {
            productsOrdersRepo = new Repository<ProductsOrders>();
        }

        public void AddProductsOrder(ProductsOrders ProductsOrder)
        {
            productsOrdersRepo.AddProduct(ProductsOrder);
            
        }
        public void UpdateProductsOrder(ProductsOrders ProductsOrder)
        {
            productsOrdersRepo.UpdateProduct(ProductsOrder);
        }
        public void RemoveProductsOrder(ProductsOrders ProductsOrder)
        {
            productsOrdersRepo.UpdateProduct(ProductsOrder);
        }
        public void RemoveProductsOrderById(int id)
        {
            productsOrdersRepo.RemoveProductById(id);
        }

        //Querries
        public ICollection<ProductsOrders> GetAllProductsOrders()
        {
            return productsOrdersRepo.ProductDbSet.Include(po=>po.UsersAccountsIdNavigation).Include(po=>po.ProductEntityIdNavigation).ThenInclude(p=>p.CategoriesIdNavigation).ToList();
        }
        public ProductsOrders GetProductsOrderById(int id)
        {
            return productsOrdersRepo.GetProductById(id);
        }
    }
}
