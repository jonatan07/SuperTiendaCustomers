namespace SuperTiendaCustomer.Domain.Specification
{
    public class FilterComparer : Enumeration
    {
        public static FilterComparer And = new FilterComparer(1, nameof(And).ToLowerInvariant());
        public static FilterComparer Or = new FilterComparer(2, nameof(Or).ToLowerInvariant());

        public FilterComparer(int id, string name) : base(id, name)
        {
        }

        public static IEnumerable<FilterComparer> List()
        {
            return new[] { And, Or };
        }

        public static FilterComparer FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.InvariantCultureIgnoreCase));

            if (state == null)
                throw new Exception($"Possible values for comparer: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }

        public static FilterComparer From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
                throw new Exception($"Possible values for comparer: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }
    }
}