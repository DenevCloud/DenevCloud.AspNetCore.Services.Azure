using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenevCloud.AspNetCore.Services.Azure.KeyVaults
{
    public interface IKeyVaultManager
    {
        string GetSecret(string SecretName);
        Task<string> GetSecretAsync(string SecretName);
        bool SetNewSecret(string SecretName, string SecretValue);
        Task<bool> SetNewSecretAsync(string SecretName, string SecretValue);
    }
}
