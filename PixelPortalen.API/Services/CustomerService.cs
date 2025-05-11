using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PixelPortalen.API.Repositories.Interfaces;
using PixelPortalen.Shared.DTO;
using PixelPortalen.Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PixelPortalen.API.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly string _jwtSecret;
        private readonly string _issuer;
        private readonly string _audience;

        public CustomerService(ICustomerRepository customerRepository, IConfiguration config)
        {
            this.customerRepository = customerRepository;

            _jwtSecret = config["JwtSecretKey"];
            _issuer = config["Issuer"];
            _audience = config["Audience"];
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await customerRepository.GetAllCustomers();
        }

        public async Task<Customer?> GetByEmail(string email)
        {
            return await customerRepository.GetByEmail(email);
        }
        public async Task<Customer?> GetByPhoneNumber(string phoneNumber)
        {
            return await customerRepository.GetByPhoneNumber(phoneNumber);
        }

        public async Task AddCustomer(CustomerDTO customer)
        {
            var hasher = new PasswordHasher<Customer>();
            var dummyCustomer = new Customer();

            var newCustomer = new Customer
            {
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Password = hasher.HashPassword(dummyCustomer, customer.Password),
                Address = new AddressInfo
                {
                    Address = customer.Address.Address,
                    City = customer.Address.City,
                    Country = customer.Address.Country,
                    PostalCode = customer.Address.PostalCode,
                }
            };

            await customerRepository.Add(newCustomer);
        }

        public async Task<bool> ToggleUserRole(string email)
        {
            var customer = await GetByEmail(email);
            if (customer == null) return false;

            customer.Roll = customer.Roll == "Admin" ? "User" : "Admin";

            await customerRepository.Edit(customer);
            return true;
        }


        public async Task<CustomerDTO> UpdateCustomer(string email, CustomerDTO customer)
        {
            var existingCustomer = await GetByEmail(email);

            if (existingCustomer is null) return null;

            //existingCustomer.Email = string.IsNullOrEmpty(customer.Email) ? existingCustomer.Email : customer.Email;
            existingCustomer.PhoneNumber = string.IsNullOrEmpty(customer.PhoneNumber) ? existingCustomer.PhoneNumber : customer.PhoneNumber;
            existingCustomer.FirstName = string.IsNullOrEmpty(customer.FirstName) ? existingCustomer.FirstName : customer.FirstName;
            existingCustomer.LastName = string.IsNullOrEmpty(customer.LastName) ? existingCustomer.LastName : customer.LastName;
            existingCustomer.Address.Country = string.IsNullOrEmpty(customer.Address.Country) ? existingCustomer.Address.Country : customer.Address.Country;
            existingCustomer.Address.City = string.IsNullOrEmpty(customer.Address.City) ? existingCustomer.Address.City : customer.Address.City;
            existingCustomer.Address.Address = string.IsNullOrEmpty(customer.Address.Address) ? existingCustomer.Address.Address : customer.Address.Address;
            existingCustomer.Address.PostalCode = string.IsNullOrEmpty(customer.Address.PostalCode) ? existingCustomer.Address.PostalCode : customer.Address.PostalCode;

            await customerRepository.Edit(existingCustomer);
            return new CustomerDTO
            {
                Address = new AddressInfoDTO
                {
                    Address = existingCustomer.Address.Address,
                    PostalCode = existingCustomer.Address.PostalCode,
                    City = existingCustomer.Address.City,
                    Country = existingCustomer.Address.Country,
                },
                Email = existingCustomer.Email,
                FirstName = existingCustomer.FirstName,
                LastName = existingCustomer.LastName,
                Password = existingCustomer.Password,
                PhoneNumber = existingCustomer.PhoneNumber,
            };
        }

        public async Task DeleteCustomer(string email)
        {
            var customer = await GetByEmail(email);
            if (customer != null)
            {
                await customerRepository.Remove(customer);
            }
        }

        public async Task<AuthResult> LoginUser(LoginDTO loginInfo)
        {
            var user = await customerRepository.GetByEmail(loginInfo.Email);
            if (user == null || !VerifyPassword(loginInfo.Password, user.Password, user))
            {
                return new AuthResult { IsValid = false };
            }

            return new AuthResult
            {
                IsValid = true,
                Roll = user.Roll,
                Email = user.Email,
                CustomerId = user.Id
            };
        }

        private bool VerifyPassword(string password, string hashedPassword, Customer user)
        {
            var hasher = new PasswordHasher<Customer>();
            var verificationResult = hasher.VerifyHashedPassword(user, hashedPassword, password);

            return verificationResult == PasswordVerificationResult.Success;
        }

        public string GenerateJwtToken(string email, string role, int userId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role),
                new Claim("id", userId.ToString())
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}