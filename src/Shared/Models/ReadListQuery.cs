namespace Shared.Models;

public class ReadListQuery : ITenantable
{
    public int TenantId { get; set; }

    public int? SkipCount { get; set; }

    public int? MaxResultCount { get; set; }

    public string Sorting { get; set; }

    public string Search { get; set; }

    public int GetSkipCount() => this.SkipCount.GetValueOrDefault();

    public int GetMaxResultCount() => this.MaxResultCount.GetValueOrDefault(10);
}