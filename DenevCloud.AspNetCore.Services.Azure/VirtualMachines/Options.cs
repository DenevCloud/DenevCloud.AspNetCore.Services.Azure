using Microsoft.Azure.Management.Compute;
using Microsoft.Rest;
using System;

namespace DenevCloud.AspNetCore.Services.Azure.VirtualMachines
{
    public class Options
    {
        public string subscription_id { get; set; }
        public string VM_client_id { get; set; }
        public string VM_client_sercet { get; set; }
        public string VM_resource { get; set; }
        public string VM_resource_group { get; set; }

        public ComputeManagementClient computeManagementClient(ServiceClientCredentials ClientCredentials)
        {
            var CMC = new ComputeManagementClient(ClientCredentials);
            CMC.SubscriptionId = subscription_id;
            return CMC;
        }
    }
}
