using Microsoft.Azure.Management.Dns.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenevCloud.AspNetCore.Services.Azure.AzureDNS
{
    internal class AzureDNSOptions
    {
        internal string DNSclientId { get; set; } 
        internal string DNSsecret { get; set; } 
        internal string DNSresourceGroupName { get; set; } 
        internal Zone DNSZoneParams { get; set; } 
        internal string DNSzoneName { get; set; } 
    }
}
