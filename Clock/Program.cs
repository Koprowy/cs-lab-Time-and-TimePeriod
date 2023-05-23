using System;
using System.Threading;

namespace ClockApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Clock App");

            // Utworzenie obiektu zegara
            Clock clock = new Clock();

            // Pętla główna zegara
            while (true)
            {
                // Pobranie aktualnego czasu
                Time currentTime = clock.GetCurrentTime();

                // Wyświetlenie aktualnego czasu
                Console.WriteLine($"Current Time: {currentTime}");

                // Odczekanie sekundy
                Thread.Sleep(1000);
            }
        }
    }

    // Struktura reprezentująca punkt w czasie
    public struct Time
    {
        public byte Hours { get; }
        public byte Minutes { get; }
        public byte Seconds { get; }

        public Time(byte hours, byte minutes, byte seconds)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        public override string ToString()
        {
            return $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";
        }
    }

    // Struktura reprezentująca odcinek czasowy
    public struct TimePeriod
    {
        public long TotalSeconds { get; }

        public TimePeriod(long totalSeconds)
        {
            TotalSeconds = totalSeconds;
        }

        public override string ToString()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(TotalSeconds);
            return $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }
    }

    // Klasa reprezentująca zegar
    class Clock
    {
        private Time startTime;

        public Clock()
        {
            startTime = new Time(0, 0, 0);
        }

        public Time GetCurrentTime()
        {
            DateTime now = DateTime.Now;
            Time currentTime = new Time((byte)now.Hour, (byte)now.Minute, (byte)now.Second);

            // Obliczenie różnicy czasu
            TimePeriod elapsedTime = GetElapsedTime(currentTime, startTime);

            // Dodanie różnicy czasu do początkowego czasu
            Time resultTime = AddTime(startTime, elapsedTime);

            return resultTime;
        }

        private TimePeriod GetElapsedTime(Time endTime, Time startTime)
        {
            long totalSeconds = ((endTime.Hours - startTime.Hours) * 3600) +
                                ((endTime.Minutes - startTime.Minutes) * 60) +
                                (endTime.Seconds - startTime.Seconds);

            return new TimePeriod(totalSeconds);
        }

        private Time AddTime(Time time, TimePeriod timePeriod)
        {
            long totalSeconds = (time.Hours * 3600) +
                                (time.Minutes * 60) +
                                time.Seconds +
                                timePeriod.TotalSeconds;

            byte hours = (byte)((totalSeconds / 3600) % 24);
            byte minutes = (byte)((totalSeconds / 60) % 60);
            byte seconds = (byte)(totalSeconds % 60);

            return new Time(hours, minutes, seconds);
        }
    }
}
