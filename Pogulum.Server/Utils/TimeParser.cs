namespace Pogulum.Server.Utils;

public static class TimeParser
{

    public static string FromIsoToRecent(string isoTime)
    {
        return FromIsoToRecent(DateTime.Parse(isoTime));
    }

    public static string FromIsoToRecent(DateTime time)
    {
        var unix = ((DateTimeOffset)time).ToUnixTimeMilliseconds();
        var unixCurrent = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        var dtMillis = unixCurrent - unix;
        var dtSeconds = dtMillis / 1000;
        if (dtSeconds < 60)
            return $"{dtSeconds} seconds ago";

        var dtMinutes = dtSeconds / 60;
        if (dtMinutes < 60)
            return $"{dtMinutes} minutes ago";

        var dtHours = dtMinutes / 60;
        if (dtHours < 24)
            return $"{dtHours} hours ago";

        var dtDays = dtHours / 24;
        return $"{dtDays} days ago";
    }
}