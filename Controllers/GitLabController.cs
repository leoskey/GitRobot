using GitRobot.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Volo.Abp.TextTemplating;

namespace GitRobot.Controllers;

[Route("api/[controller]")]
public class GitLabController : ControllerBase
{
    private readonly ITemplateRenderer _templateRenderer;
    private readonly WeWorkClient _weWorkClient;

    public GitLabController(
        ITemplateRenderer templateRenderer,
        WeWorkClient weWorkClient
    )
    {
        _templateRenderer = templateRenderer;
        _weWorkClient = weWorkClient;
    }

    /// <summary>
    /// 企业微信
    /// </summary>
    /// <param name="robotId"></param>
    [HttpPost("wework/{robotId}")]
    public async Task WeChatCallback([FromRoute] string robotId, [FromBody] GitlabRequest request)
    {
        var message = await _templateRenderer.RenderAsync(request.object_kind, request);
        await _weWorkClient.SendAsync(robotId, JsonConvert.SerializeObject(new
        {
            msgtype = "markdown",
            markdown = new
            {
                content = message
            }
        }));
    }
}