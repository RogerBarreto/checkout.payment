Checkout Payment
================

### Business & Non Function Requirement Assumptions
1. Merchants should authenticate in Checkout Identity server and get a token prior to use the Gateway

2. The Merchant would be of 2 types
  - Merchants Users - Authenticate using password
  - Merchant APIs - Authenticate using ClientId and ClientSecret 

3. High scalability of the services (each component should scale horizontally acording with the demand)
4. Process the payment operation in an async manner
5. Resilient payment requests, retry in the event of network instability or internal errors from Acquiring Bank API.
6. Merchants can only see payments that they requested
7. Maximum Expiry data validates to 20 years ahead.
8. The Identity/Gateway APIs will be in Public Subnet
9. CQRS Microservices and Resources will reside in Private Subnet

### This project consist of:
* **Checkout.Payment.Identity** - Solution containing a real identity server that Authenticates the Merchant and generates a valid JWT token with claims
* **Checkout.Payment.Gateway** - Solution containing the logic to authenticate a merchant, send and check for payment status
* **Checkout.Payment.Command** - Solution containing the CQRS Command logic to perform command actions (Payment Request)
* **Checkout.Payment.Processor** - Solution contining lambda ready logic to process qeueud payment requests against an AcquiringBank API
* **Checkout.Payment.AcquiringBankMock** - Solution containing logic to mock random payment results from an AcquiringBank API
* **Checkout.Payment.Query** - Solution containing the CQRS Query logic to perform query actions (Check payment status)
* **LocalStack** - AWS Mock container to simulate SNS, SQS and Lambda cloud behavior.
* **Redis** - Redis container to simulate Redis Distributed Cache persistency across Command/Query.

## Architecture Logical View
![Architecture Logical View](https://rogerbarreto.github.io/checkout.payment/Documents/ArchitectureLogicView.svg)

## Workflow Sequence Diagram

![Workflow Sequence View](https://rogerbarreto.github.io/checkout.payment/Documents/SequenceOverview.svg)

----
## Getting Started

### 1. Go to the base directory of the repository and run the batch to build the docker images
```
	> build-containers.bat 
```
### 2. Run all the containers in docker-composer 
```
	> docker-compose up -d
```
>`(Remove -d arg if you want to  see the composer containers logs in the same window)`

### 3. Wait for localstack **Ready** state and Run the batch to setup localstack services (SNS -> SQS -> Lambda (Processor)
```
	> setup-localstack.bat
```
### 4. Access the Gateway  
-----
## Using the Payment Gateway:

### 1. Access Gateway Swagger at URL: http://localhost:8001/swagger
Valid **Merchant User Password** credentials follows the given pattern

Avaliable merchants: **100**
> The number of merchants and its details can be updated in (Checkout.Payment.Identity UserRepository logic)

| id | username | password |
| -- | -------- | -------- |
| **1-100** | merchant{**id**} | merchant{**id**}password |
| 3 | merchant**3** | merchant**3**password |


Valid **Merchant API** credentials follows the given pattern

Avaliable merchants: **100**
> The number of merchants and its details can be updated in (Checkout.Payment.Identity ApiKeyRepository and Client logic)

| id | Api Key | Api Secret |
| -- | ------- | ---------- |
| **1-100** | merchant.api.{**id**}.key | merchant.api.{**id**}.secret |
| 3 | merchant.api.**3**.key | merchant.api.**3**.secret |  

### 2. Get a  merchant token: (POST /v1/Authentication) using a valid credential
### 3. Set the JWT token in the Authorize Swagger button
### 4. Create a Payment in the Gateway - **POST /v1/payment**
### 5. Get a Payment Status in the Gateway - **GET /v1/payment/{paymentId}**

| Code | Payment StatusCode Description |
| ---- | ----------- |
| 100 | Processing |
| 200 | Succeeded |
| 400 | Rejected - Incorrect |
| 401 | Rejected Insufficient Funds |
| 402 | Rejected Card Blocked |
| 403 | Rejected Custom |
| 500 | Unexpected |
|

### **Improvements Backlog**
* Add domain validations (FluentValidation)
* Implement UnitTests to all Solutions
* CodeCoverage Check during image build
* End-to-End Integatrion Tests
* Performance Tests using tools like Locust
* Transaction-Id header Middleware to map the transaction workflow across the APIs
* Use of a Common Solution Nuget package for SeedWork / Extensions / HttpClients
* Add dedicated Health Check endpoints for LoadBalancing check
* Add Metrics UI for each API
* Setup a full CICD script to deploy in the cloud

### **Tools / Swagger UIs**
* Redis UI: http://localhost:8081
* Payment Gateway http://localhost:8001
* Payment Command http://localhost:8002
* Payment Query http://localhost:8004
* Acquiring Bank Mock http://localhost:8003

### **Useful commands**

### Manual setup for localstack (SNS + SQS + Subscription)

```
aws --endpoint-url=http://localhost:34566 sns create-topic --name payment-process-topic
"TopicArn": "arn:aws:sns:us-east-1:000000000000:payment-process"

aws --endpoint-url=http://localhost:34566 sqs create-queue --queue-name payment-process-queue
"QueueUrl": "http://localhost:34566/000000000000/payment-process-queue"

aws --endpoint-url=http://localhost:34566 sns subscribe --topic-arn arn:aws:sns:us-east-1:000000000000:payment-process-topic --protocol sqs --notification-endpoint arn:aws:sns:us-east-1:000000000000:payment-process-queue
```
### Getting SQS Arn (Needed to subscribe Lambda into the SQS)
```
aws sqs get-queue-attributes --queue-url http://localhost:34566/000000000000/payment-process-queue --attribute-name QueueArn --endpoint-url http://localhost:34566
```

### Generating .NET Core Processor Lambda Content to localstack
```
dotnet publish Checkout.Payment.Processor\Checkout.Payment.Processor.sln -c Release -o ZipLambda
powershell Compress-Archive ZipLambda\* -DestinationPath CheckoutPaymentProcessor.zip -Force
```

### Create Processor lambda
```
aws --endpoint-url=http://localhost:34566 lambda create-function --function-name=checkout-payment-processor --runtime=dotnetcore3.1 --handler=Checkout.Payment.Processor::Checkout.Payment.Processor.Function::FunctionHandler --zip-file fileb://CheckoutPaymentProcessor.zip --role=r1
```
### Update Processor lambda code
```
aws --endpoint-url=http://localhost:34566 lambda update-function-code --function-name=checkout-payment-processor --zip-file fileb://CheckoutPaymentProcessor.zip
```

### Subscribing Processor lambda to SQS
```
aws --endpoint-url=http://localhost:34566 lambda create-event-source-mapping --event-source-arn arn:aws:sqs:us-east-1:000000000000:payment-process-queue --function-name checkout-payment-processor
```

### Debug/Troubleshoot Lambda Logs
```
aws --endpoint-url=http://localhost:34566 logs describe-log-streams --log-group-name "/aws/lambda/checkout-payment-processor"
aws --endpoint-url=http://localhost:34566 logs get-log-events --log-group-name "/aws/lambda/checkout-payment-processor" --log-stream-name "yyyy/MM/dd/[LATEST]12345678"
```


