using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Cloudflare.Net.Test
{
    internal class Program
    {

        private static async Task Main(string[] args)
        {
            var client = new CloudflareApiClient(options =>
            {
                options.Email = args[0];
                options.ApiKey = args[1];
            });
            await client.LoginAsync();

            var zones = await client.Zone.GetZonesAsync();//"2fcb2405648c7b3313c69f383173930e"
            var zone = zones.Result.FirstOrDefault(x => x.Name == "thoo.nl");
            var dnsRecord = await client.Zone.GetZoneAnalyticsDashboard(zone?.Id);
            
            Debugger.Break();
        }

    }
}
