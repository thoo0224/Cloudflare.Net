<div align="center">

# Cloudflare.Net

.NET Cloudflare API wrapper

<img src="https://github.com/thoo0224/Cloudflare.Net/blob/main/resources/icon.png" width="100"/>

[![Nuget](https://img.shields.io/nuget/v/Cloudflare.Net.Thoo?logo=nuget)](https://www.nuget.org/packages/Cloudflare.Net.Thoo) [![Nuget DLs](https://img.shields.io/nuget/dt/Cloudflare.Net.Thoo?logo=nuget)](https://www.nuget.org/packages/Cloudflare.Net.Thoo) [![GitHub License](https://img.shields.io/github/license/thoo0224/Cloudflare.Net)](https://github.com/thoo0224/Clouflare.Net/blob/master/LICENSE)

</div>

### How to start
```cs
// You can create the api client in 2 ways.
// With this way you need to login manually.
CloudflareApiClient client = new CloudflareApiClientBuilder()
	.WithApiKey("your-api-key")
	.WithEmail("your-email")
	.Create();
await client.LoginAsync();

// And with this way you can create the api client and login automatically.
CloudflareApiClient client = await new CloudflareApiClientBuilder()
	.WithApiKey("your-api-key")
	.WithEmail("your-email")
	.CreateAndLoginAsync();

// If you would like to modify the rest client that is used, that is possible using the ModifyRestClient method in the CloduflareApiClientBuilder!
new CloudflareApiClientBuilder()
	.ModifyRestClient(restClient => 
	{
		// Do whatever you would like to do.
	})

// It's pretty self explanatory but here are a few methods.
CloudflareResponse<List<Zone>> zones = await client.Zone.GetZonesAsync();
Zone firstZone = zones.FirstOrDefault();
CloudflareResponse<List<DnsRecord>> dnsRecords = await client.Dns.GetDnsRecordsAsync(firstZone);
await client.Dns.CreateDnsRecordAsync(firstZone, "test", "8.8.8.8", DnsRecordType.A);
```

### NuGet
```
Install-Package Cloudflare.Net.Thoo
```

### To do
* Adding more endpoints
* gRPC

### Packages
* [RestSharp] (https://github.com/restsharp/RestSharp)
* [Newtonsoft.Json] (https://github.com/JamesNK/Newtonsoft.Json)
* [XUnit] (https://github.com/xunit/xunit)

### Contribution
Any type of contribution is appreciated!

### License
Cloudflare.Net (Apache-2.0) [License](https://github.com/thoo0224/Clouflare.Net/blob/master/LICENSE)
