using Entities.Concretes.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingDataAccess.Repositories.Concretes.UserAccountsRepos
{
    public class UserPasswordRepository
    {
        private readonly Repository<UsernamePassword> UsersPassword;

        public UserPasswordRepository()
        {
            UsersPassword = new Repository<UsernamePassword>();
        }

        //Commands
        public void AddUserAccount(UsernamePassword userAccount)
        {
            UsersPassword.AddProduct(userAccount);
        }
        public void UpdateUserAccount(UsernamePassword userAccount)
        {
            UsersPassword.UpdateProduct(userAccount);
        }
        public void RemoveUserAccount(UsernamePassword userAccount)
        {
            UsersPassword.UpdateProduct(userAccount);
        }
        public void RemoveUserAccountById(int id)
        {
            UsersPassword.RemoveProductById(id);
        }

        //QUerries
        public ICollection<UsernamePassword> GetAllUserAccounts()
        {
            return UsersPassword.ProductDbSet.ToList();
        }
        public UsernamePassword GetUserAccountById(int id)
        {
            var pet = UsersPassword.ProductDbSet.FirstOrDefault(u => u.Id == id);
            return pet;
        }

        public ICollection<UsernamePassword> FindUsersByWord(string word)
        {
            return UsersPassword.ProductDbSet.Where(up => up.Username.Contains(word) || up.Password.Contains(word)).ToList();
        }
    }
}
