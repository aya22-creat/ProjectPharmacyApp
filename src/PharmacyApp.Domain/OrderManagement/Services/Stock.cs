using PharmacyApp.Domain.OrderManagement.OrderAggregate;
using PharmacyApp.Domain.CatalogManagement.Product.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PharmacyApp.Domain.OrderManagement.Services
{
    public class OrderStockService
    {
        private readonly IStockService _stockService;

        public OrderStockService(IStockService stockService)
        {
            _stockService = stockService;
        }

        public async Task ReduceStockAsync(Order order, CancellationToken cancellationToken)
        {
            foreach (var item in order.Items)
            {
                await _stockService.ReserveStockAsync(item.ProductId, item.Quantity, cancellationToken);
            }
        }
    }
}
