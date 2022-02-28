using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenPersonalBudget.API.Helpers
{
    public class EnumHelper
    {
        public static T GetEnumFromString<T>(string key)
        {
            return (T)Enum.Parse(typeof(T), key);
        }

        public static string GetStringFromEnum<T>(int index)
        {
            return Enum.GetName(typeof(T), index);
        }

        public List<string> ConvertEnumToList<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<string>().ToList();
        }
    }
}
