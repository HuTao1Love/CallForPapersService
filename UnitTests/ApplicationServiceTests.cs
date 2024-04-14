using System.Diagnostics.CodeAnalysis;
using System.Net;
using Application;
using Application.Contracts;
using Application.Exceptions;
using Application.Services;
using FluentValidation;
using Models;
using Moq;

namespace UnitTests;

public class ApplicationServiceTests
{
    private const string GuidExample = "b0d4ce5d-2757-4699-948c-cfa72ba94f86";
    private readonly IApplicationRepository repository;
    private readonly IApplicationService applicationService;

    public ApplicationServiceTests()
    {
        repository = Mock.Of<IApplicationRepository>();
        applicationService = new ApplicationService(repository, new ApplicationDtoValidator());
    }

    [Theory]
    [InlineData(GuidExample, "activity", null, null, null)]
    [InlineData(GuidExample, null, null, null, null)]
    public async Task ExceptionsOnCreateTest(
        string? author,
        string? activity,
        string? name,
        string? description,
        string? outline)
    {
        var dto = new ApplicationNoIdDto(
            author is not null ? Guid.Parse(author) : null,
            activity,
            name,
            description,
            outline);

        await Assert.ThrowsAsync<ValidationException>(() => applicationService.CreateAsync(dto));
    }

    [Fact]
    public async Task ExceptionOnCreateWithoutAuthorTest()
    {
        var dto = new ApplicationNoIdDto(
            null,
            "activity",
            "name",
            "description",
            "outline");

        await Assert.ThrowsAsync<WithHttpCodeException>(() => applicationService.CreateAsync(dto));
    }

    [Fact]
    public async Task TooMuchStringSizeExceptionTest()
    {
        var dto = new ApplicationNoIdDto(
            FromString(GuidExample),
            null,
            string.Concat(Enumerable.Repeat("X", 101)),
            null,
            null);

        await Assert.ThrowsAsync<ValidationException>(() => applicationService.CreateAsync(dto));
    }

    [Theory]
    [InlineData(GuidExample, "report", "name", "description", "outline")]
    [InlineData(GuidExample, "report", null, null, null)]
    public async Task SuccessCreateTest(
        string author,
        string? activity,
        string? name,
        string? description,
        string? outline)
    {
        var dto = new ApplicationNoIdDto(Guid.Parse(author), activity, name, description, outline);

        // if something happened, exception will throw & test failed
        await applicationService.CreateAsync(dto);
    }

    [Theory]
    [InlineData(GuidExample, null, null, null, null, HttpStatusCode.BadRequest)]
    public async Task ExceptionsOnUpdateTest(
        string? author,
        string? activity,
        string? name,
        string? description,
        string? outline,
        HttpStatusCode statusCode)
    {
        var guid = FromString(GuidExample).Value;

        Mock.Get(repository)
            .Setup(r => r.FindByIdAsync(guid, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Models.Application(guid, guid, DateTime.Now));
        var dto = new ApplicationNoIdDto(FromString(author), activity, name, description, outline);
        var exception = await Assert.ThrowsAsync<WithHttpCodeException>(
            () => applicationService.UpdateAsync(guid, dto));

        Assert.Equal(statusCode, exception.StatusCode);
    }

    [Fact]
    public async Task SuccessfulUpdateTest()
    {
        var entity = new Models.Application(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, Activity.Discussion);
        var update = new ApplicationNoIdDto(null, null, "name", null, null);

        Mock.Get(repository)
            .Setup(r => r.FindByIdAsync(entity.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity);
        var result = await applicationService.UpdateAsync(entity.Id, update);

        Assert.Equal(update.Name, result.Name);
    }

    [Fact]
    public async Task NotExistApplicationTest()
    {
        var dto = new ApplicationNoIdDto(null, null, null, null, null);

        await Assert.ThrowsAsync<WithHttpCodeException>(
            () => applicationService.UpdateAsync(FromString(GuidExample).Value, dto));

        await Assert.ThrowsAsync<WithHttpCodeException>(
            () => applicationService.DeleteAsync(FromString(GuidExample).Value));

        await Assert.ThrowsAsync<WithHttpCodeException>(
            () => applicationService.SubmitAsync(FromString(GuidExample).Value));
    }

    [return: NotNullIfNotNull("guid")]
    private static Guid? FromString(string? guid)
    {
        return guid is null ? null : Guid.Parse(guid);
    }
}