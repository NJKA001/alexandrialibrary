using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models
{
    public class SearchFilter : ISearchFilter
    {
        public SearchFilter(Func<SearchViewModel, Card, bool> check, float score)
        {
            _check = check;
            _score = score;
        }

        private readonly Func<SearchViewModel, Card, bool> _check;
        private readonly float _score;

        public float Score(SearchViewModel search, Card card)
        {
            return _check(search, card) ? _score : 0f;
        }
    }
}