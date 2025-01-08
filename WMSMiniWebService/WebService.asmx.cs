using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using WMSMiniWebService.WMSsyncService;
using System.IO;
using System.Text;
using AtlantisInventorySync;
using System.Web.Services.Protocols;


namespace WMSMiniWebService
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    //[WebService(Namespace = "WMSMiniWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]

    public class WebService : System.Web.Services.WebService
    {

        static List<ERPItem> GItemList { get; set; }
        static List<ERPLot> GLotList { get; set; }

        InventoryHandler invhandler = new InventoryHandler();

        [WebMethod]
        public string CheckDBConnection()
        {
            DBGeneralOperations dboperations = new DBGeneralOperations();
            return dboperations.CheckDBConnection();

        }

        [WebMethod]
        public TItems ItemInfo(long itemid)
        {
            Items itemhandler = new Items();
            return itemhandler.ItemInstance(itemid);
        }

        [WebMethod]
        public DataTable GetOpenReceivesList(short branchid)
        {
            ReceivesController receives = new ReceivesController();
            DataTable dt = new DataTable();
            try { dt = receives.GetTransList(branchid); }
            catch { }
            
            return dt;
        }

        [WebMethod]
        public long InsertNewReceivesTranscode(TransCodeHeader thistranscode)
        {
            ReceivesController receives = new ReceivesController();
           // WriteToLog(" InsertNewReceivesTranscode hitted t = " + thistranscode.tradecode); 
            return receives.InsertNewTranscode(thistranscode);

        }


        [WebMethod]
        public TItems ItemInfoByCode(string itemcode)
        {
            Items itemhandler = new Items();
            TItems titem = new TItems();
            titem = itemhandler.ItemInstanceByCode(itemcode);
            return titem;
        }

        [WebMethod]
        public TItemLot LotInfo(long lotid)
        {
            ItemLots lothandler = new ItemLots();
            return lothandler.ItemLotInstance(lotid);
        }

        [WebMethod]
        [SoapDocumentMethod(OneWay = true)] 
        public void AtlSyncItemsAndLots()
        {
            AtlantisInventoryController atlcontroller = new AtlantisInventoryController();
            atlcontroller.SyncItemsAndLots();
        }

        [WebMethod]
        [SoapDocumentMethod(OneWay = true)]
        public void AtlDetailsQtyFromWmsMiniDB(short branchid,short storeid,short comid)
        {
            AtlantisInventoryController atlcontroller = new AtlantisInventoryController();
            atlcontroller.GetDetailsQtyFromWmsMiniDB(branchid, storeid , comid);
        }

        [WebMethod]
        [SoapDocumentMethod(OneWay = true)]
        public void TESTGetDetailsQtyFromWmsMiniDB(short branchid, short storeid, short comid)
        {
            AtlantisInventoryController atlcontroller = new AtlantisInventoryController();
            atlcontroller.TESTGetDetailsQtyFromWmsMiniDB(branchid, storeid , comid);
        }


        [WebMethod]
        [SoapDocumentMethod(OneWay = true)]
        public void AtlGeInventoryNow(int branchid, bool forcedelete)
        {
            AtlantisInventoryController atlcontroller = new AtlantisInventoryController();
            atlcontroller.GetDetailsQtyNow(branchid, forcedelete);
        }


        [WebMethod]
        public long AtlGeInventory(int branchid,bool forcedelete)
        {
            AtlantisInventoryController atlcontroller = new AtlantisInventoryController();
            return atlcontroller.GetDetailsQty(branchid, forcedelete);
        }

        [WebMethod]
        public TItemLot LotInfoByCode(string lotcode, long itemid)
        {
            ItemLots lothandler = new ItemLots();
            return lothandler.ItemLotInstanceByCode(lotcode, itemid);
        }

        [WebMethod]
        public DataSet GetItemPartial(long startid, long endid)
        {
            Items itemhandler = new Items();

            return itemhandler.GetItemPartial(startid, endid);
        }

        [WebMethod]
        public DataSet GetItemLotPartial(long startid, long endid)
        {
            ItemLots lothandler = new ItemLots();

            return lothandler.GetItemLotPartial(startid, endid);
        }

        [WebMethod]
        public long SyncERPItems()
        {
            Items itemhandler = new Items();
            return itemhandler.SyncERPItems();
        }

        //[WebMethod]
        //public long ImportInventoryHeader(DataSet ds)
        //{
        //    WMSInvOffline InvOfflineHandler = new WMSInvOffline();
        //    return InvOfflineHandler.UploadInventoryHeader(ds);
        //}


        [WebMethod]
        public long ImportInventoryHeaderCType(TInventoryHeader invhdr)
        {
            InventoryHeader invhdrhandler = new InventoryHeader();
            return invhdrhandler.UpdateInventoryHeader(invhdr);
          
        }

        [WebMethod]
        public long ImportInventory(DataSet ds)
        {
            WMSInvOffline InvOfflineHandler = new WMSInvOffline();
            return InvOfflineHandler.UploadInventory(ds);
        }

        [WebMethod]
        public long ItemExists(long itemid)
        {
            DB db = new DB();
            long fitemid = 0;
            fitemid = db.DBFastGetNumResultFromSQLSelect("SELECT ITEMID FROM TITEMS WHERE ITEMID = " + itemid.ToString());
            if (fitemid > 0)
            {
                return 1;
            }
            else {

                return -1;
            }

        }

        [WebMethod]
        public long LotExists(long lotid)
        {
            DB db = new DB();
            long flotid = 0;
            flotid = db.DBFastGetNumResultFromSQLSelect("SELECT LOTID FROM TITEMLOT WHERE LOTID = " + lotid.ToString());
            if (flotid > 0)
            {
                return 1;
            }
            else return -1;
        }



        [WebMethod]
        public InventoryInfo GetInventoryInfo(long invHeaderId)
        {
            DB db = new DB();
            StringBuilder sqlstr = new StringBuilder();
            InventoryInfo i = new InventoryInfo();


            IDataReader dr = db.DBReturnDatareaderResults("select * from VINVENTORYHEADERSTATS WHERE INVHDRID = " + invHeaderId.ToString());


            while(dr.Read()){
                i.InvCount = DbUtils.INTdr(dr["invRecords"]);
                i.LastBarcode =  DbUtils.STRINGdr(dr["lastbarcode"]);
                i.LastInvdate = DbUtils.DATEdr(dr["lastinvdate"]);
            }
            dr.Close();
            dr.Dispose();

            db.DBDisconnect();

            return i;
        }



        [WebMethod]
        public long InsertNewItem(ERPItem newitem)
        {
            Items itemfunctions = new Items();
           return itemfunctions.FInsertItem(newitem);

        }

        [WebMethod]
        public long InsertNewLot(ERPLot newlot)
        {
            ItemLots lotfunctions = new ItemLots();
            return lotfunctions.FInsertLot(newlot);

        }



        [WebMethod]
        public long ImportInventoryOnline(TInventory inv)
        {   
            long affectrows = 0;
            try
            {
             affectrows += invhandler.UpdateInventory(inv);
            }
            catch (Exception ex)
            {
                invhandler.db.f_sqlerrorlog(1, "ImportInventoryOline>>inv.lotcode=" + inv.LotCode.ToString() + "itemcode:" + inv.ItemCode, ex.ToString());
            }
            return affectrows;
        }


        [WebMethod]
        public long ImportInventoryCTypeList(List<TInventory> inv, bool clearprevious)
        {
            DB db = new DB();
            long affectrows = 0;
            long InvHdrIDServer = 0;

            try
            {

                TInventory linv;
                linv = new TInventory();
                InvHdrIDServer = inv[0].InvHdrIDServer;
                //CLEAR INVENTORY BEFORE SYNC ,IF A ROW IS DELETED IN MOB IT DELETES HERE TOO
                if (clearprevious)
                {
                    try
                    {
                        db.DBExecuteSQLCmd("DELETE FROM twmsinventory where InvHdrID = " + InvHdrIDServer.ToString());
                    }
                    catch { }
                }
                //

                for (int i = 0; i < inv.Count; i++)
                {
                    linv.BranchID = inv[i].BranchID;
                    linv.CompID = inv[i].CompID;
                    linv.InvDate = inv[i].InvDate;
                    linv.InvHdrID = inv[i].InvHdrID;
                    linv.InvHdrIDServer = inv[i].InvHdrIDServer;
                    linv.InvID = inv[i].InvID;
                    linv.InvQty = inv[i].InvQty;
                    linv.InvQtySecondary = inv[i].InvQtySecondary;
                    linv.ItemCode = inv[i].ItemCode;
                    linv.LotCode = inv[i].LotCode;
                    linv.ItemID = inv[i].ItemID;
                    linv.LotID = inv[i].LotID;
                    linv.MUnitPrimary = inv[i].MUnitPrimary;
                    linv.MUnitSecondary = inv[i].MUnitSecondary;
                    linv.StoreID = inv[i].StoreID;
                    linv.MobInvID = inv[i].InvID;

                    //if (ItemExists(linv.ItemID) < 0)
                    //{
                    //    if (linv.ItemID > 0)
                    //    {
                    //        AtlService.GetItemFromERP(linv.ItemID);
                    //    }
                    //}

                    //if (LotExists(linv.LotID) < 0)
                    //{

                    //    if (linv.LotID > 0)
                    //    {
                    //        AtlService.GetLotFromERP(linv.LotID);
                    //    }
                    //}



                    affectrows += invhandler.UpdateInventory(linv);
                }
                db.DBDisconnect();
            }
            catch (Exception ex) { invhandler.db.f_sqlerrorlog(1, "SOA_ImportInventoryCType>>inv.length=" + inv.Count.ToString() + "itemcode:" + inv[0].ItemCode, ex.ToString()); }
            return affectrows;
        }

        [WebMethod]
        public long ImportInventoryCType(TInventory[] inv,bool clearprevious)
        {
            DB db = new DB();
            long affectrows = 0;
            long InvHdrIDServer = 0;

            try
            { 

                TInventory linv;
                linv = new TInventory();
                InvHdrIDServer = inv[0].InvHdrIDServer;
                //CLEAR INVENTORY BEFORE SYNC ,IF A ROW IS DELETED IN MOB IT DELETES HERE TOO
                if (clearprevious)
                {
                    try
                    {
                        db.DBExecuteSQLCmd("DELETE FROM twmsinventory where InvHdrID = " + InvHdrIDServer.ToString());
                    }
                    catch { }
                }
                //

                for (int i = 0; i < inv.Length; i++)
                {
                    linv.BranchID = inv[i].BranchID;
                    linv.CompID = inv[i].CompID;
                    linv.InvDate = inv[i].InvDate;
                    linv.InvHdrID = inv[i].InvHdrID;
                    linv.InvHdrIDServer = inv[i].InvHdrIDServer;
                    linv.InvID = inv[i].InvID;
                    linv.InvQty = inv[i].InvQty;
                    linv.InvQtySecondary = inv[i].InvQtySecondary;
                    linv.ItemCode = inv[i].ItemCode;
                    linv.LotCode = inv[i].LotCode;
                    linv.ItemID = inv[i].ItemID;
                    linv.LotID = inv[i].LotID;
                    linv.MUnitPrimary = inv[i].MUnitPrimary;
                    linv.MUnitSecondary = inv[i].MUnitSecondary;
                    linv.StoreID = inv[i].StoreID;
                    linv.MobInvID = inv[i].InvID;


                    //COMMENTED 03/01/2018 items and lots syncs in agent process

                    //if (ItemExists(linv.ItemID) < 0)
                    //{
                    //    if (linv.ItemID > 0)
                    //    {
                    //        AtlService.GetItemFromERP(linv.ItemID);
                    //    }
                    //}

                    //if (LotExists(linv.LotID) < 0)
                    //{

                    //    if (linv.LotID > 0)
                    //    {
                    //        AtlService.GetLotFromERP(linv.LotID);
                    //    }
                    //}

                    

                    affectrows += invhandler.UpdateInventory(linv);
                }
                db.DBDisconnect();
            }
            catch (Exception ex) { invhandler.db.f_sqlerrorlog(1, "SOA_ImportInventoryCType>>inv.length=" + inv.Length.ToString() + "itemcode:" + inv[0].ItemCode, ex.ToString()); }
            return affectrows;
        }

        [WebMethod]
        public long ImportInventoryAlter(TInventory inv, bool clearprevious)
        {
            DB db = new DB();
            long affectrows = 0;

            try
            {
                //CLEAR INVENTORY BEFORE SYNC ,IF A ROW IS DELETED IN MOB IT DELETES HERE TOO
                if (clearprevious)
                {
                    try
                    {
                        db.DBExecuteSQLCmd("DELETE FROM twmsinventory where InvHdrID = " + inv.InvHdrIDServer.ToString());
                    }
                    catch { }
                }

                affectrows += invhandler.UpdateInventory(inv);
                
                //db.DBDisconnect();
            }
            catch (Exception ex) 
            { 
                invhandler.db.f_sqlerrorlog(1, "SOA_ImportInventoryCType>>inv.lotcode=" + inv.LotCode.ToString() + "itemcode:" + inv.ItemCode, ex.ToString()); 
            }
            return affectrows;
        }

        [WebMethod]
        public long Sqlerrorlog()
        {
            DB db = new DB();
            db.f_sqlerrorlog(1, "test", "test");

            return 1;
        }

        [WebMethod]
        public DataSet GetItemMunits()
        {
            WMSInvOffline invhandlerOffline = new WMSInvOffline();
            return invhandlerOffline.GetItemsMunits();
        }

        [WebMethod]
        public SyncInfo GetSyncInfo()
        {
            WMSInvOffline invhandlerOffline = new WMSInvOffline();
            return invhandlerOffline.GetSyncInfo();
        }

        //NEW METHODS FOR INV

        [WebMethod]
        public DataSet SOA_GetStores()
        {
            WMSInvOffline InvOfflineHandler = new WMSInvOffline();
           return InvOfflineHandler.GetStores();
        }

        [WebMethod]
        public ResultWithMessage UploadInventoryStatusCheck(TInventoryHeader invhdr) 
        {
            return invhandler.CheckIfInventoryCanUpload(invhdr);
        }

        [WebMethod]
        public List<ERPItem> SOA_GetItemsList(int storeid, long startindex, long endindex)
        {
            WMSInvOffline InvOfflineHandler = new WMSInvOffline();
            List<ERPItem> partiallist = new List<ERPItem>();

            long range = 0;

            if (endindex > 0 && startindex > -1) range = endindex - startindex;
            if (GItemList == null || GItemList.Count == 0)
            {
                GItemList = InvOfflineHandler.GetItemsList(storeid, 0, 0);
            }

            if (GItemList.Count > 0)
            {
                if (startindex > -1 && range > 0)
                {
                    if ((GItemList.Count - 1) >= startindex + range)
                        partiallist = GItemList.GetRange(Int32.Parse(startindex.ToString()), Int32.Parse(range.ToString()));
                    else if ((GItemList.Count - 1) >= startindex)
                        partiallist = GItemList.GetRange(Int32.Parse(startindex.ToString()), Int32.Parse(GItemList.Count.ToString()) - Int32.Parse(startindex.ToString()));
                }
                else
                {
                    return GItemList;

                }
            }

            return partiallist;
        }
        
        [WebMethod]
        public List<ERPLot> SOA_GetLotsList(int storeid, long startindex, long endindex)
        {
            WMSInvOffline InvOfflineHandler = new WMSInvOffline();
            List<ERPLot> partiallist = new List<ERPLot>();

            long range = 0;

            if (endindex > 0 && startindex > -1) range = endindex - startindex;
            if (GLotList == null|| GLotList.Count == 0)
            {
                GLotList = InvOfflineHandler.GetLotsList(storeid, 0, 0);
            }

            if (GLotList.Count > 0)
            {
                if (startindex > -1 && range > 0)
                {
                    if ((GLotList.Count - 1) >= startindex + range)
                        partiallist = GLotList.GetRange(Int32.Parse(startindex.ToString()), Int32.Parse(range.ToString()));
                    else if ((GItemList.Count - 1) >= startindex)
                        partiallist = GLotList.GetRange(Int32.Parse(startindex.ToString()), Int32.Parse(GLotList.Count.ToString()) - Int32.Parse(startindex.ToString()));
                }
                else
                {
                    return GLotList;
                }
            }

            return partiallist;
        }
        

        [WebMethod]
        public long Clearitemlist() {
            GItemList = null;
            return 1;
        }


        [WebMethod]
        public long Clearlotlist()
        {
            GLotList = null;
            return 1;
        }

        [WebMethod]
        public int InsertTransCodeDetails(List<TransCodeDetail> ldtl)
        {
            ReceivesController receives = new ReceivesController();
            long receiveid = receives.GetReceiveIDfromFTRID(ldtl[0].FtrID);

          
           foreach (TransCodeDetail a in ldtl) a.ReceiveID = receiveid;
           foreach (TransCodeDetail a in ldtl) receives.InsertNewTranscodeDetail(a); 

 
           return 1;
               
        }

        [WebMethod]
        public long InsertTransCodeHeader(TransCodeHeader t) 
        {
            ReceivesController receives = new ReceivesController();
            return receives.InsertNewTranscode(t); 
        }
        [WebMethod]
        public long GetInventoryLotQty(long invHrdId,long lotid) 
        {
            DB db = new DB();
            long lotqty = db.DBGetNumResultwithZero(" select CAST(SUM(ISNULL(invqtysecondary,invqty)) AS INTEGER) from TWMSInventory where invhdrid = " + invHrdId.ToString() + " and lotid = " + lotid.ToString());
            
            if (lotqty >= 0)
            {
                return lotqty;
            }
            else {

                return 0;
            }
       
        }

        [WebMethod]
        public DataSet SOA_GetLots(int storeid, long startid, long endid)
        {
            WMSInvOffline InvOfflineHandler = new WMSInvOffline();
            //WMSInvOffline InvOfflineHandler = new WMSInvOffline();
            return InvOfflineHandler.GetLots(storeid, startid, endid);
        }

        [WebMethod]
        public decimal SOA_GetERPLotSecondQTY(long lotid,int branchid)
        {
            WMSInvOffline InvOfflineHandler = new WMSInvOffline();
            return InvOfflineHandler.GetERPLotSecondQTY(branchid, lotid);
        }


        [WebMethod]
        public TransCodeDetail GetTransCodeDetails(string zbarcode) 
        {
            ReceivesController receives = new ReceivesController();
            TransCodeDetail tdtl = new TransCodeDetail();

            tdtl = receives.GetTransCodeDetail(zbarcode);

            return tdtl;
        
        
        }


        [WebMethod]
        public DataTable  InsertIntoReceives(TransCodeDetail treceive)
        {
            ReceivesController receives = new ReceivesController();
            return receives.InsertNewReceiveDTL(treceive);
    
        }

        [WebMethod]
        public DataTable GetTransRemains(long ftrid)
        {
            ReceivesController receives = new ReceivesController();
            return receives.GetTransRemains(ftrid);

        }

        private void WriteToLog(string message)
        {
            string applicationfolfer = HttpContext.Current.Server.MapPath("~");
            string filepath;
            string filename = "log.txt";

            filepath = applicationfolfer + "\\" + filename;

            try
            {
                if (!File.Exists(filepath))
                {
                    using (FileStream fs = File.Create(filepath))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes(" ");
                        fs.Write(info, 0, info.Length);
                    }
                }


                string linetext = DateTime.Now.ToString() + " - " + message;
                FileInfo f = new FileInfo(filepath);

                if (f.Length > 1000000)
                {
                    try { File.WriteAllText(filepath, String.Empty); }
                    catch { }
                }

                TextWriter tsw = new StreamWriter(filepath, true);

                tsw.WriteLine(linetext);
                tsw.Close();
            }
            catch (Exception ex) { }
        }




        [WebMethod]
        public DataTable GetInventoryTasks(int branchid)
        {
            return invhandler.GetInventoryTasks(branchid);
        }

        [WebMethod]
        public DataTable GetInventoryRecords(long InvHdrID,string sqlfilter,bool top10rows)
        {
            return invhandler.GetInventoryRecords(InvHdrID,sqlfilter,top10rows);
        }

        [WebMethod]
        public DataTable GetInventoryRecord (long InvID)
        {
            return invhandler.GetInventoryRecord(InvID);
        }

        [WebMethod]
        public long DeleteInventoryRecord(long InvID)
        {
            return invhandler.DeleteInventoryRecord(InvID);
        }

    }

    }
 