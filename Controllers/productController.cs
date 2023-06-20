using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class productController: ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<productModel>>> getProducts()
        {
            var function = new ProductData();
            var list = await function.listProducts();
            return list;
        }

        [HttpPost]
        public async Task postProduct([FromBody] productModel parameters)   
        {
            var function = new ProductData();
            await function.insertProduct(parameters);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> putProduct(int id, [FromBody] productModel parameters)
        {
            var function = new ProductData();
            parameters.id = id;
            await function.updateProduct(parameters);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> deleteProduct(int id)
        {
            var function = new ProductData();
            var parameters = new productModel();
            parameters.id = id;
            await function.deleteProduct(parameters);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<productModel>>> getProductById(int id)
        {
            var function = new ProductData();
            var parameters = new productModel();
            parameters.id = id;
            var list = await function.listProductById(parameters);
            return list;
        }
    }
}
