using MongoDB.Driver;

namespace MinimalAPIMongo.Services
{
    public class MongoDbService
    {


        /// <summary>
        /// Armazena a configuracao da aplicacao
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Armazena uma referencia ao MongoDB
        /// </summary>
        private readonly IMongoDatabase _database;



        /// <summary>
        /// Recebe a config da aplicazcao como parametro
        /// </summary>
        /// <param name="configuration"></param>
        public MongoDbService(IConfiguration configuration)
        {

            //Atribui a configuracao recebida em _configuration
            _configuration = configuration;


            //Aqui obetem a string de concexao atravez do _configuration
            var connectionString = _configuration.GetConnectionString("DbConnection");


            //Cria um objeto mongoUrl que recebe como parametrp a string de conexao
            var mongoUrl = MongoUrl.Create(connectionString);


            //Cria um client para se conectar ao banco
            var mongoClient = new MongoClient(mongoUrl);


            //Obtem o nome do banco especificado na string de conexao
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);


        }

        //Propriedade para acessar o banco de dados retornando a referencia ao db
        public IMongoDatabase GetDatabase => _database;
    }
}
