namespace CollabIntegrationExample.WebApp;

public class ConstHelper
{
    public static string Report_RequestInteractionAggregateExample = @"{
      ""startDateTime"": ""2025-01-21T05:00:00.000Z"",
      ""endDateTime"": ""2025-01-22T04:59:59.999Z"",
      ""dateFilterField"": ""FirstAcceptedAtUtc"",
      ""groupFields"": [
        ""AgentId"",
        ""FirstAcceptedQuarterHourUtc""
      ],
      ""aggregates"": {
        ""_id"": [
          ""Count""
        ],
        ""AnsweredApplied"": [
          ""Sum""
        ],
        ""AgentFirstResponseTime"": [
          ""Sum"",
          ""Count""
        ],
        ""AcceptTime"": [
          ""Sum""
        ]
      },
      ""queues"": [],
      ""filter"": {
        ""ChannelType"": {
          ""$ne"": 0
        },
        ""AgentId"": {
          ""$ne"": null
        },
        ""Direction"": {
          ""$ne"": 1
        }
      },
      ""relativeTimezone"": ""America/New_York""
    }";
}