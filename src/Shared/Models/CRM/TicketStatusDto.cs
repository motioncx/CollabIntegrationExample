namespace Shared.Models.CRM;

public class TicketStatusDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool Closed { get; set; }

    public bool ClosedDefault { get; set; }

    public bool NewTicketDefault { get; set; }

    public bool Merged { get; set; }

    public bool IsDeleted { get; set; }
}