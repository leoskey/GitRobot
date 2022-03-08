using GitRobot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GitRobot.Controllers;

[Route("api/[controller]")]
public class GitLabController : ControllerBase
{
    private readonly ILogger<GitLabController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptionsMonitor<AppSettings> _optionsMonitor;

    public GitLabController(
        ILogger<GitLabController> logger,
        IHttpClientFactory httpClientFactory,
        IOptionsMonitor<AppSettings> optionsMonitor)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _optionsMonitor = optionsMonitor;
    }

    /// <summary>
    /// 企业微信
    /// </summary>
    /// <param name="robotId"></param>
    /// <param name="request"></param>
    [HttpPost("wework/{robotId}")]
    public async Task WeChatCallback([FromRoute] string robotId, [FromBody] GitlabWebhookRequest request)
    {
        switch (request.object_kind)
        {
            case "build":
                var message = BuildMessage(request);
                await SendMessageToWeworkAsync(robotId, message);
                message = BuildIfSuccessMessage(request);
                await SendMessageToWeworkAsync(robotId, message);
                break;
        }
    }

    private string BuildIfSuccessMessage(GitlabWebhookRequest request)
    {
        var status = request.build_status;
        if (!string.Equals(status, "success"))
        {
            return null;
        }

        var name = request.user.name;
        var phone = _optionsMonitor.CurrentValue.Staffs?.FirstOrDefault(t => t.Name == name)?.Phone;

        var projectName = request.repository.name;
        var content = JsonConvert.SerializeObject(new
        {
            msgtype = "text",
            text = new
            {
                content = $"Gitlab-CI 构建 {status} 通知：{projectName}-{request.commit.message}",
                mentioned_mobile_list = new List<string> { phone }
            }
        });

        return content;
    }

    private string BuildMessage(GitlabWebhookRequest request)
    {
        var markdown = @$"
Gitlab-CI 构建 {request.build_status} 通知
> 项目: [{request.repository?.name}]({request.repository?.homepage})
> 状态: <font color=""comment"">{request.build_status}</font>
> 分支: <font color=""comment"">{request.@ref}</font>
> 耗时: <font color=""comment"">{request.build_duration}</font>
> runner: <font color=""comment"">{request.runner?.description}</font>
> 提交者: <font color=""comment"">{request.user?.name}</font>
> 提交内容: <font color=""comment"">{request.commit?.message}</font>";
        var content = JsonConvert.SerializeObject(new
        {
            msgtype = "markdown",
            markdown = new
            {
                content = markdown
            }
        });

        return content;
    }

    private async Task SendMessageToWeworkAsync(string robotId, string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return;
        }

        _logger.LogInformation("企业微信请求:{Content}", message);
        var client = _httpClientFactory.CreateClient("wework");
        var response = await client.PostAsync($"/cgi-bin/webhook/send?key={robotId}", new StringContent(message));
        var responseContent = await response.Content.ReadAsStringAsync();
        _logger.LogInformation("企业微信响应:{Content}", responseContent);
    }
}