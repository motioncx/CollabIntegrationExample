using System;
using System.Collections.Generic;

namespace Shared.Models.IR;

public class ActivityDto
{
    public ChannelAccountDto From { get; set; }

    public string Text { get; set; }

    public object Value { get; set; }

    public DateTimeOffset? Timestamp { get; set; }

    public List<AttachmentDto> Attachments { get; set; }
}