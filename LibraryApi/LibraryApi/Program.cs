using LibraryApi.Data;
using LibraryApi.Models;
using Marten;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SqlKata.Compilers;
using SqlKata.Execution;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
  options.CustomSchemaIds(type => type.ToString());
});

builder.Services.AddScoped(ctx => new QueryFactory(ctx.GetRequiredService<IDocumentSession>().Connection, new PostgresCompiler()));

// builder.Services.AddDbContext<LibraryDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(typeof(Program));

// builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//   .AddEntityFrameworkStores<LibraryDbContext>()
//   .AddDefaultTokenProviders();

// builder.Services.AddIdentityServer()
//   .AddDeveloperSigningCredential() // In a production setup, AddSigningCredential should be used to reference the real RSA256 keys
//   .AddAspNetIdentity<IdentityUser>()
//   .AddInMemoryApiResources(Config.GetApiResources())
//   .AddInMemoryIdentityResources(Config.GetIdentityResources())
//   .AddInMemoryClients(Config.GetClients())
//   .AddTestUsers(Config.GetTestUsers());

builder.Services.AddMarten(options =>
{
  options.Connection(builder.Configuration.GetConnectionString("Default"));
  options.UseDefaultSerialization(enumStorage: EnumStorage.AsString);

  options.RegisterDocumentType<Genre>();
  options.RegisterDocumentType<Author>();
  options.RegisterDocumentType<Publisher>();
  options.RegisterDocumentType<Book>();
  options.RegisterDocumentType<Member>();
  options.RegisterDocumentType<Rent>();
  options.RegisterDocumentType<IdentityUser>();

  options.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
  options.Schema.Include<CustomRegistry>();
}).ApplyAllDatabaseChangesOnStartup();

builder.Services.AddCors(option =>
{
  option.AddPolicy("MyPolicy", builder =>
  {
    builder.AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader();
  });
});

builder.Services.AddAuthentication("Bearer")
  .AddIdentityServerAuthentication(options =>
  {
    options.Authority = "https://localhost:7208";
    options.RequireHttpsMetadata = false;
    options.ApiName = "resourceApi";
  });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

// app.UseIdentityServer();

app.UseHttpsRedirection();

app.UseCors("MyPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
