using Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes.Products
{
    public class Categories : BaseEntity
    {
        public string CategoryName { get; set; }
    }
}
