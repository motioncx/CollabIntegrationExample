using System;

namespace Shared.Models.IR;

public class InteractionClientRequest
{
    // From Request
    public Guid InteractionId { get; set; }

    // From Ticket creation
    public long TicketId { get; set; }

    public long AgentId { get; set; }

    public ChannelContactDto Contact { get; set; }

    // From reading of settings binding
    public CaseEditModeSettings EditModeSettings { get; set; } = new CaseEditModeSettings();

    // From WorkItem creation
    public long WorkItemId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastStateChangeAt { get; set; }
    public DateTime? LastOffered { get; set; }
    public int? OfferedTimeout { get; set; }
    public bool AutomaticAccept { get; set; } = true;
    public bool AutomaticOutOfService { get; set; }
    public bool ScreenRecording { get; set; }
    public ChannelProviderTypes ChannelProviderType { get; set; }
    public CasePopSettings CasePopSettings { get; set; }
    public JoinType Type { get; set; } = JoinType.Connecting;
}