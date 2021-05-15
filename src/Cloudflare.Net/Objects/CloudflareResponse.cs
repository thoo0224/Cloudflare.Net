using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Cloudflare.Net.Objects
{

    public class CloudflareResponse
    {

        public bool Success { get; set; }
        public List<CloudflareError> Errors { get; set; }
        public List<string> Messages { get; set; }
        public CloudflareResultInfo ResultInfo { get; set; }

    }

    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class CloudflareResponse<T> : CloudflareResponse
    {

        public T Result { get; set; }

        private object DebuggerDisplay => Success ? Result : $"Error: {Errors.FirstOrDefault()?.Message}" as object;

    }

}
