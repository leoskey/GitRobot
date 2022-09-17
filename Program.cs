using GitRobot;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseAutofac();
await builder.AddApplicationAsync<GitRobotModule>();
var app = builder.Build();
await app.InitializeApplicationAsync();
await app.RunAsync();