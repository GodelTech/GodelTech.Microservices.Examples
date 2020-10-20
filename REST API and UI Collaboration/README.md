# GodelTech.Microservices.WebAndApiCollaboration

## Overview

**GodelTech.Microservices.WebAndApiCollaboration** is example of system that includes following interacting parts:

- REST API Website
- UI Website based on Razor Pages performed API call
- Identity Server responsible for authentication and authorization

All these parts is based on **GodelTech.Microservice.Framework**.

Expected use case for this application is the web application that interacts with protected resources on behalf of authenticated user.

## GodelTech.Microservices.WebAndApiCollaboration.Web

This is the client which performs Api calls on behalf of users.

There are two main use cases in this application to show authentication and authorization processes.

Use case 1:
Given authenticated user and authorized client
When ask for protected api resources
Then protected resources is available

Steps to reproduce use case:

- open web client home page
- if you are not authenticated click on "Login" button in the upper left corner
- if you have already authenticated click on "Logout" button in the upper left corner and repeat step 2
- you will be redirected into identity server for authentication process
- click "Login" button (there are no credentials just to simplify example)
- you will be redirected back to home page
- click on "Protected Resources" button
- you should see "Protected resource" on the page

Use case 2:
Given unauthenticated user
When ask for protected api resources
Then unauthorized error page is returned

Steps to reproduce use case:

- open web client home page
- if you have already been authenticated then click on "Logout" button in the left upper corner
- click on "Protected Resources" button
- you should be redirect to unauthorized error page

Authentication and authorization process implementation details:

Authentication process uses following schemas "Cookies" and "OpenIdConnect".
Middleware for this process is setup in [GodelTech.Microservices.Security](https://github.com/GodelTech/GodelTech.Microservices.Security)
and added to client pipeline (see. UiSecurityInitializer in startup pipeline). The only thing we should configure is client settings.
They are configured in appSettings.json file (please see [GodelTech.Microservices.Security](https://github.com/GodelTech/GodelTech.Microservices.Security) for settings description).

After authentication process for user and authorization process for client successfully finished identity server sent jwt token to client.
Jwt is stored and added into api calls for authorization.

## GodelTech.Microservices.WebAndApiCollaboration.IdentityServer

This is server which is responsible for authentication and authorization process.
[Identity server 4 framework](https://github.com/IdentityServer/IdentityServer4) is used to implement "OpemId" protocol.

Identity server was setup only for example purpose and has all configuration for clients and scopes into appsettings.json file.
Also authentication process does not need names or passwords to simplify authentication process.

## GodelTech.Microservices.WebAndApiCollaboration.Api

This is protected api server which has one protected endpoint.

Authorization process for this endpoint setup in [GodelTech.Microservices.Security](https://github.com/GodelTech/GodelTech.Microservices.Security)
and added to client pipeline (see. ApiSecurityInitializer in startup pipline).
Configuration for these pipeline are provided in appsettings.json file (please see [GodelTech.Microservices.Security](https://github.com/GodelTech/GodelTech.Microservices.Security) for settings description).
