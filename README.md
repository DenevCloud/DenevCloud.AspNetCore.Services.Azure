# DenevCloud.AspNetCore.Services.Azure

Easy to use and ready-to-go ASP.NET Core services for Azure

## Usage

Startup.cs

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddDenevCloudAzure(Configuration)
                    .UseVirtualMachines(Configuration)
                    .UseKeyVaults(Configuration)
                    .UseAzureDNS(Configuration);
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
    "VMResourceGroup": "VMResourceGroup",
    "KeyVault_clientId": "KeyVault_clientId",
    "KeyVault_clientSecret": "KeyVault_clientSecret",
    "KeyVault_endpoint": "KeyVault_endpoint"
  }
```
Note: In KeyVault_endpoint you should enter only the endpoint name not the whole link (https://my-endpoint.vault.azure.net/ => KeyVault_endpoint : my-endpoint)

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

- ```IVMManager``` => A set of Virtual Machine functions
- ```INetworkInterfaceManager``` => A set of Network Interface functions including getting public and private IPv4 / IPv6 
- ```IKeyVaultManager``` => A set of Key Vault functions

Note: All of the above include Async functionality too

## Coming Soon

- Instead of appsettings.json , an option to use Azure Key Vault will be supported
- Azure Key Vault Manager (further improvement and set of functions)
- Azure Virtual Machines (further improvement and set of functions)
- Network Interfaces (further improvement and set of functions)
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
