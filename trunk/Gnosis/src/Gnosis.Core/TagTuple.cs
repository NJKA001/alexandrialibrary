﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class TagTuple
        : Tuple<object, object, object, object, object, object, object>
    {
        public TagTuple(object value1)
            : base(value1, null, null, null, null, null, null)
        {
            if (value1 == null)
                throw new ArgumentNullException("value1");
        }

        public TagTuple(object value1, object value2)
            : base(value1, value2, null, null, null, null, null)
        {
            if (value1 == null)
                throw new ArgumentNullException("value1");
        }

        public TagTuple(object value1, object value2, object value3)
            : base(value1, value2, value3, null, null, null, null)
        {
            if (value1 == null)
                throw new ArgumentNullException("value1");
        }

        public TagTuple(object value1, object value2, object value3, object value4)
            : base(value1, value2, value3, value4, null, null, null)
        {
            if (value1 == null)
                throw new ArgumentNullException("value1");
        }

        public TagTuple(object value1, object value2, object value3, object value4, object value5)
            : base(value1, value2, value3, value4, value5, null, null)
        {
            if (value1 == null)
                throw new ArgumentNullException("value1");
        }

        public TagTuple(object value1, object value2, object value3, object value4, object value5, object value6)
            : base(value1, value2, value3, value4, value5, value6, null)
        {
            if (value1 == null)
                throw new ArgumentNullException("value1");
        }

        public TagTuple(object value1, object value2, object value3, object value4, object value5, object value6, object value7)
            : base(value1, value2, value3, value4, value5, value6, value7)
        {
            if (value1 == null)
                throw new ArgumentNullException("value1");
        }

        public object[] ToArray()
        {
            return new object[] { Item1, Item2, Item3, Item4, Item5, Item6, Item7 };
        }

        public override string ToString()
        {
            return Item1.ToString();
        }

        public string[] ToStringArray()
        {
            return ToArray()
                .Where(item => item != null)
                .Select(item => item.ToString())
                .ToArray();
        }

        public uint ToUInt32()
        {
            return Item1.ToUInt32();
        }

        public byte[] ToByteArray()
        {
            return Item1.ToByteArray();
        }

        public DateTime ToDateTime()
        {
            return new DateTime(Item1.ToInt32(), Item2.ToInt32(), Item3.ToInt32());
        }

        public TimeSpan ToTimeSpan()
        {
            return new TimeSpan(Item1.ToInt32(), Item2.ToInt32(), Item3.ToInt32(), Item4.ToInt32(), Item5.ToInt32());
        }
    }
}