using PharmacyApp.Common.Common.DomainEvent;
using MediatR;

namespace PharmacyApp.Domain.CartManagement.Events.Cartitem
{
    public class CartItemAddedEvent : DomainEvent, INotification
    {
        public Guid CartId { get; }
        public Guid ProductId { get; }

        public Guid CustomerId { get;}
        public Guid ItemId { get; }
      
        public string ProductName { get;}
        public int Price { get;  }
          public int Quantity { get; }
       


        public CartItemAddedEvent(Guid cartId, Guid productId, int quantity)
        {
            CartId = cartId;
            ProductId = productId;
            CustomerId=CustomerId;
            ItemId=ItemId;
            ProductName = string.Empty;
            Price = 0;
            Quantity = quantity;
        }
    }
}
