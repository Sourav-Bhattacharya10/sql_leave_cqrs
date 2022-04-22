using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Extensions.AspNetCore.Configuration.Secrets;

using Application;
using Persistence;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);
                            
// if (builder.Environment.IsDevelopment())
// {
    string keyVaultName = builder.Configuration["KeyVaultName"];
    keyVaultName = keyVaultName.ToLower();
    var kvUri = "https://" + keyVaultName + ".vault.azure.net";
    var secretClient = new SecretClient(new Uri(kvUri), new DefaultAzureCredential()); // new DefaultAzureCredential(includeInteractiveCredentials: true)
    builder.Configuration.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
// }

IConfiguration configuration = builder.Configuration;

// Add services to the container.
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigurePersistenceServices(configuration);
builder.Services.ConfigureInfrastructureServices(configuration);

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
// .AddFluentValidation(config => 
// {
//     config.RegisterValidatorsFromAssemblyContaining<CreateEmployeeValidator>();
// })
// .AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Leave Management API", Version = "v1"});
});



builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policyBuilder => policyBuilder.AllowAnyOrigin()
                                                                  .AllowAnyMethod()
                                                                  .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Leave Management API v1");
});

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
