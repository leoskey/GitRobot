namespace GitRobot.Models;

public class Project
{
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string web_url { get; set; }
    public string avatar_url { get; set; }
    public string git_ssh_url { get; set; }
    public string git_http_url { get; set; }
    public string @namespace { get; set; }
    public int visibility_level { get; set; }
    public string path_with_namespace { get; set; }
    public string default_branch { get; set; }
    public object ci_config_path { get; set; }
}