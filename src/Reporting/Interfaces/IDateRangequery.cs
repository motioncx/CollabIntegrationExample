using System;
using CollabIntegrationExample.WebApp.Command.Reports.TypeDefinitions;
using TimeZoneConverter;

namespace CollabIntegrationExample.WebApp.Command.Reports.Interfaces;

/// <summary>
 /// Indicates a query with a time range which can either be absolute
 /// (start and end times specified explicitly) or relative based on
 /// the resolution.
 /// </summary>
 public interface IDateRangeQuery
 {
     /// <summary>
     /// Absolute start time of the query.
     /// </summary>
     public DateTimeOffset? StartDateTime { get; }
     
     /// <summary>
     /// Absolute end time of the query.
     /// </summary>
     public DateTimeOffset? EndDateTime { get; }
   
     
     /// <summary>
     /// Time resolution period start (relative to now).
     /// </summary>
     public int? RelativePeriodStart { get; }
     
     /// <summary>
     /// Time resolution period end (relative to now). 
     /// </summary>
     public int? RelativePeriodEnd { get; }
     
     /// <summary>
     /// Timezone to use for resolving periods. 
     /// </summary>
     public string RelativeTimezone { get; }
     
     /// <summary>
     /// Time resolution (hour, quarter hour, day). 
     /// </summary>
     public TimeResolution? Resolution { get; }
 }

 /// <summary>
 /// Indicates a date range query with an option to choose the date field to filter on.
 /// </summary>
 public interface IDateRangeQueryWithFilterField: IDateRangeQuery
 {
     
     /// <summary>
     /// Allows the user to specify which date field is being filtered.
     /// </summary>
     public string DateFilterField { get;  }
     
 }
 

 /// <summary>
 /// Extension methods for date range queries
 /// </summary>
 public static class DateRangeQueryExtensions
 {

     public static TimeZoneInfo GetTimezone(this IDateRangeQuery self)
     {
         var timezone = TimeZoneInfo.Utc;
         if (!string.IsNullOrEmpty(self.RelativeTimezone))
         {
             timezone = TZConvert.GetTimeZoneInfo(self.RelativeTimezone);
         }
         return timezone;
     }

     public static (long Start, long End) ToEpochSecondRange(this IDateRangeQuery self)
     {
         var (start, end) = self.ToDateRange();
         return (start.ToUnixTimeSeconds(), end.ToUnixTimeSeconds());
     }

     public static (DateTime Start, DateTime End) ToUtcDateRange(this IDateRangeQuery self,
         DateTimeOffset? currentTime = null)
     {
         var (startDateOffset, endDateOffset) = self.ToDateRange(currentTime);
         return (startDateOffset.UtcDateTime, endDateOffset.UtcDateTime);
     }

     public static (DateTimeOffset Start, DateTimeOffset End) ToDateRange(this IDateRangeQuery self, 
         DateTimeOffset? currentTime = null)
     {
         if (self.StartDateTime is not null && self.EndDateTime is not null)
         {
             var start = self.StartDateTime.Value;
             var end = self.EndDateTime.Value;
             if (start > end)
             {
                 (start, end) = (end, start);
             }
             return (start, end);
         }

         if (self.RelativePeriodStart is not null &&
             self.RelativePeriodEnd is not null &&
             self.Resolution is not null)
         {
             var effectiveTime = currentTime ?? DateTimeOffset.Now;       

             var effectiveTimeZone = !string.IsNullOrEmpty(self.RelativeTimezone) ?
                 TZConvert.GetTimeZoneInfo(self.RelativeTimezone) : 
                 TimeZoneInfo.Utc;

             return self.Resolution.Value.RelativePeriodToTimeRange(self.RelativePeriodStart.Value, 
                 self.RelativePeriodEnd.Value, effectiveTimeZone, effectiveTime);
         }

         throw new ArgumentException("Time range invalid or ambiguous.");
     }
 }