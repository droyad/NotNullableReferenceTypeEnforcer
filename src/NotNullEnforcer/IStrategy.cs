namespace NotNullEnforcer
{
    internal interface IStrategy
    {
        void Validate(object value);
    }
}