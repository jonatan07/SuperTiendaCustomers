using System.Linq.Expressions;

namespace SuperTiendaCustomer.Domain.Specification
{
    public class OrderType : Enumeration
    {
        public static OrderType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(orderType => string.Equals(orderType.Name, name, StringComparison.InvariantCultureIgnoreCase));

            if (state == null)
            {
                throw new Exception($"Possible values for order type: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static OrderType From(int id)
        {
            var state = List().SingleOrDefault(orderType => orderType.Id == id);

            if (state == null)
                throw new Exception($"Possible values for order type: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }

        #region Enumerators

        public static OrderType Asc = new OrderType(1, nameof(Asc), Expression.Equal);
        public static OrderType Desc = new OrderType(2, nameof(Desc), Expression.Equal);

        #endregion

        #region Constructor & properties

        public OrderType(int id, string name, Func<Expression, Expression, BinaryExpression> expression)
            : base(id, name)
        {
        }

        public static IEnumerable<OrderType> List()
        {
            return new[] { Asc, Desc };
        }

        #endregion
    }
}