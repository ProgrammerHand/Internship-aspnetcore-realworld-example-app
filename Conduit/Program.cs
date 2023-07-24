using Conduit.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Serilog;
using Conduit.Features.User.Application.Queries;
using Conduit.Features.User.Application.Commands;
using Conduit.Infrastructure.Security;
using Conduit.Infrastructure.Security.Interface;
using Conduit.Infrastructure.Middleware;
using Conduit.Features.Article.Application.Commands;
using Conduit.Features.Article.Application.Queries;

var builder = WebApplication.CreateBuilder(args);

var authenticationSettings = new AuthenticationSettings();

builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddScoped<Authentication>();
builder.Services.AddScoped<Registration>(); 
builder.Services.AddScoped<GetAll>();
builder.Services.AddScoped<GetCurrent>();
builder.Services.AddScoped<Update>();
builder.Services.AddScoped<Create>();
builder.Services.AddScoped<Feed>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IJWTtoken, JWTtoken>();
//builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

//Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("Front", builder =>
    {
        builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});

var inmemory = builder.Configuration.GetValue<bool>("UseInMemory");
var connectionString = builder.Configuration.GetConnectionString("Azure_SQL_ConnectionString") ?? throw new InvalidOperationException("Connection string 'Azure_SQL_ConnectionString' not found.");
builder.Services.AddDbContext<ConduitContext>(options =>
{
    if (inmemory)
    {
        options.UseInMemoryDatabase("InMemory");
    }
    else
    {
        options.UseSqlServer(connectionString);
    }


});

//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
//             .AddEntityFrameworkStores<ConduitContext>();

//builder.Services.AddIdentityServer()
//    .AddApiAuthorization<User, ConduitContext>()
//    .AddDeveloperSigningCredential();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        //ValidIssuer = authenticationSettings.JwtIssuer,
        //ValidAudience = authenticationSettings.JwtIssuer,
        //new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
    };
})
    .AddIdentityServerJwt();

builder.Services.AddMediatR(cfg =>
     cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Inject an implementation of ISwaggerProvider with defaulted settings applied
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer",
    });

    x.SupportNonNullableReferenceTypes();

    x.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
                    {   new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    Array.Empty<string>()}
    });
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "RealWorld API", Version = "v1" });
    x.CustomSchemaIds(y => y.FullName);
    x.DocInclusionPredicate((version, apiDescription) => true);
    //x.TagActionsBy(y => new List<string>()
    //{
    //                y.GroupName ?? throw new InvalidOperationException()
    //});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "swagger/{documentName}/swagger.json";
    });

    // Enable middleware to serve swagger-ui assets(HTML, JS, CSS etc.)
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "RealWorld API V1");
    });
//}

app.UseHttpsRedirection();

app.UseErrorHandlingMiddleware();

app.UseHttpCodeHandlingMiddleware();

app.UseAuthorization();

app.MapControllers();

app.UseCors("Front");

//app.UseMiddleware<ErrorHandler>;
app.Run();
