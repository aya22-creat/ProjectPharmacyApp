using MediatR;
using PharmacyApp.Application.Cart.DTO;

namespace PharmacyApp.Application.Cart.Command.AddItem{

    public record AddItemToCartCommand(

        Guid CustomerId,
        Guid ProudctId,
        String ProductName ,
        decimal UnitPrice ,
        int Quantity ,
        String Currency 


    ): IRequest<CartDto>;

    






    


  


  
    
}