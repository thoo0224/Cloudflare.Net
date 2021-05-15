using System;

namespace Cloudflare.Net
{
    [Obsolete("Please use CloudflareApiClientBuilder#create to create an api client. (This constructor will be removed within the next few updates.)")]
    public class CloudflareApiClientOptions
    {

        public string BaseUrl { get; set; }
        public string Email { get; set; }
        public string ApiKey { get; set; }

    }
}
