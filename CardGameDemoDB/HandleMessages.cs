using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CardGameDemoDB
{
    public class HandleMessages
    {
        public void SendMessage(TcpClient client, string message)
        {

            // Get the network stream for the client connection
            NetworkStream stream = client.GetStream();

            // Convert the message to a byte array and send it to the client
            byte[] buffer = Encoding.ASCII.GetBytes(message);
            stream.Write(buffer, 0, buffer.Length);
            Console.WriteLine("Sent message to client: " + message);
        }

        public void SendMessageAndParameter(TcpClient client, string message, string variable)
        {
            // Get the network stream for the client connection
            NetworkStream stream = client.GetStream();

            // Concatenate the two messages into a single string
            string fullMessage = $" {message} {variable}";

            // Convert the message to a byte array and send it to the client
            byte[] buffer = Encoding.ASCII.GetBytes(fullMessage);
            stream.Write(buffer, 0, buffer.Length);

            Console.WriteLine($"Sent message to client: {fullMessage}");
        }

        public void SendMessageAndNumber(TcpClient client, string message, int variable)
        {
            // Get the network stream for the client connection
            NetworkStream stream = client.GetStream();

            // Concatenate the two messages into a single string
            string fullMessage = $" {message} {variable}";

            // Convert the message to a byte array and send it to the client
            byte[] buffer = Encoding.ASCII.GetBytes(fullMessage);
            stream.Write(buffer, 0, buffer.Length);

            Console.WriteLine($"Sent message to client: {fullMessage}");
        }



        public string ReceiveMessage(TcpClient client)
        {
            // Get the network stream for the client connection
            NetworkStream stream = client.GetStream();

            // Create a byte array to hold the incoming data
            byte[] buffer = new byte[1024];
            // Read the incoming data into the buffer
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            //Console.WriteLine("Received " + bytesRead + " bytes from client");

            // Convert the data to a string and return it
            string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received message from client: " + message);
            return message;
        }

        public void SendNumber(TcpClient client, int number)
        {
            NetworkStream stream = client.GetStream();

            // Convert the number to a byte array
            byte[] numberBytes = BitConverter.GetBytes(number);

            // Send the number to the client
            stream.Write(numberBytes, 0, numberBytes.Length);
            Console.WriteLine("Sent number to client: " + number);
        }

        public int ReceiveNumber(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            // Read the bytes from the stream
            byte[] numberBytes = new byte[sizeof(int)];
            stream.Read(numberBytes, 0, sizeof(int));

            // Convert the byte array to an integer
            int number = BitConverter.ToInt32(numberBytes, 0);
            Console.WriteLine("Received number from client: " + number);
            return number;
        }
    }
}
