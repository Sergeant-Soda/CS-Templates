using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Define the port to listen on
        int port = 4444;

        // Create a TCP listener
        TcpListener listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        Console.WriteLine($"Listening on port {port}...");

        while (true)
        {
            try
            {
                // Accept incoming client connections asynchronously
                TcpClient client = await listener.AcceptTcpClientAsync();
                Console.WriteLine("Client connected.");

                // Handle the client connection asynchronously
                _ = HandleClientAsync(client);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    static async Task HandleClientAsync(TcpClient client)
    {
        {
            try
            {
                using (NetworkStream stream = client.GetStream())
                {
                    WriteStream(stream, "\n*** Welcome! ***\n ");
                  
                    while (true)
                    {
                        // Ready to accept user input
                        WriteStream(stream, "\n>");

                        // Read the client's command asynchronously
                        cmd = ReadStream(stream);

                        // Initiate cmd
                        if (cmd == "help".ToLower() || cmd == "?")
                        {
                            WriteStream(stream, "\nUsage: ");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling client: {ex.Message}");
            }
        }
    }

    public static void WriteStream(NetworkStream stream, string data) // Function used to write data to the current stream
    {
        byte[] serverResponse = Encoding.UTF8.GetBytes(data);
        stream.WriteAsync(serverResponse, 0, serverResponse.Length);
    }

    public static string ReadStream(NetworkStream stream) // Function used to read user input
    {
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string userInput = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();

        return userInput;
    }

}
