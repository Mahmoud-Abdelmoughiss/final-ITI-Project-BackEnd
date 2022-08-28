using EcommerseApplication.DTO;
using EcommerseApplication.Models;
using EcommerseApplication.Repository;
using EcommerseApplication.Respository;
using EcommerseApplication.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Stripe;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddScoped<IProductCategory, ProductCategoryRespository>();
builder.Services.AddScoped<IDiscount, DiscountRepository>();
builder.Services.AddScoped<IRequest, RequestRepository>();
//
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

//For Stripe Payment Getway
builder.Services.Configure<StripeSettings>(configuration.GetSection("Stripe"));

// For Entity Framework

string con = builder.Configuration.GetConnectionString("cs");
builder.Services.AddDbContext<Context>(optionBuider =>
{
    optionBuider.UseSqlServer(con);
});
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<Context>()
    .AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});



//
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IProductCategory, ProductCategoryRespository>();
builder.Services.AddScoped<IDiscount, DiscountRepository>();
builder.Services.AddScoped<Ifeedback, feedbackRepository>();
builder.Services.AddScoped<Ipartener, PartenerRepository>();
builder.Services.AddScoped<ConsumerRespons>();
builder.Services.AddScoped<Ishipper, shipperRepository>();
builder.Services.AddScoped<IshippingDetails, shippingDetailsRepository>();
builder.Services.AddScoped<IsubCategory,subcategoryRepository>();
builder.Services.AddScoped<IUserAddress, UserAddressRepository>();
builder.Services.AddScoped<IUserPayement, UserPayementRespository>();
builder.Services.AddScoped<Ifeedback,feedbackRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IShopping_SessionRepository, Shopping_SessionRepository>();
builder.Services.AddScoped<IPayment_DetailsRepository, Payment_DetailsRepository>();
builder.Services.AddScoped<ICart_ItemRepository, Cart_ItemRepository>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();

builder.Services.AddScoped<IProductRepository, ProductRepository> ();
builder.Services.AddScoped<IProduct_InventoryRepository, Product_InventoryRepository> ();
builder.Services.AddScoped<IOrder_DetailsRepository, Order_DetailsRepository> ();
builder.Services.AddScoped<IOrder_ItemsRepository, Order_ItemsRepository> ();
builder.Services.AddScoped<IProduct_ImageRepository, Product_ImageRepository> ();
builder.Services.AddScoped<IshipperRequest, ShipperRequestRepository> ();
builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();



app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

//StripeConfiguration.ApiKey = "sk_test_51LViyELU6Z3GuMBNmd97CSIEuWwRwxZIHdD6Z3wwldONp2dcWVFFb6vsY3ZFqEgRSKfJIzWX9exMoEalgqbM6Zq900Fm7uEUiQ";
StripeConfiguration.SetApiKey(configuration.GetSection("Stripe")["SecretKey"]);
app.UseStaticFiles();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
