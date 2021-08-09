using DenevCloud.AspNetCore.Services.Azure.AzureDNS;
using DenevCloud.AspNetCore.Services.Azure.KeyVaults;
using DenevCloud.AspNetCore.Services.Azure.VirtualMachines;
using DenevCloud.AspNetCore.Services.Azure.VirtualMachines.NetworkInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DenevCloud.AspNetCore.Services.Azure
{
    public static class ExtensionPoint
    {
        public static IServiceCollection AddDenevClouAzure(this IServiceCollection services, IConfiguration Configuration, bool UseDNS = false, bool UseKeyVault = false, bool UseVirtualMachines = false)
        {
            services.AddOptions();

            services.Configure<GeneralOptions>(Configuration.GetSection("DenevCloud"));

            if (UseDNS)
                services.Configure<AzureDNSOptions>(Configuration.GetSection("DenevCloud"));
            if(UseKeyVault)
                services.Configure<KeyVaultOptions>(Configuration.GetSection("DenevCloud"));
            if (UseVirtualMachines)
            {
                services.Configure<VirtualMachinesOptions>(Configuration.GetSection("DenevCloud"));
                services.AddTransient<IVMManager, VMManager>();
                services.AddTransient<INetworkInterfaceManager, NetworkInterfaceManager>();
            }

            return services;
        }
    }
}