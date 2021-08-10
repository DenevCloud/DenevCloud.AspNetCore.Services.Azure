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
        public string DNSclientId { get; set; }
        public string DNSsecret { get; set; }
        public string DNSresourceGroupName { get; set; }
        public Zone DNSZoneParams { get; set; }
        public string DNSzoneName { get; set; } 
    }
}
