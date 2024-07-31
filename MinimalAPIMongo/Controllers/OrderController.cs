using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIMongo.Domains;
using MinimalAPIMongo.Services;
using MinimalAPIMongo.ViewModels;
using MongoDB.Driver;
using System.Collections.ObjectModel;

namespace MinimalAPIMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
       
        
            private readonly IMongoCollection<Order> _order;
        private readonly IMongoCollection<Product> _product;
        private readonly IMongoCollection<Client> _client;

        public OrderController(MongoDbService mongoDbService)
     {
            _order = mongoDbService.GetDatabase.GetCollection<Order>("order");
            _product = mongoDbService.GetDatabase.GetCollection<Product>("product");
            _client = mongoDbService.GetDatabase.GetCollection<Client>("clients");
     }

           [HttpGet]
         public async Task<ActionResult<List<Order>>> Get()
     {
         try
        {
                var order = await _order.Find(FilterDefinition<Order>.Empty).ToListAsync();



                foreach (var item in order)
                {

                    if (item.ProductId != null)
                    {
                      

                      var  produtosOrder = Builders<Product>.Filter.In(p => p.Id, item.ProductId);

                       item.Products = await _product.Find(produtosOrder).ToListAsync();
                    }
                   
               

                }
                return Ok(order);

            
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }


    }


        [HttpPost]
        public async Task<ActionResult<Order>> Create(OrderViewModel orderViewModel)
        {
            try
            {
                Order order = new Order();

                order.Id = orderViewModel.Id;
                order.Date = orderViewModel.Date;
                order.Status = orderViewModel.Status;
                order.ProductId = orderViewModel.ProductId;
                order.ClientId = orderViewModel.ClientId;

                //ira buscar na collection _client e verificar se o Id que foi passado existe, se existir guarde na variavel client'
                var client = await _client.Find(x => x.Id == order.ClientId).FirstOrDefaultAsync();

                if (client == null)
                {
                    return NotFound();
                }

                //Pega todos os dados que inserimos no client e adicionamos no Client
                order.Client = client;

                //Insere na ordem
                await _order!.InsertOneAsync(order);

                //Retorna um statuscode 
                return StatusCode(201, order);

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


