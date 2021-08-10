using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace DenevCloud.AspNetCore.Services.Azure.KeyVaults
{
    public class KeyVaultManager : IKeyVaultManager
    {
        private readonly GeneralOptions generalOptions;
        private readonly KeyVaultOptions keyVaultOptions;

        public KeyVaultManager(IOptions<GeneralOptions> generalOptions, IOptions<KeyVaultOptions> keyVaultOptions)
        {
            this.generalOptions = generalOptions.Value;
            this.keyVaultOptions = keyVaultOptions.Value;
        }

        public string GetSecret(string SecretName)
        {
            var credential = new ClientSecretCredential(generalOptions.tenant_id, keyVaultOptions.KeyVault_clientId, keyVaultOptions.KeyVault_clientSecret);
            var client = new SecretClient(vaultUri: new Uri($"https://{keyVaultOptions.KeyVault_endpoint}.vault.azure.net/"), credential);
            return client.GetSecret(SecretName).Value.Value;
        }

        public async Task<string> GetSecretAsync(string SecretName)
        {
            var credential = new ClientSecretCredential(generalOptions.tenant_id, keyVaultOptions.KeyVault_clientId, keyVaultOptions.KeyVault_clientSecret);
            var client = new SecretClient(vaultUri: new Uri($"https://{keyVaultOptions.KeyVault_endpoint}.vault.azure.net/"), credential);
            var result = await client.GetSecretAsync(SecretName);
            return result.Value.Value;
        }

        public bool SetNewSecret(string SecretName, string SecretValue)
        {
            var credential = new ClientSecretCredential(generalOptions.tenant_id, keyVaultOptions.KeyVault_clientId, keyVaultOptions.KeyVault_clientSecret);
            var client = new SecretClient(vaultUri: new Uri($"https://{keyVaultOptions.KeyVault_endpoint}.vault.azure.net/"), credential);

            KeyVaultSecret keyVaultSecret = new KeyVaultSecret(SecretName, SecretValue);

            client.SetSecret(keyVaultSecret);

            return true;
        }

        public async Task<bool> SetNewSecretAsync(string SecretName, string SecretValue)
        {
            var credential = new ClientSecretCredential(generalOptions.tenant_id, keyVaultOptions.KeyVault_clientId, keyVaultOptions.KeyVault_clientSecret);
            var client = new SecretClient(vaultUri: new Uri($"https://{keyVaultOptions.KeyVault_endpoint}.vault.azure.net/"), credential);

            KeyVaultSecret keyVaultSecret = new KeyVaultSecret(SecretName, SecretValue);

            await client.SetSecretAsync(keyVaultSecret);

            return true;
        }
    }
}