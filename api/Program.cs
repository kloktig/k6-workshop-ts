using api.Context;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

const string serviceName = "klotig.API";
const string serviceVersion = "1.0.0";

var jaegerHost = Environment.GetEnvironmentVariable("JAEGERHOST") ?? "localhost";
var dbHost = Environment.GetEnvironmentVariable("DBHOST") ?? "localhost";

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(dbHost)));
builder.Services.AddControllers();
builder.Services.AddOpenTelemetry()
    .ConfigureResource(b => b.AddService(serviceName: serviceName, serviceVersion: serviceVersion, serviceInstanceId: Environment.MachineName))
    .WithTracing(b => b.AddSource(serviceName)
        .AddAspNetCoreInstrumentation()
        .AddSqlClientInstrumentation(options => options.SetDbStatementForText = true)
        .AddJaegerExporter(o => o.AgentHost = jaegerHost))
    .WithMetrics(b => b
        .AddMeter(serviceName)
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddRuntimeInstrumentation()
        .AddProcessInstrumentation()
        .AddPrometheusExporter())
    ;
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.UseOpenTelemetryPrometheusScrapingEndpoint();
app.Run();
