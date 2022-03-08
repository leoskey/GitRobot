using System.ComponentModel.DataAnnotations;

namespace GitRobot.Models;

public class GitlabWebhookRequest
{
    [Required]
    public string object_kind { get; set; }
    public string build_status { get; set; }
    public decimal? build_duration { get; set; }
    public string @ref { get; set; }
    public GitlabWebhookRequestRepository repository { get; set; }
    public GitlabWebhookRequestRunner runner { get; set; }
    public GitlabWebhookRequestCommit commit { get; set; }
    public GitlabWebhookRequestUser user { get; set; }
}