using Entities.Abstracts;
using Entities.Concretes.Products;
using Entities.Concretes.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes.Orders
{
    public class ProductsOrders:BaseEntity
    {
        //Foreign key
        public int UsersAccountsId { get; set; }
        public int ProductEntityId { get; set; }

        //Navigation Property
        public virtual UsersAccounts UsersAccountsIdNavigation { get; set; }
        public virtual ProductEntity ProductEntityIdNavigation { get; set; }
    }
}
