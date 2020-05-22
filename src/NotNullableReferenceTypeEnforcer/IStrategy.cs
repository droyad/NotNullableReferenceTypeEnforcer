namespace NotNullableReferenceTypeEnforcer
{
    internal interface IStrategy
    {
        void Validate(object value);
    }
}