using EcommerceAPI_StoredProcedures_.Services.Products;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IOrdersService, OrdersService>();
builder.Services.AddTransient<ITotalSalesService, TotalSalesService>();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOrigin",
        builder =>
        {
            builder.WithOrigins(new string[]
                    {
                      "https://localhost:3000",
                      "http://localhost:3000",
                       "http://localhost:1212"


                    })
                .WithMethods("POST", "GET", "PUT", "DELETE")
         .AllowAnyHeader()
         .AllowCredentials();
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseCors("AllowOrigin");

app.MapControllers();

app.Run();
