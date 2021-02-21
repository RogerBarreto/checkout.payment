dotnet publish Checkout.Payment.Processor\Checkout.Payment.Processor.sln -c Release -o ZipLambda
powershell Compress-Archive ZipLambda\* -DestinationPath CheckoutPaymentProcessor.zip -Force
aws --endpoint-url=http://localhost:34566 lambda update-function-code --function-name=checkout-payment-processor --zip-file fileb://CheckoutPaymentProcessor.zip
