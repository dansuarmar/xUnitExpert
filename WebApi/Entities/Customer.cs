namespace WebApi.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public DateTime CreatedDate { get; set; }
        public decimal Credit { get; set; }
    }
}
