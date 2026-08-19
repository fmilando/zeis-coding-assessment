### Add one README.md file per project.

* All decisions made should be detailed on these file for the context of the project.
* Reference other projects when possible for context
* Create an open GitHub project, push this code and send the link to Paulo Santana (include de PDF as part of the documentation/requirements (try to add links to other)









### Run code analysis

Execute code analysis to make sure the code is clean and follows best practices

* dotnet format --verify-no-changes
* dotnet build --no-restore --warnaserror /p:RunAnalyzers=true /p:Configuration=Debug



Test the application using 

* dotnet test --configuration



### Idempotency-key for write operations!! do it - OK





### Usage of Clean architecture + CQRS approach

* Why the usage of Clean Architecture
* Why the use of Vertical Slices
* why CQRS: separates reads from writes. in an ecommerce system, the reads should be extremely fast but the writes can be slow
* why EF Core for Writes? it is a requirement
* why the usage of Dapper for Reads? performance!
* at the application-level, why use the Features over UseCases?
* why not use MediatR lib approach? don't see any particular benefit for the system.
* why send the Commands to Queue? so that commands survive restarts and to use async writes without
* write the complete project diagram
* include a global exception handler using ProblemDetails
* describe why implicit conversion between results and the holding types
* (some issue): the domain events can be triggered from non-committed entities. there's no way to prevent it.
* Database: PostgreSQL v10+
* Create a Global Exception handler
* Create background worker that checks if there is any unsent message or event and resend it
* Make sure all build warning are removed
* All API return types match the error codes
* Describe all API arguments and expected results
* Database should created at startup by applying Migrations (do not forget: dotnet tool install --global dotnet-ef)
* Add endpoint for status health checks and for live checks
* Use DbTransactions for add-to-stock and decrement-stock

#### 

#### Database

* intercepting and logging all db errors

connection string format: Host=localhost;Port=5432;Database=zeiss-products;Username=ze1ss;Password=ze1s<s>-pr0ds

* Create migrations
dotnet ef migrations add InitialCreate --project ./Infrastructure/Zeiss.Products.Infrastructure --startup-project ./Presentation/Zeiss.Products.WebApi



* Generate CI/CD migrations generator script



dotnet ef migrations script --idempotent --project ./Infrastructure/Zeiss.Products.Infrastructure --startup-project ./Presentation/Zeiss.Products.WebApi --output ./Infrastructure/Zeiss.Products.Infrastructure/Migrations/Migrations.sql



* Apply the database locally

dotnet ef database update --project ./Infrastructure/Zeiss.Products.Infrastructure --startup-project ./Presentation/Zeiss.Products.WebApi







### The domain model - OK

* OK Entities: Product, Inventory
* Why the structure of the Product was kept simple and did not include other meaningful fields normal for an ecommerce platforms, like product variants
* Why the Inventory includes the AvailableQuantity and why not include the ReservedQuantity. Wanted to keep the challenge scope.
* opted to not over complicate the domain model. in real-world scenario, there should be Product, ProductVariant and Inventory and the SKU would be placed in the ProductVariant. but for simplicity only Product was created and SKU was placed in the Product





### swagger page - OK

\- use Swagger for the API / OpenAPI

\- include the authentication part to allow testing (it should be hidden if the env is production)

\- AddSwagger and UseSwagger should only be added if non-production

* add token to the swagger page requests



### caching - OK 

\- use distributed cache to read products. this is for performance benefits - skipped

\- use Redis for caching. This should make it possible to read the product data if the DB is down.

* include a response Cache-Control + ETag + Last-Modified HTTP response headers to indicate that the message is not up to date





### rate limiting - skip this

* ensure the server is not blown with requests! create some kind of configurable backoff time





### Security - OK

\- JWT over other solutions? simple and industry standard. also it is self-sufficient and does not require any external storage to validate the tokens

\- the accounts are expected to be managed externally. accounts will only be injected for demo-purposes



### Messaging - OK

* XXXXX why the usage of Rebus for RabbitMQ instead of OpenMassTransit? simplicity and performance
* why the usage of RabbitMQ over other messaging services? simplicity but other solutions would be equally valid.
* using MassaTransit open-source 8.x (paid version only starting 9.x
* include RabbitMQ Admin Portal to check the messages sent to the queues/topics
* command queues:

  * product.create - commands for new products
  * product.update - commands for update products
  * product.delete - commands for delete products
  * inventory.update - commands for inventory updates
  * inventories.out-of-stock - topic to publish out of stock events
  * products.created - topic to publish events
  * products.updated - topic to publish product update events
  * products.deleted - topic to publish product delete events



### Usage of outbox + inbox - OK

\- inbox: prevent duplicate command processing. idempotency.

\- outbox: to ensure command completion message was sent

* use inbox and outbox patterns with Dapper for better performance!



### Logging - OK

\- why the usage of Serilog for logging? is an industry standard for formatted logging.

\- why the usage of Elasticsearch + Kibana? industry standard along with Kibana, it's open-source, and extremely fast. Elasticsearch can also be easily be used with other tools





### Tests - do this

* add real-case scenarios. don't try to use
* why XUnit? simple and industry standard. also preference. (check the performance)

\- why integration tests? how should this be approached

\- DevContainers for E2E tests? industry standard and is the correct way to test against the production-like scenarios

