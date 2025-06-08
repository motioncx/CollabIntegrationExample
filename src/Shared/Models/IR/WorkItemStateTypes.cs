namespace Shared.Models.IR;

public enum WorkItemStateTypes
{
    Waiting,
    Ended,
    ConnectingWithAgent,
    ConnectedWithAgent,
    AgentDisconnected,
    Inactive,
    Abandoned,
    Redirected,
    Error,
    Transferred,
    Parked,
    Started,
    InWorklow,
}