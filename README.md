# Carton

[![AppVeyor](https://img.shields.io/appveyor/ci/fedjabosnic/hotfix.svg?style=flat-square)](https://ci.appveyor.com/project/fedjabosnic/carton/history)


## Server

The `Carton.Service` project is a RESTful api for managing carts of items

> The api is a `prototype` and currently manages carts and items in memory.

### API
The following api endpoints are exposed:

- `/api/v1/carts`
    - `POST` - creates a new cart
- `/api/v1/carts/{cartId}`
    - `GET` - returns the specified cart
    - `DELETE` - deletes the specified cart
- `api/v1/carts/{cartId}/items`
    - `POST` - adds a new item to the specified cart
    - `DELETE` - clears the items from the specified cart
- `api/v1/carts/{cartId}/items/{itemId}`
    - `PUT` - updates the specified item in the specified cart
    - `DELETE` - removes the specified item from the specified cart

### Swagger

The server api uses swagger:
- `/swagger` for the ui
- `/swagger/v1/swagger.json` for the schema


## Client API

The `Carton.Client` project is a `dotnet standard` client-side api.

#### Usage

```csharp
var service = Carton.Client.V1.Service.Create("http://api.carton.com");

// Create a cart
var cart = await service.CreateCart();

// Add items to the cart
var item1 = await service.AddItemToCart(cart.Data.CartId, "fork", 1);
var item2 = await service.AddItemToCart(cart.Data.CartId, "book", 3);

// Update an item in the cart
service.UpdateItemInCart(cart.Data.CartId, item1.Data.ItemId, "fork", 3);

// Get latest changes to the cart
cart = await service.GetCart(cart.Data.CartId);


// Show what's in our cart...
Console.WriteLine($"Cart: '{cart.Data.CartId}' contains ({final.Data.Items.Count} items):");

foreach(var item in cart.Data.Items)
{
    Console.WriteLine($"- '{item.ItemId}': {item.Quantity} x {item.Product}");
}

```