namespace CalculatorApp.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public static class EnumHelper
    {
        public static object[] GetValuesAndDescriptions(Type enumType)
        {
            var kvPairList = new List<string>();

            Array listValue = Enum.GetValues(enumType);
            for (int i = 0; i < listValue.Length; i++)
            {
                object value = listValue.GetValue(i);
                var enumValue = (Enum)listValue.GetValue(i);
                kvPairList.Add(GetDescription(enumValue));
            }

            var valuesAndDescriptions = from kv in kvPairList
                                        select new
                                        {
                                            Description = kv
                                        };

            return valuesAndDescriptions.ToArray();
        }

        public static string GetDescription(this Enum value)
        {
            System.Reflection.FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }
    }
}
