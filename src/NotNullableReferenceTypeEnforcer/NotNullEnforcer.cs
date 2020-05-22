namespace NotNullableReferenceTypeEnforcer
{
    public interface INotNullEnforcer
    {
        void Validate(object value);
    }

    public class NotNullableReferenceTypeEnforcer : INotNullEnforcer
    {
        readonly IStrategy _strategy = new ReflectionStrategy();

        private NotNullableReferenceTypeEnforcer()
        {
        }

        private static INotNullEnforcer Instance { get; } = new NotNullableReferenceTypeEnforcer();

        static void Validate(object value)
            => Instance.Validate(value);

        void INotNullEnforcer.Validate(object value)
            => _strategy.Validate(value);
    }
}