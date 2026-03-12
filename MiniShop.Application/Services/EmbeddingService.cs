using OpenAI;
using OpenAI.Embeddings;
using Microsoft.Extensions.Configuration;


namespace MiniShop.Application.Services
{
    public class EmbeddingService
    {
        private readonly OpenAIClient _client;
        public EmbeddingService(IConfiguration config)
        {
            var apiKey = config["OpenAI:ApiKey"];
            _client = new OpenAIClient(apiKey);
        }
        public async Task<float[]> GetEmbeddingAsync(string text)
        {
            var embeddingClient = _client.GetEmbeddingClient("text-embedding-3-small");

            var response = await embeddingClient.GenerateEmbeddingAsync(text);
            Console.WriteLine(response.Value.GetType().FullName);

            return null;
        }
    }
}
