using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication.Classes
{
    public class DbOrder
    {
        public int UserID { get; set; }
        public int OrderID { get; set; }
        public DateTime Date { get; set; }
        public string? Status { get; set; }
        public decimal? TotalCost { get; set; }
        public string? DeliveryMethod { get; set; }
        public string? UserFullName { get; set; }
        public string? UserPhone { get; set; }
        public string? UserEmail { get; set; }
    }
}
