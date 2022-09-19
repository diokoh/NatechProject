using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NatechProject.Models
{
    public class Salesman
    {
        public int SalesmanId { get; set; }

        public string Name { get; set; }
        public ICollection<Sale>? Sales { get; set; }
        [NotMapped]
        public double Commission { get; set; }   

        public ICollection<MonthSales>? monthSales { get; set; }

    }
    [NotMapped]
    public class MonthSales
    {
        public string Month { get; set; }
        
        public double TotalSales { get; set; }
        public double TotalCommission { get; set; }
    }

}
