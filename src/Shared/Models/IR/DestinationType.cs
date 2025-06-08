namespace Shared.Models.IR;

public enum DestinationType
{
    Workflow,
    ImmediateQueue,
    UserExtension,
    QueueVoicemail,
    CallerIdForUserExtension,
    UserVoicemail,
    ExternalNumber,
    PhoneRingGroup,
    CallerIdForRoutingProfile,
}