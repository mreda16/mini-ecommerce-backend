# mini-ecommerce

A simplified e-commerce backend built with ASP.NET Core Web API, Entity Framework Core, and Blazor.

This project follows Clean Architecture principles and demonstrates:

* Domain-driven design 
* Result pattern for domain validation returns
* DTO projection directly from the database (no in-memory transformation)
* Pagination support with PagedResult
* Discount calculation logic
* Angular integration

---

## Architecture

The solution follows Clean Architecture with strict separation of concerns:

```
mini-ecommerce
 ├── mini-ecommerce.Domain
 ├── mini-ecommerce.Application
 ├── mini-ecommerce.Infrastructure
 ├── mini-ecommerce.API
 └── angular-client
```

### Domain

Contains:

* Entities (Product, Order, OrderItem)
* Business rules and invariants
* Discount calculation logic
* Result<T> pattern for validation

The domain layer does not depend on any external libraries.

---

### Application

Contains:

* DTOs
* Service classes (ProductService, OrderService)
* Repository interfaces
* PagedResult<T>

Responsibilities:

* Orchestrates use cases
* Handles Result wrapping
* Transforms aggregates into DTOs
* Coordinates repositories

No CQRS is used.

---

### Infrastructure

Contains:

* AppDbContext
* EF Core configurations
* ProductRepository
* OrderRepository

Important design decision:

* Products are projected directly to ProductDto in the repository.
* No in-memory mapping of entities to DTOs.
* AsNoTracking is used for read operations.

---

### API

Exposes REST endpoints for:

* Creating products
* Listing products with pagination
* Getting product details by ID
* Creating orders
* Getting order details

Controllers are thin and delegate to services.

---

### Angular

Contains UI Pages:

* Calls the API
* Integrates pagination UI
* Displays paged products
* note: i avoided feature-based arch to make it simple

---

## Features

### Product

Create Product:

* Name required
* Price must be greater than zero
* Quantity must be greater than or equal to zero

List Products:

* Paginated endpoint
* Returns PagedResult<ProductDto>

Get Product Details:

* Returns ProductDto
* Uses direct EF projection

---

### Order

Create Order:

* Validates customer name
* Validates product existence
* Validates stock availability
* Reduces stock
* Applies discount rules
* Persists order

Discount Rules:

* 2–4 items → 5%
* 5 or more items → 10%

Get Order:

* Returns:

    * Order items
    * Item subtotals
    * Discount
    * Final total

---

## Result Pattern

The domain uses the following Result<T> class:

```csharp
public class Result<T>
{
    public bool IsSuccess { get; }
    public string Error { get; }
    public T Value { get; }

    private Result(bool success, T value, string error)
    {
        IsSuccess = success;
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value)
        => new(true, value, string.Empty);

    public static Result<T> Failure(string error)
        => new(false, default!, error);
}
```

Business rule violations return failures instead of throwing exceptions.

---

## Design Decisions

* Clean Architecture with clear dependency flow.
* No CQRS to keep complexity minimal.
* DTO projection performed inside repositories.
* No AutoMapper used.
* Controllers contain no business logic.
* Domain does not depend on EF Core.
* Pagination implemented using PagedResult<T>.

---

## Running the Solution

1. Update connection string in `appsettings.json`.
2. Run database migrations.
3. Start the API project.
4. Start the Blazor project.
5. Navigate to the Products page.

---

This project demonstrates clean layering (did not make it in self-contained modules following Modular Monolith to keep it simple), correct API design, and enforceable business rules in a simplified e-commerce context.