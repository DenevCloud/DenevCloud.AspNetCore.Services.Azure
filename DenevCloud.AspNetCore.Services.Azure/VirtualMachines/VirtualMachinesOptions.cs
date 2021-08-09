using Microsoft.Azure.Management.Compute;
using Microsoft.Rest;
using System;

namespace DenevCloud.AspNetCore.Services.Azure.VirtualMachines
{
    public class VirtualMachinesOptions
    {
        public string subscriptionId { get; set; }
        public string VMclient_id { get; set; }
        public string VMclient_sercet { get; set; }
        public string VMresource { get; set; }
        public string VMResourceGroup { get; set; }

        public ComputeManagementClient computeManagementClient(ServiceClientCredentials ClientCredentials)
        {
            var CMC = new ComputeManagementClient(ClientCredentials);
            CMC.SubscriptionId = subscriptionId;
            return CMC;
        }
    }
}
