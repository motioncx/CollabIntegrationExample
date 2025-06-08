using System;

namespace Shared.Models.CRM;

public class TicketHistoryDto
{
    public DateTime? EventAt { get; set; }

    public long EventBy { get; set; }

    public int Type { get; set; }

    public string Details { get; set; }

    public ContactDto User { get; set; }
}