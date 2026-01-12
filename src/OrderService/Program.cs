var builder = WebApplication.CreateBuilder(args);

// Controllers only (no OpenAPI customization)
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
