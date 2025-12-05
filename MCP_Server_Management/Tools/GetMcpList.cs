using MCP_Management.Models;
using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace MCP_Management.Tools;

public class GetMcpList
{
    private readonly Context? _context;

    public GetMcpList(Context? context)
    {
        _context = context; 
    }

    [McpServerTool, Description("Obtiene la lista de nombres de los servidores MCP disponibles con sus respectivos URL's")]
    public async Task<List<McpServerModel>> GetMcpListMethod()
    {
        var mcpServerList = await _context!.Set<McpServerModel>().ToListAsync();

        return mcpServerList;
    }
}
