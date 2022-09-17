using Polly;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.TextTemplating.Scriban;
using Volo.Abp.VirtualFileSystem;

namespace GitRobot;

[DependsOn(typeof(AbpAspNetCoreMvcModule))]
[DependsOn(typeof(AbpAutofacModule))]
[DependsOn(typeof(AbpTextTemplatingScribanModule))]
public class GitRobotModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddControllers();
        context.Services.AddRouting(options => options.LowercaseUrls = true);
        context.Services.AddEndpointsApiExplorer();
        context.Services.AddSwaggerGen();

        // var configuration = context.Services.GetConfiguration();
        // context.Services.Configure<AppSettings>(configuration);

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<GitRobotModule>("GitRobot");
        });

        context.Services.AddHttpClient("wework", options =>
                options.BaseAddress = new Uri("https://qyapi.weixin.qq.com"))
            .AddTransientHttpErrorPolicy(policy =>
                policy.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(500)));
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseConfiguredEndpoints();
        app.UseEndpoints(endpoint =>
        {
            endpoint.MapGet("/", context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });
        });
    }
}