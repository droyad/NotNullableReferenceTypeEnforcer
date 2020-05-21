using NUnit.Framework;

namespace Tests
{
    public class NonReferenceTypesTests : TestBase
    {
        [Test]
        public void WhenNull()
            => ValidateShouldPass(new TestType());

        class TestType
        {
            public int Int { get; set; }
            public int? NullableInt { get; set; }
            public TestStruct Struct { get; set; }
            public TestStruct? StructNullable { get; set; }
        }

        struct TestStruct
        {
        }
    }
}