using Microsoft.AspNetCore.Mvc;
using MinimalAPIMongo.Domains;
using MinimalAPIMongo.Services;
using MongoDB.Driver;

namespace MinimalAPIMongo.Controllers
{
    public class OrderController : Controller
    {
       
        
            private readonly IMongoCollection<Order> _order;
    
        public OrderController(MongoDbService mongoDbService)
     {
        _order = mongoDbService.GetDatabase.GetCollection<Order>("product");

     }

           [HttpGet]
         public async Task<ActionResult<List<Order>>> Get(Order o)
     {
         try
        {
            var order = await _order.Find(FilterDefinition<Order>.Empty).ToListAsync();



                var all = _order.Find(x => x.Id == o.Id);
            


            return Ok(o);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }


    }


    [HttpPost]
    public async Task<ActionResult> Post(Order order)
    {
        try
        {
                
            await _order.InsertOneAsync(order);
            return Ok(order);
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
            Order p = await _order.FindOneAndDeleteAsync(x => x.Id == id);

            return Ok(p + "foi deletado");
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }


    }

            [HttpPut]
             public async Task<ActionResult> Update(Order p)
                 {
                try
                {
                    var a = Builders<Order>.Filter.Eq(x => x.Id, p.Id);


                    await _order.ReplaceOneAsync(a, p);

                    return Ok(a + "foi atualizado");
                }
                catch (Exception e)
                {

                    return BadRequest(e.Message);
                }


                    }

    }
}


