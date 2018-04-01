using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Threading;

using Lightstreamer.Interfaces.Data;
using System.Collections;

namespace EFGHermes.StocksPrices.WCF.Adapters
{
    public class StockAdapter : IDataProvider
    {
        private IItemEventListener _listener;
        private volatile bool go;

        public void Init(IDictionary parameters, string configFile)
        {
        }

        public bool IsSnapshotAvailable(string itemName)
        {
            return false;
        }

        public void SetListener(IItemEventListener eventListener)
        {
            _listener = eventListener;
        }

        public void Subscribe(string itemName)
        {
            if (itemName.Equals("greetings"))
            {
                Thread t = new Thread(new ThreadStart(Run));
                t.Start();
            }
        }

        public void Unsubscribe(string itemName)
        {
            if (itemName.Equals("greetings"))
            {
                go = false;
            }
        }

        public void Run()
        {
            go = true;
            int c = 0;
            Random rand = new Random();

            while (go)
            {
                IDictionary eventData = new Hashtable();
                eventData["message"] = c % 2 == 0 ? "Hello" : "World";
                eventData["timestamp"] = DateTime.Now.ToString("s");
                _listener.Update("greetings", eventData, false);
                c++;
                Thread.Sleep(1000 + rand.Next(2000));
            }
        }
    }
}