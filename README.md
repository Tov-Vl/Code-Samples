# Code-Samples-AgentRateImportService

Code samples for the C#/.NET Backend Developer position from the project AgentRateImportService I've worked on.

AgentRateImportService is a service for importing the rate of the mount engineers into the WFM (Workforce Management System).

## Files description

- `AgentRateEntities` - Entity Framework's DbContext.
- `AgentRepository` - Agents repository with `GetAgents()` method, returning all the agents (mount engineers) from DB.
- `CachingSoEngineerRepository` - `SoEngineerRepository`'s decorator for caching all of its results for all the invocations of the `GetSoEnginners()` method during a single request (via registering it using `PerResolveLifetimeManager` in DI container in `ImportServiceHostFactory`).
- `ImportService` - The class implementing the service's API (`Import()` method).
- `ImportServiceHostFactory ` - DI container's factory.
- `StatisticImportService` - `ImportService`'s decorator for creating and sending a report of the services work (import duration, number of imported agents, their names, etc).
