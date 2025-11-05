using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PharmacyApp.Application.Product.Command.DeleteProduct
{
    public record DeleteProductCommand(
        Guid ProductId
    ) : IRequest;
}