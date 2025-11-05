using PharmacyApp.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PharmacyApp.Domain.CatalogManagement.OrderManagement.ValueObjects
{
    /// <summary>
    /// Represents all possible states of an Order using the Smart Enum pattern.
    /// </summary>
    public class OrderStatus : ValueObject
    {
        public static readonly OrderStatus Pending   = new(0, "Pending");
        public static readonly OrderStatus Shipped   = new(1, "Shipped");
        public static readonly OrderStatus Completed = new(2, "Completed");
        public static readonly OrderStatus Cancelled = new(3, "Cancelled");
        public static readonly OrderStatus Returned  = new(4, "Returned");

        private static readonly List<OrderStatus> _allStatuses = new()
        {
            Pending, Shipped, Completed, Cancelled, Returned
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

      // Try parse methods extra coding insteread of exception
        public static bool TryFromId(int id, out OrderStatus? status)
        {
            status = _allStatuses.FirstOrDefault(s => s.Id == id);
            return status != null;
        }
//qulity
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }

        public override string ToString() => Name;
    }
}
