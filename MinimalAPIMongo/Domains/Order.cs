﻿using Microsoft.AspNetCore.Mvc;
using MinimalAPIMongo.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace MinimalAPIMongo.Domains
{
    public class Order
    {
        [BsonId]
        //define o nome do campo no MongoDB como "_id" e o tipo como "ObjectId"
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("date")]
        public DateTime Date = DateTime.Now;

        [BsonElement("status")]
        public string? Status { get; set; }

        [BsonElement("products")]
        
        public List<Product>? Products { get; set; }


        //referencia feita dos ids digitados para link dos produtos
        [BsonElement("productId")]
        [JsonIgnore]
        public List<string>? ProductId { get; set; }

        [BsonElement("clientId")]
        [JsonIgnore]
        public string? ClientId { get; set; }
       
        public Client? Client { get; set; }



    }

}
