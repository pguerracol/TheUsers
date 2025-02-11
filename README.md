# TheUsers Web Api
Description: Web Api to support Users crud operations. 

# Architecture style
The code follows the Onion architecture pattern introduced by Jeffrey Palermo that provides a better way to build applications in a way that allows for decoupled and testable code. The main philosophy of the Onion Architecture is that it's built around the Domain of the application and it depends on the abstraction rather than on the details.

The architecture is depicted as an Onion because it has multiple layers and each layer interacts with its neighboring layers only. The main layers in the Onion Architecture are:
1.	Domain Entities Layer (Core Layer): This is the most internal part of the onion and it holds the business objects (entities) used by the application.
2.	Domain Services Layer: This layer contains interfaces that define operations that can be performed on the domain entities. These operations should adhere to business rules and business logic.
3.	Application Services Layer: This layer is responsible for coordinating tasks and doesn't contain business logic. It depends on the domain layer and uses the interfaces defined in the domain services layer to get the job done.
4.	Infrastructure Layer: This is the outermost layer of the application and it typically contains concerns like UI, Tests, External Services, and Data Access Code. The important thing about this layer is that it depends on the inner layers, meaning the business logic doesn't depend on data access or other infrastructure concerns.

# Practices and libraries used
1.	Custom Handling Exception. To keep clean code relevant from controllers web api and delegate error handling to Net extensions or Net middlewares.
2.	Fluent Validation. To appy fluent builder code style pattern and check business rules seamlesly.

# Assumptions
1. In memory collection. The challenge doc had a bit ambiguos instruction to not use orms like Entity and or micro orm like Dapper, but additional was required to use Sql server, so i focused mainly on the architecture to be flexible in these aspects and in the meantime fast solution like in-memory collection object was followed.
