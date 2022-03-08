using System.Text.Encodings.Web;
using System.Text.Unicode;
using GitRobot.Models;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.AddHttpClient("wework", options =>
        options.BaseAddress = new Uri("https://qyapi.weixin.qq.com"))
    .AddTransientHttpErrorPolicy(policy =>
        policy.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(500)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.Run();