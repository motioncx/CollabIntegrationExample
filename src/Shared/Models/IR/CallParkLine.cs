using System;

namespace Shared.Models.IR;

public class CallParkLine
  {
    public int TenantId { get; init; }

    public string LineNum { get; init; }

    public long? AgentId { get; set; }

    public string Comment { get; set; }

    public string AgentFullName { get; set; }

    public string CustomerFullName { get; set; }

    public string CustomerPhoneNumber { get; set; }

    public Guid? InteractionId { get; set; }

    public DateTime ParkedAtUtc { get; set; }

    public string CallSid { get; set; }

    public string QueueSid { get; set; }

    public string ErrorMsg { get; set; }

    public bool IsInUse { get; set; }

    public long WorkitemId { get; set; }
    
    

    public CallParkLine Clone()
    {
      return new CallParkLine()
      {
        TenantId = this.TenantId,
        LineNum = this.LineNum,
        AgentId = this.AgentId,
        Comment = this.Comment,
        AgentFullName = this.AgentFullName,
        CustomerFullName = this.CustomerFullName,
        InteractionId = this.InteractionId,
        ParkedAtUtc = this.ParkedAtUtc,
        CallSid = this.CallSid,
        QueueSid = this.QueueSid,
        IsInUse = this.IsInUse,
        CustomerPhoneNumber = this.CustomerPhoneNumber,
        WorkitemId = this.WorkitemId
      };
    }

    public void Park(
      long workitemId,
      Guid interactionId,
      string callSid,
      string parkComment,
      string agentFullName,
      string customerFullName,
      long agentId,
      string phone)
    {
      //McxLogger.Here(nameof (Park), "/home/runner/work/Nugets/Nugets/nuget.srcs/Channel/Motion.Channels.Shared.TwilioVoice/Models/CallParkLine.cs", 53).Information("Parking Line Cache Set For Line {ParkLine} for WorkitemId {WorkitemId} InteractionId {InteractionId}, CallSid {CallSid} with comment {Comment}", (object) this.LineNum, (object) workitemId, (object) interactionId, (object) callSid, (object) parkComment);
      this.WorkitemId = workitemId;
      this.InteractionId = new Guid?(interactionId);
      this.CallSid = callSid;
      this.Comment = parkComment;
      this.ParkedAtUtc = DateTime.UtcNow;
      this.IsInUse = true;
      this.AgentFullName = agentFullName;
      this.CustomerFullName = customerFullName;
      this.AgentId = new long?(agentId);
      this.CustomerPhoneNumber = phone;
    }

    public void UnPark()
    {
      this.InteractionId = new Guid?();
      this.CallSid = (string) null;
      this.QueueSid = (string) null;
      this.Comment = (string) null;
      this.ParkedAtUtc = DateTime.MinValue;
      this.IsInUse = false;
      this.AgentFullName = (string) null;
      this.CustomerFullName = (string) null;
      this.AgentId = new long?();
      this.CustomerPhoneNumber = (string) null;
      this.WorkitemId = 0L;
    }
  }