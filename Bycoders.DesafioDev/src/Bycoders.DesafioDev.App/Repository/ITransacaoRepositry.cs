using System.Net.Http;

namespace Bycoders.DesafioDev.App.Repository
{
    public interface ITransacaoHttpRepository
    {
    }

    public class TransacaoHttpRepository
    {
        private readonly HttpClient _client;
        public TransacaoHttpRepository(HttpClient client)
        {
            _client = client;
        }

        //public class 
    }
}
