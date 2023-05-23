using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class TimeTests
{
    [TestMethod]
    public void Time_ConstructorWithValidValues_CreatesTimeObject()
    {
        // Arrange
        byte hours = 10;
        byte minutes = 30;
        byte seconds = 45;

        // Act
        Time time = new Time(hours, minutes, seconds);

        // Assert
        Assert.AreEqual(hours, time.Hours);
        Assert.AreEqual(minutes, time.Minutes);
        Assert.AreEqual(seconds, time.Seconds);
    }

    [TestMethod]
    public void Time_AddTimePeriod_ReturnsCorrectTime()
    {
        // Arrange
        Time startTime = new Time(10, 30, 0);
        TimePeriod duration = new TimePeriod(2, 15, 30);
        Time expectedEndTime = new Time(12, 45, 30);

        // Act
        Time endTime = startTime + duration;

        // Assert
        Assert.AreEqual(expectedEndTime, endTime);
    }
}

[TestClass]
public class TimePeriodTests
{
    [TestMethod]
    public void TimePeriod_ConstructorWithValidValues_CreatesTimePeriodObject()
    {
        // Arrange
        byte hours = 2;
        byte minutes = 30;
        byte seconds = 15;

        // Act
        TimePeriod timePeriod = new TimePeriod(hours, minutes, seconds);

        // Assert
        Assert.AreEqual(hours, timePeriod.Hours);
        Assert.AreEqual(minutes, timePeriod.Minutes);
        Assert.AreEqual(seconds, timePeriod.Seconds);
    }

    [TestMethod]
    public void TimePeriod_AddTimePeriod_ReturnsCorrectTimePeriod()
    {
        // Arrange
        TimePeriod duration1 = new TimePeriod(1, 30, 0);
        TimePeriod duration2 = new TimePeriod(0, 45, 30);
        TimePeriod expectedSum = new TimePeriod(2, 15, 30);

        // Act
        TimePeriod sum = duration1 + duration2;

        // Assert
        Assert.AreEqual(expectedSum, sum);
    }
}