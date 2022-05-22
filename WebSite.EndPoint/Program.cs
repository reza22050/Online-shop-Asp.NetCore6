using Application.BasketsService;
using Application.Catalogs.CatalogItems.AddNewCatalogItem;
using Application.Catalogs.CatalogItems.CatalogItemServices;
using Application.Catalogs.CatalogItems.GetCatalogItemPDP;
using Application.Catalogs.CatalogItems.GetCatalogItemPLP;
using Application.Catalogs.CatalogItems.UriComposer;

using Application.Catalogs.GetMenuItem;
using Application.Discounts;
using Application.HomePageService;
using Application.Interfaces.Contexts;
using Application.Orders;
using Application.Orders.CustomerOrdersServices;
using Application.Payments;
using Application.Users;
using Application.Visitors.SaveVisitorInfo;
using Application.Visitors.VisitorOnline;
using Infrastructure.IdentityConfig;
using Infrastructure.MappingProfile;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Context.MongoContext;
using WebSite.EndPoint.Hubs;
using WebSite.EndPoint.Middlewares;
using WebSite.EndPoint.Utilities.Filters;
using WebSite.EndPoint.Utilities.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();//.AddRazorRuntimeCompilation();

#region Connection String
builder.Services.AddTransient<IDataBaseContext, DataBaseContext>();
builder.Services.AddTransient<IIdentityDatabaseContext, IdentityDatabaseContext>();

builder.Services.AddDbContext<DataBaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:SqlServer"]);
});

builder.Services.AddIdentityService(builder.Configuration);
builder.Services.AddAuthentication();
builder.Services.ConfigureApplicationCookie(option =>
{
    option.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    option.LoginPath = "/Account/Login";
    option.AccessDeniedPath = "/Account/AccessDenied";
    option.SlidingExpiration = true;
});
#endregion

builder.Services.AddDistributedSqlServerCache(option =>
{
    option.ConnectionString = builder.Configuration["ConnectionStrings:SqlServer"];
    option.SchemaName = "dbo";
    option.TableName = "CacheDAta";
});


builder.Services.AddTransient(typeof(IMongoDbContext<>), typeof(MongoDbContext<>));
builder.Services.AddTransient<ISaveVisitorInfoService, SaveVisitorInfoService>();
builder.Services.AddTransient<IVisitorOnlineService, VisitorOnlineService>();
builder.Services.AddTransient<IGetMenuItemService, GetMenuItemService>();
builder.Services.AddTransient<GetCatalogItemPLPService, GetCatalogItemPLPService>();
builder.Services.AddTransient<IUriComposerServie, UriComposerServie>();
builder.Services.AddTransient<IGetCatalogItemPDPService, GetCatalogItemPDPService>();
builder.Services.AddTransient<IGetCatalogItemPLPService, GetCatalogItemPLPService>();
builder.Services.AddTransient<IBasketService, BasketService>();
builder.Services.AddTransient<IUserAddressService, UserAddressService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IPaymentService, PaymentService>();
builder.Services.AddTransient<IDiscountService, DiscountService>();
builder.Services.AddTransient<IDiscountHistoryService, DiscountHistoryService>();
builder.Services.AddTransient<ICatalogItemService, CatalogItemService>();
builder.Services.AddTransient<ICustomerOrdersServices, CustomerOrdersServices>();
builder.Services.AddTransient<IHomePageService,  HomePageService>();

builder.Services.AddScoped<SaveVisitorFilter>();
builder.Services.AddSignalR();


//Mapper
builder.Services.AddAutoMapper(typeof(CatalogMappingProfile));
builder.Services.AddAutoMapper(typeof(UserMappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCustomExceptionHandler();
app.UseSetVisitorId();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "product/{Slug}",
    pattern: "{controller=Product}/{action=Details}");

app.MapHub<OnlineVisitorHub>("/chathub");
app.Run();
