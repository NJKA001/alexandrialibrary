using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class GenConDeckProductGroup : ProductGroup
    {
        public GenConDeckProductGroup()
            : base("Gen Con Decks")
        {
            AddProduct(new TheMassingAtOsgiliathProduct());
            AddProduct(new TheBattleOfLakeTownProduct());
            AddProduct(new TheStoneOfErechProduct());
        }
    }
}