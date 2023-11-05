using Entities.Concretes.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingDataAccess.Repositories.Concretes.ProductReps
{
    public class ProductEntityRepository
    {
        private readonly Repository<ProductEntity> productEntityRep;
        public ProductEntityRepository()
        {
            productEntityRep = new();
        }
        
        //Commands
        public void AddProductEntity(ProductEntity ProductEntity)
        {
            productEntityRep.AddProduct(ProductEntity);
        }
        public void UpdateProductEntity(ProductEntity ProductEntity)
        {
            productEntityRep.UpdateProduct(ProductEntity);
        }
        public void RemoveProductEntity(ProductEntity ProductEntity)
        {
            productEntityRep.UpdateProduct(ProductEntity);
        }
        public void RemoveProductEntityById(int id)
        {
            productEntityRep.RemoveProductById(id);
        }
        
        //Querries
        public ICollection<ProductEntity> GetAllProductEntities()
        {
            return productEntityRep.ProductDbSet.Include(p=>p.CategoriesIdNavigation).ToList();
        }
        public ProductEntity GetProductEntityById(int id)
        {
            return productEntityRep.GetProductById(id);
        }
     
        public ICollection<ProductEntity> GetProductEntityByWord(string word)
        {
            return productEntityRep.ProductDbSet.Where(e => e.ToString().Contains(word)).ToList();
        }
    }
}
