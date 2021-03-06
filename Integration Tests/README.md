# GodelTech.Microservices.IntegrationTests

## Overview

**GodelTech.Microservices.IntegrationTests** is example of integration tests implementation of
microservice build on **GodelTech.Microservice.Framework**.

**Microservice.Api** is the project under the test.
This microservice defines the ages of the user nicknames and returns the youngest.
Service [https://api.agify.io](https://api.agify.io) is used to define age of the nickname.

There is no controllers to add users. Default user with name **User** is configured during migration.

The main use cases:

### Use case 1

Given default user with name **User**
When post request is sent with user nicknames
Then user nicknames with ages are stored into database

Steps to reproduce use case:

- create JSON Body for User nicknames. Example,

```json
{
    "UserName": "User",
    "Nicknames": [
        "Spiny",
        "Prikle",
        "fPL",
        "potter",
        "cap",
        "noise",
        "Gremlin"
    ]
}
```

- sent POST request to [https://localhost:44362/names/add](https://localhost:44362/names/add) with created body
- you should see nicknames that have been processed in response

### Use case 2

Given default user with name **User**
When get request is sent with user name
Then the youngest nickname is returned

Steps to reproduce use case:

- sent GET request to [https://localhost:44362/names/get/the_youngest/User](https://localhost:44362/names/get/the_youngest/User) with created body
- you should see one of the inserted nickname in response

## Microservice.IntegrationTests

This is the project with integration tests that cover use cases presented above and some use cases with errors.

The main differences in web host configuration are:

- EntityFrameworkInitializer was overwriten by InMemoryDataBaseInitializer. It allows to use in memory database during integration testsinstead of SqlServer;
- External calls to [https://api.agify.io](https://api.agify.io) were mocked by fake services.

## Microservice.SubSystemTests

Docker [https://www.docker.com/](https://www.docker.com/) should be installed on a host where these tests will be run.

This is the project with integration tests that cover use cases presented above and some use cases with errors.

The main diferences in web host configuration are:

- EntityFrameworkInitializer was overwriten by InMemoryDataBaseInitializer. It allows to use in memory database during integration testsinstead of SqlServer;

- External calls to [https://api.agify.io](https://api.agify.io) were mocked using Wiremock docker container.
Pulling wiremock image is reduced the time of test execution. Command: docker pull rodolpheche/wiremock

## Comparison between Microservice.IntegrationTests and Microservice.SubSystemTests

Microservice.IntegrationTests uses test server, inmemory database and mocked external http calls.
Microservice.SubSystemTests uses web kestrel\IIS server, inmemory database and real http request to server (Wiremock) with mocks.

Microservice.IntegrationTests should be executed faster then Microservice.SubSystemTests and do these tests do not need installed docker on host. On the other hand mocked services can have errors and they can not get 100% that external calls will work as expected. That can lead to unexpected bugs.
