using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;
using System.Security.Claims;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class productController: ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<productModel>>> getProducts()
        {
            var function = new ProductData();
            var list = await function.listProducts();
            return list;
        }

        [HttpPost]
        [Authorize]
        public async Task postProduct([FromBody] productModel parameters)   
        {
            var function = new ProductData();
            await function.insertProduct(parameters);
        }

        // Role authorization
        /*
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostProduct([FromBody] productModel parameters)
        {
            // Get the user claims from the current authenticated user
            var userClaims = User.Claims;

            // Check if the user has the "admin" role claim
            var isAdmin = userClaims.Any(c => c.Type == "role" && c.Value == "admin");
            if (!isAdmin)
            {
                // Return an unauthorized response if the user is not an admin
                return Unauthorized(new
                {
                    success = false,
                    message = "You don´t have enough permissions",
                    result = ""
                });
            }

            // User is authorized as an admin, proceed with the logic
            var function = new ProductData();
            await function.insertProduct(parameters);

            // Return a success response
            return Ok(new
            {
                success = true,
                message = "Product added successfully",
                result = parameters
            });
        }
        */

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> putProduct(int id, [FromBody] productModel parameters)
        {
            var function = new ProductData();
            parameters.id = id;
            await function.updateProduct(parameters);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> deleteProduct(int id)
        {
            var function = new ProductData();
            await function.deleteProduct(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<List<productModel>>> getProductById(int id)
        {
            var function = new ProductData();
            var list = await function.listProductById(id);
            return list;
        }
    }
}
