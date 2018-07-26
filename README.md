#TODO:

- Could use DTO/View model to decouple more, but I prefer to use models due to small scope to save time
- Other Exception handlers
- Not saving user and pass at source code.
- Correlation ID
- Customizing EF mapping SQL char limitation and automatic CreatedDate 
- Authentication and Authorization was already outside scope
- Capture log from ILogger at Middleware.ErrorHandlingMiddleware

#Some utilized libs to mention:
- MongoDB.Driver
- Microsoft.EntityFrameworkCore;
- Microsoft.EntityFrameworkCore.InMemory;
- Swashbuckle.AspNetCore.Swagger
- Microsoft.AspNetCore.TestHost
- FluentValidation
- FluentAssertions
- xUnit
- Moq - Only for mongodb driver, but changed the strategy

# Other Notes
- Return BadRequest and others via Action Filter OnExecuting no need bacause all validation is overriden by fluentValidation
- There is only one try catch in the RushOrders.Middleware.ErrorHandlingMiddleware