using Volo.Abp.Domain.Services;

namespace GitRobot;

public class WeWorkClient : DomainService
{
    private readonly ILogger<WeWorkClient> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public WeWorkClient(
        ILogger<WeWorkClient> logger,
        IHttpClientFactory httpClientFactory
    )
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task SendAsync(string robotId, string message)
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