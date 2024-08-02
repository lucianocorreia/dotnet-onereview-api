using OneReview.Domain;

namespace OneReview.Services;

public class ProductService
{
    private static readonly List<Product> ProductRepository = [];

    public void Create(Product product)
    {
        ProductRepository.Add(product);
    }

    public Product? Get(Guid productId)
    {
        // create product in database
        return ProductRepository.Find(p => p.Id == productId);
    }

}
