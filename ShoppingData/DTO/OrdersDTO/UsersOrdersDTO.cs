using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingDataAccess.DTO.OrdersDTO
{
    public class UsersOrdersDTO
    {
        public ProductDTO Product { get; set; }
        public DateTime OrderDate { get; set; }
        public UsersOrdersDTO(ProductDTO product,DateTime orderDate)
        {
            Product = product;
            OrderDate = orderDate;
        }
    }
}
