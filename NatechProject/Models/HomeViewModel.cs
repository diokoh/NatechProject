namespace NatechProject.Models
{
    public class HomeViewModel
    {
        public int SalesmanCount { get; set; }  
        public double TotalSales { get; set; }

        public HomeViewModel(int salesmanCount, double totalSales)
        {
            SalesmanCount = salesmanCount;
            TotalSales = totalSales;
        }
    }
}
