using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EchoServer
{
    class Program
    {
        private const int PORT = 7777;

        static void Main(string[] args)
        {
            string serverCertificateFile = @"C:\Users\Jan\source\repos\Certificates\ServerSSL.cer";
            bool clientCertificateRequired = false;
            bool checkCertificateRevocation = true;
            SslProtocols enabledSSLProtocols = SslProtocols.Tls;
            X509Certificate serverCertificate = new X509Certificate(serverCertificateFile, "secret");

                TcpListener serverListener = new TcpListener(IPAddress.Loopback, 7777);
                serverListener.Start();
                while (true)
                {
                    var incomingsocket = serverListener.AcceptTcpClient();
                    Stream unsecureStream = incomingsocket.GetStream();
                    bool leaveInnerStreamOpen = false;
                    SslStream sslStream = new SslStream(unsecureStream, leaveInnerStreamOpen);
                    sslStream.AuthenticateAsServer(serverCertificate, clientCertificateRequired, enabledSSLProtocols, checkCertificateRevocation);
                    StreamReader reader = new StreamReader(sslStream);
                    StreamWriter writer = new StreamWriter(sslStream);
                    string incomingMessage = reader.ReadLine();
                    

                    Console.WriteLine("Client: " + incomingMessage);
                    string answer = incomingMessage.ToUpper(); // convert string to upper case
                    writer.WriteLine(answer); // send back upper case string
                }
        }
    }
}
