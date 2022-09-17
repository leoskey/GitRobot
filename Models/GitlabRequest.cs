namespace GitRobot.Models;

public class GitlabRequest
{
    public string object_kind { get; set; }
    public ObjectAttributes object_attributes { get; set; }
    public User user { get; set; }
    public Project project { get; set; }
    public Commit commit { get; set; }
    public List<Build> builds { get; set; }
}