using Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes.Products
{
    public class ProductEntity : BaseEntity
    {
        public string ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public byte[]? Picture { get; set; }
        public float Price { get; set; }

        //Foreign key
        public int CategoriesId { get; set; }

        //Navigation Property
        public virtual Categories CategoriesIdNavigation { get; set; }

        public override string ToString()
        {
            return $"{ProductName} {ProductDescription}";
        }
    }
}
