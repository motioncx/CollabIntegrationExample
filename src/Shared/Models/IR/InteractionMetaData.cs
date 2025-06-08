using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Shared.Models.IR;


public class InteractionMetaDataBase
{
    public int TenantId { get; set; }

    public long WorkItemId { get; set; }

    public Guid? RelatedInteractionId { get; set; }

    public Guid InteractionId { get; set; }

    public string ChannelUniqueIdentifier { get; set; }

    public ChannelProviderTypes ChannelProviderType { get; set; }

    public ChannelTypes ChannelType { get; set; }

    public int QueueId { get; set; }

    public CustomerData CustomerData { get; set; }

    public MotionEventDirection Direction { get; set; }

    public string From { get; set; }

    public string To { get; set; }
}

public class InteractionMetaData : InteractionMetaDataBase
{
    [Obsolete("Please use ChannelProviderType")]
    [JsonConverter(typeof(StringEnumConverter))]
    public ChannelProviderTypes ChannelProvider { get; set; }

    #region Channel Properties

    // Used in clients for channel chat
    public string Token { get; set; }

    // Used in clients for channel voice
    public DestinationType? CallTreatmentType { get; set; }

    #endregion

    #region Properties populated in create/queue handler

    public List<Guid> Interactions { get; set; }

    public WorkItemStateTypes State { get; set; } = WorkItemStateTypes.Waiting;

    [Obsolete("Please use WorkItemId")]
    public long WorkitemId { get; set; } // Verify if we do not use this on clients

    public long? AgentId { get; set; }

    #region Required for MPC

    public InteractionClientRequest WorkItemRequested { get; set; } = new InteractionClientRequest();

    #endregion

    #region Properties Used in different logics for voice channel

    // Used for ring groups
    public bool IsRingGroup { get; set; }

    // Used in Transferring
    public string PhoneNumber { get; set; }

    // Used in parked interactions
    public CallParkLine ParkedLine { get; set; }
        
    // Used in Conference to queue, We need to remove this when proper conference design is in place
    public bool ConferenceToQueue { get; set; }

    // Used in Conference to queue, We need to remove this when proper conference design is in place
    public string ConferenceHostAgentPhone { get; set; }

    #endregion

    #endregion
}