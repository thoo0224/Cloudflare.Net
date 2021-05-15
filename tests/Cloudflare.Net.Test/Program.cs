using System;
using System.Threading.Tasks;
using Cloudflare.Net.Enums;

namespace Cloudflare.Net.Test
{
    internal class Program
    {

        private static async Task Main(string[] args)
        {
            var apiToken = Environment.GetEnvironmentVariable("apiToken");
            var apiKey = Environment.GetEnvironmentVariable("apiKey");
            var email = Environment.GetEnvironmentVariable("email");

            var client = await new CloudflareApiClientBuilder()
                .WithAuthenticationType(AuthenticationType.ApiKey)
                .WithApiToken(apiToken)
                .WithApiKey(apiKey)
                .WithEmail(email)
                .CreateAndLoginAsync();

            var zones = await client.Zone.GetZonesAsync();
            foreach (var zone in zones.Result)
            {
                Console.WriteLine(zone?.Name ?? "None");
            }
        }

    }
}
