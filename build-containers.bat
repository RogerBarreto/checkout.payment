cd Checkout.Payment.Identity
docker build -f Dockerfile -t checkout-payment-identity .
cd ..\Checkout.Payment.Command
docker build -f Dockerfile -t checkout-payment-command .
cd ..\Checkout.Payment.Query
docker build -f Dockerfile -t checkout-payment-query .
cd ..\Checkout.Payment.Gateway
docker build -f Dockerfile -t checkout-payment-gateway .
cd ..\Checkout.Payment.AcquiringBankMock
docker build -f Dockerfile -t checkout-payment-acquiringbankmock .
cd ..


::Individually Running Containers
::docker run --name Checkout.Payment.Identity -d -p 5001:443 checkoutpaymentidentity
::docker run --name Checkout.Payment.Gateway -d -p 8001:80 checkout-payment-gateway
::docker run --name Checkout.Payment.Command -d -p 8002:80 checkout-payment-command
::docker run --name Checkout.Payment.AcquiringBankMock -d -p 8003:80 checkout-payment-acquiringbankmock
::docker run --name Checkout.Payment.Query -d -p 8004:80 checkout-payment-query
