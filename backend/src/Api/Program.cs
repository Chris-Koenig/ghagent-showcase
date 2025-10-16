var builder = WebApplication.CreateBuilder(args);

// Minimal service registrations
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ghagent-showcase API",
        Version = "v1",
        Description = "Minimal scaffold API with health endpoint."
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ghagent-showcase API v1");
    c.RoutePrefix = string.Empty; // serve UI at root
});

app.MapControllers();

app.Run();
