namespace SuperTiendaCustomer.Domain.Specification
{
    public class Filter
    {
        public string Field { get; private set; }
        public FilterOperator Operator { get; }
        public FilterComparer Comparer { get; }
        public object? Value { get; }

        public Filter(string field, FilterOperator @operator, FilterComparer comparer, object? value)
        {
            Field = field;
            Operator = @operator;
            Comparer = comparer;
            Value = value;
        }

        public void SetField(string value)
        {
            Field = value;
        }
    }
}
