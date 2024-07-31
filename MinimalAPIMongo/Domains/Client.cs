using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MinimalAPIMongo.Domains
{
    public class Client
    {
        [BsonId]
        //define o nome do campo no MongoDB como "_id" e o tipo como "ObjectId"
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("cpf")]
        public string? Cpf { get; set; }

        [BsonElement("phone")]
        public string? Phone { get; set; }

        [BsonElement("address")]
        public string? Address { get; set; }

        public Dictionary<string, string> AdditionalAttributes { get; set; }

        //Ao ser instanciado o obejeto da classe product, o atributo additionalattributes ja vira com um novo dicionario e portanto habilitado para adicionar +
        //atributos
        public void product()
        {

            AdditionalAttributes = new Dictionary<string, string>();

        }

    }
}
