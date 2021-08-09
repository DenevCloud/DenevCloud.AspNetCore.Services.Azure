using Microsoft.Azure.Management.Compute.Models;
using System.Threading;
using System.Threading.Tasks;

namespace DenevCloud.AspNetCore.Services.Azure.VirtualMachines
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "<Pending>")]
    public interface IVMManager
    {
        VirtualMachine GetVirtualMachine(string VMName);
        Task<VirtualMachine> GetVirtualMachineAsync(string VMName);
        Task<VirtualMachine> GetVirtualMachineAsync(string VMName, CancellationToken token);
        void StartVirtualMachine(string VMName);
        Task StartVirtualMachineAsync(string VMName);
        Task StartVirtualMachineAsync(string VMName, CancellationToken token);
        void RestartVirtualMachine(string VMName, bool skipShutdown = false);
        Task RestartVirtualMachineAsync(string VMName);
        Task RestartVirtualMachineAsync(string VMName, CancellationToken token);
        void DeallocateVirtualMachine(string VMName);
        Task DeallocateVirtualMachineAsync(string VMName, CancellationToken token);
        Task DeallocateVirtualMachineAsync(string VMName);
        void PowerOffVirtualMachine(string VMName, bool skipShutdown = false);
        Task PowerOffVirtualMachineAsync(string VMName, bool skipShutdown = false);
        Task PowerOffVirtualMachineAsync(string VMName, CancellationToken token, bool skipShutdown = false);
        //void GetListVirtualMachines();
        //Task GetListVirtualMachinesAsync();
        //Task GetListVirtualMachinesAsync(CancellationToken token);
    }
}