using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models
{
    public class CardEffectViewModel
    {
        public string Prefix { get; set; }
        public bool IsCritical { get; set; }
        public bool HasPrefix { get { return !string.IsNullOrEmpty(Prefix); } }
        public bool IsShadow { get { return Prefix == "Shadow"; } }
        public string Text { get; set; }
    }
}