aws --endpoint-url=http://localhost:34566 sns create-topic --name payment-process-topic
"TopicArn": "arn:aws:sns:us-east-1:000000000000:payment-process"

aws --endpoint-url=http://localhost:34566 sqs create-queue --queue-name payment-process-queue
"QueueUrl": "http://localhost:34566/000000000000/payment-process-queue"

aws --endpoint-url=http://localhost:34566 sns subscribe --topic-arn arn:aws:sns:us-east-1:000000000000:payment-process-topic --protocol sqs --notification-endpoint arn:aws:sns:us-east-1:000000000000:payment-process-queue
"SubscriptionArn": "arn:aws:sns:us-east-1:000000000000:payment-process-topic:11324408-f9b1-4f0c-b9c2-1a718025f03e"

Check message in the queue
aws --endpoint-url=http://localhost:34566 sqs receive-message --queue-url http://localhost:34566/000000000000/payment-process-queue

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

{
	"PaymentId":"849faf0a-81a6-49a8-84f5-e2b17ca34fd2",
	"CardNumber":"371449635398431",
	"CardCVV":100,
	"Amount":25.0,
	"CurrencyType":"USD",
	"PaymentStatus":"Processing",
	"PaymentStatusDetails":null,
	"BankPaymentId":null}