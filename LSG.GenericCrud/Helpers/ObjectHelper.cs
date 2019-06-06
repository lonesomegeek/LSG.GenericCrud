using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using LSG.GenericCrud.Models;

namespace LSG.GenericCrud.Helpers
{
    public static class ObjectHelper
    {

        public static List<T> AggregateCombine<T, T2>(this IEnumerable<T2> items, Func<T2,T2, T> combine) where T2 : class, IEntity, new()
        {
            // TODO: Add validation for array length
            var result = new List<T>();
            var itemArray = items.ToArray();
            var current = itemArray[0];

            for (int i = 0; i < itemArray.Count(); i++)
            {
                var next = itemArray[i];
                result.Add(combine(current, next));
                current = next;
            }

            return result;
        }
        public static T CopyObject<T>(this T source)
        {
            var copy = Activator.CreateInstance<T>();
            var properties = typeof(T).GetProperties().Where(_ => _.DeclaringType == typeof(T) && !Attribute.IsDefined(_, typeof(IgnoreInChangesetAttribute)));
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
                    _.DeclaringType == typeof(T) && !Attribute.IsDefined(_, typeof(IgnoreInChangesetAttribute)));
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
