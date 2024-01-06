using Assistant_Bdd.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

//builder.Services.AddRazorPages();
builder.Services.AddDbContext<AssistantContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("AssistantContext")));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.Run(async (context) =>
{
    await context.Response.WriteAsync("Fail c'est le projet pour la BDD ^^");
});

app.Run();
