using PixelPortalen.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelPortalen.Shared.DTO
{
    public class OrderDTO
    {
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime ShipDate { get; set; }
        public List<OrderDetailDTO> Details { get; set; }
    }
}
