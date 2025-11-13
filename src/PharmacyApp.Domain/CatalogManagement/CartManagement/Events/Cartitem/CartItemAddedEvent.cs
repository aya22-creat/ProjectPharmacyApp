using PharmacyApp.Common.Common.DomainEvent;

namespace PharmacyApp.Domain.CatalogManagement.CartManagement.Events.Cartitem
{
    public class CartItemAddedEvent : DomainEvent
    {
        public Guid CartId { get; }
        public Guid ProductId { get; }
        public int Quantity { get; }

        public CartItemAddedEvent(Guid cartId, Guid productId, int quantity)
        {
            CartId = cartId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
