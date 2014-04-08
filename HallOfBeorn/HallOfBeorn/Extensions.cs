using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HallOfBeorn
{
    public static class Extensions
    {
        public static string ToUrlSafeString(this string self)
        {
            if (string.IsNullOrEmpty(self))
                return string.Empty;

            return self.Replace(" - ", "-")
                .Replace(' ', '-')
                .Replace('_', '-')
                .Replace("&", "and")
                .Replace(".", string.Empty)
                .Replace(",", string.Empty)
                .Replace(":", string.Empty)
                .Replace("?", string.Empty)
                .Replace("!", string.Empty);
        }

        public static string ToDisplayString(this string self, string title)
        {
            if (string.IsNullOrEmpty(self))
                return string.Empty;

            return self
                .Replace("~", string.Empty)
                .Replace("[Card]", title);
        }

        public static string ToSearchString(this string value)
        {
            return value.Replace(' ', '+').Replace("!", string.Empty).Replace("?", string.Empty);
                //.Replace("'", string.Empty);
        }

        public static IEnumerable<SelectListItem> GetSelectListItems(this Type enumType)
        {
            return enumType.GetSelectListItems(" ");
        }

        public static IEnumerable<SelectListItem> GetSelectListItems(this Type enumType, string separator)
        {
            var listItems = new List<SelectListItem>();

            foreach (var item in System.Enum.GetValues(enumType))
            {
                var number = (int)item;

                var text = number > 0 ? item.ToString().Replace("_", separator) : "Any";
                var value = item.ToString();

                listItems.Add(
                    new SelectListItem()
                    {
                        Text = text,
                        Value = value
                    }
                );
            }

            return listItems;
        }

        public static IEnumerable<SelectListItem> GetSelectListItems(this IEnumerable<string> list)
        {
            var listItems = new List<SelectListItem>() { new SelectListItem() { Selected = true, Text = "Any", Value = "Any" } };

            foreach (var item in list)
            {
                listItems.Add(
                    new SelectListItem()
                    {
                        Text = item.Replace(".", string.Empty),
                        Value = item
                    }
                );
            }

            return listItems;
        }

        public static IEnumerable<SelectListItem> GetSelectListItems(this IEnumerable<byte> list)
        {
            var listItems = new List<SelectListItem>() { new SelectListItem() { Selected = true, Text = "Any", Value = "-1" } };

            foreach (var item in list)
            {
                listItems.Add(
                    new SelectListItem()
                    {
                        Text = item.ToString(),
                        Value = item.ToString()
                    }
                );
            }

            return listItems;
        }

        // Returns the first letter in a string, or a space if there are no letters in the given string
        public static char GetFirstLetter(this string self)
        {
            if (!string.IsNullOrEmpty(self))
            {
                for (var i = 0; i < self.Length; i++)
                {
                    if (char.IsLetter(self[i]))
                        return self[i];
                }
            }

            return ' ';
        }
    }
}