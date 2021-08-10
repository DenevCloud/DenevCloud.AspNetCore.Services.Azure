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
        public string DNSclientId { get; internal set; }
        public string DNSsecret { get; internal set; }
        public string DNSresourceGroupName { get; internal set; }
        public Zone DNSZoneParams { get; internal set; }
        public string DNSzoneName { get; internal set; } 
    }
}
