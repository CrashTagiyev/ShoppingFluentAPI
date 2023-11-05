using Entities.Abstracts;
using Entities.Concretes.Products;
using Microsoft.EntityFrameworkCore;
using ShoppingData.DataContext.ProductDataContexts;
using ShoppingDataAccess.Repositories.Abstracts.ProductRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace ShoppingDataAccess.Repositories.Concretes
{

    public class Repository<T> : ICommandRepository<T>, IQuerryRepository<T> where T : BaseEntity, new()
    {
        private readonly ShoppingDBContext _productDbContext;
        internal readonly DbSet<T> ProductDbSet;

        public Repository()
        {
            _productDbContext = new ShoppingDBContext();
            ProductDbSet = _productDbContext.Set<T>();
        }

        //Querries
        public ICollection<T> GetAllProducts()
        {
            return ProductDbSet.ToList();
        }

        public T GetProductById(int id)
        {
            var Product = ProductDbSet.FirstOrDefault(p => p.Id == id);
            if (Product == null) throw new Exception("Product is not found ");
            return Product;
        }


        public void RemoveProduct(T product)
        {
            if (GetProductById(product.Id) == null) throw new ArgumentNullException();
            SaveChanges();
        }


        //Commands
        public void AddProduct(T product)
        {
            if (product == null) throw new ArgumentNullException();
            ProductDbSet.Add(product);
            SaveChanges();
        }
        public void RemoveProductById(int id)
        {
            var Product = GetProductById(id);
            if (Product == null) throw new ArgumentNullException();
            ProductDbSet.Remove(ProductDbSet.FirstOrDefault(p => p.Id == id));
            SaveChanges();
        }

        public void UpdateProduct(T product)
        {
            if (product == null) throw new ArgumentNullException();
            if (GetProductById(product.Id) == null) throw new Exception("Product did not found");
            ProductDbSet.Update(product);
        }
        public void SaveChanges()
        {
            _productDbContext.SaveChanges();
        }



    }
}
