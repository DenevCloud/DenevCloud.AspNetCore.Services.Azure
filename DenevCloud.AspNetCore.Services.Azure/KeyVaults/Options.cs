using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenevCloud.AspNetCore.Services.Azure.KeyVaults
{
    public class Options
    {
        public string keyVault_client_id { get; set; }
        public string keyVault_client_secret { get; set; }
        public string keyVault_endpoint { get; set; }
    }
}
