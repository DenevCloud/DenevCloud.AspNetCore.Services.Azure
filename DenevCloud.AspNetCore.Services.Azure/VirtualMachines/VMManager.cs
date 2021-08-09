using Microsoft.Azure.Management.Compute;
using Microsoft.Azure.Management.Compute.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Rest;
using Microsoft.Rest.Azure;
using Microsoft.Rest.Azure.Authentication;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DenevCloud.AspNetCore.Services.Azure.VirtualMachines
{
    public class VMManager : IVMManager
    {
        private readonly GeneralOptions generalOptions;
        private readonly VirtualMachinesOptions virtualMachinesOptions;

        private ServiceClientCredentials ClientCredentials;
        private DateTime LastLogIn;

        public VMManager(IOptions<GeneralOptions> generalOptions, IOptions<VirtualMachinesOptions> virtualMachinesOptions)
        {
            this.generalOptions = generalOptions.Value;
            this.virtualMachinesOptions = virtualMachinesOptions.Value;
        }

        internal async Task CheckLogInAsync()
        {
            if (LastLogIn < DateTime.Now.AddMinutes(-55))
            {
                ClientCredentials = await ApplicationTokenProvider.LoginSilentAsync(generalOptions.tenant_id, virtualMachinesOptions.VMclient_id, virtualMachinesOptions.VMclient_sercet);
                LastLogIn = DateTime.Now;
            }
        }

        internal void CheckLogIn()
        {
            if (LastLogIn < DateTime.Now.AddMinutes(-55))
            {
                ClientCredentials = ApplicationTokenProvider.LoginSilentAsync(generalOptions.tenant_id, virtualMachinesOptions.VMclient_id, virtualMachinesOptions.VMclient_sercet).Result;
                LastLogIn = DateTime.Now;
            }
        }

        internal IPage<VirtualMachine> GetVMList()
        {
            CheckLogIn();
            return virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.List(virtualMachinesOptions.VMResourceGroup);
        }

        internal async Task<IPage<VirtualMachine>> GetVMListAsync()
        {
            await CheckLogInAsync();
            return virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.List(virtualMachinesOptions.VMResourceGroup);
        }

        public VirtualMachine GetVirtualMachine(string VMName)
        {
            CheckLogIn();
            return virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.Get(virtualMachinesOptions.VMResourceGroup, VMName, InstanceViewTypes.InstanceView);
        }

        public async Task<VirtualMachine> GetVirtualMachineAsync(string VMName)
        {
            await CheckLogInAsync();
            return virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.Get(virtualMachinesOptions.VMResourceGroup, VMName, InstanceViewTypes.InstanceView);
        }

        public async Task<VirtualMachine> GetVirtualMachineAsync(string VMName, CancellationToken token)
        {
            await CheckLogInAsync();
            return await virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.GetAsync(virtualMachinesOptions.VMResourceGroup, VMName, InstanceViewTypes.InstanceView, token);
        }

    }
}
