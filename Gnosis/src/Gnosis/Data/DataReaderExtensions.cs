using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public static class DataReaderExtensions
    {
        public static IDictionary<string, int> GetOrdinals(this IDataReader reader)
        {
            var ordinals = new Dictionary<string, int>();

            if (reader.FieldCount <= 0)
                return ordinals;

            for (var i = 0; i < reader.FieldCount; i++)
                ordinals.Add(reader.GetName(i), i);

            return ordinals;
        }
    }
}
