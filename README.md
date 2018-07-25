#TODO:

- Could use DTOs/ViewModels to decouple better, but I choose to use only model to save time due to small scope
- Other Exception handlers
- Return BadRequest and others via Action Filter OnExecuting.
- Not saving user and pass at source code.
- Correlation ID
- Customizing EF mapping SQL char limitation and automatic CreatedDate 
- Authentication and Authorization was already outside scope

#Some utilized libs to mention:
- MongoDB.Driver
- Microsoft.EntityFrameworkCore;
- Microsoft.EntityFrameworkCore.InMemory;
- Swashbuckle.AspNetCore.Swagger
- Microsoft.AspNetCore.TestHost
- FluentValidation
- FluentAssertions
- xUnit
- Moq

# Other Notes
- There is only one try catch that is at the RushOrders.Middleware.ErrorHandlingMiddleware