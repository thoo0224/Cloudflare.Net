using System;

namespace Cloudflare.Net.Objects.User
{
    public class UserVerify
    {

        public string Id { get; set; }
        public string Status { get; set; }
        public DateTime NotBefore { get; set; }
        public DateTime ExpiresOn { get; set; }

    }
}
