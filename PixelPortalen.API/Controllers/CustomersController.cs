using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PixelPortalen.API.Data;
using PixelPortalen.API.Services;
using PixelPortalen.Shared.Models;
using PixelPortalen.Shared.DTO;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace PixelPortalen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService customerService;
        private readonly string _jwtSecret;
        private readonly string _issuer;
        private readonly string _audience;

        public CustomersController(CustomerService customerService)
        {
            this.customerService = customerService;

        }

        // GET: api/Customers
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await customerService.GetAllCustomers();

            return Ok(customers);
        }

        // GET: api/Customers/5
        [HttpGet("{email}")]
        public async Task<ActionResult<Customer>> GetCustomer(string email)
        {
            var customer = await customerService.GetByEmail(email);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{email}")]
        public async Task<IActionResult> PutCustomer(string email, CustomerDTO customer)
        {
            var result = await customerService.UpdateCustomer(email, customer);
            if (result is null)
                return NotFound("Did not find a customer by that email.");

            return Ok("Customer was updated successfully.");
        }
        
        
        [HttpPut("toggle-role/{email}")]
        public async Task<IActionResult> ToggleUserRole(string email)
        {
            var result = await customerService.ToggleUserRole(email);
            if (!result)
                return NotFound("Did not find a customer by that email.");

            return Ok("Customer role toggled successfully.");
        }


        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("register")]
        public async Task<ActionResult<Customer>> RegisterCustomer(CustomerDTO customer)
        {
            var userExists = await customerService.GetByEmail(customer.Email);
            if (userExists is not null)
                return BadRequest("En användare med den e-postadressen finns redan.");

            var phoneExists = await customerService.GetByPhoneNumber(customer.PhoneNumber);
            if (phoneExists is not null)
                return BadRequest("En användare med det telefonnumret finns redan.");

            await customerService.AddCustomer(customer);

            return CreatedAtAction(nameof(GetCustomer), new { email = customer.Email }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteCustomer(string email)
        {
            await customerService.DeleteCustomer(email);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO user)
        {
            var result = await customerService.LoginUser(user);

            if (result.IsValid == false)
                return Unauthorized();

            var token = customerService.GenerateJwtToken(result.Email ,result.Roll, result.CustomerId);
            result.Token = token;

            return Ok(result);
        }
    }
}