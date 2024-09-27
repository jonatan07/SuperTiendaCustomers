namespace SuperTiendaCustomer.Domain.Specification
{
    public class OrderField<T> where T : class
    {
        public OrderField(string fieldName)
        {
            FieldName = fieldName;
        }

        public string FieldName { get; }
    }
}
