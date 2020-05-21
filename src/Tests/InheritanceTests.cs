using NUnit.Framework;

namespace Tests
{
    public class InheritanceTests : TestBase
    {
        [Test]
        public void WhenChildNull()
        {
            var parent = new Parent();
            parent.Child = null!;
            ValidateShouldThrow(parent);
        }
        
        [Test]
        public void WhenChildPropNull()
        {
            var parent = new Parent();
            parent.Child.Prop = null!;
            ValidateShouldThrow(parent);
        }

        class Parent
        {
            public IChild Child { get; set; } = new Child1();
        }

        interface IChild
        {
            object Prop { get; set; }
        }
        
        class Child1 : IChild
        {
            public object Prop { get; set; } = new object();
        }

        class Child2 : IChild
        {
            public object Prop { get; set; } = new object();
        }
    }
}