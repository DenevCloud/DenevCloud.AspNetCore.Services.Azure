using DenevCloud.AspNetCore.Services.Azure.AzureDNS;
using DenevCloud.AspNetCore.Services.Azure.KeyVaults;
using DenevCloud.AspNetCore.Services.Azure.VirtualMachines;
using DenevCloud.AspNetCore.Services.Azure.VirtualMachines.NetworkInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DenevCloud.AspNetCore.Services.Azure
{
    public static class ExtensionPoint
    {
       
        private static ConfigurationBuilder builder = (ConfigurationBuilder)new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            
#if DEBUG
            .AddJsonFile($"appsettings.Development.json", optional: true)
#else
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
#endif
            .AddEnvironmentVariables();

        private static IConfiguration Configuration = builder.Build();

        public static IServiceCollection AddDenevCloudAzure(this IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<GeneralOptions>(Configuration.GetSection("DenevCloud"));
            return services;
        }

        public static IServiceCollection UseVirtualMachines(this IServiceCollection services)
        {
            services.Configure<VirtualMachines.Options>(Configuration.GetSection("DenevCloud"));
            services.AddTransient<IVMManager, VMManager>();
            services.AddTransient<INetworkInterfaceManager, NetworkInterfaceManager>();
            return services;
        }

        public static IServiceCollection UseKeyVaults(this IServiceCollection services)
        {
            services.Configure<KeyVaults.Options>(Configuration.GetSection("DenevCloud"));
            services.AddTransient<IKeyVaultManager, KeyVaultManager>();
            return services;
        }

        public static IServiceCollection UseAzureDNS(this IServiceCollection services)
        {
            services.Configure<AzureDNS.Options>(Configuration.GetSection("DenevCloud"));
            return services;
        }
    }
}