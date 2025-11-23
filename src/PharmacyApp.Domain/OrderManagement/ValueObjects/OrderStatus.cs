using PharmacyApp.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PharmacyApp.Domain.CatalogManagement.OrderManagement.ValueObjects
{
    public class OrderStatus : ValueObject
    {
        public static readonly OrderStatus Pending = new(0, "Pending");
        public static readonly OrderStatus Confirmed = new(1, "Confirmed");
        public static readonly OrderStatus Processing = new(2, "Processing");
        public static readonly OrderStatus Shipping = new(3, "Shipping");
        public static readonly OrderStatus Delivered = new(4, "Delivered");
        public static readonly OrderStatus Completed = new(5, "Completed");
        public static readonly OrderStatus Cancelled = new(6, "Cancelled");
        public static readonly OrderStatus Returned = new(7, "Returned");

        private static readonly List<OrderStatus> _allStatuses = new()
        {
            Pending, Confirmed, Processing, Shipping, Delivered, Completed, Cancelled, Returned
        };

        public int Id { get; }
        public string Name { get; }

        private OrderStatus(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static IReadOnlyList<OrderStatus> GetAll() => _allStatuses.AsReadOnly();

        public static OrderStatus FromId(int id)
        {
            var status = _allStatuses.FirstOrDefault(s => s.Id == id);
            if (status == null)
                throw new ArgumentException($"Invalid OrderStatus Id: {id}", nameof(id));
            return status;
        }

        public static OrderStatus FromName(string name)
        {
            var status = _allStatuses.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (status == null)
                throw new ArgumentException($"Invalid OrderStatus Name: {name}", nameof(name));
            return status;
        }

        public static bool TryFromId(int id, out OrderStatus? status)
        {
            status = _allStatuses.FirstOrDefault(s => s.Id == id);
            return status != null;
        }

        //  Added define allowed transitions
        public bool CanTransitionTo(OrderStatus nextStatus)
        {
            return this switch
            {
                var s when s == Pending => nextStatus == Confirmed || nextStatus == Cancelled,
                var s when s == Confirmed => nextStatus == Processing,
                var s when s == Processing => nextStatus == Shipping,
                var s when s == Shipping => nextStatus == Delivered || nextStatus == Returned,
                var s when s == Delivered => nextStatus == Completed,
                var s when s == Completed => false,
                var s when s == Cancelled => false,
                var s when s == Returned => false,
                _ => false
            };
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }

        public override string ToString() => Name;
    }
}
