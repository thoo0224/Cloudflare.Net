using System;
using System.Diagnostics;

namespace Cloudflare.Net.Objects.User
{
    [DebuggerDisplay("{" + nameof(Email) + "}")]
    public class CloudflareUser
    {

        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Telephone { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool TwoFactorAuthenticationEnabled { get; set; }
        public bool Suspended { get; set; }

    }
}
