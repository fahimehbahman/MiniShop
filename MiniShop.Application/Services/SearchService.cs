using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Microsoft.Extensions.Configuration;

namespace MiniShop.Application.Services;

public class SearchService
{
    private readonly SearchClient _client;

    public SearchService(IConfiguration config)
    {
        var endpoint = config["AzureSearch:Endpoint"]!;
        var apiKey = config["AzureSearch:ApiKey"]!;
        var indexName = config["AzureSearch:IndexName"]!;

        _client = new SearchClient(new Uri(endpoint), indexName, new AzureKeyCredential(apiKey));
    }

    public async Task UploadDocumentAsync(SearchDoc doc)
    {
        var batch = IndexDocumentsBatch.Upload(new[] { doc });
        await _client.IndexDocumentsAsync(batch);
    }
}


public class SearchDoc
{
    public string id { get; set; } = default!;
    public string content { get; set; } = default!;
    public float[] contentVector { get; set; } = default!;
    public string source { get; set; } = default!;
}