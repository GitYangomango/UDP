using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPclient
{
    class program
    {
        static void Main(string[] args)
        {
            UdpClient client = new UdpClient();
            IPEndPoint ipremoto = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            client.Connect(ipremoto);

            Console.Write(">");
            string inputs = Console.ReadLine();
            if (inputs != null)
            {
                //Invio il messaggio al server
                byte[] bytesent = Encoding.ASCII.GetBytes(inputs);
                client.Send(bytesent, bytesent.Length);
                Console.WriteLine("il messaggio è stato inviato");
                //Adesso resto in ascolto dell'eco dal server
                byte[] receivedbytes = client.Receive(ref ipremoto);
                string messaggio = Encoding.ASCII.GetString(receivedbytes);
                Console.WriteLine("eco ricevuto: " + messaggio);
                //Invio "stop server" al Server
                bytesent = Encoding.ASCII.GetBytes("stop server");
                client.Send(bytesent, bytesent.Length);
                Console.WriteLine("Ho inviato 'stop server'");
                //Chiudo
                client.Close();
                Console.WriteLine("Premi un tasto qualsiasi per chiudere...");
                Console.ReadKey();
            }
        }
    }
}