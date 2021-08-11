using Microsoft.Azure.Management.Dns.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenevCloud.AspNetCore.Services.Azure.AzureDNS
{
    public class AzureDNSOptions
    {
        public string DNS_clientId { get; set; }
        public string DNS_secret { get; set; }
        public string DNS_resourceGroupName { get; set; }
        public string DNS_ZoneLocation { get; set; }
        public string DNS_zoneName { get; set; } 
    }
}
