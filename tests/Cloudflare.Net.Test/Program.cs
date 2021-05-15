using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Cloudflare.Net.Test
{
    internal class Program
    {

        private static async Task Main(string[] args)
        {
            var apiKey = Environment.GetEnvironmentVariable("apiKey");
            var email = Environment.GetEnvironmentVariable("email");

            var client = await new CloudflareApiClientBuilder()
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
