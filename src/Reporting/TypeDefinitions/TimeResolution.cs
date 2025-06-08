using System;
using System.Collections.Generic;

namespace CollabIntegrationExample.WebApp.Command.Reports.TypeDefinitions;

public enum TimeResolution
{
    QuarterHour = 0,
    Hour = 1,
    Day = 2,
}

public static class TimeResolutionExtensions
{
    public static DateTimeOffset Quantize(DateTimeOffset dateTime, uint seconds)
    {
        var utcSeconds = dateTime.ToUnixTimeSeconds();
        var quantizedSeconds = (utcSeconds / seconds) * seconds;
        var quantizedUtc = DateTimeOffset.FromUnixTimeSeconds(quantizedSeconds);
        return quantizedUtc.ToOffset(dateTime.Offset);
    }

    public static TimeSpan ToTimespan(this TimeResolution self)
    {
        return self switch
        {
            TimeResolution.Day => TimeSpan.FromDays(1),
            TimeResolution.Hour => TimeSpan.FromHours(1),
            TimeResolution.QuarterHour => TimeSpan.FromMinutes(15),
            _ => throw new InvalidOperationException("Unknown TimeResolution")
        };
    }
   
    public static DateTimeOffset Quantize(this TimeResolution resolution, DateTimeOffset dateTime)
    {
        return resolution switch
        {
            TimeResolution.Day => new DateTimeOffset(dateTime.Date, dateTime.Offset),
            TimeResolution.Hour => Quantize(dateTime, 3600),
            TimeResolution.QuarterHour => Quantize(dateTime, 900),
            _ => throw new InvalidOperationException("Unknown or unexpected time resolution.")
        };
    }

    public static (DateTimeOffset start, DateTimeOffset end) RelativePeriodToTimeRange(this TimeResolution self, 
        int startPeriod, int endPeriod, TimeZoneInfo timeZone, DateTimeOffset? currentTime=null)
    {
        var effectiveTime = currentTime ?? DateTimeOffset.UtcNow;

        if (self == TimeResolution.Day)
        {
            var localTimeOffset = TimeZoneInfo.ConvertTime(effectiveTime, timeZone);
            var localDate = localTimeOffset.Date;
            var localDateWithOffset = new DateTimeOffset(localDate, localTimeOffset.Offset);
            var startDate = localDateWithOffset.AddDays(startPeriod);
            startDate = new DateTimeOffset(startDate.Date, timeZone.GetUtcOffset(startDate));
            
            var endDate = localDateWithOffset.AddDays(endPeriod + 1);
            endDate = new DateTimeOffset(endDate.Date, timeZone.GetUtcOffset(endDate));
            return (startDate, endDate);
        }
        else
        {
            var effectiveTimeUtc = effectiveTime.ToUnixTimeSeconds();
            var periodTimeSpan = self.ToTimespan();
            var periodDurationSeconds = (int) periodTimeSpan.TotalSeconds;
            
            // Integer division rounds to the start of the period.
            var quantizedStartSeconds = (effectiveTimeUtc / periodDurationSeconds) * periodDurationSeconds;
            var currentPeriodOffset = DateTimeOffset.FromUnixTimeSeconds(quantizedStartSeconds);
            var startingPeriodUtcTime = currentPeriodOffset + (startPeriod * periodTimeSpan);
            
            // Ending period is the start of the next period.
            // NOTE: the database stores times in sub-second resolution, so don't subtract to get
            //       the very end time, we can compare with < instead of <=.
            var endPeriodUtcTime = currentPeriodOffset + ((endPeriod + 1) * periodTimeSpan);

            var startPeriodLocalTime = TimeZoneInfo.ConvertTime(startingPeriodUtcTime, timeZone);
            var endPeriodLocalTime = TimeZoneInfo.ConvertTime(endPeriodUtcTime, timeZone);

            return (startPeriodLocalTime, endPeriodLocalTime);
        }
    }
    
    
    /// <summary>
    /// Returns an enumerable of the period start date/times between start and end (inclusive)
    /// </summary>
    /// <param name="self"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static IEnumerable<DateTimeOffset> ExpandPeriods(this TimeResolution self, DateTimeOffset start,
        DateTimeOffset end)
    {
        var firstPeriod = self.Quantize(start);
        var lastPeriod = self.Quantize(end);
        var periodTimeSpan = self.ToTimespan();
        
        for (var period = firstPeriod; period <= lastPeriod; period += periodTimeSpan) 
        {
            yield return period;
        }
    }
    
    public static DateTimeOffset Trim(this DateTimeOffset dateTime, long ticks)
    {
        return new DateTimeOffset(dateTime.Ticks - (dateTime.Ticks % ticks), dateTime.Offset);
    }
    
    
}