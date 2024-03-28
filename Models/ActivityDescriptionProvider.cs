namespace Models;

public static class ActivityDescriptionProvider
{
    public static string GetDescription(this Activity activity)
        => activity switch
        {
            Activity.Report => "Доклад, 35-45 минут",
            Activity.MasterClass => "Мастеркласс, 1-2 часа",
            Activity.Discussion => "Дискуссия / круглый стол, 40-50 минут",
            _ => "Неизвестный тип",
        };
}