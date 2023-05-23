using System;

public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
{
    private readonly long totalSeconds;

    public TimePeriod(int hours, int minutes, int seconds)
    {
        if (hours < 0 || minutes < 0 || seconds < 0)
            throw new ArgumentException("Invalid time period.");

        totalSeconds = hours * 3600L + minutes * 60L + seconds;
    }

    public TimePeriod(int hours, int minutes) : this(hours, minutes, 0)
    {
    }

    public TimePeriod(int seconds) : this(0, 0, seconds)
    {
    }

    public TimePeriod(Time startTime, Time endTime)
    {
        var startSeconds = startTime.Hours * 3600L + startTime.Minutes * 60L + startTime.Seconds;
        var endSeconds = endTime.Hours * 3600L + endTime.Minutes * 60L + endTime.Seconds;

        totalSeconds = endSeconds - startSeconds;
    }

    public TimePeriod(string timePeriod)
    {
        if (!TryParseTimePeriod(timePeriod, out var parsedHours, out var parsedMinutes, out var parsedSeconds))
            throw new ArgumentException("Invalid time period format.");

        if (parsedHours < 0 || parsedMinutes < 0 || parsedSeconds < 0)
            throw new ArgumentException("Invalid time period.");

        totalSeconds = parsedHours * 3600L + parsedMinutes * 60L + parsedSeconds;
    }

    public override string ToString()
    {
        var hours = totalSeconds / 3600L;
        var minutes = (totalSeconds / 60L) % 60L;
        var seconds = totalSeconds % 60L;

        return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
    }

    public override bool Equals(object obj)
    {
        if (obj is TimePeriod otherTimePeriod)
            return Equals(otherTimePeriod);

        return false;
    }

    public bool Equals(TimePeriod other)
    {
        return totalSeconds == other.totalSeconds;
    }

    public override int GetHashCode()
    {
        return totalSeconds.GetHashCode();
    }

    public int CompareTo(TimePeriod other)
    {
        return totalSeconds.CompareTo(other.totalSeconds);
    }

    public static bool operator ==(TimePeriod left, TimePeriod right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(TimePeriod left, TimePeriod right)
    {
        return !(left == right);
    }

    public static bool operator <(TimePeriod left, TimePeriod right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(TimePeriod left, TimePeriod right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator <=(TimePeriod left, TimePeriod right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >=(TimePeriod left, TimePeriod right)
    {
        return left.CompareTo(right) >= 0;
    }

    public static TimePeriod operator +(TimePeriod timePeriod1, TimePeriod timePeriod2)
    {
        return timePeriod1.Plus(timePeriod2);
    }

    public static TimePeriod operator -(TimePeriod timePeriod1, TimePeriod timePeriod2)
    {
        return timePeriod1.Minus(timePeriod2);
    }

    private TimePeriod Plus(TimePeriod other)
    {
        var totalSeconds = this.totalSeconds + other.totalSeconds;
        return new TimePeriod(totalSeconds);
    }

    private TimePeriod Minus(TimePeriod other)
    {
        var totalSeconds = this.totalSeconds - other.totalSeconds;
        return new TimePeriod(totalSeconds);
    }

    private static bool TryParseTimePeriod(string timePeriod, out int hours, out int minutes, out int seconds)
    {
        hours = 0;
        minutes = 0;
        seconds = 0;

        var parts = timePeriod.Split(':');
        if (parts.Length != 3)
            return false;

        if (!int.TryParse(parts[0], out hours) || !int.TryParse(parts[1], out minutes) || !int.TryParse(parts[2], out seconds))
            return false;

        return true;
    }
}
