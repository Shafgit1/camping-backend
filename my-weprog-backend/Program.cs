using my_weprog_backend.Data;
using my_weprog_backend.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<EnumPrices>();
builder.Services.AddSingleton<IDataContext, Database>();

builder.Services.AddCors();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

app.UseAuthorization();
app.MapControllers();

app.Run();
