using System;
using System.Linq;
using System.Reflection;

namespace NotNullableReferenceTypeEnforcer
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

            var (isInNullableReferenceScope, defaultIsNullable) = IsNullable(type);

            foreach (var prop in properties)
                Validate(value, prop, isInNullableReferenceScope, defaultIsNullable, type);
        }

        private void Validate(object value, PropertyInfo prop, bool isInNullableReferenceScope, bool defaultIsNullable, Type type)
        {
            if (prop.PropertyType.IsPrimitive)
                return;

            if (prop.PropertyType == typeof(string))
            {
                var checkForNull = isInNullableReferenceScope && !IsNullable(prop, defaultIsNullable);
                if (checkForNull && prop.GetValue(value) == null)
                    throw new NotNullPropertyIsNullException($"{type.FullName}.{prop.Name} is null and does not have a nullable type annotation");
                return;
            }

            var propValue = prop.GetValue(value);
            if (propValue != null)
            {
                Validate(propValue);
            }
            else if (isInNullableReferenceScope && !IsNullable(prop, defaultIsNullable))
            {
                throw new NotNullPropertyIsNullException($"{type.FullName}.{prop.Name} is null and does not have a nullable type annotation");
            }
        }

        private (bool isNullable, bool defaultIsNullable) IsNullable(Type type)
        {
            var nullability = GetNullabilityAttributeValue(type);
            if (nullability == null)
                return (false, false);

            foreach (var attr in type.CustomAttributes)
                if (attr.AttributeType.FullName == "System.Runtime.CompilerServices.NullableContextAttribute")
                    return (true, (byte) attr.ConstructorArguments[0].Value == 2);

            return (true, false);
        }

        private bool IsNullable(PropertyInfo prop, bool defaultIsNullable)
            => GetNullabilityAttributeValue(prop) ?? defaultIsNullable;

        private bool? GetNullabilityAttributeValue(MemberInfo member)
        {
            foreach (var attr in member.CustomAttributes)
                if (attr.AttributeType.FullName == "System.Runtime.CompilerServices.NullableAttribute")
                    return (byte) attr.ConstructorArguments[0].Value == 2;

            return null;
        }
    }
}