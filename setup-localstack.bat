dotnet publish Checkout.Payment.Processor\Checkout.Payment.Processor.sln -c Release -o ZipLambda
powershell Compress-Archive ZipLambda\* -DestinationPath CheckoutPaymentProcessor.zip -Force

aws --endpoint-url=http://localhost:34566 sns create-topic --name payment-process-topic
aws --endpoint-url=http://localhost:34566 sqs create-queue --queue-name payment-process-queue
aws --endpoint-url=http://localhost:34566 sns subscribe --topic-arn arn:aws:sns:us-east-1:000000000000:payment-process-topic --protocol sqs --notification-endpoint arn:aws:sns:us-east-1:000000000000:payment-process-queue
aws --endpoint-url=http://localhost:34566 lambda create-function --function-name=checkout-payment-processor --runtime=dotnetcore3.1 --handler=Checkout.Payment.Processor::Checkout.Payment.Processor.Function::FunctionHandler --zip-file fileb://CheckoutPaymentProcessor.zip --role=r1
aws --endpoint-url=http://localhost:34566 lambda create-event-source-mapping --event-source-arn arn:aws:sqs:us-east-1:000000000000:payment-process-queue --function-name checkout-payment-processor