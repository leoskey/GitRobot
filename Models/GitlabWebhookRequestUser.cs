namespace GitRobot.Models;

public class GitlabWebhookRequestUser
{
    public string name { get; set; }
    public string username { get; set; }
    public string avatar_url { get; set; }
    public string email { get; set; }
}