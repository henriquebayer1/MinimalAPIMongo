namespace MinimalAPIMongo.Domains
{
    public class Client
    {

        public string? Id { get; set; }

        public string? Cpf { get; set; }

        public string? Phone { get; set; }

        public string? Adress { get; set; }

        public Dictionary<string, string> AdditionalAttributes { get; set; }

        //Ao ser instanciado o obejeto da classe product, o atributo additionalattributes ja vira com um novo dicionario e portanto habilitado para adicionar +
        //atributos
        public void product()
        {

            AdditionalAttributes = new Dictionary<string, string>();

        }

    }
}
