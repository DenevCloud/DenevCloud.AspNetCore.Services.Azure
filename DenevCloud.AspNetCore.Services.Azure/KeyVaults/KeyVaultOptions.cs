using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenevCloud.AspNetCore.Services.Azure.KeyVaults
{
    public class KeyVaultOptions
    {
        public string KeyVault_clientId { get; internal set; }
        public string KeyVault_clientSecret { get; internal set; }
        public string KeyVault_endpoint { get; internal set; }
    }
}
