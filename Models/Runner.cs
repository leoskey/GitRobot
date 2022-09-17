namespace GitRobot.Models;

public class Runner
{
    public int id { get; set; }
    public string description { get; set; }
    public bool active { get; set; }
    public bool is_shared { get; set; }
}