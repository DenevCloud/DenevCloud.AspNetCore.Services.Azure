using Microsoft.Azure.Management.Dns.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenevCloud.AspNetCore.Services.Azure.AzureDNS
{
    public class Options
    {
        public string DNS_client_id { get; set; }
        public string DNS_secret { get; set; }
        public string DNS_resource_group { get; set; }
        public string DNS_zone_location { get; set; }
        public string DNS_zone_name { get; set; } 
    }
}
