
using Microsoft.AspNetCore.Mvc;
using MiniShop.Application.Services;

[ApiController]
[Route("api/docs")]
public class DocumentController : ControllerBase
{
    private readonly EmbeddingService _embeddingService;
    private readonly SearchService _searchService;

    public DocumentController(
        EmbeddingService embeddingService,
        SearchService searchService)
    {
        _embeddingService = embeddingService;
        _searchService = searchService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromBody] UploadRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Text))
            return BadRequest("Text is required.");

        var vector = await _embeddingService.GetEmbeddingAsync(req.Text);

        var doc = new SearchDoc
        {
            id = Guid.NewGuid().ToString("N"),
            content = req.Text,
            contentVector = vector,
            source = string.IsNullOrWhiteSpace(req.Source) ? "manual-upload" : req.Source!
        };

        await _searchService.UploadDocumentAsync(doc);

        return Ok(new { doc.id, message = "Document indexed" });
    }
}

public class UploadRequest
{
    public string Text { get; set; } = default!;
    public string? Source { get; set; }
}