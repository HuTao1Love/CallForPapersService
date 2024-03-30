using System.Diagnostics.CodeAnalysis;
using Abstractions.Services;
using Application;
using Application.Exceptions;
using Models.Exceptions;
using Moq;
using Repositories;

namespace Test;

public class ApplicationServiceTests
{
    private const string GuidExample = "b0d4ce5d-2757-4699-948c-cfa72ba94f86";
    private readonly IApplicationService _applicationService = new ApplicationService(Mock.Of<IApplicationRepository>());

    [Theory]
    [InlineData(null, "activity", "name", "description", "outline", typeof(AuthorMustBeDefinedException))]
    [InlineData(GuidExample, "activity", null, null, null, typeof(InvalidActivityException))]
    [InlineData(GuidExample, null, null, null, null, typeof(AnyFieldMustBeDefinedException))]
    public async Task ExceptionsOnCreateTest(
        string? author,
        string? activity,
        string? name,
        string? description,
        string? outline,
        Type exception)
    {
        var dto = new ApplicationNoIdDto(
            author is not null ? System.Guid.Parse(author) : null,
            activity,
            name,
            description,
            outline);

        await Assert.ThrowsAsync(exception, () => _applicationService.CreateAsync(dto));
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

        await Assert.ThrowsAsync<StringIsTooLongException>(() => _applicationService.CreateAsync(dto));
    }

    [Theory]
    [InlineData(GuidExample, "report", "name", "description", "outline")]
    [InlineData(GuidExample, "report", null, null, null)]
    public async Task SuccessCreateTest(string author, string? activity, string? name, string? description, string? outline)
    {
        var dto = new ApplicationNoIdDto(Guid.Parse(author), activity, name, description, outline);

        // if something happened, exception will throw & test failed
        await _applicationService.CreateAsync(dto);
    }

    [Theory]
    [InlineData(GuidExample, null, null, null, null, typeof(InvalidUpdateException))]
    public async Task ExceptionsOnUpdateTest(
        string? author,
        string? activity,
        string? name,
        string? description,
        string? outline,
        Type exception)
    {
        var dto = new ApplicationNoIdDto(FromString(author), activity, name, description, outline);
        await Assert.ThrowsAsync(exception, () => _applicationService.UpdateAsync(FromString(GuidExample).Value, dto));
    }

    [Fact]
    public async Task NotExistApplicationTest()
    {
        var dto = new ApplicationNoIdDto(null, null, null, null, null);

        await Assert.ThrowsAsync<NullException>(() =>
            _applicationService.UpdateAsync(FromString(GuidExample).Value, dto));

        await Assert.ThrowsAsync<NullException>(() => _applicationService.DeleteAsync(FromString(GuidExample).Value));

        await Assert.ThrowsAsync<NullException>(() => _applicationService.SubmitAsync(FromString(GuidExample).Value));
    }

    [return: NotNullIfNotNull("guid")]
    private static Guid? FromString(string? guid)
    {
        return guid is null ? null : Guid.Parse(guid);
    }
}