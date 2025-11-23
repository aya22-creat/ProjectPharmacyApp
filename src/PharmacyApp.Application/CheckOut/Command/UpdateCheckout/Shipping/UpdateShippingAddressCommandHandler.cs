using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.CheckOut.DTO;
using PharmacyApp.Domain.CheckoutFunctionality.Repositories;
using PharmacyApp.Domain.CheckoutFunctionality.ValueObjects;
using PharmacyApp.Common.Common.Exception; // إذا عرفتي NotFoundException

namespace PharmacyApp.Application.CheckOut.Command.UpdateCheckout.Shipping
{   
    public class UpdateShippingAddressCommandHandler : IRequestHandler<UpdateShippingAddressCommand, CheckoutDto>
    {
        private readonly ICheckOutRepository _checkoutRepository;

        public UpdateShippingAddressCommandHandler(ICheckOutRepository checkoutRepository)
        {
            _checkoutRepository = checkoutRepository;
        }

        public async Task<CheckoutDto> Handle(UpdateShippingAddressCommand request, CancellationToken cancellationToken)
        {
            var checkout = await _checkoutRepository.GetByIdAsync(request.CheckoutId, cancellationToken)            ?? throw new InvalidOperationException("Checkout not found");
            var newAddress = new Address(
                request.NewShippingAddress.Street,
                request.NewShippingAddress.City,
                request.NewShippingAddress.Country,
                request.NewShippingAddress.ZipCode
            );
            
            //checkout.UpdateShippingAddress(newAddress);

            await _checkoutRepository.UpdateAsync(checkout, cancellationToken);

            return new CheckoutDto(checkout);
        }
    }
}
