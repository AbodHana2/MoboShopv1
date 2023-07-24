using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoboShopv1.Models.ViewModels
{
    public class ProductViewModel
    {
        public List<Product>? Mobile { get; set; }
        public List<Product>? Hardware { get; set; }
        public List<Product>? Accessoris { get; set; }
    }
}
