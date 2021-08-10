using Microsoft.Azure.Management.Compute;
using Microsoft.Rest;
using System;

namespace DenevCloud.AspNetCore.Services.Azure.VirtualMachines
{
    public class VirtualMachinesOptions
    {
        public string subscriptionId { get; internal set; }
        public string VMclient_id { get; internal set; }
        public string VMclient_sercet { get; internal set; }
        public string VMresource { get; internal set; }
        public string VMResourceGroup { get; internal set; }

        public ComputeManagementClient computeManagementClient(ServiceClientCredentials ClientCredentials)
        {
            var CMC = new ComputeManagementClient(ClientCredentials);
            CMC.SubscriptionId = subscriptionId;
            return CMC;
        }
    }
}
