using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WMSMobileClient.Components;

namespace WMSMobileClient
{
    static class Program
    {
        public static InventoryHeader iInvHeader = new InventoryHeader();
        public static PackingHeader iPackHeader = new PackingHeader();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {

            AppSettings appsettings = new AppSettings();
            //constructor set app default settings to ApplicationSettings static object

            Application.Run(new FrmMenu());
        }
    }
}