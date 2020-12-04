using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductRegister.Contexts;
using ProductRegister.Models;
using ProductRegister.Services;

namespace ProductRegister.Controllers
{
    [Authorize]    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _dbContext;

        public ProductController(ProductContext productContext)
        {
            _dbContext = productContext;
        }

        [HttpGet]
        public ActionResult<List<Product>> Get()
        {
            ProductContext productContext = _dbContext;
            ProductService productService = new ProductService(productContext);
            List<Product> products = productService.Find();
            return products;
        }

        [HttpGet("{codigo}")]
        public ActionResult<Product> Get(int codigo)
        {
            ProductContext productContext = _dbContext;
            ProductService productService = new ProductService(productContext);
            Product product = productService.FindById(codigo);
            return product;
        }

        [HttpPost]
        public void Post([FromBody] Product product)
        {
            ProductContext productContext = _dbContext;            
            ProductService productService = new ProductService(productContext);
            productService.AddProduct(product);
        }

        [HttpPut("{codigo}")]
        public void Put(int codigo, [FromBody] Product product)
        {
            ProductContext productContext = _dbContext;
            product.Codigo = codigo;
            ProductService productService = new ProductService(productContext);
            productService.UpdateProduct(product);
        }

        [HttpDelete("{codigo}")]
        public void Delete(int codigo)
        {
            ProductContext productContext = _dbContext;
            ProductService productService = new ProductService(productContext);
            productService.DeleteProduct(codigo);
        }
    }
}
