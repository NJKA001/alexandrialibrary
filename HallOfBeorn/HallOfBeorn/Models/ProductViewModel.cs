using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models
{
    public class ProductViewModel
    {
        public ProductViewModel(Product product)
        {
            _product = product;
        }

        private readonly Product _product;

        public string Name { get { return _product.Name; } }
        public string Code { get { return _product.Code; } }
        public string ImagePath
        {
            get
            {
                var ext = (_product.ImageType == ImageType.Jpg) ? "jpg" : "png";

                return string.Format("/Images/Products/{0}.{1}", _product.Code, ext);
            }
        }
        public int ImageWidth
        {
            get
            {
                return _product.CardSets.Any(x => x.SetType == SetType.Core || x.SetType == SetType.Deluxe_Expansion || x.SetType == SetType.Saga_Expansion) ?
                    235 : 142;
            }
        }
        public int ImageHeight
        {
            get
            {
                return 235;
            }
        }
        
        public string Link
        {
            get {
                return string.Format("/Cards/Search?CardSet={0}", _product.CardSets.First().Name.Replace(' ','+'));
            }
        }

        public bool IsStartOfProductGroup
        {
            get
            {
                return _product.CardSets.Any(x => (x.SetType == SetType.Core || x.SetType == SetType.Deluxe_Expansion || x.Name == "The Hobbit: Over Hill and Under Hill" || x.Name == "The Black Riders" || x.Name == "The Massing at Osgiliath" || x.Name == "Passage Through Mirkwood Nightmare" || x.Name == "The Hunt for Gollum Nightmare") || (_product.Name == "Khazad-dûm Nightmare" || _product.Name == "The Hobbit: Over Hill and Under Hill Nightmare"));
            }
        }

        public bool IsEndOfProductGroup
        {
            get
            {
                return _product.CardSets.Any(x => (x.SetType == SetType.Core || x.SetType == SetType.Deluxe_Expansion));
            }
        }
    }
}