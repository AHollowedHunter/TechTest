using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MudBlazor.Services;
using UserManagement.Blazor.Services;

namespace UserManagement.Blazor;
public class Program
{
    // TODO Ensure that this matches the path used for the launched UserManagement.Web
    const string ApiHost = "https://localhost:7084";

    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
            .AddMudServices()
            .TryAddScoped<IThemeService, DefaultThemeService>();

        builder.Services.AddHttpClient<IUserApiClient, UserApiClient>(client =>
        {
            client.BaseAddress = new($"{ApiHost}");
        });

        builder.Services.AddHttpClient<IUserLogApiClient, UserLogApiClient>(client =>
        {
            client.BaseAddress = new($"{ApiHost}");
        });


        await builder.Build().RunAsync();
    }
}
