namespace MCP_Management.Models;

public class McpServerModel
{
    public Guid IdModel { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string? Url { get; set; }
}
