using Abstractions.Services;
using Application.Exceptions;

namespace Application;

public static class SizeCheckerService
{
    public static void AssertSize(this ApplicationNoIdDto? applicationNoIdDto)
    {
        if (applicationNoIdDto is null) return;

        AssertSize(applicationNoIdDto.Name, 100);
        AssertSize(applicationNoIdDto.Description, 300);
        AssertSize(applicationNoIdDto.Outline, 1000);
    }

    private static void AssertSize(string? value, int maxSize)
    {
        if (!CheckSize(value, maxSize)) throw new StringIsTooLongException(value ?? string.Empty);
    }

    private static bool CheckSize(string? value, int maxSize) => value is null || (value.Length <= maxSize);
}