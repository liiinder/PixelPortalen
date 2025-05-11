using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelPortalen.Shared.DTO
{
    public class AddressInfoDTO
    {
        [Required]
        public string? Country { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string PostalCode { get; set; }
    }
}
