using Microsoft.Azure.Management.Compute.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenevCloud.AspNetCore.Services.Azure.VirtualMachines.NetworkInterfaces
{
    public interface INetworkInterfaceManager
    {
        List<string> GetListPublicIpv4(VirtualMachine virtualMachine, string NicName = null);
        Task<List<string>> GetListPublicIpv4Async(VirtualMachine virtualMachine, string NicName = null);
        List<string> GetListPublicIpv6(VirtualMachine virtualMachine, string NicName = null);
        Task<List<string>> GetListPublicIpv6Async(VirtualMachine virtualMachine, string NicName = null);
    }
}