using Admin.EndPoint.MappingProfiles;
using Application.Catalogs.CatalogTypes;
using Application.Interfaces.Contexts;
using Application.Visitors.GetTodayReport;
using Infrastructure.MappingProfile;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Context.MongoContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

#region Connection String
builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();
builder.Services.AddDbContext<DataBaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:SqlServer"]);
});
#endregion

builder.Services.AddScoped<IGetTodayReportService, GetTodayReportService>();
builder.Services.AddTransient(typeof(IMongoDbContext<>), typeof(MongoDbContext<>));


//Mapper
builder.Services.AddAutoMapper(typeof(CatalogMappingProfile));
builder.Services.AddAutoMapper(typeof(CatalogVMMappingProfile));

builder.Services.AddTransient<ICatalogTypeService, CatalogTypeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
