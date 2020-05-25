namespace Funbites.Patterns.ReferenceableEvents
{
    public interface IGenericReferenceableEventListener<ARGUMENT_TYPE>
    {
        void OnEventRaised(ARGUMENT_TYPE argument);
    }
}