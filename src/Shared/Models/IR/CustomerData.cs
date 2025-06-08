namespace Shared.Models.IR;

public class CustomerData
{
    public long? ContactId { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string ExternalId { get; set; }

    public string ChannelParticipantId { get; set; }

    public bool? Verified { get; set; }
}