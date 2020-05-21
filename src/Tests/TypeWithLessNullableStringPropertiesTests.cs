using NUnit.Framework;

namespace Tests
{
    public class TypeWithLessNullableStringPropertiesTests : TestBase
    {
        [Test]
        public void WhenNull()
            => ValidateShouldThrow(new NullableString {Prop = null!});

        [Test]
        public void WhenNotNull()
            => ValidateShouldPass(new NullableString {Prop = "Blah"});

        class NullableString
        {
            public string? OtherProp1 { get; set; } = null;
            public string OtherProp2 { get; set; } = "";
            public string OtherProp3 { get; set; } = "";
            public string Prop { get; set; } = "";
        }
    }
}