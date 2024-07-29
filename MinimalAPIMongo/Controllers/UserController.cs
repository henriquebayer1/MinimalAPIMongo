using Microsoft.AspNetCore.Mvc;
using MinimalAPIMongo.Domains;
using MinimalAPIMongo.Services;
using MongoDB.Driver;

namespace MinimalAPIMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly IMongoCollection<User> _user;

        public UserController(MongoDbService mongoDbService)
        {
            _user = mongoDbService.GetDatabase.GetCollection<User>("user");

        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            try
            {
                var product = await _user.Find(FilterDefinition<User>.Empty).ToListAsync();
                return Ok(product);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }


        }


        [HttpPost]
        public async Task<ActionResult> Post(User user)
        {
            try
            {
                await _user.InsertOneAsync(user);
                return Ok(user);
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
                User p = await _user.FindOneAndDeleteAsync(x => x.Id == id);

                return Ok(p + "foi deletado");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }


        }

        [HttpPut]
        public async Task<ActionResult> Update(User p)
        {
            try
            {
                var a = Builders<User>.Filter.Eq(x => x.Id, p.Id);


                await _user.ReplaceOneAsync(a, p);

                return Ok(a + "foi atualizado");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }


        }

    }
}
