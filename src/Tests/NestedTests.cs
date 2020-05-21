using NUnit.Framework;

namespace Tests
{
    public class NestedTests : TestBase
    {
        [Test]
        public void WhenNull()
        {
            var parent = new Parent();
            parent.Child.Grandchild.Prop = null!;
            ValidateShouldThrow(parent);
        }


        [Test]
        public void WhenNullInsideAStruct()
        {
            var parent = new Parent();
            parent.Child.GrandchildStruct = new GrandchildStruct() {Prop = null!};
            ValidateShouldThrow(parent);
        }

        class Parent
        {
            public Child Child { get; } = new Child();
        }

        class Child
        {
            public Grandchild Grandchild { get; } = new Grandchild();
            public GrandchildStruct GrandchildStruct { get; set; }
        }

        class Grandchild
        {
            public object Prop { get; set; } = new object();
        }

        struct GrandchildStruct
        {
            public object Prop { get; set; }
        }
    }
}