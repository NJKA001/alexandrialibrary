using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot.Core2008Set
{
    public class A_Lannister_Pays_His_Debts
        : EventBase
    {
        public A_Lannister_Pays_His_Debts()
            : base("A Lannister Pays His Debts", CardSet.Core_2008)
        {
            text.Response("After you lose a challenge, kneel one of your Lannister characters to choose and kill a participating character controlled by the winning opponent.");
        }
    }
}
