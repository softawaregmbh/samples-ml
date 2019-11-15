using ArffTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegressionSimple1
{
    public static class ArffExtensions
    {
        public static T MapTo<T>(this object[] instance, ArffHeader arffHeader)
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            var data = Activator.CreateInstance<T>();

            for (int column = 0; column < instance.Length; column++)
            {
                var attribute = arffHeader.Attributes[column];
                var property = properties.FirstOrDefault(p => p.Name.Equals(attribute.Name, StringComparison.CurrentCultureIgnoreCase));
                var value = instance[column];

                if (property != null && value != null)
                {
                    if (property.PropertyType == typeof(Single) && value.GetType() == typeof(double))
                    {
                        property.SetValue(data, Convert.ToSingle(value));
                    }
                    else
                    {
                        property.SetValue(data, value);
                    }
                }
            }

            return data;
        }
    }
}
