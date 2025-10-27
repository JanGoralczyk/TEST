using System;
using System.Threading.Tasks;

namespace KSEFClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            System.Console.WriteLine("KSEF 2.0 Client - Console Demo");
            System.Console.WriteLine("==============================");
            System.Console.WriteLine();

            var ksefService = new KSEFService();

            try
            {
                System.Console.WriteLine("This is a demonstration of KSEF 2.0 integration.");
                System.Console.WriteLine("Note: Actual KSEF authentication requires proper credentials and may need certificates.");
                System.Console.WriteLine();

                // Demo credentials (these would be real in actual implementation)
                System.Console.Write("Enter identifier (or press Enter for demo): ");
                var identifier = System.Console.ReadLine();
                if (string.IsNullOrWhiteSpace(identifier))
                    identifier = "demo_user";

                System.Console.Write("Enter password (or press Enter for demo): ");
                var password = ReadPassword();
                if (string.IsNullOrWhiteSpace(password))
                    password = "demo_password";

                System.Console.WriteLine();
                System.Console.WriteLine("Attempting to connect to KSEF 2.0...");

                var success = await ksefService.InitializeSessionAsync(identifier, password);
                
                if (success)
                {
                    System.Console.WriteLine("✓ Successfully established session with KSEF 2.0!");
                    
                    System.Console.WriteLine("Checking session status...");
                    var status = await ksefService.GetSessionStatusAsync();
                    System.Console.WriteLine($"Status: {status}");

                    System.Console.WriteLine("Terminating session...");
                    var terminated = await ksefService.TerminateSessionAsync();
                    System.Console.WriteLine(terminated ? "✓ Session terminated successfully" : "✗ Failed to terminate session");
                }
                else
                {
                    System.Console.WriteLine("✗ Failed to establish session with KSEF 2.0");
                    System.Console.WriteLine("This is expected in demo mode - actual KSEF endpoints require proper authentication");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Error: {ex.Message}");
                System.Console.WriteLine("This is expected in demo mode - the KSEF service structure is ready for real implementation");
            }
            finally
            {
                ksefService.Dispose();
            }

            System.Console.WriteLine();
            System.Console.WriteLine("Press any key to exit...");
            System.Console.ReadKey();
        }

        private static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = System.Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    System.Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    System.Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);
            
            return password;
        }
    }
}