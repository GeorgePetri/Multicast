# Multi(cast)

[![.NET](https://github.com/GeorgePetri/Multicast/actions/workflows/dotnet.yml/badge.svg)](https://github.com/GeorgePetri/Multicast/actions/workflows/dotnet.yml)  
Register webhooks and send events to them.

## Running

Use the build-in VS Code launch.json  
**Or** run `dotnet run --project Multicast`

## Design

### Architecture

The project architecture is based on Uncle Bob's Clean Architecture and DDD.

- Multicast.Domain: Contains the Entities (I used Models since Entities is a convention used with EF) and the Interfaces for the Services. Does not depend on any other project. Sometimes called Core.
- Multicast.Persistence: Contains the EF Core DbContext, Migrations and the persistence Service implementations. Depends on Multicast.Domain. Sometimes called Infrastructure, or Data Access Layer.
- Multicast.Web: Contains the Web API Controllers and HttpClients. Depends on Multicast.Domain.
- Multicast.{ProjectName}.Tests: Contains the Unit Tests for the project. Depends on Multicast.{ProjectName}.

### Why Clean Architecture and not N-Layer?

In N-Layer, the layers are dependent on each other. An example design would be Multicast.Web -> Multicast.Domain -> Multicast.Persistence.
This means that there is an implicit dependency between the Web and Persistence layers, and the Domain layer isn't pure since it depends on EF.
Using Clean Architecture, the Domain is pure and does not depend on any other layer. The Web and Persistence layers depend on the Domain layer, making them effectively libraries that can be replaced without affecting the Domain layer.

### Domain

Used immutable records for the models. Their value semantics and immutability make them easier to reason about and use and make them an excellent choice for DDD Entities.
Only Service interfaces are here, no implementations, to keep the Domain layer pure and the code modular

### Persistence

EF Core is used for the persistence layer, because it is an excellent ORM that is the industry standard. It features powerful and easy to use querying using LINQ, migrations, escape hatches to SQL and a lot of other features.
Entities are are standard classes and they are configured using `IEntityTypeConfiguration<T>` classes. This allows for a clean separation of concerns and makes the code easier to read and maintain when the project grows.

### Web

Here the controllers and clients are defined. The controllers are thin and only contain the necessary logic to call the services.
The application features:

- Swagger UI and OpenApi when developing and testing
- Problem Details for RFC 9457 compliant error responses
- Api Versioning

### Tests

The tests are written using xUnit and Moq. They mirror the project structure and test the services and controllers. The tests are run using GitHub Actions.

### Multicast root project

Used to glue everything together. It contains the Program.cs and Startup.cs files, and the DI configuration.
No actual business logic is here, only the necessary code to start the application.

## Future improvements

### Better testing

Currently only unit tests for a controller are implemented. More tests are needed, especially for the services and the persistence layer.
Integration and end-to-end tests are also needed.
All the tests should be run in the CI pipeline.
Gates and quality checks can be added to the pipeline and run on every PR to ensure the quality of the codebase.

### Containers and orchestration

The application can be containerized and deployed to Kubernetes. This would make the application more scalable and independent of the host environment.
Several changes would need to be made:

- Adding a Dockerfile
- Adding a Kubernetes manifest
- Configuring CI/CD to build and deploy
- Adding health checks
- Using configs and secrets that work well with Kubernetes

### Security

#### Authorization and authentication

Authorization and authentication are not implemented. This is a critical feature that needs to be added.
The application can be secured using OAuth2 or JWT tokens.

#### Throttling and rate limiting

Throttling and rate limiting can be added to prevent abuse of the API, this is somewhat dependent on the authentication and authorization mechanism.
It can be implement using a library, a service or both.

### Monitoring and logging

Proper logging and monitoring are essential for a production application.
`ILogger` is an abstraction that supports high performance structured logging and can be used to log to various sinks.
A proper solution should take into account the current logging and monitoring infrastructure used by the organization.

### Scalability

Multiple instances of the application can be run behind a load balancer to increase the application's availability and scalability. Kubernetes can be used for this.
Sending the events to the webhooks can be done using a message queue like RabbitMQ or Kafka. This would allow for better scalability and reliability.
If the DB is the bottleneck, it can be sharded or replaced with a more scalable solution.

### Developer Experience

Improving the developer experience is important for the productivity of the team.
This can be improved by:

- Adding proper documentation
- Maintaining good quality code by using gates, linters, analyzers, autoformatters and code reviews.
- CD to Dev and maybe even PR environments

<sub><sup>@Future_George: This is a homework project for a job interview, it can be deleted.<sup><sub>
