namespace GitRobot.Models;

public class ObjectAttributes
{
    public int id { get; set; }
    public string @ref { get; set; }
    public bool tag { get; set; }
    public string sha { get; set; }
    public string before_sha { get; set; }
    public string source { get; set; }
    public string status { get; set; }
    public string detailed_status { get; set; }
    public List<string> stages { get; set; }
    public string created_at { get; set; }
    public string finished_at { get; set; }
    public int duration { get; set; }
    public List<object> variables { get; set; }
}