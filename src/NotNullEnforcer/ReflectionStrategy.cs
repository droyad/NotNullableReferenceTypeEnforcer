using System;
using System.Linq;
using System.Reflection;

namespace NotNullEnforcer
{
    internal class ReflectionStrategy : IStrategy
    {
        public void Validate(object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var type = value.GetType();
            var properties = type
                .GetProperties()
                .Where(p => p.CanRead);

            var (typeHasNullableAttribute, defaultIsNullable) = IsNullable(type);

            foreach (var prop in properties)
                Validate(value, prop, typeHasNullableAttribute, defaultIsNullable, type);
        }

        private void Validate(object value, PropertyInfo prop, bool typeHasNullableAttribute, bool defaultIsNullable, Type type)
        {
            if (prop.PropertyType.IsPrimitive)
                return;

            if (prop.PropertyType == typeof(string))
            {
                var checkForNull = typeHasNullableAttribute && !IsNullable(prop, defaultIsNullable);
                if (checkForNull && prop.GetValue(value) == null)
                    throw new NotNullPropertyIsNullException($"{type.FullName}.{prop.Name} is null and does not have a nullable type annotation");
                return;
            }

            var propValue = prop.GetValue(value);
            if (propValue != null)
            {
                Validate(propValue);
            }
            else if (typeHasNullableAttribute && !IsNullable(prop, defaultIsNullable))
            {
                throw new NotNullPropertyIsNullException($"{type.FullName}.{prop.Name} is null and does not have a nullable type annotation");
            }
        }

        private (bool isNullable, bool defaultIsNullable) IsNullable(Type prop)
        {
            foreach (var attr in prop.CustomAttributes)
                if (attr.AttributeType.FullName == "System.Runtime.CompilerServices.NullableAttribute")
                {
                    foreach (var attr2 in prop.CustomAttributes)
                        if (attr2.AttributeType.FullName == "System.Runtime.CompilerServices.NullableContextAttribute")
                            return (true, (byte) attr2.ConstructorArguments[0].Value == 2);

                    return (true, false);
                }

            return (false, false);
        }

        private bool IsNullable(PropertyInfo prop, bool defaultIsNullable)
        {
            foreach (var attr in prop.CustomAttributes)
                if (attr.AttributeType.FullName == "System.Runtime.CompilerServices.NullableAttribute")
                    return (byte) attr.ConstructorArguments[0].Value == 2;

            return defaultIsNullable;
        }
    }
}