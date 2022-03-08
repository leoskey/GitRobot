namespace GitRobot.Models;

public class GitlabWebhookRequestCommit
{
    public string author_name { get; set; }
    public string author_email { get; set; }
    public string author_url { get; set; }
    public string message { get; set; }
}