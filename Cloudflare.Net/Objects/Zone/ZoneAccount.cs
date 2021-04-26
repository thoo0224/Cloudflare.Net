using System.Diagnostics;

namespace Cloudflare.Net.Objects.Zone
{
    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class ZoneAccount
    {

        public string Id { get; set; }
        public string Name { get; set; }

    }
}
