using Cloudflare.Net.Objects.Zone;

using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace Cloudflare.Net.Options
{
    public class ZoneProperties
    {

        public string Name { get; set; }
        [J("jump_start")] public bool JumpStart { get; set; }
        public ZoneType Type { get; set; }

    }
}
