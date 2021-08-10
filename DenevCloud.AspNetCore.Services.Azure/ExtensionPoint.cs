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
        public static IServiceCollection AddDenevCloudAzure(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddOptions();
            services.Configure<GeneralOptions>(Configuration.GetSection("DenevCloud"));
            return services;
        }

        public static IServiceCollection UseVirtualMachines(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<VirtualMachinesOptions>(Configuration.GetSection("DenevCloud"));
            services.AddTransient<IVMManager, VMManager>();
            services.AddTransient<INetworkInterfaceManager, NetworkInterfaceManager>();
            return services;
        }

        public static IServiceCollection UseKeyVaults(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<KeyVaultOptions>(Configuration.GetSection("DenevCloud"));
            services.AddTransient<IKeyVaultManager, KeyVaultManager>();
            return services;
        }

        public static IServiceCollection UseAzureDNS(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<AzureDNSOptions>(Configuration.GetSection("DenevCloud"));
            return services;
        }
    }
}