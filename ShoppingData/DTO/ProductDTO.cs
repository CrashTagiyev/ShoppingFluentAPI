using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ShoppingDataAccess.DTO
{
    public class ProductDTO
    {
        
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public float Price { get; set; }
        public string StringPrice { get; set; }
        public BitmapImage? ProductPicture { get; set; }
        public string Category { get; set; }


        public ProductDTO(int id, string productName, string? productDescription, float price, BitmapImage? image, string category)
        {
            Id = id;
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            ProductPicture = image;
            Category = category;
            StringPrice = $"Price:{price}$";
        }

        public override string ToString()
        {
            return $"Id:{Id}\n" +
                $"Name:{ProductName}\n" +
                $":Description:{ProductDescription}\n" +
                $"Category:{Category}\n"+
                $"Price:{Price}\n";
        }

    }
}
