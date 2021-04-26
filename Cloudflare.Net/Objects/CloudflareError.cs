using System.Diagnostics;

namespace Cloudflare.Net.Objects
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class CloudflareError
    {

        public int Code { get; set; }
        public string Message { get; set; }

        private string DebuggerDisplay => $"Error: {Code} | {Message}";

    }
}
