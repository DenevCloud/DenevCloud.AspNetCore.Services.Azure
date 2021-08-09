using Microsoft.Azure.Management.Compute.Models;
using Microsoft.Azure.Management.Network;
using Microsoft.Extensions.Options;
using Microsoft.Rest;
using Microsoft.Rest.Azure.Authentication;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace DenevCloud.AspNetCore.Services.Azure.VirtualMachines.NetworkInterfaces
{
    [SuppressMessage("Major Code Smell", "S112:General exceptions should never be thrown", Justification = "<Pending>")]
    public class NetworkInterfaceManager : INetworkInterfaceManager
    {
        private readonly GeneralOptions generalOptions;
        private readonly VirtualMachinesOptions virtualMachinesOptions;

        private ServiceClientCredentials ClientCredentials;
        private DateTime LastLogIn;

        public NetworkInterfaceManager(IOptions<GeneralOptions> generalOptions, IOptions<VirtualMachinesOptions> virtualMachinesOptions)
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

        public List<string> GetListPublicIpv4(VirtualMachine virtualMachine, string NicName = null)
        {
            CheckLogIn();

            List<string> Ips = new List<string>();
            if (NicName == null)
            {
                var nicList = virtualMachine.NetworkProfile.NetworkInterfaces;
                using (var client = new NetworkManagementClient(ClientCredentials))
                {
                    client.SubscriptionId = generalOptions.subscriptionId;
                    var NiCList = client.NetworkInterfaces.ListAll();
                    var allPublicIps = client.PublicIPAddresses.ListAll();
                    foreach (var r_nic in nicList)
                    {
                        var networkIntefaceName = r_nic.Id.Split('/').Last();

                        var nic = NiCList.FirstOrDefault(x => x.Name == networkIntefaceName);

                        if (nic != null)
                        {
                            foreach (var ip in nic.IpConfigurations)
                            {
                                if(ip.PublicIPAddress != null)
                                {
                                    var actualIp = allPublicIps.First(i => i.Id == ip.PublicIPAddress.Id && i.PublicIPAddressVersion == "IPv4");
                                    if(actualIp != null)
                                     Ips.Add(actualIp.IpAddress);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception(message: $"Something went wrong. Network Interface with the a name of {networkIntefaceName} has not been found.");
                        }
                    }
                }
            }
            else
            {
                var nicList = virtualMachine.NetworkProfile.NetworkInterfaces.FirstOrDefault(n => n.Id == NicName);
                using (var client = new NetworkManagementClient(ClientCredentials))
                {
                    client.SubscriptionId = generalOptions.subscriptionId;

                    var NiCList = client.NetworkInterfaces.ListAll();
                    var allPublicIps = client.PublicIPAddresses.ListAll();

                    var networkIntefaceName = nicList.Id.Split('/').Last();

                    var nic = NiCList.FirstOrDefault(x => x.Name == networkIntefaceName);

                    if (nic != null)
                    {
                        foreach (var ip in nic.IpConfigurations)
                        {
                            if (ip.PublicIPAddress != null)
                            {
                                var actualIp = allPublicIps.First(i => i.Id == ip.PublicIPAddress.Id && i.PublicIPAddressVersion == "IPv4");
                                if (actualIp != null)
                                    Ips.Add(actualIp.IpAddress);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception(message: $"Something went wrong. Network Interface with the a name of {networkIntefaceName} has not been found.");
                    }
                }
            }

            return Ips;
        }

        public async Task<List<string>> GetListPublicIpv4Async(VirtualMachine virtualMachine, string NicName = null)
        {
            await CheckLogInAsync();

            List<string> Ips = new List<string>();

            if (NicName == null)
            {
                var nicList = virtualMachine.NetworkProfile.NetworkInterfaces;
                using (var client = new NetworkManagementClient(ClientCredentials))
                {
                    client.SubscriptionId = generalOptions.subscriptionId;

                    var NiCList = await client.NetworkInterfaces.ListAllAsync();
                    var allPublicIps = await client.PublicIPAddresses.ListAllAsync();

                    foreach (var r_nic in nicList)
                    {
                        var networkIntefaceName = r_nic.Id.Split('/').Last();

                        var nic = NiCList.FirstOrDefault(x => x.Name == networkIntefaceName);

                        if (nic != null)
                        {
                            foreach (var ip in nic.IpConfigurations)
                            {
                                if (ip.PublicIPAddress != null)
                                {
                                    var actualIp = allPublicIps.First(i => i.Id == ip.PublicIPAddress.Id && i.PublicIPAddressVersion == "IPv4");
                                    if (actualIp != null)
                                        Ips.Add(actualIp.IpAddress);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception(message: $"Something went wrong. Network Interface with the a name of {networkIntefaceName} has not been found.");
                        }
                    }
                }
            }
            else
            {
                var nicList = virtualMachine.NetworkProfile.NetworkInterfaces.FirstOrDefault(n => n.Id == NicName);
                using (var client = new NetworkManagementClient(ClientCredentials))
                {
                    client.SubscriptionId = generalOptions.subscriptionId;

                    var NiCList = await client.NetworkInterfaces.ListAllAsync();
                    var allPublicIps = await client.PublicIPAddresses.ListAllAsync();

                    var networkIntefaceName = nicList.Id.Split('/').Last();

                    var nic = NiCList.FirstOrDefault(x => x.Name == networkIntefaceName);

                    if (nic != null)
                    {
                        foreach (var ip in nic.IpConfigurations)
                        {
                            if (ip.PublicIPAddress != null)
                            {
                                var actualIp = allPublicIps.First(i => i.Id == ip.PublicIPAddress.Id && i.PublicIPAddressVersion == "IPv4");
                                if (actualIp != null)
                                    Ips.Add(actualIp.IpAddress);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception(message: $"Something went wrong. Network Interface with the a name of {networkIntefaceName} has not been found.");
                    }
                }
            }

            return Ips;
        }

        public List<string> GetListPublicIpv6(VirtualMachine virtualMachine, string NicName = null)
        {
            CheckLogIn();

            List<string> Ips = new List<string>();

            if (NicName == null)
            {
                var nicList = virtualMachine.NetworkProfile.NetworkInterfaces;
                using (var client = new NetworkManagementClient(ClientCredentials))
                {
                    client.SubscriptionId = generalOptions.subscriptionId;

                    var NiCList = client.NetworkInterfaces.ListAll();
                    var allPublicIps = client.PublicIPAddresses.ListAll();

                    foreach (var r_nic in nicList)
                    {
                        var networkIntefaceName = r_nic.Id.Split('/').Last();

                        var nic = NiCList.FirstOrDefault(x => x.Name == networkIntefaceName);

                        if (nic != null)
                        {
                            foreach (var ip in nic.IpConfigurations)
                            {
                                if (ip.PublicIPAddress != null)
                                {
                                    var actualIp = allPublicIps.First(i => i.Id == ip.PublicIPAddress.Id && i.PublicIPAddressVersion == "IPv6");
                                    if (actualIp != null)
                                        Ips.Add(actualIp.IpAddress);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception(message: $"Something went wrong. Network Interface with the a name of {networkIntefaceName} has not been found.");
                        }
                    }
                }
            }
            else
            {
                var nicList = virtualMachine.NetworkProfile.NetworkInterfaces.FirstOrDefault(n => n.Id == NicName);
                using (var client = new NetworkManagementClient(ClientCredentials))
                {
                    client.SubscriptionId = generalOptions.subscriptionId;

                    var NiCList = client.NetworkInterfaces.ListAll();
                    var allPublicIps = client.PublicIPAddresses.ListAll();

                    var networkIntefaceName = nicList.Id.Split('/').Last();

                    var nic = NiCList.FirstOrDefault(x => x.Name == networkIntefaceName);

                    if (nic != null)
                    {
                        foreach (var ip in nic.IpConfigurations)
                        {
                            if (ip.PublicIPAddress != null)
                            {
                                var actualIp = allPublicIps.First(i => i.Id == ip.PublicIPAddress.Id && i.PublicIPAddressVersion == "IPv6");
                                if (actualIp != null)
                                    Ips.Add(actualIp.IpAddress);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception(message: $"Something went wrong. Network Interface with the a name of {networkIntefaceName} has not been found.");
                    }
                }
            }

            return Ips;
        }

        
        public async Task<List<string>> GetListPublicIpv6Async(VirtualMachine virtualMachine, string NicName = null)
        {
            await CheckLogInAsync();

            List<string> Ips = new List<string>();

            if (NicName == null)
            {
                var nicList = virtualMachine.NetworkProfile.NetworkInterfaces;
                using (var client = new NetworkManagementClient(ClientCredentials))
                {
                    client.SubscriptionId = generalOptions.subscriptionId;

                    var NiCList = await client.NetworkInterfaces.ListAllAsync();
                    var allPublicIps = await client.PublicIPAddresses.ListAllAsync();

                    foreach (var r_nic in nicList)
                    {
                        var networkIntefaceName = r_nic.Id.Split('/').Last();

                        var nic = NiCList.FirstOrDefault(x => x.Name == networkIntefaceName);

                        if (nic != null)
                        {
                            foreach (var ip in nic.IpConfigurations)
                            {
                                if (ip.PublicIPAddress != null)
                                {
                                    var actualIp = allPublicIps.First(i => i.Id == ip.PublicIPAddress.Id && i.PublicIPAddressVersion == "IPv6");
                                    if (actualIp != null)
                                        Ips.Add(actualIp.IpAddress);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception(message: $"Something went wrong. Network Interface with the a name of {networkIntefaceName} has not been found.");
                        }
                    }
                }
            }
            else
            {
                var nicList = virtualMachine.NetworkProfile.NetworkInterfaces.FirstOrDefault(n => n.Id == NicName);
                using (var client = new NetworkManagementClient(ClientCredentials))
                {
                    client.SubscriptionId = generalOptions.subscriptionId;

                    var NiCList = await client.NetworkInterfaces.ListAllAsync();
                    var allPublicIps = await client.PublicIPAddresses.ListAllAsync();

                    var networkIntefaceName = nicList.Id.Split('/').Last();

                    var nic = NiCList.FirstOrDefault(x => x.Name == networkIntefaceName);

                    if (nic != null)
                    {
                        foreach (var ip in nic.IpConfigurations)
                        {
                            if (ip.PublicIPAddress != null)
                            {
                                var actualIp = allPublicIps.First(i => i.Id == ip.PublicIPAddress.Id && i.PublicIPAddressVersion == "IPv6");
                                if (actualIp != null)
                                    Ips.Add(actualIp.IpAddress);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception(message: $"Something went wrong. Network Interface with the a name of {networkIntefaceName} has not been found.");
                    }
                }
            }

            return Ips;
        }

        public List<string> GetListPrivateIpv4(VirtualMachine virtualMachine, string NicName = null)
        {
            CheckLogIn();

            List<string> Ips = new List<string>();
            if (NicName == null)
            {
                var nicList = virtualMachine.NetworkProfile.NetworkInterfaces;

                using (var client = new NetworkManagementClient(ClientCredentials))
                {
                    client.SubscriptionId = generalOptions.subscriptionId;
                    var NiCList = client.NetworkInterfaces.ListAll();
                    var allPublicIps = client.PublicIPAddresses.ListAll();
                    foreach (var r_nic in nicList)
                    {
                        var networkIntefaceName = r_nic.Id.Split('/').Last();

                        var nic = NiCList.FirstOrDefault(x => x.Name == networkIntefaceName);

                        if (nic != null)
                        {
                            foreach (var ip in nic.IpConfigurations)
                            {
                                if (!string.IsNullOrEmpty(ip.PrivateIPAddress) && ip.PrivateIPAddressVersion == "IPv4")
                                {
                                    Ips.Add(ip.PrivateIPAddress);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception(message: $"Something went wrong. Network Interface with the a name of {networkIntefaceName} has not been found.");
                        }
                    }
                }
            }
            else
            {
                var nicList = virtualMachine.NetworkProfile.NetworkInterfaces.FirstOrDefault(n => n.Id == NicName);
                using (var client = new NetworkManagementClient(ClientCredentials))
                {
                    client.SubscriptionId = generalOptions.subscriptionId;

                    var NiCList = client.NetworkInterfaces.ListAll();
                    var allPublicIps = client.PublicIPAddresses.ListAll();

                    var networkIntefaceName = nicList.Id.Split('/').Last();

                    var nic = NiCList.FirstOrDefault(x => x.Name == networkIntefaceName);

                    if (nic != null)
                    {
                        foreach (var ip in nic.IpConfigurations)
                        {
                            if (!string.IsNullOrEmpty(ip.PrivateIPAddress) && ip.PrivateIPAddressVersion == "IPv4")
                            {
                                Ips.Add(ip.PrivateIPAddress);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception(message: $"Something went wrong. Network Interface with the a name of {networkIntefaceName} has not been found.");
                    }
                }
            }

            return Ips;
        }

        public List<string> GetListPrivateIpv6(VirtualMachine virtualMachine, string NicName = null)
        {
            CheckLogIn();

            List<string> Ips = new List<string>();
            if (NicName == null)
            {
                var nicList = virtualMachine.NetworkProfile.NetworkInterfaces;

                using (var client = new NetworkManagementClient(ClientCredentials))
                {
                    client.SubscriptionId = generalOptions.subscriptionId;
                    var NiCList = client.NetworkInterfaces.ListAll();
                    var allPublicIps = client.PublicIPAddresses.ListAll();
                    foreach (var r_nic in nicList)
                    {
                        var networkIntefaceName = r_nic.Id.Split('/').Last();

                        var nic = NiCList.FirstOrDefault(x => x.Name == networkIntefaceName);

                        if (nic != null)
                        {
                            foreach (var ip in nic.IpConfigurations)
                            {
                                if (!string.IsNullOrEmpty(ip.PrivateIPAddress) && ip.PrivateIPAddressVersion == "IPv6")
                                {
                                    Ips.Add(ip.PrivateIPAddress);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception(message: $"Something went wrong. Network Interface with the a name of {networkIntefaceName} has not been found.");
                        }
                    }
                }
            }
            else
            {
                var nicList = virtualMachine.NetworkProfile.NetworkInterfaces.FirstOrDefault(n => n.Id == NicName);
                using (var client = new NetworkManagementClient(ClientCredentials))
                {
                    client.SubscriptionId = generalOptions.subscriptionId;

                    var NiCList = client.NetworkInterfaces.ListAll();
                    var allPublicIps = client.PublicIPAddresses.ListAll();

                    var networkIntefaceName = nicList.Id.Split('/').Last();

                    var nic = NiCList.FirstOrDefault(x => x.Name == networkIntefaceName);

                    if (nic != null)
                    {
                        foreach (var ip in nic.IpConfigurations)
                        {
                            if (!string.IsNullOrEmpty(ip.PrivateIPAddress) && ip.PrivateIPAddressVersion == "IPv6")
                            {
                                Ips.Add(ip.PrivateIPAddress);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception(message: $"Something went wrong. Network Interface with the a name of {networkIntefaceName} has not been found.");
                    }
                }
            }

            return Ips;
        }

        public async Task<List<string>> GetListPrivateIpv4Async(VirtualMachine virtualMachine, string NicName = null)
        {
            await CheckLogInAsync();

            List<string> Ips = new List<string>();

            if (NicName == null)
            {
                var nicList = virtualMachine.NetworkProfile.NetworkInterfaces;
                using (var client = new NetworkManagementClient(ClientCredentials))
                {
                    client.SubscriptionId = generalOptions.subscriptionId;

                    var NiCList = await client.NetworkInterfaces.ListAllAsync();
                    var allPublicIps = await client.PublicIPAddresses.ListAllAsync();

                    foreach (var r_nic in nicList)
                    {
                        var networkIntefaceName = r_nic.Id.Split('/').Last();

                        var nic = NiCList.FirstOrDefault(x => x.Name == networkIntefaceName);

                        if (nic != null)
                        {
                            foreach (var ip in nic.IpConfigurations)
                            {
                                if (!string.IsNullOrEmpty(ip.PrivateIPAddress) && ip.PrivateIPAddressVersion == "IPv4")
                                {
                                    Ips.Add(ip.PrivateIPAddress);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception(message: $"Something went wrong. Network Interface with the a name of {networkIntefaceName} has not been found.");
                        }
                    }
                }
            }
            else
            {
                var nicList = virtualMachine.NetworkProfile.NetworkInterfaces.FirstOrDefault(n => n.Id == NicName);
                using (var client = new NetworkManagementClient(ClientCredentials))
                {
                    client.SubscriptionId = generalOptions.subscriptionId;

                    var NiCList = await client.NetworkInterfaces.ListAllAsync();
                    var allPublicIps = await client.PublicIPAddresses.ListAllAsync();

                    var networkIntefaceName = nicList.Id.Split('/').Last();

                    var nic = NiCList.FirstOrDefault(x => x.Name == networkIntefaceName);

                    if (nic != null)
                    {
                        foreach (var ip in nic.IpConfigurations)
                        {
                            if (!string.IsNullOrEmpty(ip.PrivateIPAddress) && ip.PrivateIPAddressVersion == "IPv4")
                            {
                                Ips.Add(ip.PrivateIPAddress);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception(message: $"Something went wrong. Network Interface with the a name of {networkIntefaceName} has not been found.");
                    }
                }
            }

            return Ips;
        }

        public async Task<List<string>> GetListPrivateIpv6Async(VirtualMachine virtualMachine, string NicName = null)
        {
            await CheckLogInAsync();

            List<string> Ips = new List<string>();

            if (NicName == null)
            {
                var nicList = virtualMachine.NetworkProfile.NetworkInterfaces;
                using (var client = new NetworkManagementClient(ClientCredentials))
                {
                    client.SubscriptionId = generalOptions.subscriptionId;

                    var NiCList = await client.NetworkInterfaces.ListAllAsync();
                    var allPublicIps = await client.PublicIPAddresses.ListAllAsync();

                    foreach (var r_nic in nicList)
                    {
                        var networkIntefaceName = r_nic.Id.Split('/').Last();

                        var nic = NiCList.FirstOrDefault(x => x.Name == networkIntefaceName);

                        if (nic != null)
                        {
                            foreach (var ip in nic.IpConfigurations)
                            {
                                if (!string.IsNullOrEmpty(ip.PrivateIPAddress) && ip.PrivateIPAddressVersion == "IPv6")
                                {
                                    Ips.Add(ip.PrivateIPAddress);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception(message: $"Something went wrong. Network Interface with the a name of {networkIntefaceName} has not been found.");
                        }
                    }
                }
            }
            else
            {
                var nicList = virtualMachine.NetworkProfile.NetworkInterfaces.FirstOrDefault(n => n.Id == NicName);
                using (var client = new NetworkManagementClient(ClientCredentials))
                {
                    client.SubscriptionId = generalOptions.subscriptionId;

                    var NiCList = await client.NetworkInterfaces.ListAllAsync();
                    var allPublicIps = await client.PublicIPAddresses.ListAllAsync();

                    var networkIntefaceName = nicList.Id.Split('/').Last();

                    var nic = NiCList.FirstOrDefault(x => x.Name == networkIntefaceName);

                    if (nic != null)
                    {
                        foreach (var ip in nic.IpConfigurations)
                        {
                            if (!string.IsNullOrEmpty(ip.PrivateIPAddress) && ip.PrivateIPAddressVersion == "IPv6")
                            {
                                Ips.Add(ip.PrivateIPAddress);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception(message: $"Something went wrong. Network Interface with the a name of {networkIntefaceName} has not been found.");
                    }
                }
            }

            return Ips;
        }
    }
}
