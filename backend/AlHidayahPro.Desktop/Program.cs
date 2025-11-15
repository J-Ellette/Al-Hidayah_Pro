using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace AlHidayahPro.Desktop;

/// <summary>
/// Desktop host application for Al-Hidayah Pro
/// This will launch the backend API and open a browser window
/// </summary>
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting Al-Hidayah Pro Desktop Application...");

        var host = Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            })
            .Build();

        // Start the API in a background task
        var apiTask = Task.Run(() => StartApiServer());

        // Wait for API to start
        await Task.Delay(2000);

        // Open the frontend in default browser
        OpenFrontend();

        Console.WriteLine("Al-Hidayah Pro is running!");
        Console.WriteLine("Press Ctrl+C to exit.");

        await host.RunAsync();
    }

    static void StartApiServer()
    {
        try
        {
            Console.WriteLine("Starting API server on http://localhost:5000...");
            
            // This would start the actual API server
            // For now, we'll just indicate it's ready
            var apiProcess = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "run --project ../AlHidayahPro.Api/AlHidayahPro.Api.csproj --urls http://localhost:5000",
                UseShellExecute = false,
                CreateNoWindow = false
            };
            
            Process.Start(apiProcess);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error starting API server: {ex.Message}");
        }
    }

    static void OpenFrontend()
    {
        try
        {
            var frontendUrl = "http://localhost:5173"; // Vite dev server default port
            
            Console.WriteLine($"Opening frontend at {frontendUrl}...");
            
            // Open browser based on OS
            if (OperatingSystem.IsWindows())
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = frontendUrl,
                    UseShellExecute = true
                });
            }
            else if (OperatingSystem.IsLinux())
            {
                Process.Start("xdg-open", frontendUrl);
            }
            else if (OperatingSystem.IsMacOS())
            {
                Process.Start("open", frontendUrl);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not open browser automatically: {ex.Message}");
            Console.WriteLine("Please open http://localhost:5173 in your browser manually.");
        }
    }
}
