using System;
using System.Linq;
using FluentAssertions;
using NotNullableReferenceTypeEnforcer;

namespace Tests
{
    public class TestBase
    {
        protected static void ValidateShouldPass(object value)
        {
            foreach(var attr in value.GetType().CustomAttributes)
                Console.WriteLine($"{attr.AttributeType.Name}: " + attr.ConstructorArguments[0].Value);

            foreach (var prop in value.GetType().GetProperties())
            {
                var attr = prop.CustomAttributes.FirstOrDefault();
                Console.Write(prop.Name + ": ");
                if(attr != null)
                    Console.Write(attr.ConstructorArguments[0].Value);
                Console.WriteLine();
            }

            new ReflectionStrategy().Validate(value);
        }

        protected static void ValidateShouldThrow(object value)
        {
            Action exec = () => ValidateShouldPass(value);
            exec.Should().Throw<NotNullPropertyIsNullException>();
        }
    }
}