using ModelContextProtocol.Server;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole(o => o.LogToStandardErrorThreshold = LogLevel.Trace);

// Configurar MCP
builder.Services
    .AddMcpServer()
    //.WithStdioServerTransport()
    .WithHttpTransport()    
    .WithTools<EmpaparNumeros>()
    .WithTools<RandomNumberTools>();
var app = builder.Build();

app.MapMcp();

app.Run();