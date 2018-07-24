#TODO:

- Mongo Test Integration
- Other Exception handlers
- Return BadRequest via Action OnExecuting and etc.
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
- There is no try catch because the only try catch is on the  RushOrders.Middleware.ErrorHandlingMiddleware