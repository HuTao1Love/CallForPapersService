namespace Models;

public class Activity
{
    public Activity(
        Guid activityId,
        Guid authorId,
        ActivityType? activityType = null,
        string? name = null,
        string? description = null,
        string? outline = null,
        DateTime? submittedTime = null)
    {
        ActivityId = activityId;
        AuthorId = authorId;
        ActivityType = activityType;
        Name = name;
        Description = description;
        Outline = outline;
        SubmittedTime = submittedTime;
    }

    protected Activity() { }

    public Guid ActivityId { get; set; }
    public Guid AuthorId { get; set; }
    public ActivityType? ActivityType { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Outline { get; set; }
    public DateTime? SubmittedTime { get; set; } // null if not submitted
}