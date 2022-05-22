using Admin.EndPoint.MappingProfiles;
using Application.Banners;
using Application.Catalogs.CatalogItems.AddNewCatalogItem;
using Application.Catalogs.CatalogItems.CatalogItemServices;
using Application.Catalogs.CatalogItems.UriComposer;
using Application.Catalogs.CatalogTypes;
using Application.Discounts;
using Application.Discounts.AddNewDiscountServices;
using Application.Interfaces.Contexts;
using Application.Visitors.GetTodayReport;
using FluentValidation;
using Infrastructure.ExternalApi.ImageServer;
using Infrastructure.MappingProfile;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Context.MongoContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

#region Connection String
builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();
builder.Services.AddDbContext<DataBaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:SqlServer"]);
});
#endregion


builder.Services.AddDistributedSqlServerCache(option =>
{
    option.ConnectionString = builder.Configuration["ConnectionStrings:SqlServer"];
    option.SchemaName = "dbo";
    option.TableName = "CacheDAta";
});

builder.Services.AddTransient(typeof(IMongoDbContext<>), typeof(MongoDbContext<>));
builder.Services.AddScoped<IGetTodayReportService, GetTodayReportService>();
builder.Services.AddTransient<ICatalogTypeService, CatalogTypeService>();
builder.Services.AddTransient<IAddNewCatalogItemService, AddNewCatalogItemService>();
builder.Services.AddTransient<ICatalogItemService, CatalogItemService>();
builder.Services.AddTransient<IImageUploadService, ImageUploadService>();
builder.Services.AddTransient<IAddNewDiscountServices, AddNewDiscountServices>();
builder.Services.AddTransient<IDiscountService, DiscountService>();
builder.Services.AddTransient<IBannersService, BannersService>();
builder.Services.AddTransient<IUriComposerServie, UriComposerServie>();
builder.Services.AddTransient<IDiscountHistoryService, DiscountHistoryService>();



//Mapper
builder.Services.AddAutoMapper(typeof(CatalogMappingProfile));
builder.Services.AddAutoMapper(typeof(CatalogVMMappingProfile));

//FulentValidation
builder.Services.AddTransient<IValidator<AddNewCatalogItemDto>, AddNewCatalogItemDtoValidator>();


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
app.MapControllers();

app.Run();
