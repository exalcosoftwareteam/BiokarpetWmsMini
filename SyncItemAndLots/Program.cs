using System;
using System.Collections.Generic;
using System.Text;

namespace SyncItemAndLots
{
    class Program
    {
        static void Main(string[] args)
        {
            WMSminiwebservice.WebService wmsservice = new SyncItemAndLots.WMSminiwebservice.WebService();

            try
            {
                wmsservice.AtlSyncItemsAndLots();
            }
            catch { }
            
        }
    }
}
