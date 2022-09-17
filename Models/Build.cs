namespace GitRobot.Models;

public class Build
{
    public int id { get; set; }
    public string stage { get; set; }
    public string name { get; set; }
    public string status { get; set; }
    public string created_at { get; set; }
    public string started_at { get; set; }
    public string finished_at { get; set; }
    public string when { get; set; }
    public bool manual { get; set; }
    public bool allow_failure { get; set; }
    public User user { get; set; }
    public Runner runner { get; set; }
}