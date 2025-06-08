using System;

namespace Shared.Models.CRM;

public class ReadTicketsModel : ReadListQuery
{
    public int[] QueueIds { get; set; }
    public int[] QueueViewIds { get; set; }
    public long? AssignedTo { get; set; }
    public long? CreatedBy { get; set; }
    public long? Participating { get; set; }
    public long? ContactId { get; set; }
    public int[] Statuses { get; set; }
    public bool? Favorite { get; set; }

    public string TicketId { get; set; }
    public int? Priority { get; set; }
    public string Subject { get; set; }
    public string OpenedByName { get; set; }
    public string AssignedToName { get; set; }

    public DateTime? DueAt { get; set; }
    public DateTime? DueTo { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? CreatedTo { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public DateTime? UpdatedTo { get; set; }

    public int? FieldId { get; set; }
    public string FieldSearch { get; set; }

    public int? TicketFilterId { get; set; }
}