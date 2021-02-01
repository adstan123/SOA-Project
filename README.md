# SOA-Project

SOA-Project is a project containing a Web app exposing secured REST services and using secured microservices provided by an application server, using microservices patterns.
It contains a web app consuming the REST services from a GatewayAPI secured via OAuth 2.0 .
Integration between MusicApi microservice and MusiXMatch third party API.
Containers used to deploy the solution.

![Diagram](https://github.com/adstan123/SOA-Project/blob/main/Diagram.png)

SOA patterns used:
- API gateway : Implement a service which is the entry point into the microservices-based application from external API client
- Domain Inventory : Services are grouped into manageable, domain-specific service inventories, each of which can be independently standardized, governed, and owned.
- Service normalization: service are designed with an emphasis on service boundary alignment.
- Service encapsulation :  solution logic is encapsulated by a service so that it is positioned as an enterprise resource capable of functioning beyond the boundary for which it is initially delivered.
- Containerization : Services are deployed independently, or together with composed services, as autonomous units that are packaged into independently manageable and autonomous container images, each of which includes the services’ underlying system dependencies. Tooling is provided to manage the building, deploying and operating of the containers.
- Trusted Subsystem : service is designed to use its own credentials for authentication and authorization
## Functionalities

- Login
- Search for song and artist based on lyrics
- Save the song search
- View saved searches

