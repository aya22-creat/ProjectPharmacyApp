using System;
using MediatR;
using PharmacyApp.Application.CatalogManagement.Product.DTO;

namespace PharmacyApp.Application.CatalogManagement.Product.Queries.GetProductById
{

    public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto?>;

}
