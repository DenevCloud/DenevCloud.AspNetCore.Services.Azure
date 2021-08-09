# DenevCloud.AspNetCore.Services.Azure

Easy to use and ready-to-go ASP.NET Core services for Azure

## Usage

Startup.cs

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddDenevCloudAzure(Configuration, UseVirtualMachines : true);
}
```

appsettings.json

```json
"DenevCloud": {
  "tenant_id": "tenant_id",
  "subscriptionId": "subscriptionId",
  "VMclient_id": "VMclient_id",
  "VMclient_sercet": "VMclient_sercet",
  "VMresource": "https://management.core.windows.net/",
  "VMResourceGroup": "VMResourceGroup"
}
```
Index.cshtml

```cshtml
@using DenevCloud.AspNetCore.Services.Azure.VirtualMachines
@using DenevCloud.AspNetCore.Services.Azure.VirtualMachines.NetworkInterfaces

@inject IVMManager vmManager
@inject INetworkInterfaceManager netManager

@{
    ViewData["Title"] = "Home Page";
    var vm = vmManager.GetVirtualMachine("Your_VM_Name");
}

<div class="text-center">
    <h1 class="display-4">Welcome</h1>
    <p>Learn about <a href="https://docs.microsoft.com/aspnet/core">building Web apps with ASP.NET Core</a>.</p>
    <p>Name: @vm.Name</p>
    <p>Location: @vm.Location</p>
    <p>Public Ipv4: @netManager.GetListPublicIpv4(vm).First()</p>
    <p>Public Ipv6: @netManager.GetListPublicIpv6(vm).First()</p>
</div>
```

## List of available functionality

- ```VirtualMachines.GetVirtualMachine(string VMName)``` | Returns a ```Microsoft.Azure.Management.Compute.Models.VirtualMachine``` model
- ```VirtualMachines.Start|Restart|Deallocate|PowerOffVirtualMachine```
- ```VirtualMachines.NetworkInterfaces.GetListPublicIpv4|Ipv6(VirtualMachine virtualMachine, string NicName = null)``` | Returns a ```List<string>``` of public IPv4s or IPv6 associated with the corresponding VM
- ```VirtualMachines.NetworkInterfaces.GetListPrivateIpv4|Ipv6(VirtualMachine virtualMachine, string NicName = null)``` | Returns a ```List<string>``` of private IPv4s or IPv6s associated with the corresponding VM

Note: All of the above include Async functionality too

## Coming Soon

- Instead of appsettings.json , an Azure Key Vault will be supported
- Azure Key Vault Manager
- Azure Analytics Workspace Manager (with special futures for IIS and Azure CDN Logs)
- Azure DNS Manager

## Azure Configuration
- Step 1: Create new App Registration in Azure Active Directory

![Screenshot](https://cdn.denevcloud.com/denevcloud/denevcloud.aspnetcore.services.azure.addappregistration.step1.jpg)
![Screenshot](https://cdn.denevcloud.com/denevcloud/denevcloud.aspnetcore.services.azure.addappregistration.step2.jpg)

- Step 2: Create a new secret for the new App Registration 

![Screenshot](https://cdn.denevcloud.com/denevcloud/denevcloud.aspnetcore.services.azure.addappregistration.step3.jpg)
![Screenshot](https://cdn.denevcloud.com/denevcloud/denevcloud.aspnetcore.services.azure.addappregistration.step4.jpg)

Note: Remember to save Secret and Ids

- Step 3: Assign the App with a role to the appropriete resource that will be accessed using the Access Control (IAM)

![Screenshot](https://cdn.denevcloud.com/denevcloud/denevcloud.aspnetcore.services.azure.addappregistration.step4.jpg)

Note: Please consult with your team if this follows your company's security guideness. Also for the exact Role that has to be assigned or any other details about Azure contact their support
