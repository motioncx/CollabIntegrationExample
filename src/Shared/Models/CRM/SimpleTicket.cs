using System;

namespace Shared.Models.CRM;

public class SimpleTicket
{
    public long Id { get; set; }

    public string Subject { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int Source { get; set; }
}