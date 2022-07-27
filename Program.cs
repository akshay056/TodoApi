using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TodoApiContext") ?? throw new InvalidOperationException("Connection string 'TodoApiContext' not found.")));

builder.Services.AddControllers().AddOData(options => options.Select().Filter().OrderBy());

builder.Services.AddDbContext<TodoApiContext>(opt =>
    opt.UseInMemoryDatabase("TodoList"));

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();