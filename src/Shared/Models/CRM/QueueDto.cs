namespace Shared.Models.CRM;

public class QueueDto
{
    public int Id { get; set; }

    public int TenantId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool AllowWebCreate { get; set; }

    public bool AllowPublicWebCreate { get; set; }

    public bool AllowEmailCreate { get; set; }

    public string EmailInput { get; set; }

    public bool SuppressEmailAll { get; set; }

    public bool SuppressEmailInternal { get; set; }

    public bool SuppressEmailStatusChanged { get; set; }

    public bool SuppressEmailCreate { get; set; }

    public bool SuppressEmailInternalComment { get; set; }

    public bool SuppressEmailPublicComment { get; set; }

    public bool SuppressEmailClosed { get; set; }

    public bool RequireLock { get; set; }

    public int? RequireLockDuration { get; set; }

    public bool AllowQueueTransfer { get; set; }

    public bool EnableDispositions { get; set; }

    public bool RequireDispositionOnCaseClose { get; set; }

    public bool RequireCommentOnStatusChange { get; set; }

    public int AcwTime { get; set; }

    public bool RequireContactOnCaseSave { get; set; }

    public CommentParticipantSetting CommentParticipantSetting { get; set; }

    public bool IsDisabled { get; set; }
}