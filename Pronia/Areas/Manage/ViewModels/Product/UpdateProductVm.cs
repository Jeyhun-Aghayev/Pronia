namespace Pronia.Areas.Manage.ViewModels.Product
{
    public class UpdateProductVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public List<int>? TagId { get; set; }
        public IFormFile MainImage { get; set; }
        public IFormFile SecondImage { get; set; }
        public List<IFormFile>? DetailImages { get; set; }
        public List<ProductİmagesVm> productİmages { get; set; }
    }
    public class ProductİmagesVm
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool? Isprime { get; set; }
    }
}
