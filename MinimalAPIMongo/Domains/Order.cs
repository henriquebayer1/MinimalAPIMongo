using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace MinimalAPIMongo.Domains
{
    public class Order
    {

        public string? Id { get; set; }

        public DateTime Date { get; set; }

        public string? Status { get; set; }

        private IMongoDatabase _database;

        public IMongoCollection<Product> Product
        {
            get { return _database.GetCollection<Product>("Product"); }
        }


        public Dictionary<string, string> AdditionalIds { get; set; }

        //Ao ser instanciado o obejeto da classe product, o atributo additionalattributes ja vira com um novo dicionario e portanto habilitado para adicionar +
        //atributos
        public void product()
        {

            AdditionalIds = new Dictionary<string, string>();

        }




    }

}
