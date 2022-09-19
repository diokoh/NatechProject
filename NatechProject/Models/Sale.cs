namespace NatechProject.Models
{
    public class Sale
    {
        public int SaleId { get; set; } 
        public double SalePrice { get; set; }
        public DateTime SaleDate { get; set; }

        public int SalesmanId { get; set; }
        public Salesman? Salesman { get; set; }
    }
}
