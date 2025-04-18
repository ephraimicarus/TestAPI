using BaseApi.Interfaces;
using BaseApi.Services;
using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(
	builder.Configuration.GetConnectionString("TestConn")));

builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<ICustomerDueService, CustomerDueService>();
builder.Services.AddTransient<IItemService, ItemService>();
builder.Services.AddTransient<IInventoryService, InventoryService>();
builder.Services.AddTransient<IStockDeliveryService, StockDeliveryService>();
builder.Services.AddTransient<IStockReturnService, StockReturnService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
