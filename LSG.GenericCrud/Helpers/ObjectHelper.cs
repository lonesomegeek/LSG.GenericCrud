using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSG.GenericCrud.Helpers
{
    public static class ObjectHelper
    {
        public static T CopyObject<T>(this T source)
        {
            var copy = Activator.CreateInstance<T>();
            var properties = typeof(T).GetProperties().Where(_ => _.DeclaringType == typeof(T));
            foreach (var property in properties)
            {
                copy?
                    .GetType()
                    .GetProperty(property.Name)
                    .SetValue(copy, property.GetValue(source));
            }

            return copy;
        }

        public static T ApplyChangeset<T>(this T source, T changeset)
        {
            var properties = typeof(T)
                .GetProperties()
                .Where(_ =>
                    _.DeclaringType == typeof(T));
            foreach (var property in properties)
            {
                source
                    .GetType()
                    .GetProperty(property.Name)
                    .SetValue(source, property.GetValue(changeset));
            }
            return source;
        }
    }
}
