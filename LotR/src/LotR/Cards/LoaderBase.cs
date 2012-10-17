using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;

namespace LotR.Cards
{
    public abstract class LoaderBase
    {
        protected LoaderBase()
        {
        }

        /// <summary>
        /// Returns all types in the current AppDomain implementing the interface or inheriting the type. 
        /// </summary>
        protected IEnumerable<Type> GetTypesImplementingInterface(Type desiredType)
        {
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => desiredType.IsAssignableFrom(type));

        }

        protected bool IsRealClass(Type testType)
        {
            return testType.IsAbstract == false
                && testType.IsGenericTypeDefinition == false
                && testType.IsInterface == false;
        }

        protected T GetCard<T>(T prototype)
            where T : class, ICard
        {
            var ctor = prototype.GetType().GetConstructor(new System.Type[0]);

            return (ctor != null) ? ctor.Invoke(null) as T : null;
        }

        protected IEnumerable<T> GetCards<T>(IEnumerable<T> prototypes)
            where T : class, ICard
        {
            var cards = new List<T>();

            foreach (var prototype in prototypes)
            {
                cards.Add(GetCard<T>(prototype));
            }

            return cards;
        }

        protected IEnumerable<IEncounterCard> GetCards(IEnumerable<IEncounterCard> prototypes)
        {
            var cards = new List<IEncounterCard>();

            foreach (var prototype in prototypes)
            {
                for (var i = 0; i < prototype.Quantity; i++)
                {
                    if (i == 0 || i == 2)
                        cards.Add(GetCard<IEncounterCard>(prototype));
                    else
                        cards.Insert(0, GetCard<IEncounterCard>(prototype));
                }
            }

            return cards;
        }
    }
}
