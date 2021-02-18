aws --endpoint-url=http://localhost:34566 sns create-topic --name payment-request-topic
"TopicArn": "arn:aws:sns:us-east-1:000000000000:payment-request"

aws --endpoint-url=http://localhost:34566 sqs create-queue --queue-name payment-request-queue
"QueueUrl": "http://localhost:34566/000000000000/payment-request-queue"

aws --endpoint-url=http://localhost:34566 sns subscribe --topic-arn arn:aws:sns:us-east-1:000000000000:payment-request-topic --protocol sqs --notification-endpoint arn:aws:sns:us-east-1:000000000000:payment-request-queue
"SubscriptionArn": "arn:aws:sns:us-east-1:000000000000:payment-request:47e9b20e-8b2e-418c-a776-c5bb6ea7cd9d"

Check message in the queue
aws --endpoint-url=http://localhost:34566 sqs receive-message --queue-url http://localhost:34566/000000000000/payment-request-queue
