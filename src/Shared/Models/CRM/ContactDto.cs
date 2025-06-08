// Decompiled with JetBrains decompiler
// Type: Motion.CRM.Contracts.ContactDto
// Assembly: Motion.CRM.Contracts, Version=3.54.0.279, Culture=neutral, PublicKeyToken=null
// MVID: B457F70B-19B9-4D1A-ABCB-99564E6AFE72
// Assembly location: C:\Users\mdgar\.nuget\packages\motion.crm.contracts\3.54.0.279\lib\net8.0\Motion.CRM.Contracts.dll


using System;

#nullable disable
namespace Shared.Models.CRM
{
    public class ContactDto
    {
        public long Id { get; set; }

        public int TenantId { get; set; }

        public long? RelatedUserId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostCode { get; set; }

        public string Phone { get; set; }

        public string Name
        {
            get
            {
                return this.FirstName == null || this.LastName == null ? this.Email : this.FirstName + " " + this.LastName;
            }
        }

        public string DisplayName
        {
            get
            {
                if (this.FirstName != null && this.LastName != null)
                    return this.FirstName + " " + this.LastName;
                if (this.FirstName != null)
                    return this.FirstName;
                if (this.LastName != null)
                    return this.LastName;
                if (this.Phone != null)
                    return this.Phone;
                return this.Email == null ? "No Information Provided" : this.Email;
            }
        }

        public bool IsAgent => this.Id < AgentConstants.AgentThreshold;

        public string Organization { get; set; }

        public string Title { get; set; }

        public string Alias { get; set; }

        public string ExternalId { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsProtected { get; set; }
    }
}