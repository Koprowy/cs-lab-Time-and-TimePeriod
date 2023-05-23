using System;

public struct Time : IEquatable<Time>, IComparable<Time>
{
    private readonly byte hours;
    private readonly byte minutes;
    private readonly byte seconds;

    public byte Hours => hours;
    public byte Minutes => minutes;
    public byte Seconds => seconds;

    public Time(byte hours, byte minutes, byte seconds)
    {
        if (hours >= 24 || minutes >= 60 || seconds >= 60)
            throw new ArgumentException("Invalid time.");

        this.hours = hours;
        this.minutes = minutes;
        this.seconds = seconds;
    }

    public Time(byte hours, byte minutes) : this(hours, minutes, 0)
    {
    }

    public Time(byte hours) : this(hours, 0, 0)
    {
    }

    public Time(string time)
    {
        if (!TryParseTime(time, out var parsedHours, out var parsedMinutes, out var parsedSeconds))
            throw new ArgumentException("Invalid time format.");

        if (parsedHours >= 24 || parsedMinutes >= 60 || parsedSeconds >= 60)
            throw new ArgumentException("Invalid time.");

        hours = parsedHours;
        minutes = parsedMinutes;
        seconds = parsedSeconds;
    }

    public override string ToString()
    {
        return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
    }

    public override bool Equals(object obj)
    {
        if (obj is Time otherTime)
            return Equals(otherTime);

        return false;
    }

    public bool Equals(Time other)
    {
        return hours == other.hours && minutes == other.minutes && seconds == other.seconds;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(hours, minutes, seconds);
    }

    public int CompareTo(Time other)
    {
        if (hours != other.hours)
            return hours.CompareTo(other.hours);

        if (minutes != other.minutes)
            return minutes.CompareTo(other.minutes);

        return seconds.CompareTo(other.seconds);
    }

    public static bool operator ==(Time left, Time right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Time left, Time right)
    {
        return !(left == right);
    }

    public static bool operator <(Time left, Time right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(Time left, Time right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator <=(Time left, Time right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >=(Time left, Time right)
    {
        return left.CompareTo(right) >= 0;
    }

    public static Time operator +(Time time, TimePeriod timePeriod)
    {
        return time.Plus(timePeriod);
    }

    public static Time operator +(TimePeriod timePeriod, Time time)
    {
        return time.Plus(timePeriod);
    }

    private Time Plus(TimePeriod timePeriod)
    {
        var totalSeconds = ToSeconds() + timePeriod.ToSeconds();
        totalSeconds %= 24 * 60 * 60;

        var newTime = FromSeconds(totalSeconds);
        return newTime;
    }

    private long ToSeconds()
    {
        return hours * 60 * 60 + minutes * 60 + seconds;
    }

    private static Time FromSeconds(long totalSeconds)
    {
        var hours = (byte)(totalSeconds / 3600);
        var minutes = (byte)((totalSeconds / 60) % 60);
        var seconds = (byte)(totalSeconds % 60);

        return new Time(hours, minutes, seconds);
    }

    private static bool TryParseTime(string time, out byte hours, out byte minutes, out byte seconds)
    {
        hours = 0;
        minutes = 0;
        seconds = 0;

        var parts = time.Split(':');
        if (parts.Length != 3)
            return false;

        if (!byte.TryParse(parts[0], out hours) || !byte.TryParse(parts[1], out minutes) || !byte.TryParse(parts[2], out seconds))
            return false;

        return true;
    }
}
