using System;

namespace Cloudflare.Net.Exceptions
{
    public class CloudflareException : Exception
    {

        public CloudflareException(string message)
            : base(message) { }

    }
}
