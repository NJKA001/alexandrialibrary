using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallOfBeorn.Models
{
    public interface ISearchFilter
    {
        float Score(SearchViewModel search, Card card);
    }
}
