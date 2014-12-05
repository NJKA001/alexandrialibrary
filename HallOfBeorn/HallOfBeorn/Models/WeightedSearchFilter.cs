using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models
{
    public class WeightedSearchFilter
    {
        public WeightedSearchFilter(Func<SearchViewModel, Card, bool> check, float score)
        {
            _check = check;
            _score = score;
        }

        private readonly Func<SearchViewModel, Card, bool> _check;
        private readonly float _score;

        private float getWeightedScore(Card card)
        {
            var weight = 0;

            if (card.IsUnique)
                weight += 3;

            switch (card.CardType)
            {
                case CardType.Hero:
                    weight += 13;
                    break;
                case CardType.Ally:
                    weight += 9;
                    break;
                case CardType.Objective_Ally:
                    weight += 8;
                    break;
                case CardType.Attachment:
                    weight += 7;
                    break;
                case CardType.Objective:
                    weight += 6;
                    break;
                case CardType.Event:
                    weight += 5;
                    break;
                case CardType.Enemy:
                    weight += 4;
                    break;
                case CardType.Location:
                    weight += 2;
                    break;
                case CardType.Treachery:
                    weight += 1;
                    break;
                default:
                    break;
            }

            return _score + weight;
        }

        public float Score(SearchViewModel search, Card card)
        {
            return _check(search, card) ? getWeightedScore(card) : 0f;
        }
    }
}