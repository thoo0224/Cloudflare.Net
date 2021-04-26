using System;
using System.Collections.Generic;
using System.Diagnostics;

using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace Cloudflare.Net.Objects.Zone
{
    [DebuggerDisplay("{ " + nameof(Name) + "}")]
    public class Zone
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> OriginalNameServers { get; set; }
        public string OriginalRegistrar { get; set; }
        public string OriginalDnsHost { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime ActivatedOn { get; set; }
        public ZoneOwner Owner { get; set; }
        public ZoneAccount Account { get; set; }
        public List<string> Permissions { get; set; }
        public ZonePlan Plan { get; set; }
        public ZonePlan PlanPending { get; set; }
        public string Status { get; set; }
        public bool Paused { get; set; }
        public string Type { get; set; }
        public List<string> NameServers { get; set; }

    }
}
