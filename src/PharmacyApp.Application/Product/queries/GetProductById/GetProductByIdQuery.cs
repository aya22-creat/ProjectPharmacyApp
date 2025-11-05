using System;
using MediatR;
using PharmacyApp.Application.Product.DTO;

namespace PharmacyApp.Application.Product.Queries.GetProductById
{

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto?>;

}
