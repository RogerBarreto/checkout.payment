Starting Up the Application.
1. Create the docker images
>build-containers.bat 
2. Run all the containers
>docker-compose up
3. Setup localstack with  SNS -> SQS -> Lambda (Processor)
>setup-localstack.bat

4. Access Gateway URL: http://localhost:8001/swagger
5. Generate a valid merchant token: (POST /v1/Authentication) 
	Avaliable merchants: 100 (can be changed in Userrepository in Checkout.Payment.Identity Solution)
	Username Pattern: merchant1, merchant2 ...
	Password Pattern: merchant1password, merchant2password ...

6. Set the JWT token in the Authorize Swagger button
7. Issue a Payment in the Gateway (POST /v1/payment)

8. Get a Payment Status in the Gateway (GET /v1/payment/{paymentId})





Manually setup for localstack

aws --endpoint-url=http://localhost:34566 sns create-topic --name payment-process-topic
"TopicArn": "arn:aws:sns:us-east-1:000000000000:payment-process"

aws --endpoint-url=http://localhost:34566 sqs create-queue --queue-name payment-process-queue
"QueueUrl": "http://localhost:34566/000000000000/payment-process-queue"

aws --endpoint-url=http://localhost:34566 sns subscribe --topic-arn arn:aws:sns:us-east-1:000000000000:payment-process-topic --protocol sqs --notification-endpoint arn:aws:sns:us-east-1:000000000000:payment-process-queue
"SubscriptionArn": "arn:aws:sns:us-east-1:000000000000:payment-process-topic:11324408-f9b1-4f0c-b9c2-1a718025f03e"

Check message in the queue
aws --endpoint-url=http://localhost:34566 sqs receive-message --queue-url http://localhost:34566/000000000000/payment-process-queue

dotnet publish Checkout.Payment.Processor\Checkout.Payment.Processor.sln -c Release -o ZipLambda

powershell Compress-Archive ZipLambda\* -DestinationPath CheckoutPaymentProcessor.zip -Force


aws --endpoint-url=http://localhost:34566 lambda create-function --function-name=checkout-payment-processor --runtime=dotnetcore3.1 --handler=Checkout.Payment.Processor::Checkout.Payment.Processor.Function::FunctionHandler --zip-file fileb://CheckoutPaymentProcessor.zip --role=r1
aws --endpoint-url=http://localhost:34566 lambda update-function-code --function-name=checkout-payment-processor --zip-file fileb://CheckoutPaymentProcessor.zip
aws sqs get-queue-attributes --queue-url http://localhost:34566/000000000000/payment-process-queue --attribute-name QueueArn --endpoint-url http://localhost:34566
aws --endpoint-url=http://localhost:34566 lambda create-event-source-mapping --event-source-arn arn:aws:sqs:us-east-1:000000000000:payment-process-queue --function-name checkout-payment-processor

Checking lambda logs 
aws --endpoint-url=http://localhost:34566 logs describe-log-streams --log-group-name "/aws/lambda/checkout-payment-processor"
aws --endpoint-url=http://localhost:34566 logs get-log-events --log-group-name "/aws/lambda/checkout-payment-processor" --log-stream-name "2021/02/20/[LATEST]52fc809d"
