using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductRegister.Contexts;
using ProductRegister.DbAccess;
using ProductRegister.Models;
using ProductRegister.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRegister.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductOdbcController : ControllerBase
    {
        private readonly EntityAccess _entityAccess;

        public ProductOdbcController()
        {

            _entityAccess = new EntityAccess();
        }

        [HttpGet]
        public ActionResult<List<Product>> Get()
        {
            EntityAccess entityAccess = _entityAccess;
            ProductService productService = new ProductService(entityAccess);
            List<Product> products = productService.FindOdbc();
            return products;
        }

        [HttpGet("{codigo}")]
        public ActionResult<Product> Get(int codigo)
        {
            EntityAccess entityAccess = _entityAccess;
            ProductService productService = new ProductService(entityAccess);
            Product product = productService.FindByIdOdbc(codigo);
            return product;
        }

        [HttpPost]
        public void Post([FromBody] Product product)
        {
            EntityAccess entityAccess = _entityAccess;
            ProductService productService = new ProductService(entityAccess);
            productService.AddProductOdbc(product);
        }

        [HttpPut("{codigo}")]
        public void Put(int codigo, [FromBody] Product product)
        {
            EntityAccess entityAccess = _entityAccess;            
            product.Codigo = codigo;
            ProductService productService = new ProductService(entityAccess);
            productService.UpdateProductOdbc(product);
        }

        [HttpDelete("{codigo}")]
        public void Delete(int codigo)
        {
            EntityAccess entityAccess = _entityAccess;
            ProductService productService = new ProductService(entityAccess);
            productService.DeleteProductOdbc(codigo);
        }
    }
}
