using Car_wash.Models;  // for car wash dbcontext
using Microsoft.EntityFrameworkCore; // for UseSqlServer
using Microsoft.AspNetCore.Authentication.JwtBearer; // For JWT Bearer authentication
using Microsoft.IdentityModel.Tokens; // For token validation
using System.Text; // For text encoding (for signing the token)
//below both are used in line 16
using Car_wash.Repository.Interface;
using Car_wash.Repository.Implementation;
using Car_wash.Mappings;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
//Dependecy Injection(so that we can use connection string all throuhgout the app)
builder.Services.AddDbContext<CarWashDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<GenerateMail>();
builder.Services.AddScoped<ISignup, SignupRepo>();
builder.Services.AddScoped<ILogin, LoginRepo>();
builder.Services.AddScoped<IOrder, OrderRepo>();
builder.Services.AddScoped<ISearch, SearchRepo>();
builder.Services.AddScoped<IWashing, WashingRepo>();
builder.Services.AddScoped<IReview, ReviewRepo>();
builder.Services.AddScoped<ICheckout, CheckoutRepo>();
builder.Services.AddScoped<IHelper, HelperRepo>();
builder.Services.AddScoped<IAdmin, AdminRepo>();
builder.Services.AddAutoMapper(typeof(Signup_mapper));

builder.Services.AddCors(options =>{
    options.AddPolicy("AllowAllOrigins", builder => 
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    );
});
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
{
    throw new ArgumentNullException("Jwt:Key", "The JWT secret key is not set in the configuration.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Washer", policy => policy.RequireRole("Washer"));
    options.AddPolicy("User", policy => policy.RequireRole("User"));
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();

app.UseCors("AllowAllOrigins");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable Authentication and Authorization middleware
app.UseAuthentication(); 
app.UseAuthorization();  

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();