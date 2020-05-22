namespace NotNullableReferenceTypeEnforcer
{
    public interface INotNullEnforcer
    {
        void Validate(object value);
    }

    public class NotNullEnforcer : INotNullEnforcer
    {
        readonly IStrategy _strategy = new ReflectionStrategy();

        private NotNullEnforcer()
        {
        }

        public static INotNullEnforcer Instance { get; } = new NotNullEnforcer();

        public static void Validate(object value)
            => Instance.Validate(value);

        void INotNullEnforcer.Validate(object value)
            => _strategy.Validate(value);
    }
}