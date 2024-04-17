namespace CardTdd.Domain.CartAggregate
{
    public class CartProductIsNotFoundException : Exception
    {
        public CartProductIsNotFoundException(): base("The product is not found!") {}
    }
}
