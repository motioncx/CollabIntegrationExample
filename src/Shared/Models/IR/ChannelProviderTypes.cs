namespace Shared.Models.IR;

public enum ChannelProviderTypes
{
    UNKNOWN_UNKNOWN = 0, // Default value which will show when a channel is misconfigured
    BotFramework_Chat = 1,
    Twilio_Voice = 2,
    Twilio_SMS = 3,
    API_API = 4,
    TIMED_CRON = 5, // MetaData Object Stored As TimedCronEntry
    SendgridInbound_Email = 6,
    Mcx_Web = 7,
    Bandwidth_Voice = 9,
    Bandwidth_SMS = 10,
    NULL_HANDLER = 999999
}