using CashlessRegistration.Application;
using CashlessRegistration.Infrastructure;
using CashlessRegistration.Token.API.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCashlessRegistrationDBContext();
builder.Services.AddServices();

builder.Services.AddControllers(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
