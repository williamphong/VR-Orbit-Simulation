using System;
using System.Globalization;

public class TimeConverter
{
    //Converts a datetime string to a double representing the number of days since Jan 1, 1970,
    //and to an integer representing the number of seconds since Jan 1, 1970
    public static (double, long) ToFloat(string datetimeStr)
    {
        DateTime epoch = new DateTime(1970, 1, 1);
        DateTime inputDateTime = DateTime.ParseExact(datetimeStr, "MM/dd/yyyy:HH:mm:ss", CultureInfo.InvariantCulture);
        
        TimeSpan difference = inputDateTime - epoch;
        
        double daysSinceEpoch = difference.TotalDays;
        long secondsSinceEpoch = (long)difference.TotalSeconds;
        
        return (daysSinceEpoch, secondsSinceEpoch);
    }
    
    //Converts a double representing days or an integer representing seconds since Jan 1, 1970, back to string
    public static string FromFloat(double value, bool isDays = true)
    {
        DateTime epoch = new DateTime(1970, 1, 1);
        DateTime resultDateTime;
        
        if (isDays)
        {
            resultDateTime = epoch.AddDays(value);
        }
        else
        {
            resultDateTime = epoch.AddSeconds(value);
        }
        
        return resultDateTime.ToString("MM/dd/yyyy:HH:mm:ss");
    }
}

// Example Usage
public class Program
{
    public static void Main()
    {
        string datetimeStr = "02/28/2024:15:30:00";
        var (daysSinceEpoch, secondsSinceEpoch) = TimeConverter.ToFloat(datetimeStr);
        
        Console.WriteLine($"Days since epoch: {daysSinceEpoch}, Seconds since epoch: {secondsSinceEpoch}");
        
        string fromDays = TimeConverter.FromFloat(daysSinceEpoch, true);
        string fromSeconds = TimeConverter.FromFloat(secondsSinceEpoch, false);
        
        Console.WriteLine($"From Days: {fromDays}");
        Console.WriteLine($"From Seconds: {fromSeconds}");
    }
}
