namespace Models;

public class Application(
    Guid id,
    Guid authorId,
    DateTime createdTime,
    Activity? activity = null,
    string? name = null,
    string? description = null,
    string? outline = null,
    DateTime? submittedTime = null)
{
    public Guid Id { get; set; } = id;
    public Guid AuthorId { get; set; } = authorId;
    public DateTime CreatedTime { get; set; } = createdTime;
    public Activity? Activity { get; set; } = activity;
    public string? Name { get; set; } = name;
    public string? Description { get; set; } = description;
    public string? Outline { get; set; } = outline;
    public DateTime? SubmittedTime { get; set; } = submittedTime;
}