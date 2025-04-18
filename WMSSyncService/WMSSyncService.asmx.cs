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
   // [WebService(Namespace = "WMSSyncService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WMSSyncService : System.Web.Services.WebService
    {
        static List<SyncERPItem>[] GItemList = new List<SyncERPItem>[110];
        static List<SyncLot>[] GLotList = new List<SyncLot>[110];
        SyncData syncdata = new SyncData();
        CarpetCleaningHandler carpethandler = new CarpetCleaningHandler();


        [WebMethod]
        public void SOA_TestLogFile()
        {
            Log mylog = new Log();
            mylog.WriteToLog("TEST");
        }
        
        [WebMethod]
        public string SOA_WebServiceVersion()
        {
            return "v1.1. 11052012";
        }

        [WebMethod]
        public List<TOrderDetails> OrderDetailsList(long dsrnumber, short compid, short branchid)
        {
            Orders orderdtl = new Orders();
            return orderdtl.OrderDetails(dsrnumber, compid, branchid);
        }

        [WebMethod]
        public int ConnectionStatus()
        {
            SyncData CurrentInv = new SyncData();
            if (CurrentInv.DBConnectionState() > 0) return 1; else return -1;            
        }


        [WebMethod]
        public TransCodeHeader GetTransCodeHeader(long ftrid)
        {
            ReceivesController rcontroller = new ReceivesController();
            return rcontroller.GetTransCodeHeader(ftrid);
            
        }

        [WebMethod]
        public List<TransCodeDetail> GetTransCodeDetails(long ftrid)
        {
            ReceivesController rcontroller = new ReceivesController();
            return rcontroller.GetTransDetails(ftrid);
            
        }
               


        [WebMethod]
        public TransCodeDetail Test()
        {
            TransCodeDetail a = new TransCodeDetail();
            return a;
        }

 

        [WebMethod]
        public void AwakeDB()
        {
            OraDB db = new OraDB();
            db.DBExecuteSQLCmd("SELECT 1 FROM DUAL");
        }

        [WebMethod]
        public long getclasses(SyncERPItem serpitem) { 
        
        return 1;
        }

       
        [WebMethod]
        public SyncInfo SOA_CreateItemData(int StoreID, bool getzeroqtys, string lastsyncdate)
        {
            SyncData ItemList = new SyncData();
            return ItemList.FCreateInvItemsData(StoreID, getzeroqtys, lastsyncdate);
       
        }

        [WebMethod]
        public SyncInfo SOA_CreateLotData(int StoreID, bool getzeroqtys, string lastsyncdate)
        {
            SyncData ItemList = new SyncData();
            return ItemList.FCreateInvLotsData(StoreID, getzeroqtys, lastsyncdate);
            
        }

        [WebMethod]
        public List<SyncERPItem> SOA_GetNewItems(int StoreID, string lastsyncdate)
        {
            SyncData ItemList = new SyncData();
            return ItemList.FGetListItems(StoreID, lastsyncdate);
        }

        [WebMethod]
        public List<SyncLot> SOA_GetNewLots(int StoreID,string lastsyncdate)
        {
            SyncData CurrentInv = new SyncData();
            return CurrentInv.FGetListLots(StoreID, lastsyncdate);
        }

        [WebMethod]
        public long TestERPcon()
        {
            PackingListHeaderHandler packhandler = new PackingListHeaderHandler();
            return packhandler.TestErpCon();
        }

        [WebMethod]
        public long SOA_ClearTemporaryData(int StoreID)
        {
            SyncData CurrentInv = new SyncData();
            return CurrentInv.FClearData(StoreID);
        }

        [WebMethod]
        public long ImportPackingListCType(PackingListDetail[] pack)
        {
            PackingListHandler packhandler = new PackingListHandler();
            long affectrows = 0;
            long PackHdrServer = 0;
            OraDB db = new OraDB();
            try
            {

                PackingListDetail lpack;
                lpack = new PackingListDetail();
                PackHdrServer = pack[0].PackingListHeaderID;

                for (int i = 0; i < pack.Length; i++)
                {

                    lpack.PackingListHeaderID = pack[i].PackingListHeaderID;
                    lpack.ItemCode = pack[i].ItemCode;
                    lpack.PackingListDTLID = pack[i].PackingListDTLID;
                    lpack.ItemQTYprimary = pack[i].ItemQTYprimary;
                    lpack.ItemID = pack[i].ItemID;
                    lpack.LotID = pack[i].LotID;
                    lpack.LotCode = pack[i].LotCode;
                    lpack.ItemQTYsecondary = pack[i].ItemQTYsecondary;
                    lpack.Color = pack[i].Color;
                    lpack.Draft = pack[i].Draft;
                    lpack.Width = pack[i].Width;
                    lpack.Length = pack[i].Length;

                    affectrows += packhandler.InsertRecord(lpack);
                }
            }
            catch{ }
            return affectrows;
        }

        [WebMethod]
        public string CreateTradeCodeOnTheFly(PackingListHeader packheader, PackingListDetail[] pack, int compid, int branchid, int storeid, int dsrid, int fpa)
        {
            long erpcustomerid = 0;
            PackingListHandler packhandler = new PackingListHandler();
            long dsrNumber = 0;
            long ftrid = 0;
            string customername = "";
            string tradeCode;
            string sqlstr;
            long erpitemid = 0;
            long strid = 0;
            decimal primaryqty = 0;
            OraDB db = new OraDB();

                try
                {
                    erpcustomerid = db.DBGetNumResultFromSQLSelect("SELECT ID FROM CUSTOMER WHERE CODE ='" + packheader.CustomerCode + "'");
                    if (erpcustomerid <= 0)
                    {
                        return "CustomerID is invalid";
                    }
                }
                catch { }

                try
                {
                    customername = db.DBWmsExSelectCmdRStr2Str("SELECT NAME FROM CUSTOMER WHERE ID =" + erpcustomerid.ToString());
                }
                catch { }

                //
                dsrNumber = db.DBGetNumResultFromSQLSelect("select max(dsrNumber)+1 from fintrade");

                //FINTRATE TABLE

                tradeCode = dsrNumber.ToString() + " " + customername;

                if (tradeCode.Length > 30)
                {
                    tradeCode = tradeCode.Substring(0, 29);
                }
                ftrid = db.DBGetNumResultFromSQLSelect("select max(ID)+1 from fintrade");

                sqlstr = "insert into fintrade";
                sqlstr += "(ID,Approved,comid,braid,source,dsrid,dsrnumber,fdtid,ftrdate,curid,cusid,fyeid,domaintype,tradecode,localrate,traderrate,approved)";
                sqlstr += "values ("+ftrid.ToString()+",-1,"+ compid.ToString() + "," + branchid.ToString() + ",5," + dsrid.ToString() + "," + dsrNumber.ToString() + "," + dsrid.ToString() + ",TO_DATE(SYSDATE),1," + erpcustomerid.ToString() + ",to_char(sysdate, 'YYYY'),2,'" + tradeCode + "',1,1,1) ";

                try
                {
                    db.DBExecuteSQLCmd(sqlstr);
                    db.DBExecuteSQLCmd("commit");

                }
                catch { }

                // GET FINTRADE ID

                try
                {
                    ftrid = db.DBGetNumResultFromSQLSelect("select id from fintrade where dsrid=" + dsrid.ToString() + " and dsrnumber =  " + dsrNumber.ToString());

                }
                catch { }
                sqlstr = "";

                sqlstr = "insert into storetrade";
                sqlstr += "(ftrid,comid,cntid,vatstatus,stoid)";
                sqlstr += "values (" + ftrid.ToString() + "," + compid.ToString() + ",null,0," + storeid.ToString() + ")";


                try
                {
                    db.DBExecuteSQLCmd(sqlstr);
                    db.DBExecuteSQLCmd("commit");
                }
                catch { }

                //itemCount = dsi
                //tempItem = erpitemid


                for (int i = 0; i < pack.Length; i++)
                {
                    //ONLY THESE FIELDS ARE NEEDED FOR ALTANTIS ERP
                    erpitemid = db.DBGetNumResultFromSQLSelect("SELECT id FROM material WHERE code ='" + pack[i].ItemCode + "'");
                    primaryqty = pack[i].ItemQTYprimary;
                    strid = db.DBGetNumResultFromSQLSelect("select max(ID)+1 from storetradelines");

                    sqlstr = "insert into storetradelines";
                    sqlstr += "(ID,ftrid,comid,stoid,linenum,iteid,source,PRIMARYQTY,VTCID)";
                    sqlstr += " values ("+strid.ToString()+"," + ftrid.ToString() + "," + compid.ToString() + "," + storeid.ToString() + "," + (i + 1).ToString() + "," + erpitemid.ToString() + ",5,";

                    if (pack[i].ItemQTYprimary == 0)
                    {
                        sqlstr += "NULL,";
                    }
                    else
                    {
                        sqlstr += pack[i].ItemQTYprimary.ToString() + ",";

                    }

                    //VTCID (FPA)
                    sqlstr += fpa.ToString();

                    sqlstr += ")";
                    try
                    {
                        db.DBExecuteSQLCmd(sqlstr);
                        db.DBExecuteSQLCmd("commit");
                    }
                    catch { }
                    
                
                }
                return "1";

 
            }

        [WebMethod]
        public long ImportPackingListHeaderCType(PackingListHeader packhdr)
        {
            PackingListHeaderHandler packhandler = new PackingListHeaderHandler();
            return packhandler.InsertRecord(packhdr);
        }

        [WebMethod]
        public SyncInfo SOA_CreateTemporaryData(int StoreID)
        {
            SyncData CurrentInv = new SyncData();
            StringBuilder Rtrn = new StringBuilder();

            string AffResult = null;

            AffResult = CurrentInv.FCreateInvItemsData (StoreID, false);
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
        public long GetERPOrderID(string dsrnumber)
        {
            OrderHandler ordhandler = new OrderHandler();
            return ordhandler.GetErpOrderID(dsrnumber);

        }

        [WebMethod]
        public List<ERPMunit> SOA_GetMUnits()
        {
            SyncData CurrentInv = new SyncData();
            return CurrentInv.FGetMunits();
        }

        [WebMethod]
        public List<ERPStore> SOA_GetStores(short CompID)
        {
            SyncData CurrentInv = new SyncData();
            return CurrentInv.FGetStores(CompID);
        }


        [WebMethod]
        public SyncERPItem SOA_GetNewItem(long itemid) 
        {
         

            return syncdata.FGetItem(itemid);
        
        }

        [WebMethod]
        public SyncLot SOA_GetNewLot(long lotid)
        {
            return syncdata.FGetLot(lotid);

        }

        [WebMethod]
        public SyncLot SOA_GetNewLotbyCode(string lotcode)
        {
            return syncdata.FGetLotbyCode(lotcode);

        }

        [WebMethod]
        public SyncLot SOA_GetLotbyCodeInventory(string lotcode)
        {
            return syncdata.FGetLotbyCodeInventory(lotcode);

        }
        
        [WebMethod]
        public List<SyncLot> SOA_GetInventoryLots(int StoreID, long StartID, long EndID)
        {
            SyncData CurrentInv = new SyncData();
            return CurrentInv.FGetListLots(StoreID, StartID, EndID);
        }

        [WebMethod]
        public List<SyncERPItem> SOA_GetInventoryItems(int StoreID, long StartID, long EndID)
        {
            SyncData CurrentInv = new SyncData();
            return CurrentInv.FGetListItems(StoreID, StartID, EndID);
        }

        [WebMethod]
        public List<SyncERPItem> SOA_GetItemsList(int storeid, int branchid, long startindex, long endindex)
        {
            
            List<SyncERPItem> partiallist = new List<SyncERPItem>();

            long range = 0;

            if (endindex > 0 && startindex > -1) range = endindex - startindex;
            if (GItemList[branchid] == null || GItemList[branchid].Count ==0)
            {
                GItemList[branchid] = syncdata.FGetListItems(storeid, 0, 0);
            }

            if (GItemList[branchid].Count > 0)
            {
                if (startindex > -1 && range > 0)
                {
                    if ((GItemList[branchid].Count - 1) >= startindex + range)
                        partiallist = GItemList[branchid].GetRange(Int32.Parse(startindex.ToString()), Int32.Parse(range.ToString()));
                    else if ((GItemList[branchid].Count - 1) >= startindex)
                        partiallist = GItemList[branchid].GetRange(Int32.Parse(startindex.ToString()), Int32.Parse(GItemList[branchid].Count.ToString()) - Int32.Parse(startindex.ToString()));
                }
                else
                {
                    return GItemList[branchid];

                }
            }

            return partiallist;
        }

        [WebMethod]
        public List<SyncLot> SOA_GetLotsList(int storeid, int branchid, long startindex, long endindex)
        {
            List<SyncLot> partiallist = new List<SyncLot>();

            long range = 0;

            if (endindex > 0 && startindex > -1) range = endindex - startindex;
            if (GLotList[branchid] == null || GLotList[branchid].Count == 0)
            {
                GLotList[branchid] = syncdata.FGetListLots(storeid, 0, 0);
            }

            if (GLotList[branchid].Count > 0)
            {
                if (startindex > -1 && range > 0)
                {
                    if ((GLotList[branchid].Count - 1) >= startindex + range)
                        partiallist = GLotList[branchid].GetRange(Int32.Parse(startindex.ToString()), Int32.Parse(range.ToString()));
                    else if ((GLotList[branchid].Count - 1) >= startindex)
                        partiallist = GLotList[branchid].GetRange(Int32.Parse(startindex.ToString()), Int32.Parse(GLotList[branchid].Count.ToString()) - Int32.Parse(startindex.ToString()));
                }
                else
                {
                    return GLotList[branchid];
                }
            }

            return partiallist;
        }

        [WebMethod]
        public long ClearGItemList(int branchid) 
        {
            GItemList[branchid].Clear();
            return 1;

        }

        [WebMethod]
        public long ClearGLotList(int branchid)
        {
            GLotList[branchid].Clear();
            return 1;

        }

        //FOR CARPETCLEANING

        [WebMethod]
        public long CarpetCleaningTrans(CarpetTrans[] storetranslist)
        {
            long affectrows = 0;
            OraDB db = new OraDB();
            try
            {
                CarpetTrans trans;
                trans = new CarpetTrans();

                for (int i = 0; i < storetranslist.Length; i++)
                {


                    trans.TRANSID = storetranslist[i].TRANSID;
                    trans.ERPCompID = storetranslist[i].ERPCompID;
                    trans.ERPBRACHID = storetranslist[i].ERPBRACHID;
                    trans.AlpisStoreTransID = storetranslist[i].AlpisStoreTransID;
                    trans.WMSTRANSID = storetranslist[i].WMSTRANSID;
                    trans.AlpisStoreTransDate = storetranslist[i].AlpisStoreTransDate;
                    trans.DispatchDate = storetranslist[i].DispatchDate;
                    trans.ERPTransSeriesID = storetranslist[i].ERPTransSeriesID;
                    trans.ERPCustomerID = storetranslist[i].ERPCustomerID;
                    trans.ERPSupplierID = storetranslist[i].ERPSupplierID;
                    trans.ERPFromStoreID = storetranslist[i].ERPFromStoreID;
                    trans.ERPToStoreID = storetranslist[i].ERPToStoreID;
                    trans.ERPItemID = storetranslist[i].ERPItemID;
                    trans.ERPItemMUnit = storetranslist[i].ERPItemMUnit;
                    trans.ERPItemSMUnit = storetranslist[i].ERPItemSMUnit;
                    trans.ERPItemQty = storetranslist[i].ERPItemQty;
                    trans.ERPITEMSQTY = storetranslist[i].ERPITEMSQTY;
                    trans.ERPItemUnitPrice = storetranslist[i].ERPItemUnitPrice;
                    trans.ERPItemQtyPrice = storetranslist[i].ERPItemQtyPrice;
                    trans.EXTRASERVICE = storetranslist[i].EXTRASERVICE;
                    trans.ERPTRANSCODE = storetranslist[i].ERPTRANSCODE;
                    trans.DOCTYPE = storetranslist[i].DOCTYPE;
                    trans.isnew = storetranslist[i].isnew;
                    affectrows += carpethandler.InsertRecord(trans);
                }
            }
            catch{ }
            return affectrows;
        }
        [WebMethod]
        public Customer GetCustomerbycode(string customercode)
        {
            CustomerHandler cushandler = new CustomerHandler();
            return cushandler.GetCustomerbycode(customercode);

        }


        [WebMethod]
        public long CarpetCleaningCancelTrans(long wmstransid)
        {
            if (wmstransid > 0)
            {
                carpethandler.CancelErpSend(wmstransid);

                return 1;
            }
            else 
            {
                return -1;
            }

        }
 

    }
}
