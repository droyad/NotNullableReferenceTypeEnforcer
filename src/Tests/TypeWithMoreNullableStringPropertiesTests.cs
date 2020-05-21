using NUnit.Framework;

namespace Tests
{
    public class TypeWithMoreNullableStringPropertiesTests : TestBase
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
            public string? OtherProp2 { get; set; } = null;
            public string? OtherProp3 { get; set; } = null;
            public string? OtherProp4 { get; set; } = "";
            public string Prop { get; set; } = "";
        }
    }
}