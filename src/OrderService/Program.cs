var builder = WebApplication.CreateBuilder(args);

// Register controllers
builder.Services.AddControllers();
// Optional: OpenAPI / Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi() ;
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Map controller routes
app.MapControllers();

app.Run();

