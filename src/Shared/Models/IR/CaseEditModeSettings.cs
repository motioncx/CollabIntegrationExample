namespace Shared.Models.IR;

/// <summary>
/// When a ticket is needed to be used in a join of interaction, this info is required by clients (eg. MPC)
/// </summary>
public class CaseEditModeSettings
{
    public ContactInputGroupPopupSettings ContactSettings { get; set; }
}

public class ContactInputGroupPopupSettings
{
    public string ContactId { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string ExternalId { get; set; }

    public bool DisplayContactProfile { get; set; }
}