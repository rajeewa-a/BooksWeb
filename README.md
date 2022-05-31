# BooksWeb
An online bookstore using AspNetCore MVC.

## Functionality

**Admin-**

-   Login
-   Manage Books
-   Manage Book Cover Types
-   Manage Categories/Genres
-   Manage Customer Orders
- View Feedback

**Customer-**

-   Register
-   Login
-   View Book Details
-   Manage Cart
-   Make Payments
-   View Order Details
- Send Feedback

## **Development Environment**

 - Visual Studio 2022
 - .NET 6.0
 - Microsoft SQL Server 2017

## Configuration
Change the connection string and Stripe secret key and publishable key in the BooksWeb/appsettings.json file. (Note: Uses Stripe as the payment gateway. Stripe account is required for payment functionality.)

      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      },
      "AllowedHosts": "*",
      "ConnectionStrings": {
        "DefaultConnection": "Server=DESKTOP-MPDGLCC\\SQLEXPRESS; database=bookstore; Trusted_Connection=True; " //connection string
      },
    
      "Stripe": {
        "SecretKey": "", 
        "PublishableKey": ""
      }
    
    }

  
## Screenshots
![1](https://user-images.githubusercontent.com/70063725/171090590-51e33997-ef81-48ed-9b8e-bf3833a057f3.PNG)
![2](https://user-images.githubusercontent.com/70063725/171090604-132d8821-4860-4899-b125-26c1db9862a9.PNG)
![3](https://user-images.githubusercontent.com/70063725/171090615-dc47c871-7351-4ff6-89ba-d66221e10735.PNG)
![4](https://user-images.githubusercontent.com/70063725/171090618-57f3ad7d-5f23-4c73-9812-a2055e3cb046.PNG)
![5](https://user-images.githubusercontent.com/70063725/171090620-f5bf8380-95f7-4788-8289-198ad401e9f7.PNG)
![6](https://user-images.githubusercontent.com/70063725/171090623-723d3204-29bb-4bbe-b352-2f2831d5366c.PNG)
![7](https://user-images.githubusercontent.com/70063725/171090628-941b31e5-e267-477a-8176-112698e2cfa3.PNG)
![8](https://user-images.githubusercontent.com/70063725/171090630-fce159e7-ad9d-43ab-8f60-939e5e1a91c0.PNG)
![9](https://user-images.githubusercontent.com/70063725/171090632-7eaf280e-691c-4cb1-9798-9c0f3921c10a.PNG)
![10](https://user-images.githubusercontent.com/70063725/171090637-7539bc47-dd55-4b69-be65-ee542f8e2ee4.PNG)
![11](https://user-images.githubusercontent.com/70063725/171090640-5d11e6b7-1b40-4df1-a0a8-24d9e97af020.PNG)
![12](https://user-images.githubusercontent.com/70063725/171090652-8af56db4-4188-4d9f-b098-511071fb9e66.PNG)
![13](https://user-images.githubusercontent.com/70063725/171090659-fec4675d-b448-4d82-8fba-d724cf0c3864.PNG)
![14](https://user-images.githubusercontent.com/70063725/171090663-54fa712f-876d-49d9-93b5-4e308414e0fc.PNG)
![15](https://user-images.githubusercontent.com/70063725/171090671-3a1c6168-a18e-4470-89d0-75dcb75aa96b.PNG)

