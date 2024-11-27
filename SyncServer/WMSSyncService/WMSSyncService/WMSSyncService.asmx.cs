using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Text;

namespace WMSSyncService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://WMSSyncService.intranet.albio.gr/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WMSSyncService : System.Web.Services.WebService
    {
        [WebMethod]
        public string SOA_WebServiceVersion()
        {
            return "v1.1. 11052012";
        }

        [WebMethod]
        public int ConnectionStatus()
        {
            SyncData CurrentInv = new SyncData();
            if (CurrentInv.DBConnectionState() > 0) return 1; else return -1;            
        }

        [WebMethod]
        public SyncInfo SOA_CreateTemporaryData(int StoreID)
        {
            SyncData CurrentInv = new SyncData();
            StringBuilder Rtrn =  new StringBuilder();

            string AffResult = null;
            
            AffResult = CurrentInv.FCreateInvItemsData(StoreID, false);
            Rtrn.Append("Δημιουργία Προσωρινού Πίνακα Ειδών:" + AffResult + " Εγγραφές"); 
            Rtrn.AppendLine();
            AffResult = CurrentInv.FCreateInvLotsData(StoreID, false);
            Rtrn.Append("Δημιουργία Προσωρινού Πίνακα Παρτίδων:" + AffResult + " Εγγραφές");
            Rtrn.AppendLine();
            AffResult = CurrentInv.FCreateInvStatusData(StoreID, false);
            Rtrn.Append("Δημιουργία Προσωρινού Πίνακα Αποθεμάτων:" + AffResult + " Εγγραφές");
            Rtrn.AppendLine();

            CurrentInv.syncinfo.Comments = Rtrn.ToString();
            return CurrentInv.syncinfo;
        }

        [WebMethod]
        public SyncInfo SOA_CreateTemporaryDataExt(int StoreID,bool includezerovalues)
        {
            SyncData CurrentInv = new SyncData();
            StringBuilder Rtrn = new StringBuilder();

            string AffResult = null;


            AffResult = CurrentInv.FCreateInvItemsData(StoreID, includezerovalues);
            Rtrn.Append("Δημιουργία Προσωρινού Πίνακα Ειδών:" + AffResult + " Εγγραφές");
            Rtrn.AppendLine();
            AffResult = CurrentInv.FCreateInvLotsData(StoreID, includezerovalues);
            Rtrn.Append("Δημιουργία Προσωρινού Πίνακα Παρτίδων:" + AffResult + " Εγγραφές");
            Rtrn.AppendLine();
            AffResult = CurrentInv.FCreateInvStatusData(StoreID, includezerovalues);
            Rtrn.Append("Δημιουργία Προσωρινού Πίνακα Αποθεμάτων:" + AffResult + " Εγγραφές");
            Rtrn.AppendLine();

            CurrentInv.syncinfo.Comments = Rtrn.ToString();
            return CurrentInv.syncinfo;
        }

        [WebMethod]
        public List<ERPStore> SOA_GetStores(short CompID)
        {
            SyncData CurrentInv = new SyncData();
            return CurrentInv.FGetStores(CompID);
        }

        [WebMethod]
        public List<ERPMunit> SOA_GetMUnits()
        {
            SyncData CurrentInv = new SyncData();
            return CurrentInv.FGetMunits();
        }

        [WebMethod]
        public List<SyncERPItem> SOA_GetInventoryItems(int StoreID,long StartID,long EndID)
        {
            SyncData CurrentInv = new SyncData();
            return CurrentInv.FGetListItems(StoreID, StartID, EndID);           
        }

        [WebMethod]
        public List<SyncLot> SOA_GetInventoryLots(int StoreID, long StartID, long EndID)
        {
            SyncData CurrentInv = new SyncData();
            return CurrentInv.FGetListLots(StoreID, StartID, EndID);
        }

        [WebMethod]
        public DataSet SOA_GetCurrentInventoryStatusByLot(int StoreID, long StartID, long EndID)
        {
            SyncData CurrentInv = new SyncData();
            return CurrentInv.FgetCurrentInventoryByLot(StoreID, StartID, EndID);
        }

        [WebMethod]
        public long SOA_ClearTemporaryData(int StoreID)
        {
            SyncData CurrentInv = new SyncData();
            return CurrentInv.FClearData(StoreID);
        }

    }
}
