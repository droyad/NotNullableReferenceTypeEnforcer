using NUnit.Framework;

namespace Tests
{
    public class TypeWithoutNullableAttributeTests : TestBase
    {
        [Test]
        public void WhenNull()
            => ValidateShouldPass(new NoNullableAttribute {Child = null});

        [Test]
        public void WhenNullableChildClassHasNull()
            => ValidateShouldThrow(new NoNullableAttribute {Child = new Child() {Prop = null!}});

#nullable disable
        class NoNullableAttribute
        {
            public Child Child { get; set; }
        }
#nullable enable

        class Child
        {
            public string Prop { get; set; } = "";
        }
    }
}