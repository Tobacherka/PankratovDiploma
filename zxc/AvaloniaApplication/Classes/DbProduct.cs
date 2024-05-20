using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication.Classes
{
    public class DbProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public byte[]? image { get; set; }
        public double? Weight { get; set; }
        public string? Materials { get; set; }
        public string? Color { get; set; }
        public string? Warranty { get; set; }
        public string? Description { get; set; }
    }
}
