﻿using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Xml.Schema;

namespace EchoClient
{
    internal class Client
    {
        private int PORT;

        public Client(int port)
        {
            this.PORT = port;
        }

        public void Start()
        {

            using (TcpClient connectionSocket = new TcpClient("192.168.24.165", PORT))
            using (Stream ns = connectionSocket.GetStream())
            using (StreamReader sr = new StreamReader(ns))
            using (StreamWriter sw = new StreamWriter(ns))
            {
                Console.WriteLine("Client have connected");
                sw.AutoFlush = true; // enable automatic flushing

                // three different clients - run only one of them

                //Client1(sr, sw);        // read 1 line from console and send to server
                Client2(sr, sw);      // read 5 lines and send to server
                //Client3(sr, sw);      // send 100 messages to server

                Console.WriteLine("Client finished");
            }
        }

        private void Client3(StreamReader sr, StreamWriter sw)
        {
            for (int i = 0; i < 100; i++)
            {
                string message = "Michael " + i;
                sw.WriteLine(message);
                string serverAnswer = sr.ReadLine();
                Console.WriteLine("Server: " + serverAnswer);
            }
            
        }

        private void Client2(StreamReader sr, StreamWriter sw)
        {
            
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Type a line");
                string message = Console.ReadLine();
                sw.WriteLine(message);
                string serverAnswer = sr.ReadLine();
                Console.WriteLine("Server: " + serverAnswer);
            }
            
        }

        private void Client1(StreamReader sr, StreamWriter sw)
        {
            // send
            Console.WriteLine("Type a line");
            string message = Console.ReadLine();
            sw.WriteLine(message);

            // receive
            string serverAnswer = sr.ReadLine();
            Console.WriteLine("Server: " + serverAnswer);
            
        }
    }
}