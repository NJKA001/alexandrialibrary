using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models
{
    public class CardEffect
    {
        private readonly List<Token> tokens = new List<Token>();

        public bool IsCritical { get; set; }
        
        public List<Token> Tokens
        {
            get { return tokens; }
        }
    }
}