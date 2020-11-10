using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDMongoDB.Modelos;
using CRUDMongoDB.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CRUDMongoDB.Controllers
{
    //Se convierte en una API
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private IProductCollection db = new ProductCollection();

        //Metodo que devuelve todo los productos
        [HttpGet]
        public async Task<ActionResult> GetAllProducts() {
            return Ok(await db.GetAllProducts());
        }

        //Metodo que devuelve todo los productos
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAllProductDetails(string id)
        {
            return Ok(await db.GetAllById(id));
        }

        //Metodo que devuelve todo los productos
        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null)
                return BadRequest();
            if (product.Name == string.Empty)
            {
                ModelState.AddModelError("Nombre", "El nombre no puede estar vacio");
            }

            await db.InsertProduct(product);
            return Created("Created", true);
        }

        //Metodo que devuelve todo los productos
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct([FromBody] Product product, string id)
        {
            if (product == null)
                return BadRequest();
            if (product.Name == string.Empty)
            {
                ModelState.AddModelError("Nombre", "El nombre no puede estar vacio");
            }
            product.Id = new MongoDB.Bson.ObjectId(id);
            await db.UpdateProduct(product);
            return Created("Created", true);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            await db.DeleteProduct(id);
            return NoContent();//Success
        }
    }
}
