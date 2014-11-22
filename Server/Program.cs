using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;
using NetMQ.zmq;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            RunServer();
        }

        static void RunServer()
        {
            using (NetMQContext ctx = NetMQContext.Create())
            {
                using (DealerSocket server = ctx.CreateDealerSocket())
                {
                    server.Bind("tcp://127.0.0.1:5556");

                    string receivedMessage = server.ReceiveString(NetMQ.zmq.SendReceiveOptions.SendMore);
                    string endpoint = server.Options.GetLastEndpoint;
                    Console.WriteLine("Server received message '{0}' from '{1}'", receivedMessage, endpoint);

                    server.Send(string.Format("Zepplins {0}", endpoint));
                }
            }
        }
    }
}
