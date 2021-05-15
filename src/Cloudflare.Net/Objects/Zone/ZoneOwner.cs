using System.Diagnostics;

namespace Cloudflare.Net.Objects.Zone
{
    [DebuggerDisplay("{" + nameof(Email) + "}")]
    public class ZoneOwner
    {

        public string Id { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }

    }
}
