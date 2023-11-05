using Entities.Concretes.Products;
using Entities.Concretes.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingDataAccess.Repositories.Concretes.UserAccountsRepos
{
    public class UsersAccountsRepository
    {
        private readonly Repository<UsersAccounts> UsersAccounts;

        public UsersAccountsRepository()
        {
            UsersAccounts = new Repository<UsersAccounts>();
        }

        //Commands
        public void AddUserAccount(UsersAccounts userAccount)
        {
            UsersAccounts.AddProduct(userAccount);
        }
        public void UpdateUserAccount(UsersAccounts userAccount)
        {
            UsersAccounts.UpdateProduct(userAccount);
        }
        public void RemoveUserAccount(UsersAccounts userAccount)
        {
            UsersAccounts.UpdateProduct(userAccount);
        }
        public void RemoveUserAccountById(int id)
        {
            UsersAccounts.RemoveProductById(id);
        }

        //QUerries
        public ICollection<UsersAccounts> GetAllUserAccounts()
        {
            return UsersAccounts.ProductDbSet.Include(u=>u.UsernamePasswrodIdNavigation).ToList();
        }
        public UsersAccounts GetUserAccountById(int id)
        {
            var pet = UsersAccounts.ProductDbSet.Include(u=>u.UsernamePasswrodIdNavigation).FirstOrDefault(u=>u.Id==id);
            return pet;
        }

        public ICollection<UsersAccounts> FindUsersByWord(string word)
        {
            return UsersAccounts.ProductDbSet.Include(u=>u.UsernamePasswrodIdNavigation).Where(u => u.ToString().Contains(word)).ToList();
        }
    }
}
