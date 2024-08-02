using System.Security.Cryptography.X509Certificates;

using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

using OneReview.Domain;
using OneReview.Services;

namespace OneReview.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(ProductService productService) : ControllerBase
{
    private readonly ProductService _productService = productService;

    [HttpPost]
    public IActionResult Create(CreateProductRequest request)
    {
        var product = request.ToDomain();

        // create product in database
        _productService.Create(product);

        return CreatedAtAction(
            actionName: nameof(Get),
            routeValues: new
            {
                ProductId = product.Id
            },
            value: ProductResponse.FromDomain(product));
    }

    [HttpGet("{productId:guid}")]
    public IActionResult Get(Guid productId)
    {
        var product = _productService.Get(productId);

        return product is null
            ? Problem(statusCode: StatusCodes.Status404NotFound, title: "Product not found", detail: $"Product with id {productId} was not found.")
            : Ok(ProductResponse.FromDomain(product));
    }
}

public record CreateProductRequest(string Name, string Category, string SubCategory)
{
    public Product ToDomain()
    {
        return new Product()
        {
            Name = Name,
            Category = Category,
            SubCategory = SubCategory
        };
    }
}

public record ProductResponse(Guid Id, string Name, string Category, string SubCategory)
{
    public static ProductResponse FromDomain(Product product) =>
        new ProductResponse(
            Id: product.Id,
            Name: product.Name,
            Category: product.Category,
            SubCategory: product.SubCategory
        );

}
