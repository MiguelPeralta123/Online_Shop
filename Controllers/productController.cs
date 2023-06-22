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
