using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using FutureBinanceApp.Domain.Entities;

namespace Bot.SocketAPI
{
    class SocketAPI
    {
        public string data = null;
        public string url = String.Empty;

        public delegate void ReloadDataHandler(List<AccountModel> accounts);
        public delegate void EOFHandler();

        public ReloadDataHandler ReloadData;
        public EOFHandler EOF;
        public SocketAPI(string url = "localhost")
        {
            this.url = url;
        }

        public void StartListening()
        {
            byte[] bytes = new Byte[1024];


            IPHostEntry ipHostInfo = Dns.GetHostEntry(url);
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 56341);

            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(5);

                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");

                    Socket handler = listener.Accept();
                    data = null;

                    while (true)
                    {
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);

                        if (data.IndexOf("<EOF>") > -1)
                        {
                            EOF?.Invoke();
                            break;
                        }
                        else if (data.IndexOf("ReloadData") > -1)
                        {
                            var accounts = Utils.AccountData.Reload();
                            ReloadData?.Invoke(accounts);
                            handler.Send(Encoding.ASCII.GetBytes(@"OK RELOAD"));
                            break;
                        }
                        else if (data.IndexOf("ping") > -1)
                        {
                            handler.Send(Encoding.ASCII.GetBytes(@"pong"));
                            break;
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
