using finalexam_back.Data;
using finalexam_back.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace finalexam_back.Controllers
{
    public class AddToCartRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class DeleteFromCart
    {
        public int ProductId { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : Controller
    {
        private readonly DataContext _context;

        public CartController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cart>>> Index()
        {
            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await this._context.Users.SingleOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                var cartItems = await _context.Carts
                    .Include(c => c.Product)
                    .Where(c => c.UserId == user.Id)
                    .ToListAsync();

                return Ok(cartItems);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddToCartRequest request)
        {
            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await this._context.Users.SingleOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {

                // Check if the product exists
                var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == request.ProductId);
                if (product == null)
                {
                    return BadRequest($"Product {request.ProductId} not found.");
                }

                var cartItem = await _context.Carts.FirstOrDefaultAsync(c => c.ProductId == request.ProductId && c.UserId == user.Id);

                if (cartItem != null)
                {
                    cartItem.Quantity += request.Quantity;
                    product.Quantity -= request.Quantity;
                }
                else
                {
                    cartItem = new Cart
                    {
                        ProductId = request.ProductId,
                        UserId = (int)user.Id,
                        Quantity = request.Quantity
                    };

                    product.Quantity -= request.Quantity;
                    _context.Carts.Add(cartItem);
                }

                await _context.SaveChangesAsync();
                return Ok(cartItem);

            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> deleteProduct([FromBody] DeleteFromCart request)
        {
            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await this._context.Users.SingleOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == request.ProductId);
                if (product == null)
                {
                    return BadRequest($"Product {request.ProductId} not found.");
                }

                var cartItem = await _context.Carts.FirstOrDefaultAsync(c => c.ProductId == request.ProductId && c.UserId == user.Id);
                
                if (cartItem != null)
                {
                    try
                    {
                        product.Quantity += cartItem.Quantity;
                        _context.Carts.Remove(cartItem);
                        await _context.SaveChangesAsync();

                        return Ok($"Product {cartItem.ProductId} deleted");
                    }
                    catch (Exception ex)
                    {
                        return BadRequest($"{ex.Message} id: {cartItem.ProductId}");
                    }
                }
                else
                {
                    return BadRequest("cartItem not found");
                }
            }
            else
            {
                return BadRequest();
            }
            

        }
    }
}
