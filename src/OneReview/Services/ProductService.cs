using OneReview.Domain;
using OneReview.Errors;
using OneReview.Persistence.Database.Repositories;

namespace OneReview.Services;

public class ProductService(ProductsRepository productsRepository)
{
    private readonly ProductsRepository _productsRepository = productsRepository;

    public async Task CreateAsync(Product product)
    {
        throw new NotFoundException("Product not found");

        await _productsRepository.CreateAsync(product);
    }

    public async Task<Product?> GetAsync(Guid productId)
    {
        return await _productsRepository.GetByIdAsync(productId);
    }

}
