using Shared.Models.CRM;

namespace Shared.Models.IR;

public class ChannelContactDto : ContactDto
{
    public string ChannelParticipantId { get; set; }

    public bool? Verified { get; set; }
}