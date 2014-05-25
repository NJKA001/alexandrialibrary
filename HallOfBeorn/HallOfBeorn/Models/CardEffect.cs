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

        public string GetText(string title)
        {
            if (tokens.Count == 0)
                return string.Empty;

            var text = new System.Text.StringBuilder();

            foreach (var token in tokens)
            {
                if (token.IsTitleReference)
                    text.AppendFormat(" {0}.", title);
                else if (token.IsIcon)
                    text.AppendFormat("<img src='{0}' style='height:16px;margin-left:2px;margin-right:-4px;margin-bottom:-2px;' />", token.ImagePath);
                else
                    text.Append(token.Text);
            }

            return text.ToString();
        }
    }
}