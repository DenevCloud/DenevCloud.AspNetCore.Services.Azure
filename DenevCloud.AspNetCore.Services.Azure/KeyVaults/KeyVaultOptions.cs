using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenevCloud.AspNetCore.Services.Azure.KeyVaults
{
    public class KeyVaultOptions
    {
        public string KeyVault_clientId { get; set; }
        public string KeyVault_clientSecret { get; set; }
        public string KeyVault_endpoint { get; set; }
    }
}
