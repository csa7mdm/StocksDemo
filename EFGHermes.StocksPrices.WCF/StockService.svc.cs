using EFGHermes.StocksPrices.WCF.Adapters;
using Lightstreamer.DotNet.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EFGHermes.StocksPrices.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class StockService : IStockService
    {
        public List<Stock> GetDataUsingDataContract()
        {
            string host = "localhost";
            int reqrepPort = 6661;
            int notifPort = 6662;

            try
            {
                DataProviderServer server = new DataProviderServer();
                server.Adapter = new StockAdapter();

                TcpClient reqrepSocket = new TcpClient(host, reqrepPort);
                server.RequestStream = reqrepSocket.GetStream();
                server.ReplyStream = reqrepSocket.GetStream();

                TcpClient notifSocket = new TcpClient(host, notifPort);
                server.NotifyStream = notifSocket.GetStream();

                server.Start();
                System.Console.WriteLine("Remote Adapter connected to Lightstreamer Server.");
                System.Console.WriteLine("Ready to publish data...");
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Could not connect to Lightstreamer Server.");
                System.Console.WriteLine("Make sure Lightstreamer Server is started before this Adapter.");
                System.Console.WriteLine(e);
            }

            return new List<Stock>();
        }

       
    }
}
