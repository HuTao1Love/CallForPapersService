using System;

namespace Models;

public class Application
{
    public Application(
        Guid id,
        Guid authorId,
        DateTime createdTime,
        Activity? activity = null,
        string? name = null,
        string? description = null,
        string? outline = null,
        DateTime? submittedTime = null)
    {
        Id = id;
        AuthorId = authorId;
        CreatedTime = createdTime;
        Activity = activity;
        Name = name;
        Description = description;
        Outline = outline;
        SubmittedTime = submittedTime;
    }

    protected Application() { }

    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public DateTime CreatedTime { get; set; }
    public Activity? Activity { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Outline { get; set; }
    public DateTime? SubmittedTime { get; set; }
}