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
{
    "FunctionName": "checkout-payment-processor",
    "FunctionArn": "arn:aws:lambda:us-east-1:000000000000:function:checkout-payment-processor",
    "Runtime": "dotnetcore3.1",
    "Role": "r1",
    "Handler": "Checkout.Payment.Processor::Checkout.Payment.Processor.Function::FunctionHandler",
    "CodeSize": 1288561,
    "Description": "",
    "Timeout": 3,
    "LastModified": "2021-02-20T18:15:59.294+0000",
    "CodeSha256": "EB+BzbfB1fLCqP0jd7o/75wgLmbTdZfJnOqnKXH2kdg=",
    "Version": "$LATEST",
    "VpcConfig": {},
    "TracingConfig": {
        "Mode": "PassThrough"
    },
    "RevisionId": "8e067245-18b6-4359-a9be-b71ccfcfee0b",
    "State": "Active",
    "LastUpdateStatus": "Successful",
    "PackageType": "Zip"
}

aws sqs get-queue-attributes --queue-url http://localhost:34566/000000000000/payment-process-queue --attribute-name QueueArn --endpoint-url http://localhost:34566
{
    "Attributes": {
        "QueueArn": "arn:aws:sqs:us-east-1:000000000000:payment-process-queue"
    }
}

aws --endpoint-url=http://localhost:34566 lambda create-event-source-mapping --event-source-arn arn:aws:sqs:us-east-1:000000000000:payment-process-queue --function-name checkout-payment-processor
{
    "UUID": "b173bc2f-6508-4f04-8ffd-e403a5b6188b",
    "StartingPosition": "LATEST",
    "BatchSize": 10,
    "EventSourceArn": "arn:aws:sqs:us-east-1:000000000000:payment-process-queue",
    "FunctionArn": "arn:aws:lambda:us-east-1:000000000000:function:checkout-payment-processor",
    "LastModified": "2021-02-20T18:43:55+00:00",
    "LastProcessingResult": "OK",
    "State": "Enabled",
    "StateTransitionReason": "User action"
}

aws --endpoint-url=http://localhost:34566 logs describe-log-streams --log-group-name "/aws/lambda/checkout-payment-processor"
aws --endpoint-url=http://localhost:34566 logs get-log-events --log-group-name "/aws/lambda/checkout-payment-processor" --log-stream-name "2021/02/20/[LATEST]52fc809d"

aws --endpoint-url=http://localhost:34566 lambda delete-function --function-name checkout-payment-processor

Message Payload

{
    "Messages": [
        {
            "MessageId": "5fa14638-9f0d-51ef-4d30-af37dc73a984",
            "ReceiptHandle": "yzrddgvhrvufdioliwpaxadxvzgmblvoyycbdruqermhmoyteaspumnxtpoedqxnmhcvdbyafkwjmcjhfgpalgfdtrljseqfygmwewtulhtwcymotcjrcythmbdtzmwdgzduzuiuycqqxflvuwtctxerzhveqjxfzkirldvrxvdsgkuedjqtsytrb",
            "MD5OfBody": "50c57fd71f48021e644f36d8f27750ca",
            "Body": "{\"Type\": \"Notification\", \"MessageId\": \"8b432fc9-c6ea-4914-b479-27780df535cd\", \"TopicArn\": \"arn:aws:sns:us-east-1:000000000000:payment-process-topic\", \"Message\": \"{\\\"PaymentId\\\":\\\"849faf0a-81a6-49a8-84f5-e2b17ca34fd2\\\",\\\"CardNumber\\\":\\\"371449635398431\\\",\\\"CardCVV\\\":100,\\\"Amount\\\":25.0,\\\"CurrencyType\\\":\\\"USD\\\",\\\"PaymentStatus\\\":\\\"Processing\\\",\\\"PaymentStatusDetails\\\":null,\\\"BankPaymentId\\\":null}\", \"Timestamp\": \"2021-02-20T05:29:55.347Z\", \"SignatureVersion\": \"1\", \"Signature\": \"EXAMPLEpH+..\", \"SigningCertURL\": \"https://sns.us-east-1.amazonaws.com/SimpleNotificationService-0000000000000000000000.pem\"}",
            "Attributes": {
                "SenderId": "AIDAIT2UOQQY3AUEKVGXU",
                "SentTimestamp": "1613798995362",
                "ApproximateReceiveCount": "1",
                "ApproximateFirstReceiveTimestamp": "1613799051733",
                "MessageGroupId": ""
            }
        }
    ]
}



{\"PaymentId\":\"99826540-3b9e-4cf1-b8a6-f65cb7c4bca2\",\"CardNumber\":\"371449635398431\",\"CardCVV\":323,\"Amount\":1000.0,\"ExpiryDate\":\"2023-02-20T18:36:05.085Z\",\"CurrencyType\":\"EUR\",\"PaymentStatus\":\"Processing\",\"PaymentStatusDetails\":null,\"BankPaymentId\":null}


{
	"PaymentId":"849faf0a-81a6-49a8-84f5-e2b17ca34fd2",
	"CardNumber":"371449635398431",
	"CardCVV":100,
	"Amount":25.0,
	"CurrencyType":"USD",
	"PaymentStatus":"Processing",
	"PaymentStatusDetails":null,
	"BankPaymentId":null}
	
	
	{\"PaymentId\":\"bde63838-f8b3-481b-8b28-6a675d4d8e39\",\"CardNumber\":\"371449635398431\",\"CardCVV\":100,\"Amount\":25.0,\"ExpiryDate\":\"2022-01-01T00:00:00\",\"CurrencyType\":\"USD\",\"PaymentStatus\":\"Processing\",\"PaymentStatusDetails\":null,\"BankPaymentId\":null}