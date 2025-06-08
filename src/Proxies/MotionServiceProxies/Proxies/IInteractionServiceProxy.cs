using Shared.Models.IR;
using Refit;

namespace MotionServiceProxies.HttpProxy;

public interface IInteractionServiceProxy
{
    [Get("/ir/v1/interaction/transcript")]
    Task<ApiResponse<TranscriptResult>> TranscriptsAsync(
        Guid interactionId,
        bool hideSystem,
        CancellationToken cancellationToken = default (CancellationToken));
}
