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
        private readonly Options virtualMachinesOptions;

        private ServiceClientCredentials ClientCredentials;
        private DateTime LastLogIn;

        public VMManager(IOptions<GeneralOptions> generalOptions, IOptions<Options> virtualMachinesOptions)
        {
            this.generalOptions = generalOptions.Value;
            this.virtualMachinesOptions = virtualMachinesOptions.Value;
        }

        internal async Task CheckLogInAsync()
        {
            if (LastLogIn < DateTime.Now.AddMinutes(-55))
            {
                ClientCredentials = await ApplicationTokenProvider.LoginSilentAsync(generalOptions.tenant_id, virtualMachinesOptions.VM_client_id, virtualMachinesOptions.VM_client_sercet);
                LastLogIn = DateTime.Now;
            }
        }

        internal void CheckLogIn()
        {
            if (LastLogIn < DateTime.Now.AddMinutes(-55))
            {
                ClientCredentials = ApplicationTokenProvider.LoginSilentAsync(generalOptions.tenant_id, virtualMachinesOptions.VM_client_id, virtualMachinesOptions.VM_client_sercet).Result;
                LastLogIn = DateTime.Now;
            }
        }

        public VirtualMachine GetVirtualMachine(string VMName)
        {
            CheckLogIn();
            return virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.Get(virtualMachinesOptions.VM_resource_group, VMName, InstanceViewTypes.InstanceView);
        }

        public async Task<VirtualMachine> GetVirtualMachineAsync(string VMName)
        {
            await CheckLogInAsync();
            return virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.Get(virtualMachinesOptions.VM_resource_group, VMName, InstanceViewTypes.InstanceView);
        }

        public async Task<VirtualMachine> GetVirtualMachineAsync(string VMName, CancellationToken token)
        {
            await CheckLogInAsync();
            return await virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.GetAsync(virtualMachinesOptions.VM_resource_group, VMName, InstanceViewTypes.InstanceView, token);
        }

        public void StartVirtualMachine(string VMName)
        {
            CheckLogIn();
            virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.Start(virtualMachinesOptions.VM_resource_group, VMName);
        }

        public async Task StartVirtualMachineAsync(string VMName)
        {
            await CheckLogInAsync();
            await virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.StartAsync(virtualMachinesOptions.VM_resource_group, VMName);
        }

        public async Task StartVirtualMachineAsync(string VMName, CancellationToken token)
        {
            await CheckLogInAsync();
            await virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.StartAsync(virtualMachinesOptions.VM_resource_group, VMName, token);
        }

        public void RestartVirtualMachine(string VMName, bool skipShutdown = false)
        {
            CheckLogIn();
            virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.Restart(virtualMachinesOptions.VM_resource_group, VMName);
        }

        public async Task RestartVirtualMachineAsync(string VMName)
        {
            await CheckLogInAsync();
            await virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.RestartAsync(virtualMachinesOptions.VM_resource_group, VMName);
        }

        public async Task RestartVirtualMachineAsync(string VMName, CancellationToken token)
        {
            await CheckLogInAsync();
            await virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.RestartAsync(virtualMachinesOptions.VM_resource_group, VMName, token);
        }

        public void DeallocateVirtualMachine(string VMName)
        {
            CheckLogIn();
            virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.Deallocate(virtualMachinesOptions.VM_resource_group, VMName);
        }

        //public async Task DeallocateVirtualMachineAsync(string VMName, CancellationToken token)
        //{
        //    await CheckLogInAsync();
        //    await virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.DeallocateAsync(virtualMachinesOptions.VMResourceGroup, VMName, token);
        //}

        public async Task DeallocateVirtualMachineAsync(string VMName)
        {
            await CheckLogInAsync();
            await virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.DeallocateAsync(virtualMachinesOptions.VM_resource_group, VMName);
        }

        public void PowerOffVirtualMachine(string VMName, bool skipShutdown = false)
        {
            CheckLogIn();
            virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.PowerOff(virtualMachinesOptions.VM_resource_group, VMName, skipShutdown);
        }

        public async Task PowerOffVirtualMachineAsync(string VMName, bool skipShutdown = false)
        {
            await CheckLogInAsync();
            await virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.PowerOffAsync(virtualMachinesOptions.VM_resource_group, VMName, skipShutdown);
        }

        public async Task PowerOffVirtualMachineAsync(string VMName, CancellationToken token, bool skipShutdown = false)
        {
            await CheckLogInAsync();
            await virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.PowerOffAsync(virtualMachinesOptions.VM_resource_group, VMName, skipShutdown, token);
        }

        internal void GetListVirtualMachines()
        {
            CheckLogIn();
            virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.List(virtualMachinesOptions.VM_resource_group);
        }

        internal async Task GetListVirtualMachinesAsync()
        {
            await CheckLogInAsync();
            await virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.ListAsync(virtualMachinesOptions.VM_resource_group);
        }

        internal async Task GetListVirtualMachinesAsync(CancellationToken token)
        {
            await CheckLogInAsync();
            await virtualMachinesOptions.computeManagementClient(ClientCredentials).VirtualMachines.ListAsync(virtualMachinesOptions.VM_resource_group, token);
        }
    }
}