﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIMongo.Domains;
using MinimalAPIMongo.Services;
using MongoDB.Driver;

namespace MinimalAPIMongo.Controllers
{
   

        [Route("api/[controller]")]
        [ApiController]
        [Produces("application/json")]
        public class ClientController : ControllerBase
        {
          
            private readonly IMongoCollection<Client> _client;

            
            public ClientController(MongoDbService mongoDbService)
            {
                
                _client = mongoDbService.GetDatabase.GetCollection<Client>("clients");
            }

            [HttpGet]
            public async Task<ActionResult<List<Client>>> Get()
            {
                try
                {
                    var clients = await _client.Find(FilterDefinition<Client>.Empty).ToListAsync();
                    return Ok(clients);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            [HttpPost]
            public async Task<ActionResult> Create(Client client)
            {
                try
                {
                    await _client.InsertOneAsync(client);
                    return StatusCode(201, client);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            [HttpGet("{id}")]
            public async Task<ActionResult> GetById(string id)
            {
                try
                {
                    var client = await _client.Find(c => c.Id == id).FirstOrDefaultAsync();
                    if (client == null)
                    {
                        return NotFound("Cliente não encontrado!");
                    }
                    return Ok(client);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            [HttpPut("{id}")]
            public async Task<ActionResult> Update(string id, Client updatedClient)
            {
                try
                {
                    var filter = Builders<Client>.Filter.Eq(c => c.Id, id);
                    var client = await _client.Find(filter).FirstOrDefaultAsync();
                    if (client == null)
                    {
                        return NotFound("Cliente não encontrado!");
                    }

                    // Atualize as propriedades do cliente com os novos valores
                   
                    client.Cpf = updatedClient.Cpf;
                    client.Phone = updatedClient.Phone;
                    client.Address = updatedClient.Address;
                    client.AdditionalAttributes = updatedClient.AdditionalAttributes;

                    // Atualize o cliente no banco de dados
                    var result = await _client.ReplaceOneAsync(filter, client);

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

            [HttpDelete("{id}")]
            public async Task<ActionResult> Delete(string id)
            {
                try
                {
                    var filter = Builders<Client>.Filter.Eq(c => c.Id, id);
                    var result = await _client.DeleteOneAsync(filter);

                    if (result.DeletedCount > 0)
                    {
                        return Ok("Cliente deletado com sucesso!");
                    }
                    else
                    {
                        return NotFound("Cliente não encontrado!");
                    }
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
        }
    }
