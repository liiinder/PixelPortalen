using PixelPortalen.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelPortalen.Shared.DTO
{
    public class CustomerDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public AddressInfoDTO Address { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
