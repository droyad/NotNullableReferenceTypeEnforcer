using NUnit.Framework;

namespace Tests
{
    public class TypeWithMoreNullableObjectPropertiesTests : TestBase
    {
        [Test]
        public void WhenNull()
            => ValidateShouldThrow(new NullableObject {Prop = null!});

        [Test]
        public void WhenNotNull()
            => ValidateShouldPass(new NullableObject {Prop = new object()});

        class NullableObject
        {
            public object? OtherProp1 { get; set; } = null;
            public object? OtherProp2 { get; set; } = null;
            public object? OtherProp3 { get; set; } = null;
            public object OtherProp4 { get; set; } = new object();
            public object Prop { get; set; } = new object();
        }
    }
}