using MCP_Management;
using MCP_Management.Tools;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole(o => o.LogToStandardErrorThreshold = LogLevel.Trace);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(connectionString);
});

// Configurar MCP
builder.Services
    .AddMcpServer()
    //.WithStdioServerTransport()
    .WithHttpTransport()
    .WithTools<GetMcpList>();

var app = builder.Build();

app.MapMcp();

app.Run();