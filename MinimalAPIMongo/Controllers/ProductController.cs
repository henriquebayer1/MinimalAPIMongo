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

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, Product updatedClient)
        {
            try
            {
                var filter = Builders<Product>.Filter.Eq(c => c.Id, id);
                var client = await _product.Find(filter).FirstOrDefaultAsync();
                if (client == null)
                {
                    return NotFound("Cliente não encontrado!");
                }

                // Atualize as propriedades do cliente com os novos valores

                client.Name = updatedClient.Name;
                client.Price = updatedClient.Price;
                
                // Atualize o cliente no banco de dados
                var result = await _product.ReplaceOneAsync(filter, client);

                if (result.IsAcknowledged && result.ModifiedCount > 0)
                {
                    return Ok(client);
                }
                else
                {
                    return BadRequest("Erro na atualização");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



    }
}
