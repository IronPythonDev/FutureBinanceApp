using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FutureBinanceApp.Web.Utils
{
    public class BotSocketServerUtils
    {
        public static async Task ReloadDataAsync(string url , int port , bool IsReadReturnText)
        {
            byte[] bytes = new byte[1024];

            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(url);
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                Socket sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    await sender.ConnectAsync(remoteEP);

                    byte[] msg = Encoding.ASCII.GetBytes("ReloadData");

                    int bytesSent = sender.Send(msg);

                    if (IsReadReturnText)
                    {
                        int bytesRec = sender.Receive(bytes);
                    }

                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                catch (ArgumentNullException ex)
                {
                    throw ex;
                }
                catch (SocketException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
