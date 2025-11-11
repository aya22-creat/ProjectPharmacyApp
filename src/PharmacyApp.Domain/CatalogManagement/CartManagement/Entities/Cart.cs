using System;
using System.Threading;
using System.Threading.Tasks;
using PharmacyApp.Common.Common.Exception;
using PharmacyApp.Common.Common;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Repositories;
using PharmacyApp.Domain.CatalogManagement.CartManagement.ValueObjects;
using PharmacyApp.Domain.CatalogManagement.CartManagement.Enums;
using PharmacyApp.Domain.CatalogManagement.CartManagement.Events;

// code without copon 
namespace PharmacyApp.Domain.CatalogManagement.CartManagement.Entities
{
    public class Cart : AggregateRoot<Guid>
    {
        public Guid CustomerId { get; private set; }
        public CartState State { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private readonly List<CartItem> _items = new();
        public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

        // Constructor for EF Core
        private Cart() : base() { }

        public Cart(Guid customerId) : base(Guid.NewGuid())
        {
            if (customerId == Guid.Empty)
                throw new ArgumentException("CustomerId cannot be empty", nameof(customerId));

            CustomerId = customerId;
            State = CartState.Active!;
            CreatedAt = DateTime.UtcNow;
        }

        // Add item to cart
        public void AddItem(Guid productId, int quantity, Money price)
        {
            if (State != CartState.Active)
                throw new InvalidOperationException("Cannot add items to inactive cart");

            var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.IncreaseQuantity(quantity);
            }
            else
            {
                var newItem = new CartItem(productId, quantity, price);
                _items.Add(newItem);
            }

            AddDomainEvent(new CartItemAddedEvent(Id, productId, quantity));
        }

        // Remove item from cart
        public void RemoveItem(Guid cartItemId)
        {
            if (State != CartState.Active)
                throw new InvalidOperationException("Cannot remove items from inactive cart");

            var item = _items.FirstOrDefault(i => i.Id == cartItemId);

            if (item == null)
                throw new InvalidOperationException($"CartItem with ID {cartItemId} not found");

            _items.Remove(item);

            AddDomainEvent(new CartItemRemovedEvent(Id, cartItemId, item.ProductId));
        }

        // Update item quantity
        public void UpdateItemQuantity(Guid cartItemId, int newQuantity)
        {
            if (State != CartState.Active)
                throw new InvalidOperationException("Cannot update items in inactive cart");

            var item = _items.FirstOrDefault(i => i.Id == cartItemId);

            if (item == null)
                throw new InvalidOperationException($"CartItem with ID {cartItemId} not found");

            item.UpdateQuantity(newQuantity);
        }

        // Clear all items from cart
        public void ClearCart()
        {
            if (State != CartState.Active)
                throw new InvalidOperationException("Cannot clear inactive cart");

            _items.Clear();
        }

        // Mark cart as checked out
        public void MarkAsCheckedOut()
        {
            if (State != CartState.Active)
                throw new InvalidOperationException("Only active carts can be checked out");

            if (_items.Count == 0)
                throw new InvalidOperationException("Cannot checkout empty cart");

            State = CartState.CheckedOut;

            var total = GetTotal();
            AddDomainEvent(new CartCheckedOutEvent(Id, CustomerId, total.Amount, total.Currency!));
        }

        // Mark cart as abandoned
        public void MarkAsAbandoned()
        {
            State = CartState.Abandoned;

            var total = GetTotal();
            AddDomainEvent(new CartAbandonedEvent(Id, CustomerId, total.Amount));
        }

        // Mark cart as expired
        public void MarkAsExpired()
        {
            State = CartState.Expired;
        }

        // Get cart total
        public Money GetTotal()
        {
            if (_items.Count == 0)
                return Money.Zero("EGP");

            var total = Money.Zero("EGP");

            foreach (var item in _items)
            {
                total = total.Add(item.GetSubtotal());
            }

            return total;
        }

        // Get total items count
        public int GetTotalItemsCount()
        {
            return _items.Sum(i => i.Quantity);
        }

        // Check if cart is empty
        public bool IsEmpty()
        {
            return _items.Count == 0;
        }

        // Check if cart contains product
        public bool ContainsProduct(Guid productId)
        {
            return _items.Any(i => i.ProductId == productId);
        }

        // Get item by product ID
        public CartItem? GetItemByProductId(Guid productId)
        {
            return _items.FirstOrDefault(i => i.ProductId == productId);
        }
    }
}
