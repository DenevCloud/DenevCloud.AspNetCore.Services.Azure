using Microsoft.Azure.Management.Dns.Models;
using Microsoft.Extensions.Options;

namespace DenevCloud.AspNetCore.Services.Azure.AzureDNS
{
    public class AzureDNSManager
    {
        private readonly AzureDNSOptions azureDNSOptions;
        private readonly Zone defaultZone;

        public AzureDNSManager(IOptions<AzureDNSOptions> azureDNSOptions)
        {
            this.azureDNSOptions = azureDNSOptions.Value;
            defaultZone = new Zone(azureDNSOptions.Value.DNS_ZoneLocation);
        }


    }
}
