using System;
using System.Collections.Generic;

namespace Shared.Models.CRM;
public class TicketDto
  {
    public long Id { get; set; }

    public int TenantId { get; set; }

    public int StatusId { get; set; }

    public string StatusName { get; set; }

    public DateTime? StatusChangeAt { get; set; }

    public int? Priority { get; set; }

    public string Subject { get; set; }

    public int QueueId { get; set; }

    public string QueueName { get; set; }

    public Guid? FileStoreGuid { get; set; }

    public int? DispositionId { get; set; }

    public string DispositionName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedById { get; set; }

    public string CreatedByName { get; set; }

    public string CreatedByEmail { get; set; }

    public int CreatedQueueId { get; set; }

    public long? UpdatedById { get; set; }

    public string UpdatedByName { get; set; }

    public string UpdatedByEmail { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime? AssignedAt { get; set; }

    public long? AssignedToId { get; set; }

    public string AssignedToEmail { get; set; }

    public string AssignedToName { get; set; }

    public DateTime? ClosedAt { get; set; }

    public long? ClosedById { get; set; }

    public string ClosedByEmail { get; set; }

    public string ClosedByName { get; set; }

    public int Source { get; set; }

    public long? LockedById { get; set; }

    public DateTime? LockedUntil { get; set; }

    public string IRWorkItemInfoDetails { get; set; }

    public long? ContactId { get; set; }

    public string ContactEmail { get; set; }

    public string ContactName { get; set; }

    public IEnumerable<SimpleTicket> RelatedTickets { get; set; }

    public IEnumerable<ContactDto> Participants { get; set; }

    public IEnumerable<ContactDto> Contacts { get; set; }

    public IEnumerable<TicketFieldValueDto> CustomData { get; set; }

    public long? MergedInto { get; set; }

    public bool IsFavorite { get; set; }

    public RichContent RichDescription { get; set; }

    public long? ParentTicketId { get; set; }

    public ContactDto Contact { get; set; }

    public ContactDto AssignedTo { get; set; }

    public ContactDto LockedBy { get; set; }

    public ContactDto UpdatedBy { get; set; }

    public QueueDto Queue { get; set; }

    public SimpleDispositionDto Disposition { get; set; }

    public TicketStatusDto Status { get; set; }

    public EmailAccountDto EmailAccount { get; set; }
  }