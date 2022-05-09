using Application.BasketsService;
using Application.Catalogs.CatalogItems.AddNewCatalogItem;
using Application.Catalogs.CatalogItems.GetCatalogItemPDP;
using Application.Catalogs.CatalogItems.GetCatalogItemPLP;
using Application.Catalogs.CatalogItems.UriComposer;

using Application.Catalogs.GetMenuItem;
using Application.Interfaces.Contexts;
using Application.Visitors.SaveVisitorInfo;
using Application.Visitors.VisitorOnline;
using Infrastructure.IdentityConfig;
using Infrastructure.MappingProfile;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Context.MongoContext;
using WebSite.EndPoint.Hubs;
using WebSite.EndPoint.Utilities.Filters;
using WebSite.EndPoint.Utilities.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();//.AddRazorRuntimeCompilation();

#region Connection String
builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();

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

builder.Services.AddTransient(typeof(IMongoDbContext<>), typeof(MongoDbContext<>));
builder.Services.AddTransient<ISaveVisitorInfoService, SaveVisitorInfoService>();
builder.Services.AddTransient<IVisitorOnlineService, VisitorOnlineService>();
builder.Services.AddTransient<IGetMenuItemService, GetMenuItemService>();
builder.Services.AddTransient<GetCatalogItemPLPService, GetCatalogItemPLPService>();
builder.Services.AddTransient<IUriComposerServie, UriComposerServie>();
builder.Services.AddTransient<IGetCatalogItemPDPService, GetCatalogItemPDPService>();
builder.Services.AddTransient<IGetCatalogItemPLPService, GetCatalogItemPLPService>();
builder.Services.AddTransient<IBasketService, BasketService>();

builder.Services.AddScoped<SaveVisitorFilter>();
builder.Services.AddSignalR();


//Mapper
builder.Services.AddAutoMapper(typeof(CatalogMappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSetVisitorId();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<OnlineVisitorHub>("/chathub");
app.Run();
