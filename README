# Cloudflare.Net

<img src="https://github.com/thoo0224/Cloudflare.Net/blob/main/resources/Icon.png" width="100"/>

[![Nuget](https://img.shields.io/nuget/v/Cloudflare.Net.Thoo?logo=nuget)](https://www.nuget.org/packages/Cloudflare.Net.Thoo)
[![Nuget DLs](https://img.shields.io/nuget/dt/Cloudflare.Net.Thoo?logo=nuget)](https://www.nuget.org/packages/Cloudflare.Net.Thoo)
[![GitHub License](https://img.shields.io/github/license/thoo0224/Cloudflare.Net)](https://github.com/thoo0224/Clouflare.Net/blob/master/LICENSE)

# How to start
```cs
CloudflareApiClient client = new CloudflareApiClient(options => 
{
  options.ApiKey = "your-api-key";
  options.Email = "your-email";
});
await client.LoginAsync();

// It's pretty self explanatory but here are a few methods.
CloudflareResponse<List<Zone>> zones = await client.Zone.GetZonesAsync();
Zone firstZone = zones.FirstOrDefault();
CloudflareResponse<List<DnsRecord>> dnsRecords = await client.Dns.GetDnsRecordsAsync(firstZone);
await client.Dns.CreateDnsRecordAsync(firstZone, "test", "8.8.8.8", DnsRecordType.A);
```

# NuGet
```
Install-Package Cloudflare.Net.Thoo
```

# To do
* Adding more endpoints
* gRPC

# Packages
* [RestSharp] (https://github.com/restsharp/RestSharp)
* [Newtonsoft.Json] (https://github.com/JamesNK/Newtonsoft.Json)
* [XUnit] (https://github.com/xunit/xunit)

# Contribution
Feel free to contribute!

# License
Cloudflare.Net (Apache) [License](https://github.com/thoo0224/Clouflare.Net/blob/master/LICENSE)
