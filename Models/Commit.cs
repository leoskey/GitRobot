namespace GitRobot.Models;

public class Commit
{
    public string id { get; set; }
    public string message { get; set; }
    public string title { get; set; }
    public DateTime timestamp { get; set; }
    public string url { get; set; }
    public Author author { get; set; }
}