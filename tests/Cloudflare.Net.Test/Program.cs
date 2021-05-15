using Cloudflare.Net.Enums;

using System;
using System.Threading.Tasks;

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

            var response = await client.User.GetPermissionsAsync();
            if (!response.Success) {
                foreach (var error in response.Errors)
                {
                    Console.WriteLine($"Error: {error.Message} (${error.Code})");
                }
                return;
            }

            foreach (var zone in response.Result)
            {
                Console.WriteLine(zone?.Name);
            }
        }

    }
}
