using Abstractions.Services;

namespace Application;

public static class NotNullFieldCountService
{
    public static bool AllRequiredDefined(this ApplicationDto applicationDto)
    {
        ArgumentNullException.ThrowIfNull(applicationDto);
        return AllDefined(applicationDto.Activity, applicationDto.Name, applicationDto.Outline);
    }

    public static bool AnyDefined(this ApplicationDto applicationDto)
    {
        ArgumentNullException.ThrowIfNull(applicationDto);
        return AnyDefined(applicationDto.Activity, applicationDto.Name, applicationDto.Description, applicationDto.Outline);
    }

    public static bool AllDefined(params object?[] values)
        => values.All(i => i is not null);

    public static bool AnyDefined(params object?[] values)
        => values.Any(i => i is not null);
}