namespace Pronia.ViewModels
{
    public class ShopVM
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Tag> Tags { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<ProductTag> ProductTags { get; set; }
    }
}
