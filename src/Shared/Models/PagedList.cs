using System.Collections.Generic;

namespace Shared.Models;

public class PagedList<T>
{
    public long TotalCount { get; set; }

    public IEnumerable<T> Items { get; set; }
}