aws --endpoint-url=http://localhost:34566 sns create-topic --name payment-request-topic
"TopicArn": "arn:aws:sns:us-east-1:000000000000:payment-request"

aws --endpoint-url=http://localhost:34566 sqs create-queue --queue-name payment-request-queue
"QueueUrl": "http://localhost:34566/000000000000/payment-request-queue"

aws --endpoint-url=http://localhost:34566 sns subscribe --topic-arn arn:aws:sns:us-east-1:000000000000:payment-request-topic --protocol sqs --notification-endpoint arn:aws:sns:us-east-1:000000000000:payment-request-queue
"SubscriptionArn": "arn:aws:sns:us-east-1:000000000000:payment-request:47e9b20e-8b2e-418c-a776-c5bb6ea7cd9d"

Check message in the queue
aws --endpoint-url=http://localhost:34566 sqs receive-message --queue-url http://localhost:34566/000000000000/payment-request-queue

Message Payload
{
    "Messages": [
        {
            "MessageId": "a2af0e4b-7036-7484-4c4f-1af3b9b992f7",
            "ReceiptHandle": "ahunwdjewcxputyxthigspkqptkduxzbzdqudluiacqkatozyaasgvvqigfemidittjdcnliezcgfugessrjhhoohyybiosuhxmdagzuvyfzhjgohajmaljlrcfakgwykldmanxmwklifsimwbuhzjecajeolacooazmcrpdyyorfegsgqbeapvdz",
            "MD5OfBody": "c60df46e391d91afdd526444d4865f60",
            "Body": "{\"Type\": \"Notification\", \"MessageId\": \"01a9fa46-06cb-4f21-bc07-57a2ae0ab375\", \"TopicArn\": \"arn:aws:sns:us-east-1:000000000000:payment-request\", \"Message\": \"{\\\"PaymentId\\\":\\\"5a3582e3-2e5a-4086-b7ee-d0474611092c\\\",\\\"CardNumber\\\":\\\"371449635398431\\\",\\\"CardCVV\\\":100,\\\"Amount\\\":100,\\\"CurrencyType\\\":1,\\\"PaymentStatus\\\":0}\", \"Timestamp\": \"2021-02-18T16:52:41.648Z\", \"SignatureVersion\": \"1\", \"Signature\": \"EXAMPLEpH+..\", \"SigningCertURL\": \"https://sns.us-east-1.amazonaws.com/SimpleNotificationService-0000000000000000000000.pem\"}",
            "Attributes": {
                "SenderId": "AIDAIT2UOQQY3AUEKVGXU",
                "SentTimestamp": "1613667161674",
                "ApproximateReceiveCount": "1",
                "ApproximateFirstReceiveTimestamp": "1613667425451",
                "MessageGroupId": ""
            }
        }
    ]
}