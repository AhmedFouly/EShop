namespace Catalog.Products.Models
{
    public class Product : Aggregate<Guid>
    {
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!; 
        public string ImageFile { get; private set; } = default!;
        public decimal Price { get; private set; }
        public List<string> Category { get; private set; } = new();

        public static Product Create(Guid id, string name, List<string> category, string description, string imageFile, decimal price)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            Product product = new()
            {
                Id = id,
                Name = name,
                Description = description,
                ImageFile = imageFile,
                Price = price,
                Category = category
            };

            product.AddDomainEvent(new ProductCreatedEvent(product));

            return product;
        }

        public void Update(string name, List<string> category, string description, string imageFile, decimal price)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            Name = name;
            Description = description;
            ImageFile = imageFile;
            Price = price;
            Category = category;

            if(Price != price)
            {
                Price = price;
                AddDomainEvent(new ProductPriceChangedEvent(this));
            }
        }
    }
}
