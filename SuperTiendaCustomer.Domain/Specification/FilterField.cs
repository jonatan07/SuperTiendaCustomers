namespace SuperTiendaCustomer.Domain.Specification
{

    public class FilterField<T>
        where T : class
    {
        public string Name { get; }

        public FilterField(string name)
        {
            Name = name;
        }
    }
}
