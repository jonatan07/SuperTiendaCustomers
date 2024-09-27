namespace SuperTiendaCustomer.Domain.Specification
{

    public class Order<T>
        where T : class
    {
        public Order(OrderField<T> orderField, OrderType orderType)
        {
            OrderField = orderField;
            OrderType = orderType;
        }

        public OrderField<T> OrderField { get; }
        public OrderType OrderType { get; }
    }
}
