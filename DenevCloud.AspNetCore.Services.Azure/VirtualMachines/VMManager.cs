using Microsoft.Azure.Management.Compute;
using Microsoft.Azure.Management.Compute.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Rest;
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

        public void StartVirtualMachine(string VMName)
        {
            CheckLogIn();
            virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.Start(virtualMachinesOptions.VMResourceGroup, VMName);
        }

        public async Task StartVirtualMachineAsync(string VMName)
        {
            await CheckLogInAsync();
            await virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.StartAsync(virtualMachinesOptions.VMResourceGroup, VMName);
        }

        public async Task StartVirtualMachineAsync(string VMName, CancellationToken token)
        {
            await CheckLogInAsync();
            await virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.StartAsync(virtualMachinesOptions.VMResourceGroup, VMName, token);
        }

        public void RestartVirtualMachine(string VMName, bool skipShutdown = false)
        {
            CheckLogIn();
            virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.Restart(virtualMachinesOptions.VMResourceGroup, VMName);
        }

        public async Task RestartVirtualMachineAsync(string VMName)
        {
            await CheckLogInAsync();
            await virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.RestartAsync(virtualMachinesOptions.VMResourceGroup, VMName);
        }

        public async Task RestartVirtualMachineAsync(string VMName, CancellationToken token)
        {
            await CheckLogInAsync();
            await virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.RestartAsync(virtualMachinesOptions.VMResourceGroup, VMName, token);
        }

        public void DeallocateVirtualMachine(string VMName)
        {
            CheckLogIn();
            virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.Deallocate(virtualMachinesOptions.VMResourceGroup, VMName);
        }

        public async Task DeallocateVirtualMachineAsync(string VMName, CancellationToken token)
        {
            await CheckLogInAsync();
            await virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.DeallocateAsync(virtualMachinesOptions.VMResourceGroup, VMName, token);
        }

        public async Task DeallocateVirtualMachineAsync(string VMName)
        {
            await CheckLogInAsync();
            await virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.DeallocateAsync(virtualMachinesOptions.VMResourceGroup, VMName);
        }

        public void PowerOffVirtualMachine(string VMName, bool skipShutdown = false)
        {
            CheckLogIn();
            virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.PowerOff(virtualMachinesOptions.VMResourceGroup, VMName, skipShutdown);
        }

        public async Task PowerOffVirtualMachineAsync(string VMName, bool skipShutdown = false)
        {
            await CheckLogInAsync();
            await virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.PowerOffAsync(virtualMachinesOptions.VMResourceGroup, VMName, skipShutdown);
        }

        public async Task PowerOffVirtualMachineAsync(string VMName, CancellationToken token, bool skipShutdown = false)
        {
            await CheckLogInAsync();
            await virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.PowerOffAsync(virtualMachinesOptions.VMResourceGroup, VMName, skipShutdown, token);
        }

        internal void GetListVirtualMachines()
        {
            CheckLogIn();
            virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.List(virtualMachinesOptions.VMResourceGroup);
        }

        internal async Task GetListVirtualMachinesAsync()
        {
            await CheckLogInAsync();
            await virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.ListAsync(virtualMachinesOptions.VMResourceGroup);
        }

        internal async Task GetListVirtualMachinesAsync(CancellationToken token)
        {
            await CheckLogInAsync();
            await virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.ListAsync(virtualMachinesOptions.VMResourceGroup, token);
        }
    }
}