using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelPortalen.Shared.DTO
{
    public class AuthResult
    {
        public int CustomerId { get; set; }
        public bool IsValid { get; set; }
        public string Roll { get; set; } = string.Empty;
        public string? Token { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
