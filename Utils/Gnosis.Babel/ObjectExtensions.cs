using System;

namespace Gnosis.Babel
{
    public static class ObjectExtensions
    {
        public static bool IsDefined(this object value)
        {
            return (value != null && value != DBNull.Value);
        }

        public static bool AsBoolean(this object value)
        {
            if (value == null || value == DBNull.Value)
                return false;

            var boolResult = false;
            if (bool.TryParse(value.ToString(), out boolResult))
                return boolResult;

            var result = 0;
            int.TryParse(value.ToString(), out result);
            return result == 1;
        }

        public static string AsString(this object value)
        {
            return value.IsDefined() ? value.ToString() : string.Empty;
        }

        public static byte AsByte(this object value)
        {
            return value.IsDefined() ? byte.Parse(value.ToString()) : (byte)0;
        }

        public static short AsShort(this object value)
        {
            return value.IsDefined() ? short.Parse(value.ToString()) : (short)0;
        }

        public static int AsInteger(this object value)
        {
            return value.IsDefined() ? int.Parse(value.ToString()) : 0;
        }

        public static long AsLong(this object value)
        {
            return value.IsDefined() ? long.Parse(value.ToString()) : 0L;
        }

        public static float AsFloat(this object value)
        {
            return value.IsDefined() ? float.Parse(value.ToString()) : 0f;
        }

        public static double AsDouble(this object value)
        {
            return value.IsDefined() ? double.Parse(value.ToString()) : 0d;
        }

        public static decimal AsDecimal(this object value)
        {
            return value.IsDefined() ? decimal.Parse(value.ToString()) : 0m;
        }

        public static DateTime AsDateTime(this object value)
        {
            return value.IsDefined() ? DateTime.Parse(value.ToString()) : DateTime.MinValue;
        }

        public static Uri AsUri(this object value)
        {
            return value.IsDefined() ? new Uri(value.ToString()) : new Uri("file:///");
        }

        public static string AsAffinity(this object value)
        {
            var type = (value != null) ? value.GetType().Name : "String";

            var model = value as IModel;
            if (model != null)
                type = "Int64";

            switch (type)
            {
                case "Boolean":
                case "Byte":
                case "Int16":
                case "Int32":
                case "Int64":
                    return "INTEGER";
                case "Single":
                case "Double":
                case "Decimal":
                    return "REAL";
                default:
                    return "TEXT";
            }
        }

        public static string AsDefaultString(this object value)
        {
            if (value == null)
                return "''";

            var model = value as IModel;
            if (model != null)
                return model.Id.ToString();

            if (value is string)
                return string.Format("'{0}'", value);
            else if (value is DateTime)
                return string.Format("'{0:s}'", value);

            return value.ToString();
        }

        public static object AsPersistentValue(this object value)
        {
            if (value == null)
                return string.Empty;

            var model = value as IModel;
            if (model != null)
                return model.Id;

            return value;
        }
    }
}
