using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace udpserver
{
    class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine("server in attesa di connessione");
            UdpClient client = new UdpClient(8080);

            IPEndPoint ipremoto = new IPEndPoint(IPAddress.Any, 8080);

            
            while (true)
            {
                //Ricevo il messaggio dal client
                byte[] receivedbytes = client.Receive(ref ipremoto);

                if (receivedbytes != null)
                {
                    if (Encoding.ASCII.GetString(receivedbytes) == "stop server")
                    {
                        Console.WriteLine("Chiusura del programma!");
                        break;
                    }
                    else
                    {
                        //Traduco il messaggio (in codifica ASCII) e lo visualizzo
                        string messaggio = Encoding.ASCII.GetString(receivedbytes);
                        Console.WriteLine("messaggio ricevuto: " + messaggio);
                        //Invio l'eco al client
                        string inputs = messaggio;
                        byte[] bytesent = Encoding.ASCII.GetBytes(inputs);
                        client.Send(bytesent, bytesent.Length, ipremoto);
                        Console.WriteLine("L'eco è stato inviato");
                    }
                }
                else
                {
                    Console.WriteLine("messaggio ricevuto vuoto");
                }
            }
            //Chiudo
            Console.WriteLine("Premi un tasto qualsiasi per chiudere...");
            Console.ReadKey();
        }
    }
}