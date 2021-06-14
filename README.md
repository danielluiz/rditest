# RDI Test Solution
  
&nbsp;  

## Real test

I made now two new endpoints that do as asked:

* /card/save
* /card/validate

Both are in the swagger docs

## Proposition


You are tasked with writing a piece of software to do a token generation for cashless registration.
Your microservice should expose 2 different APIs
You must use the .Net core platform to develop the solution.
  
&nbsp;  
## Concept

I followed the cashless concept that I found here:
https://www.gevme.com/en/gevme-cashless-token-system-works/
  
&nbsp;  
## Solution

The solution is based on two endpoints:

* Buy Token(s) (/token/buy)
* Spend Token(s) (/token/spend)

### Buy Token(s)   
  
&nbsp;  
Customers can buy tokens, they login through the app and buy with the payment methods available on the system.

The login method will not be available for this test solution, but it would be a simple login that returns a JWT token, that remains valid for 1 day (time will be 1 year for testing tokens).

Each token will have the value of R$ 0.10

Here are some customer tokens for testing:
* eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJleHAiOjE2NTUxNjI0NzYsImN1c3RvbWVySWQiOiJDVVMxIn0.ykdhzatP3cICsCKm6r7zq-5f-bQnqXP22p37HWuy-Us
* eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJleHAiOjE2NTUxNjU4MTEsImN1c3RvbWVySWQiOiJDVVMyIn0.aheibdjYvJR6f04THJYr9moC7wMkJzwDEu1h9gAfHuc

The endpoint has to be called like this:

    ROUTE: /token/buy
    METHOD: POST
    HEADERS:
    Authorization: Bearer [Token]
    DATA:
    {
        "paymentMethod": "",
        "paymentData": {
            [This object will change based on paymentMethod]
        }
        "tokenQuantity": 0        
    }

These are the payment methods available at the moment:

    CreditCard
    DebitCard
    GooglePay
    ApplePay
    Pix

For the CreditCard and DebitCard payment methods, the paymentData is:

    {
        "cardNumber": "",
        "expirationDate": "",
        "cardName": "",
        "cvv": "",
        "customerDocument": ""
    }

For the GooglePay, ApplePay and Pix payment methods, the paymentData is:
    
    {
        "token": ""
    }


  
&nbsp; 
### Spend Tokens
  
&nbsp; 

To spend tokens, the customer scans the vendor QR Code through the app and select the number of tokens to transfer, it can come pre-selected if the vendor generates a specific QR Code, but QR code generation and consuming will be not be covered in this solution, the scan process is offline, every QR code transports the vendor code and has to be read in the client.

The endpoit has to be called as follows:

    ROUTE: /token/spend
    METHOD: POST
    HEADERS:
    Authentication: Bearer [Token]
    DATA:
    {
        "vendorId": "",
        "tokenQuantity": ""
    }

If the user has that token quantity, it will be transfered to the vendor account.

### Status

So that we can see the app is working, there's an status endpoint with all the in-memory data, the app is configured to open on it, there are three  endpoints:

* /status
* /status/token1
* /status/token2

The other two endpoints generate new tokens for customers 1 and 2

### Swagger

It's enabled on the route "/swagger"

## Improvements

Some points I couldn't do due to time constraints or because I thought about it too late

* Make everything async
* Validate input in a better way (Make a method to check the properties on the buy endpoint)
* Add Serilog/Insigths or some other log library
* Abstract results
* More tests

