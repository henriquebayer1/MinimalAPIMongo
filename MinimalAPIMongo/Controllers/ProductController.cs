using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIMongo.Domains;
using MinimalAPIMongo.Services;
using MongoDB.Driver;

namespace MinimalAPIMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IMongoCollection<Product> _product;

        public ProductController(MongoDbService mongoDbService)
        {
            _product = mongoDbService.GetDatabase.GetCollection<Product>("product");
            
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get() 
        {
            try
            {
                var product = await _product.Find(FilterDefinition<Product>.Empty).ToListAsync();
                return Ok(product);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        
        
        }


        [HttpPost]
        public async Task<ActionResult> Post(Product product)
        {
            try
            {
                 await _product.InsertOneAsync(product);
                return Ok(product);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }


        }


        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
         Product p =  await _product.FindOneAndDeleteAsync(x => x.Id == id);

                return Ok(p + "foi deletado");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }


        }

        [HttpPut]
        public async Task<ActionResult> Update(Product p)
        {
            try
            {
                var a = Builders<Product>.Filter.Eq(x => x.Id, p.Id);
               

                 await _product.ReplaceOneAsync(a, p);

                return Ok(a + "foi atualizado");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }


        }


    }
}
