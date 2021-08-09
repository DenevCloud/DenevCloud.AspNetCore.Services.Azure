using Microsoft.Azure.Management.Compute.Models;
using System.Threading;
using System.Threading.Tasks;

namespace DenevCloud.AspNetCore.Services.Azure.VirtualMachines
{
    public interface IVMManager
    {
        VirtualMachine GetVirtualMachine(string VMName);
        Task<VirtualMachine> GetVirtualMachineAsync(string VMName);
        Task<VirtualMachine> GetVirtualMachineAsync(string VMName, CancellationToken token);

    }
}
