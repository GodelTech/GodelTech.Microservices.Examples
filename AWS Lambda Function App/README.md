# GodeTech.Microservices.LambdaFunctionApp

## Overview

GodeTech.Microservices.LambdaFunctionApp is example of aws serverless application which is based on GodelTech.Microservice.Framework.

## Deploy GodeTech.Microservices.LambdaFunctionApp  

GodeTech.Microservices.LambdaFunctionApp is deployed using AWS Cloud formation template (see serverless.template).

Following cli tools requires:
 - [aws cli](https://github.com/aws/aws-cli).
 - [dotnet lambda cli](https://github.com/aws/aws-lambda-dotnet).

## Steps to deploy AWS serverless application

1 - Create user with required permissions in aws IAN service.
Required permissions depend on the resources (stack, s3, roles and so on) and actions are used for deploing process.

2 - Configure aws cli tools using following command and credentials from step 1:

```sh
$ aws configure
AWS Access Key ID [None]: *****
AWS Secret Access Key [None]: *****
Default region name [NONE]: ******
Default output format [NONE]: json
```

3 - Check AWS cloud formation template (serverless.template) and add additional properties if they are required.
Description of properties for AWS::Serverless::Function you can find here:
<https://docs.aws.amazon.com/serverless-application-model/latest/developerguide/sam-resource-function.html>.

4 - Configure parameters for aws tools in aws-lambda-tools-default.json.

Description of some properties:
  * "profile" - name of aws profile. By default this name "default";
  * "region" - aws region;
  * "template" - path to AWS cloud formation template;
  * "s3-bucket" - name of s3 bucket where built application will be uploaded;
  * "stack-name" - name of stack which will be used to deploy application.

5 - Deploy serverless application using command:

```sh
$ dotnet lambda deploy-serverless
```

.NEt Core 3.0 has new build concept called [ReadyToRun](https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-0#readytorun-images).
It can improve startup time of application that has large ammount of code.
If you want build your application as R2R you need to provide additional build parameters:

```sh
$ dotnet lambda deploy-serverless --msbuild-parameters "/p:PublishReadyToRun=true --self-contained false"
```

### Usefull links

1.[Announcing AWS Lambda support for .NET Core 3.1](https://aws.amazon.com/blogs/compute/announcing-aws-lambda-supports-for-net-core-3-1/)
2.[.NET Core 3.0 on Lambda with AWS Lambda’s Custom Runtime](https://aws.amazon.com/blogs/developer/net-core-3-0-on-lambda-with-aws-lambdas-custom-runtime/)