namespace BlazorAPI.Client.Code;

public static class DateUtils
{
    public static string Remains(this DateTime? date)
    {
        if (date == null)
            return String.Empty;

        int days = (int)(date.Value - DateTime.Today).TotalDays;

        switch (days)
        {
            case -7: return $"před týdnem";
            case -2: return $"předevčírem";
            case -1: return $"včera";
            case 0: return $"dnes";
            case 1: return $"zítra";
            case 2: return $"pozítří";
            case 7: return $"za týden";
            case >= 5: return $"za {days} dní";
            case > 2: return $"za {days} dny";
            case < -2: return $"před {-days} dny";
            default: return date.Value.ToString("yyyy-MM-dd");
        }
    }
}
