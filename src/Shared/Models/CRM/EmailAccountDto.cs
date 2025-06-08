namespace Shared.Models.CRM;

public class EmailAccountDto
{
    public int Id { get; set; }

    public int TenantId { get; set; }

    public string MailFromName { get; set; }

    public string MailFromDomain { get; set; }

    public string HostUrl { get; set; }

    public string ApiKey { get; set; }

    public string MailFromEmail { get; set; }

    public string MailToDomainList { get; set; }

    public int DefaultQueueId { get; set; }
}