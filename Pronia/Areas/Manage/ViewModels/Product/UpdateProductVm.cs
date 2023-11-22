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
    }
}
