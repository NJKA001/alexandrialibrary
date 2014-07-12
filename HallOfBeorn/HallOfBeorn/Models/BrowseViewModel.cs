using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models
{
    public class BrowseViewModel
    {
        public BrowseViewModel()
        {
            Products = new List<ProductViewModel>();
        }

        public List<ProductViewModel> Products { get; set; }
    }
}