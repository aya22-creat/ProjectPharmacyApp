
using PharmacyApp.Common.Common;
using PharmacyApp.Common.Common.Exception;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.ValueObjects;
using PharmacyApp.Domain.CatalogManagement.Common;

namespace PharmacyApp.Domain.CatalogManagement.OrderManagement.Entities
{
    public class OrderItem : BaseEntity<Guid>
    {
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; } = string.Empty;
        public Money UnitPrice { get; private set; } = null!;
        public int Quantity { get; private set; }
        public Money? Discount { get; private set; }
        public Money TotalPrice => CalculateTotalPrice();



        private OrderItem() { }


        public OrderItem(Guid productId, string productName, Money unitPrice, int quantity)
        {
            if (productId == Guid.Empty)
                throw new DomainException("Product ID cannot be empty.");

            if (string.IsNullOrWhiteSpace(productName))
                throw new DomainException("Product name cannot be empty.");

            if (unitPrice == null)
                throw new ArgumentNullException(nameof(unitPrice));

            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than zero.");

            Id = Guid.NewGuid();
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }



        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
                throw new DomainException("Quantity must be greater than zero.");

            Quantity = newQuantity;
        }
        public void ApplyDiscount(Money discount)
        {
            DiscountValidator.ValidateOrderDiscount(discount, GetSubtotal());
            Discount = discount;
        }

        public Money GetSubtotal()
        {
            return UnitPrice * Quantity;
        }

        public Money GetTotal()
        {
            var total = GetSubtotal();
            if (Discount != null)
                total = total - Discount;

            return total;
        }

        public Money GetTotalDiscount()
        {
            return Discount ?? Money.Zero();
        }

        internal void SetOrderId(Guid orderId)
        {
            OrderId = orderId;
        }

        private Money CalculateTotalPrice()
        {
            var subtotal = UnitPrice.Multiply(Quantity);
            return subtotal.Subtract(Discount ?? Money.Zero(UnitPrice.Currency));
        }

    }
}