using Entities.Concretes.Products;
using Entities.Concretes.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingDataAccess.Repositories.Concretes.CategoriesRepos
{
    public class CategoriesRepository
    {
        private readonly Repository<Categories> CategoriesRepo;

        public CategoriesRepository()
        {
            CategoriesRepo = new Repository<Categories>();
        }

        //Commands
        public void AddCategory(Categories Category)
        {
            CategoriesRepo.AddProduct(Category);
        }
        public void UpdateCategory(Categories Category)
        {
            CategoriesRepo.UpdateProduct(Category);
        }
        public void RemoveCategory(Categories Category)
        {
            CategoriesRepo.UpdateProduct(Category);
        }
        public void RemoveCategoryById(int id)
        {
            CategoriesRepo.RemoveProductById(id);
        }

        //QUerries
        public ICollection<Categories> GetAllCategories()
        {
            return CategoriesRepo.GetAllProducts();
        }
        public Categories GetCategoryById(int id)
        {

            var pet = CategoriesRepo.GetProductById(id);
            return pet;

        }

        public ICollection<Categories> FindCategoryByWord(string word)
        {
            return CategoriesRepo.ProductDbSet.Where(u => u.ToString().Contains(word)).ToList();
        }
    }
}
