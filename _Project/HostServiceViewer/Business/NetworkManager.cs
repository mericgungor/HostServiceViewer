using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Business
{
    public class NetworkManager
    {
        public bool Ping(string ip)
        {
            var ping = new Ping();
            var reply = ping.Send(ip, 1000); // 1 minute time out (in ms)
            if (reply.Status == IPStatus.Success)
                return true;
            else
                return false;
        }

        public bool Telnet(string ip, int port)
        {

            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.ReceiveTimeout = 1;
                socket.SendTimeout = 1;

                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                //------------------- 1. Yol -------------------------------

                //try
                //{
                //    socket.Connect(remoteEndPoint);

                //    result = true;
                //}
                //catch
                //{
                //    result = false;
                //}
                //------------------- 2. Yol -------------------------------
                try
                {
                    //socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, false);


                    IAsyncResult sResult = socket.BeginConnect(ip, port, null, null);
                    bool success = sResult.AsyncWaitHandle.WaitOne(1000, true);

                    return socket.Connected;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    if (null != socket)
                        socket.Close();
                }
            }
        }
        public bool Page(string url)
        {

            WebRequest request = WebRequest.Create(url);
            using (WebResponse response = request.GetResponse())
            {
                var statusCode = ((HttpWebResponse)response).StatusCode;
                if (statusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
