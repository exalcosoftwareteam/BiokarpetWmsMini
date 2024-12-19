
using System;
using System.Text;
using System.Data;
using System.Data.SqlServerCe;
using System.Net;
using System.IO;
//using WMSRetailClient.SOARetailWMSMiniProvider;
using WMSMobileClient.WMSservice;
using WMSMobileClient.WMSSyncService;
using System.Runtime.InteropServices;
using System.Collections.Generic;


namespace WMSMobileClient.Components
{


    public static class Logger
    {
        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
            // Update the underlying file.
            w.Flush();
        }

        public static void Flog(string errmsg)
        {

            string fname = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).DirectoryName + "\\" + "log.txt";

            try
            {
                if (File.Exists(fname))
                {
                    using (FileStream f = File.OpenRead(fname))
                    {
                        if (f.Length > 10240)
                        {
                            f.Close();
                            File.Delete(fname);
                        }
                        else
                            f.Close();
                    }

                }
            }
            catch { }

            using (StreamWriter w = File.AppendText(fname))
            {
                Log(errmsg, w);
                w.Close();

            }


        }
    }


    public class MUnitHandler
    {
        DB db = new DB();

        public MUnits MunitInfo(int munitid)
        {
            MUnits munit = new MUnits();
            try { munit = Parse(db.DBFillDataTable(" SELECT * FROM TItemMunits WHERE MunitID=" + munitid.ToString(), "TBMunit").Rows[0]); }
            catch { }
            return munit;
        }

        public MUnits Parse(DataRow dr)
        {
            MUnits munit = new MUnits();
            try { munit.MunitID = short.Parse(dr["MunitID"].ToString()); }
            catch { }
            try { munit.CompID = short.Parse(dr["CompID"].ToString()); }
            catch { }
            try { munit.MunitDecimals = short.Parse(dr["MunitDecimals"].ToString()); }
            catch { }
            try { munit.MUnit = dr["MUnit"].ToString(); }
            catch { }
            return munit;
        }

        public long UpdateMunit(MUnits munit)
        {
            if (db.DBGetNumResultFromSQLSelect("SELECT MunitID FROM TItemMunits WHERE MunitID=" + munit.MunitID.ToString()) > 0)
                return UpdateRecord(munit);
            else
                return InsertRecord(munit);
        }

        long InsertRecord(MUnits munit)
        {
            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;
            sqlstr.Append("INSERT INTO TItemMunits  (MunitID, CompID, MunitDecimals, MUnit) VALUES (");
            if (munit.MunitID > 0) sqlstr.Append(munit.MunitID.ToString() + ","); else sqlstr.Append("NULL,");
            if (munit.CompID > 0) sqlstr.Append(munit.CompID.ToString() + ","); else sqlstr.Append("NULL,");
            if (munit.MunitDecimals > 0) sqlstr.Append(munit.MunitDecimals.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(munit.MUnit)) sqlstr.Append("'" + munit.MUnit + "'"); else sqlstr.Append("NULL");
            sqlstr.Append(")");
            rtrn = db.DBExecuteSQLCmd(sqlstr.ToString());

            return rtrn;
        }

        long UpdateRecord(MUnits munit)
        {
            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;

            sqlstr.Append("UPDATE TItemMunits SET ");
            if (munit.MunitDecimals > 0) sqlstr.Append("MunitDecimals=" + munit.MunitDecimals.ToString() + ","); else sqlstr.Append("MunitDecimals=NULL,");
            if (!string.IsNullOrEmpty(munit.MUnit)) sqlstr.Append("MUnit='" + munit.MUnit + "'"); else sqlstr.Append("MUnit=NULL");
            sqlstr.Append(" WHERE MunitID=" + munit.MunitID.ToString());
            rtrn = db.DBExecuteSQLCmd(sqlstr.ToString());
            return rtrn;
        }

    }

    public class StoreHandler
    {
        DB db = new DB();

        public Store Parse(DataRow dr)
        {
            Store store = new Store();
            try { store.StoreID = short.Parse(dr["StoreID"].ToString()); }
            catch { }
            try { store.CompID = short.Parse(dr["CompID"].ToString()); }
            catch { }
            try { store.BranchID = short.Parse(dr["BranchID"].ToString()); }
            catch { }
            try { store.StoreName = dr["StoreName"].ToString(); }
            catch { }
            return store;
        }

        public long UpdateStore(Store store)
        {
            if (db.DBGetNumResultFromSQLSelect("SELECT StoreID FROM TStores WHERE StoreID=" + store.StoreID.ToString()) > 0)
                return UpdateRecord(store);
            else
                return InsertRecord(store);
        }

        long InsertRecord(Store store)
        {
            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;
            sqlstr.Append("INSERT INTO TStores  (StoreID, CompID, BranchId, StoreName,DSRIDTOCENTRAL,DSRIDTOBRANCH) VALUES (");
            if (store.StoreID > 0) sqlstr.Append(store.StoreID.ToString() + ","); else sqlstr.Append("NULL,");
            if (store.StoreID > 0) sqlstr.Append(store.CompID.ToString() + ","); else sqlstr.Append("NULL,");
            if (store.BranchID > 0) sqlstr.Append(store.BranchID.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(store.StoreName)) sqlstr.Append("'" + store.StoreName + "'"); else sqlstr.Append("NULL");
            sqlstr.Append(")");
            rtrn = db.DBExecuteSQLCmd(sqlstr.ToString());

            return rtrn;
        }

        long UpdateRecord(Store store)
        {
            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;

            sqlstr.Append("UPDATE TStores SET ");
            if (store.CompID > 0) sqlstr.Append("CompID=" + store.CompID.ToString() + ","); else sqlstr.Append("CompID=NULL,");
            if (store.BranchID > 0) sqlstr.Append("BranchId=" + store.BranchID.ToString() + ","); else sqlstr.Append("BranchId=NULL,");
            if (!string.IsNullOrEmpty(store.StoreName)) sqlstr.Append("StoreName='" + store.BranchID.ToString() + "'"); else sqlstr.Append("StoreName=NULL");
            sqlstr.Append(" WHERE StoreID=" + store.StoreID.ToString());
            rtrn = db.DBExecuteSQLCmd(sqlstr.ToString());
            return rtrn;
        }
    }

    public class ItemHandler
    {
        DB db = new DB();


        public Item Parse(DataRow Dr)
        {
            Item item = new Item();
            try { item.ItemID = long.Parse(Dr["ItemID"].ToString()); }
            catch { }
            try { item.CompID = short.Parse(Dr["CompID"].ToString()); }
            catch { }
            try { item.MUnitPrimary = int.Parse(Dr["MUnitPrimary"].ToString()); }
            catch { }
            try { item.EntryDate = Dr["EntryDate"].ToString(); }
            catch { }
            try { item.MUnitSecondary = int.Parse(Dr["MUnitSecondary"].ToString()); }
            catch { }
            try { item.MUnitsRelation = decimal.Parse(Dr["MUnitsRelation"].ToString()); }
            catch { }
            try { item.ItemCode = Dr["ItemCode"].ToString(); }
            catch { }
            try { item.ItemDesc = Dr["ItemDesc"].ToString().Replace("'", ""); }
            catch { }
            try { item.MUnitDesc1 = Dr["MUnitDesc1"].ToString(); }
            catch { }
            try { item.MUnitDesc2 = Dr["MUnitDesc2"].ToString(); }
            catch { }

            return item;
        }

        public Item ItemByCode(string itemcode)
        {
            Item item = new Item();
            try { item = Parse(db.DBFillDataTable("SELECT * FROM TItems WHERE ItemCode='" + itemcode + "'", "DTITEM").Rows[0]); }
            catch { }
            return item;
        }

        public Item ItemByID(long itemid)
        {
            string sqlstr = "";
            Item item = new Item();
            sqlstr = "select itemid,itemcode,cast (datepart(dd,entrydate) as nvarchar) +'/' + cast (datepart(mm,entrydate) as nvarchar) +'/'+cast(datepart(yyyy,entrydate) as nvarchar)";
            sqlstr += " as entrydate,ItemDesc,MunitPrimary,MunitSecondary,MunitDesc1,MunitDesc2 from titems WHERE ITEMID =" + itemid.ToString();
            try { item = Parse(db.DBFillDataTable(sqlstr, "DTITEM").Rows[0]); }
            catch { }
            return item;
        }

        public long ItemIDByCode(string itemcode)
        {
            return db.DBGetNumResultFromSQLSelect("SELECT ItemID FROM TItems WHERE ItemCode='" + itemcode + "'");
        }

        public string UpdateItem(Item item, bool newdb)
        {
            //if (newdb)
            //    return InsertRecord(item);


            if (AppGeneralSettings.CheckIFItemOrLotExists == 0) return InsertRecord(item);

            if (db.DBGetNumResultFromSQLSelectConON("SELECT ItemID FROM TItems WHERE ItemID=" + item.ItemID.ToString()) > 0)
                return UpdateRecord(item);
            else
                return InsertRecord(item);
        }

        string InsertRecord(Item item)
        {
            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;
            sqlstr.Append("INSERT INTO TItems(ItemID,ItemCode,CompID,ItemDesc,MUnitPrimary,MUnitSecondary,MUnitsRelation,MUnitDesc1,MUnitDesc2,ENTRYDATE) VALUES (");
            if (item.ItemID > 0) sqlstr.Append(item.ItemID.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(item.ItemCode)) sqlstr.Append("'" + item.ItemCode + "',"); else sqlstr.Append("NULL,");
            if (item.CompID > 0) sqlstr.Append(item.CompID.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(item.ItemDesc)) sqlstr.Append("'" + item.ItemDesc + "',"); else sqlstr.Append("NULL,");
            if (item.MUnitPrimary > 0) sqlstr.Append(item.MUnitPrimary.ToString() + ","); else sqlstr.Append("NULL,");
            if (item.MUnitSecondary > 0) sqlstr.Append(item.MUnitSecondary.ToString() + ","); else sqlstr.Append("NULL,");
            if (item.MUnitsRelation > 0) sqlstr.Append(item.MUnitsRelation.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(item.MUnitDesc1)) sqlstr.Append("'" + item.MUnitDesc1 + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(item.MUnitDesc2)) sqlstr.Append("'" + item.MUnitDesc2 + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(item.EntryDate)) sqlstr.Append("CONVERT(DATETIME,'" + item.EntryDate + "',103)"); else sqlstr.Append("NULL");
            sqlstr.Append(")");

            return sqlstr.ToString();
            // rtrn = db.DBExecuteSQLCmd(sqlstr.ToString());
            // return rtrn;
        }

        string UpdateRecord(Item item)
        {
            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;
            sqlstr.Append("UPDATE TItems SET ");
            if (!string.IsNullOrEmpty(item.ItemCode)) sqlstr.Append("ItemCode = '" + item.ItemCode + "',");
            if (item.CompID > 0) sqlstr.Append("CompID=" + item.CompID.ToString() + ",");
            if (!string.IsNullOrEmpty(item.ItemDesc)) sqlstr.Append("ItemDesc='" + item.ItemDesc + "',"); 
            if (item.MUnitPrimary > 0) sqlstr.Append("MUnitPrimary=" + item.MUnitPrimary.ToString() + ","); 
            if (item.MUnitSecondary > 0) sqlstr.Append("MUnitSecondary=" + item.MUnitSecondary.ToString() + ",");
            if (item.MUnitsRelation > 0) sqlstr.Append("MUnitsRelation=" + item.MUnitsRelation.ToString().Replace(",", ".") + ",");
            if (!string.IsNullOrEmpty(item.MUnitDesc1)) sqlstr.Append("MUnitDesc1='" + item.MUnitDesc1 + "',");
            if (!string.IsNullOrEmpty(item.MUnitDesc2)) sqlstr.Append("MUnitDesc2='" + item.MUnitDesc2 + "',"); 
            if (!string.IsNullOrEmpty(item.EntryDate)) sqlstr.Append("ENTRYDATE=CONVERT(DATETIME,'" + item.EntryDate + "',103)");
            sqlstr.Append(" WHERE ItemID=" + item.ItemID.ToString());
            return sqlstr.ToString();
            // rtrn = db.DBExecuteSQLCmd(sqlstr.ToString());
            //return rtrn;
        }

        public Item ParseERPItem(ERPItem erpitem)
        {
            Item item = new Item();
            item.CompID = erpitem.CompID;
            item.ItemID = erpitem.ItemID;
            item.ItemCode = erpitem.ItemCode;
            item.ItemDesc = erpitem.ItemDesc;
            item.MUnitDesc1 = erpitem.MUnitDesc1;
            item.MUnitDesc2 = erpitem.MUnitDesc2;
            item.MUnitPrimary = erpitem.MUnitPrimary;
            item.MUnitSecondary = erpitem.MUnitSecondary;
            item.MUnitsRelation = erpitem.MUnitsRelation;
            item.EntryDate = erpitem.EntryDate;

            return item;
        }

        public ERPItem ParseItemToERPitem(Item item) 
        {

            ERPItem erpitem = new ERPItem();
            erpitem.CompID = item.CompID;
            erpitem.ItemID = item.ItemID;
            erpitem.ItemCode = item.ItemCode;
            erpitem.ItemDesc = item.ItemDesc;
            erpitem.MUnitDesc1 = item.MUnitDesc1;
            erpitem.MUnitDesc2 = item.MUnitDesc2;
            erpitem.MUnitPrimary = item.MUnitPrimary;
            erpitem.MUnitSecondary = item.MUnitSecondary;
            erpitem.MUnitsRelation = item.MUnitsRelation;
            erpitem.EntryDate = item.EntryDate;

            return erpitem;
        }
        
        public Item ParseSyncERPItem(WMSSyncService.SyncERPItem erpitem)
        {
            Item item = new Item();
            item.CompID = erpitem.CompID;
            item.ItemID = erpitem.ItemID;
            item.ItemCode = erpitem.ItemCode;
            item.ItemDesc = erpitem.ItemDesc;
            item.MUnitDesc1 = erpitem.MUnitDesc1;
            item.MUnitDesc2 = erpitem.MUnitDesc2;
            item.MUnitPrimary = erpitem.MUnitPrimary;
            item.MUnitSecondary = erpitem.MUnitSecondary;
            item.MUnitsRelation = erpitem.MUnitsRelation;
            item.EntryDate = erpitem.EntryDate;
            return item;
        }


        public long DBImportItems(ERPItem[] Items)
        {
            string SqlExCmd = "";

            if (db.SQLDBConnection.State != ConnectionState.Open) db.DBConnect();
            SqlCeCommand DBExSqlCommand = null;
            long DBAffctRows = 0;
            DBExSqlCommand = new SqlCeCommand();

            DBExSqlCommand.Connection = db.SQLDBConnection;
            try { int i = Items.Length; }
            catch { return -1; }
            try { 
            for (int i = 0; i < Items.Length; i++)
            {
                SqlExCmd = UpdateItem(ParseERPItem(Items[i]), true);
                DBExSqlCommand.CommandText = SqlExCmd;
                DBAffctRows += DBExSqlCommand.ExecuteNonQuery();


            }
            }
            catch (Exception ex )
            {
                db.FCompactSqlErrorLog(ex.Message.ToString(), SqlExCmd);
            }
            db.SQLDBConnection.Close();
            return DBAffctRows;
        }
        public long DBSyncItems(WMSSyncService.SyncERPItem[] ErpItems)
        {
            string SqlExCmd = "";
            DB db = new DB();
            if (db.SQLDBConnection.State != ConnectionState.Open) db.DBConnect();
            SqlCeCommand DBExSqlCommand = null;
            long DBAffctRows = 0;
            DBExSqlCommand = new SqlCeCommand();

            DBExSqlCommand.Connection = db.SQLDBConnection;
            try 
            { 
                for (int i = 0; i < ErpItems.Length; i++)
                {
                    if (ErpItems[i].ItemID == 39730) 
                    {
                        int lk = 0;
                    }
                    DBExSqlCommand.CommandText = UpdateItem(ParseSyncERPItem(ErpItems[i]), false);
                    // = SqlExCmd;
                    DBAffctRows += DBExSqlCommand.ExecuteNonQuery();

                }
            }
            catch (Exception ex) 
            {
                db.FCompactSqlErrorLog(ex.Message.ToString(), SqlExCmd);
            
            }
            db.SQLDBConnection.Close();
            return DBAffctRows;
        }

    }


    public class PackingListHeaderHandler
    {
        DB db = new DB();


        public DataTable PackingLists()
        {
            return db.DBFillDataTable("SELECT * FROM TWMSPackingListsHeader WHERE PackingListStatus <> 2 ORDER BY PackingListHeaderID DESC", "DTPLISTHEADER");
        }

        public PackingHeader Parse(DataRow Dr)
        {
            PackingHeader packlisthdr = new PackingHeader();

            try { packlisthdr.PackingListHeaderID = long.Parse(Dr["PackingListHeaderID"].ToString()); }
            catch { }
            try { packlisthdr.OrderID = long.Parse(Dr["OrderID"].ToString()); }
            catch { }
            try { packlisthdr.StoreID = short.Parse(Dr["StoreID"].ToString()); }
            catch { }
            try { packlisthdr.OrderDtlID = long.Parse(Dr["OrderDTLID"].ToString()); }
            catch { }
            try { packlisthdr.PackingListDate = Dr["PackingListDate"].ToString(); }
            catch { }
            try { packlisthdr.CustomerTitle = Dr["CustomerTitle"].ToString(); }
            catch { }
            try { packlisthdr.CustomerCode = Dr["CustomerCode"].ToString(); }
            catch { }
            try { packlisthdr.StoreName = Dr["StoreName"].ToString(); }
            catch { }
            try { packlisthdr.Packinglistcomments = Dr["PackingListComments"].ToString(); }
            catch { }
            try { packlisthdr.PackingListDate = Dr["PackingListDate"].ToString(); }
            catch { }
            try { packlisthdr.TransCode = Dr["TransCode"].ToString(); }
            catch { }
            try { packlisthdr.TransType = int.Parse(Dr["TransType"].ToString()); }
            catch { }
            try { packlisthdr.Compid = short.Parse(Dr["Compid"].ToString()); }
            catch { }
            try { packlisthdr.Branchid = short.Parse(Dr["Branchid"].ToString()); }
            catch { }
            try { packlisthdr.Dsrid = long.Parse(Dr["Dsrid"].ToString()); }
            catch { }

            return packlisthdr;
        }

        public long UpdatePackingListHeader(PackingHeader packlisthdr)
        {
            if (packlisthdr.PackingListHeaderID > 0 && db.DBGetNumResultFromSQLSelect("SELECT PackingListHeaderID FROM TWMSPackingListsHeader WHERE PackingListHeaderID=" + packlisthdr.PackingListHeaderID.ToString()) > 0)
                return UpdateRecord(packlisthdr);
            else
                return InsertRecord(packlisthdr);
        }

        long InsertRecord(PackingHeader packlisthdr)
        {
            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;


            long newpackhdrid = 0;
            //if (!(invhdr.InvHdrID > 0))
            //{
            newpackhdrid = db.DBGetNumResultFromSQLSelect("SELECT (MAX(PackingListHeaderID) + 1)  AS NEWID FROM TWMSPackingListsHeader");
            if (!(newpackhdrid > 0)) newpackhdrid = 1;
            //}
            //else
            //    newinvhdrid = invhdr.InvHdrID;

            //TInventoryHeader
            sqlstr.Append("INSERT INTO TWMSPackingListsHeader(PackingListHeaderID,PackingListDate,Compid,BranchID,StoreID,StoreName,OrderID,OrderDtlID,CustomerCode,CustomerTitle,PackingListStatus,PackingListComments,Transtype,Transcode,Dsrid) VALUES (");
            if (newpackhdrid > 0) sqlstr.Append(newpackhdrid.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(packlisthdr.PackingListDate)) sqlstr.Append("CONVERT(DATETIME,'" + packlisthdr.PackingListDate + "',103),"); else sqlstr.Append("NULL,");
            if (packlisthdr.Compid > 0) sqlstr.Append(packlisthdr.Compid.ToString() + ","); else sqlstr.Append("NULL,");
            if (packlisthdr.Branchid > 0) sqlstr.Append(packlisthdr.Branchid.ToString() + ","); else sqlstr.Append("NULL,");
            if (packlisthdr.StoreID > 0) sqlstr.Append(packlisthdr.StoreID.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(packlisthdr.StoreName)) sqlstr.Append("'" + packlisthdr.StoreName + "',"); else sqlstr.Append("NULL,");
            if (packlisthdr.OrderID > 0) sqlstr.Append(packlisthdr.OrderID.ToString() + ","); else sqlstr.Append("NULL,");
            if (packlisthdr.OrderDtlID > 0) sqlstr.Append(packlisthdr.OrderDtlID.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(packlisthdr.CustomerCode)) sqlstr.Append("'" + packlisthdr.CustomerCode + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(packlisthdr.CustomerTitle)) sqlstr.Append("'" + packlisthdr.CustomerTitle + "',"); else sqlstr.Append("NULL,");
            if (packlisthdr.PackingListStatus > 0) sqlstr.Append(packlisthdr.PackingListStatus.ToString() + ","); else sqlstr.Append("0,");
            if (!string.IsNullOrEmpty(packlisthdr.Packinglistcomments)) sqlstr.Append("'" + packlisthdr.Packinglistcomments + "',"); else sqlstr.Append("NULL,");
            if (packlisthdr.TransType > 0) sqlstr.Append(packlisthdr.TransType.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(packlisthdr.TransCode)) sqlstr.Append("'" + packlisthdr.TransCode + "',"); else sqlstr.Append("NULL,");
            if (packlisthdr.Dsrid > 0) sqlstr.Append(packlisthdr.Dsrid.ToString()); else sqlstr.Append(AppGeneralSettings.DSRID.ToString());
            sqlstr.Append(")");
            rtrn = db.DBExecuteSQLCmd(sqlstr.ToString());

            if (rtrn > 0)
            {
                // newinvhdrid = db.DBGetNumResultFromSQLSelect("SELECT MAX(InvHdrID) AS NEWID FROM TInventoryHeader");
                return newpackhdrid;
            }
            else
                return -1;

        }

        public string PackingListHeaderTitle(long PackingListHeaderID)
        {
            return db.DBWmsExSelectCmdRStr2Str(" SELECT PackingListComments FROM TWMSPackingListsHeader WHERE PackingListHeaderID=" + PackingListHeaderID.ToString());
        }


        public long SetPackingListHeaderStatus(long PackingListHeaderID, short status)
        {
            return db.DBExecuteSQLCmd("update TWMSPackingListsHeader set PackingListStatus =" + status.ToString() + " WHERE PackingListHeaderID=" + PackingListHeaderID.ToString());
        }

        public long CheckPackingListStatus(long PackingListHeaderID)
        {
            return db.DBGetNumResultFromSQLSelect("select PackingListStatus from TWMSPackingListsHeader  WHERE PackingListHeaderID=" + PackingListHeaderID.ToString());
        }

        public string GetPackingListCounter(long packheader)
        {

            return db.DBWmsExSelectCmdRN2String("select count(PackingListDTLID) from TWMSpackinglistDetails where PackingListHeaderID = " + packheader.ToString());

        }


        long UpdateRecord(PackingHeader packlisthdr)
        {

            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;
            sqlstr.Append("UPDATE TWMSPackingListsHeader SET ");
            if (packlisthdr.StoreID > 0) sqlstr.Append("StoreID=" + packlisthdr.StoreID.ToString() + ","); else sqlstr.Append("StoreID=NULL,");
            if (!string.IsNullOrEmpty(packlisthdr.StoreName)) sqlstr.Append("StoreName = '" + packlisthdr.StoreName + "',"); else sqlstr.Append("StoreName=NULL,");
            if (packlisthdr.PackingListStatus > 0) sqlstr.Append("PackingListStatus=" + packlisthdr.PackingListStatus.ToString() + ","); else sqlstr.Append("PackingListStatus=0,");
            if (!string.IsNullOrEmpty(packlisthdr.Packinglistcomments)) sqlstr.Append("PackingListComments = '" + packlisthdr.Packinglistcomments + "'"); else sqlstr.Append("PackingListComments=NULL");
            rtrn = db.DBExecuteSQLCmd(sqlstr.ToString());
            return rtrn;
        }


    }


    public class PackingListDetailHandler
    {
        DB db = new DB();


        public string dberror;

        public PackingListDetail[] GetPackingListByHeader(long PackingListHeaderID)
        {
            string test;
            DataTable dt = new DataTable();
            dt = db.DBFillDataTable("SELECT * FROM TWMSPackingListDetails WHERE PackingListHeaderID=" + PackingListHeaderID.ToString(), "TPACKDTL");
            PackingListDetail[] packlistdtl = new PackingListDetail[dt.Rows.Count];
            test = dt.Rows[0]["PackingListHeaderID"].ToString();
            try
            {
                packlistdtl = ParseRows(dt);
            }
            catch { }
            return packlistdtl;
        }

        public PackingList PackingListRecord(long PackingListDTLID)
        {
            PackingList packlistdtl = new PackingList();
            try { packlistdtl = Parse(db.DBFillDataTable("SELECT * FROM TWMSPackingListDetails WHERE PackingListDTLID=" + PackingListDTLID.ToString(), "TPACKDTL").Rows[0]); }
            catch { }
            return packlistdtl;
        }

        public PackingListDetail[] ParseRows(DataTable dt)
        {
            PackingListDetail[] packlistdtl;

            packlistdtl = new PackingListDetail[dt.Rows.Count];

            for (int dsi = 0; dsi < dt.Rows.Count; dsi++)
            {
                packlistdtl[dsi] = new PackingListDetail();
                try { packlistdtl[dsi].PackingListDTLID = long.Parse(dt.Rows[dsi]["PackingListDTLID"].ToString()); }
                catch (Exception ex) { }

                try { packlistdtl[dsi].PackingListHeaderID = long.Parse(dt.Rows[dsi]["PackingListHeaderID"].ToString()); }
                catch (Exception ex) { }
                try { packlistdtl[dsi].ItemID = long.Parse(dt.Rows[dsi]["ItemID"].ToString()); }
                catch (Exception ex) { }
                try { packlistdtl[dsi].LotID = long.Parse(dt.Rows[dsi]["LotID"].ToString()); }
                catch (Exception ex) { }
                try { packlistdtl[dsi].LotCode = dt.Rows[dsi]["LotCode"].ToString(); }
                catch (Exception ex) { }
                try { packlistdtl[dsi].ItemCode = dt.Rows[dsi]["ItemCode"].ToString(); }
                catch (Exception ex) { }
                try { packlistdtl[dsi].ItemQTYprimary = decimal.Parse(dt.Rows[dsi]["ItemQtyPrimary"].ToString()); }
                catch (Exception ex) { }
                try { packlistdtl[dsi].ItemQTYsecondary = decimal.Parse(dt.Rows[dsi]["ItemQtySecondary"].ToString()); }
                catch (Exception ex) { }
                try { packlistdtl[dsi].ItemMunitPrimary = short.Parse(dt.Rows[dsi]["ItemMunitPrimary"].ToString()); }
                catch (Exception ex) { }
                try { packlistdtl[dsi].ItemMunitSecondary = short.Parse(dt.Rows[dsi]["ItemMunitSecondary"].ToString()); }
                catch (Exception ex) { }

            }



            return packlistdtl;
        }

        public PackingList Parse(DataRow Dr)
        {
            PackingList packlistdtl = new PackingList();

            try { packlistdtl.PackingListDTLID = long.Parse(Dr["PackingListDTLID"].ToString()); }
            catch { }
            try { packlistdtl.PackingListHeaderID = long.Parse(Dr["PackingListHeaderID"].ToString()); }
            catch { }
            try { packlistdtl.ItemID = long.Parse(Dr["ItemID"].ToString()); }
            catch { }
            try { packlistdtl.LotID = long.Parse(Dr["LotID"].ToString()); }
            catch { }
            try { packlistdtl.LotCode = Dr["LotCode"].ToString(); }
            catch { }
            try { packlistdtl.ItemCode = Dr["ItemCode"].ToString(); }
            catch { }
            try { packlistdtl.ItemDesc = Dr["ItemDesc"].ToString(); }
            catch { }

            try { packlistdtl.PackDate = Dr["PackingListDTLDate"].ToString(); }
            catch { }

            try { packlistdtl.ItemQtyPrimary = decimal.Parse(Dr["ItemQtyPrimary"].ToString()); }
            catch { }
            try { packlistdtl.ItemQtySecondary = decimal.Parse(Dr["ItemQtySecondary"].ToString()); }
            catch { }
            try { packlistdtl.ItemMunitPrimary = short.Parse(Dr["ItemMunitPrimary"].ToString()); }
            catch { }
            try { packlistdtl.ItemMunitSecondary = short.Parse(Dr["ItemMunitSecondary"].ToString()); }
            catch { }
            try { packlistdtl.Width = decimal.Parse(Dr["Width"].ToString()); }
            catch { }
            try { packlistdtl.Length = decimal.Parse(Dr["Length"].ToString()); }
            catch { }
            try { packlistdtl.Color = Dr["Color"].ToString(); }
            catch { }
            try { packlistdtl.Draft = Dr["Draft"].ToString(); }
            catch { }

            return packlistdtl;
        }

        public long UpdatePackingListDTL(PackingList packdtl)
        {


            if (db.DBGetNumResultFromSQLSelect("SELECT PackingListDTLID FROM TWMSPackingListDetails WHERE PackingListDTLID=" + packdtl.PackingListDTLID.ToString()) > 0)
                return UpdateRecord(packdtl);
            else
                return InsertRecord(packdtl);
        }

        public DataTable PackingListView(long PackingListHeadeID, string sterm, bool getlast10rec)
        {
            DataTable DT = new DataTable();
            string sqlstr = null;


            sqlstr = "SELECT PackingListDTLID,ItemID,ItemCode,LOTCode,LotID,CAST(ItemQtyPrimary as numeric(5,1)) as ItemQtyPrimary,CAST(ItemQtySecondary as numeric(5,1)) as ItemQtySecondary FROM TWMSPackingListDetails WHERE PackingListHeaderID=" + PackingListHeadeID.ToString();
            if (!string.IsNullOrEmpty(sterm) && !getlast10rec) sqlstr += " AND (ItemCode LIKE '" + sterm + "%' OR LotCode LIKE '" + sterm + "%')";

            if (getlast10rec)
            {
                sqlstr += " ORDER BY PackingListDTLID DESC";
                DataTable tmp = new DataTable();
                tmp = db.DBFillDataTable(sqlstr, "DTPACKINGLIST");

                DT = tmp.Clone();
                if (tmp.Rows.Count > 0)
                {
                    for (int i = (tmp.Rows.Count - 1); i > -1; i--)
                    {
                        if (DT.Rows.Count == 10) break;
                        DT.ImportRow(tmp.Rows[i]);

                        //DataRow Dr= DT.NewRow();                        
                        //Dr.ItemArray = tmp.Rows[i].ItemArray;

                        //try  {  DT.Rows.Add(Dr);}
                        //catch (Exception ex) { }                                              
                    }
                }
            }
            else
            {
                sqlstr += " ORDER BY ItemCode,LotCode";
                DT = db.DBFillDataTable(sqlstr, "DTPACKINGLIST");
            }

            return DT;
        }

        public long DeleteWholePackingList(long PackingListHeaderID)
        {
            db.DBExecuteSQLCmd("DELETE FROM TWMSPackingListsHeader WHERE PackingListHeaderID=" + PackingListHeaderID.ToString());
            return db.DBExecuteSQLCmd("DELETE FROM TWMSPackingListDetails WHERE PackingListHeaderID=" + PackingListHeaderID.ToString());
        }

        public long DeleteRecord(long PackingListDTLID)
        {
            return db.DBExecuteSQLCmd("DELETE FROM TWMSPackingListDetails WHERE PackingListDTLID=" + PackingListDTLID.ToString());
        }

        long InsertRecord(PackingList packdtl)
        {

            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;
            sqlstr.Append("INSERT INTO TWMSPackingListDetails(PackingListHeaderID,ItemID,LotID,ItemCode,ItemDesc,LOTCode,PackingListDTLDate,ItemQtyPrimary,ItemMunitPrimary,ItemQtySecondary,ItemMunitSecondary,Width,Length,DRAFT,COLOR) VALUES (");
            if (packdtl.PackingListHeaderID > 0) sqlstr.Append(packdtl.PackingListHeaderID.ToString() + ","); else sqlstr.Append("NULL,");
            if (packdtl.ItemID > 0) sqlstr.Append(packdtl.ItemID.ToString() + ","); else sqlstr.Append("NULL,");
            if (packdtl.LotID > 0) sqlstr.Append(packdtl.LotID.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(packdtl.ItemCode)) sqlstr.Append("'" + packdtl.ItemCode + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(packdtl.ItemDesc)) sqlstr.Append("'" + packdtl.ItemDesc + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(packdtl.LotCode)) sqlstr.Append("'" + packdtl.LotCode + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(packdtl.PackDate)) sqlstr.Append("CONVERT(DATETIME,'" + packdtl.PackDate + "',103),"); else sqlstr.Append("GETDATE(),");
            if (packdtl.ItemQtyPrimary > 0) sqlstr.Append(packdtl.ItemQtyPrimary.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (packdtl.ItemMunitPrimary > 0) sqlstr.Append(packdtl.ItemMunitPrimary.ToString() + ","); else sqlstr.Append("NULL,");
            if (packdtl.ItemQtySecondary > 0) sqlstr.Append(packdtl.ItemQtySecondary.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (packdtl.ItemMunitSecondary > 0) sqlstr.Append(packdtl.ItemMunitSecondary.ToString() + ","); else sqlstr.Append("NULL,");
            if (packdtl.Width > 0) sqlstr.Append(packdtl.Width.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (packdtl.Length > 0) sqlstr.Append(packdtl.Length.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(packdtl.Draft)) sqlstr.Append("'" + packdtl.Draft + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(packdtl.Color)) sqlstr.Append("'" + packdtl.Color + "'"); else sqlstr.Append("NULL");
            sqlstr.Append(")");
            rtrn = db.DBExecuteSQLCmd(sqlstr.ToString());
            return rtrn;
        }

        long UpdateRecord(PackingList packdtl)
        {

            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;
            sqlstr.Append("UPDATE TWMSPackingListDetails SET ");
            if (packdtl.ItemID > 0) sqlstr.Append("ItemID=" + packdtl.ItemID.ToString() + ","); else sqlstr.Append("ItemID=NULL,");
            if (packdtl.LotID > 0) sqlstr.Append("LotID=" + packdtl.LotID.ToString() + ","); else sqlstr.Append("LotID=NULL,");
            if (packdtl.ItemQtyPrimary > 0) sqlstr.Append("ItemQtyPrimary=" + packdtl.ItemQtyPrimary.ToString().Replace(",", ".") + ","); else sqlstr.Append("ItemQtyPrimary=NULL,");
            if (packdtl.ItemMunitPrimary > 0) sqlstr.Append("ItemMunitPrimary=" + packdtl.ItemMunitPrimary.ToString() + ","); else sqlstr.Append("ItemMunitPrimary=NULL,");
            if (packdtl.ItemQtySecondary > 0) sqlstr.Append("ItemQtySecondary=" + packdtl.ItemQtySecondary.ToString().Replace(",", ".") + ","); else sqlstr.Append("ItemQtySecondary=NULL,");
            if (packdtl.ItemMunitSecondary > 0) sqlstr.Append("ItemMunitSecondary=" + packdtl.ItemMunitSecondary.ToString() + ","); else sqlstr.Append("ItemMunitSecondary=NULL,");
            if (!string.IsNullOrEmpty(packdtl.ItemCode)) sqlstr.Append("ItemCode = '" + packdtl.ItemCode + "',"); else sqlstr.Append("ItemCode=NULL,");
            if (!string.IsNullOrEmpty(packdtl.LotCode)) sqlstr.Append("LotCode = '" + packdtl.LotCode + "',"); else sqlstr.Append("LotCode=NULL,");
            if (!string.IsNullOrEmpty(packdtl.Draft)) sqlstr.Append("Draft = '" + packdtl.Draft + "',"); else sqlstr.Append("Draft=NULL,");
            if (!string.IsNullOrEmpty(packdtl.Color)) sqlstr.Append("Color = '" + packdtl.Color + "',"); else sqlstr.Append("Color=NULL,");
            if (packdtl.Width > 0) sqlstr.Append("Width=" + packdtl.Width.ToString().Replace(",", ".") + ","); else sqlstr.Append("Width=NULL,");
            if (packdtl.Length > 0) sqlstr.Append("Length=" + packdtl.Length.ToString().Replace(",", ".")); else sqlstr.Append("Length=NULL");

            sqlstr.Append(" WHERE PackingListDTLID=" + packdtl.PackingListDTLID.ToString());

            rtrn = db.DBExecuteSQLCmd(sqlstr.ToString());
            return rtrn;
        }

        public string PackingListHeaderTitle(long PackingListHeaderID)
        {
            return db.DBWmsExSelectCmdRStr2Str(" SELECT PackingListComments FROM TWMSPackingListsHeader WHERE PackingListHeaderID=" + PackingListHeaderID.ToString());
        }

    }


    public class LotHandler
    {
        DB db = new DB();
        int oldtimeout = 0;

        public Lot Parse(DataRow Dr)
        {
            Lot lot = new Lot();
            try { lot.LotID = long.Parse(Dr["LotID"].ToString()); }
            catch { }
            try { lot.CompID = short.Parse(Dr["CompID"].ToString()); }
            catch { }
            try { lot.ItemID = long.Parse(Dr["ItemID"].ToString()); }
            catch { }
            try { lot.EntryDate = Dr["EntryDate"].ToString(); }
            catch { }
            try { lot.LotCode = Dr["LotCode"].ToString(); }
            catch { }
            try { lot.Width = decimal.Parse(Dr["Width"].ToString()); }
            catch { }
            try { lot.Length = decimal.Parse(Dr["Length"].ToString()); }
            catch { }
            try { lot.Draft = Dr["Draft"].ToString(); }
            catch (Exception ex) { }
            try { lot.Color = Dr["Color"].ToString(); }
            catch (Exception ex) { }
            try { lot.ItemPrimaryQty = decimal.Parse(Dr["ItemPrimaryQty"].ToString()); }
            catch { }
            try { lot.ItemSecondaryQty = decimal.Parse(Dr["ItemSecondaryQty"].ToString()); }
            catch { }

            return lot;
        }

        public Lot ParseERPLot(WMSSyncService.SyncLot Syncerplot)
        {
            Lot mylot = new Lot();
            mylot.CompID = Syncerplot.CompID;
            mylot.LotID = Syncerplot.LotID;
            mylot.ItemID = Syncerplot.ItemID;
            mylot.LotCode = Syncerplot.LotCode;
            mylot.EntryDate = Syncerplot.EntryDate;
            mylot.ItemPrimaryQty = Syncerplot.ItemPrimaryQty;
            mylot.ItemSecondaryQty = Syncerplot.ItemSecondaryQty;
            mylot.Width = Syncerplot.Width;
            mylot.Length = Syncerplot.Length;
            mylot.Draft = Syncerplot.Draft;
            mylot.Color = Syncerplot.Color;
            return mylot;
        }

        public Lot ParseERPLot(ERPLot Syncerplot)
        {
            Lot mylot = new Lot();
            mylot.CompID = Syncerplot.CompID;
            mylot.LotID = Syncerplot.LotID;
            mylot.ItemID = Syncerplot.ItemID;
            mylot.LotCode = Syncerplot.LotCode;
            mylot.EntryDate = Syncerplot.EntryDate;
            mylot.ItemPrimaryQty = Syncerplot.ItemPrimaryQty;
            mylot.ItemSecondaryQty = Syncerplot.ItemSecondaryQty;
            mylot.Draft = Syncerplot.Draft;
            mylot.Color = Syncerplot.Color;
            mylot.Width = Syncerplot.Width;
            mylot.Length = Syncerplot.Length;
            return mylot;
        }

        public ERPLot ParseLotToERPlot(Lot newlot) 
        {

            ERPLot mylot = new ERPLot();
            mylot.CompID = AppGeneralSettings.CompID;
            mylot.LotID = newlot.LotID;
            mylot.ItemID = newlot.ItemID;
            mylot.LotCode = newlot.LotCode;
            mylot.EntryDate = newlot.EntryDate;
            mylot.ItemPrimaryQty = newlot.ItemPrimaryQty;
            mylot.ItemSecondaryQty = newlot.ItemSecondaryQty;
            mylot.Draft = newlot.Draft;
            mylot.Color = newlot.Color;
            mylot.Width = newlot.Width;
            mylot.Length = newlot.Length;
            return mylot;
        
        
        }



        public long DBImportLots(ERPLot[] erplotlist)
        {
            string SqlExCmd = "";

            if (db.SQLDBConnection.State != ConnectionState.Open) db.DBConnect();
            SqlCeCommand DBExSqlCommand = null;
            long DBAffctRows = 0;
            DBExSqlCommand = new SqlCeCommand();
            DBExSqlCommand.Connection = db.SQLDBConnection;
            try
            {
                for (int i = 0; i < erplotlist.Length; i++)
                {
                    SqlExCmd = UpdateLot(ParseERPLot(erplotlist[i]), true);
                    DBExSqlCommand.CommandText = SqlExCmd;
                    DBAffctRows += DBExSqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                db.FCompactSqlErrorLog(ex.Message.ToString(), SqlExCmd);
            }
            db.SQLDBConnection.Close();
            return DBAffctRows;
        }

        public long DBSyncLots(SyncLot[] erplotlist)
        {
            string SqlExCmd = "";
            if (db.SQLDBConnection.State != ConnectionState.Open) db.DBConnect();
            SqlCeCommand DBExSqlCommand = null;
            long DBAffctRows = 0;
            DBExSqlCommand = new SqlCeCommand();
        
            DBExSqlCommand.Connection = db.SQLDBConnection;
            try
            {
                for (int i = 0; i < erplotlist.Length; i++)
                {
                    SqlExCmd = UpdateLot(ParseERPLot(erplotlist[i]), false);
                    DBExSqlCommand.CommandText = SqlExCmd;
                    DBAffctRows += DBExSqlCommand.ExecuteNonQuery();
                }
            }
            catch(Exception ex) 
            {
                db.FCompactSqlErrorLog(ex.Message.ToString(), SqlExCmd);
            }
            db.SQLDBConnection.Close();
            return DBAffctRows;
        }


        public Lot LotByID(long lotid)
        {
            string sqlstr;

            sqlstr =" select LOTID,ITEMID,LOTCODE,cast (datepart(dd,entrydate) as nvarchar) +'/' + cast (datepart(mm,entrydate) as nvarchar) +'/'+cast(datepart(yyyy,entrydate) as nvarchar) ";
            sqlstr += " as entrydate,DRAFT,COLOR,WIDTH,LENGTH,ITEMPRIMARYQTY,ITEMSECONDARYQTY FROM TITEMLOT WHERE LotID=" + lotid.ToString();
            Lot lot = new Lot();
            try { lot = Parse(db.DBFillDataTable(sqlstr, "DTLOT").Rows[0]); }
            catch { }

            return lot;
        }

        public Lot LotByCode(string lotcode,long invhdrid)
        {
            Lot lot = new Lot();
            try { 
                lot = Parse(db.DBFillDataTable("SELECT * FROM TItemLot WHERE LotCode='" + lotcode + "'", "DTLOT").Rows[0]);
                if (invhdrid > 0) lot.ErpQty2 = decimal.Round(db.DBGeDecimalResultConON("select sum(invqtysecondary) from tinventory where lotid = " + lot.LotID + " and invhdrid=" + invhdrid.ToString()),2);

            }
            catch { }

            return lot;
        }

        public Lot LotByCodeOnline(string lotcode)
        {
            Lot lot = new Lot();
            try
            {
                var onlineLot = AppGeneralSettings.WebSyncServiceProvider.SOA_GetLotbyCodeInventory(lotcode);

                lot.Color = onlineLot.Color;
                lot.CompID = onlineLot.CompID;
                lot.Draft = onlineLot.Draft;
                lot.EntryDate = onlineLot.EntryDate;
                lot.ErpQty = onlineLot.ItemPrimaryQty;
                lot.ErpQty2 = onlineLot.ItemSecondaryQty;
                lot.ItemID = onlineLot.ItemID;
                lot.Length = onlineLot.Length;
                lot.ItemPrimaryQty = onlineLot.ItemPrimaryQty;
                lot.ItemSecondaryQty = onlineLot.ItemSecondaryQty;
                lot.LotCode = onlineLot.LotCode;
                lot.LotID = onlineLot.LotID;
                lot.Width = onlineLot.Width;

                lot.ItemCode = onlineLot.ItemCode;
                lot.ItemDesc = onlineLot.ItemDesc;
                lot.MUnitPrimary = onlineLot.MunitPrimary;
                lot.MUnitSecondary = onlineLot.MunitSecondary;
                lot.MUnitDesc1 = onlineLot.MunitDesc1;
                lot.MUnitDesc2 = onlineLot.MunitDesc2;

               // if (invhdrid > 0) lot.ErpQty2 = decimal.Round(db.DBGeDecimalResultConON("select sum(invqtysecondary) from tinventory where lotid = " + lot.LotID + " and invhdrid=" + invhdrid.ToString()), 2);

            }
            catch { }

            return lot;
        }

        public string UpdateLot(Lot lot,bool newdb)
        {

            if (AppGeneralSettings.CheckIFItemOrLotExists == 0) return InsertRecord(lot); 
            //if (newdb)
            //    return InsertRecord(lot);

            if (db.DBGetNumResultFromSQLSelectConON("SELECT LOTID FROM TItemLot WHERE LOTID=" + lot.LotID.ToString()) > 0)
                return UpdateRecord(lot);
            else
                return InsertRecord(lot);
        }

        public long LotIDByCode(string lotcode)
        {
            return db.DBGetNumResultFromSQLSelect("SELECT LOTID FROM TItemLOT WHERE LOTCODE='" + lotcode + "'");
        }

        string InsertRecord(Lot lot)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("INSERT INTO TItemLot(LOTID,ItemID,LOTCode,Width,Length,ItemPrimaryQty,ItemSecondaryQty,Color,Draft,ENTRYDATE) VALUES (");
            if (lot.LotID > 0) sqlstr.Append(lot.LotID.ToString() + ","); else sqlstr.Append("NULL,");
            if (lot.ItemID > 0) sqlstr.Append(lot.ItemID.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(lot.LotCode)) sqlstr.Append("'" + lot.LotCode + "',"); else sqlstr.Append("NULL,");
            if (lot.Width > 0) sqlstr.Append(lot.Width.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (lot.Length > 0) sqlstr.Append(lot.Length.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (lot.ItemPrimaryQty > 0) sqlstr.Append(lot.ItemPrimaryQty.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (lot.ItemSecondaryQty > 0) sqlstr.Append(lot.ItemSecondaryQty.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(lot.Color)) sqlstr.Append("'" + lot.Color.Replace("'", "|") + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(lot.Draft)) sqlstr.Append("'" + lot.Draft.Replace("'", "|") + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(lot.EntryDate)) sqlstr.Append("CONVERT(DATETIME,'" + lot.EntryDate + "',103)"); else sqlstr.Append("NULL");
            sqlstr.Append(")"); 
            return sqlstr.ToString();
        }

        string UpdateRecord(Lot lot)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("UPDATE TItemLot SET ");
            if (!string.IsNullOrEmpty(lot.LotCode)) sqlstr.Append("LOTCode = '" + lot.LotCode + "',");
            if (lot.ItemID > 0) sqlstr.Append("ItemID=" + lot.ItemID.ToString() + ",");
            if (lot.Width > 0) sqlstr.Append("Width=" + lot.Width.ToString().Replace(",", ".") + ",");
            if (lot.Length > 0) sqlstr.Append("Length=" + lot.Length.ToString().Replace(",", ".") + ",");
            if (lot.ItemPrimaryQty > 0) sqlstr.Append("ItemPrimaryQty=" + lot.ItemPrimaryQty.ToString().Replace(",", ".") + ",");
            if (!string.IsNullOrEmpty(lot.EntryDate)) sqlstr.Append("ENTRYDATE=CONVERT(DATETIME,'" + lot.EntryDate + "',103)");
            sqlstr.Append(" WHERE LotID=" + lot.LotID.ToString());
            return sqlstr.ToString();
        }

    }

    public class InventoryHeaderHandler
    {
        DB db = new DB();


        public InventoryHeader InventoryHeaderRecord(long invhdrid)
        {
            string sqlstr;
            sqlstr = "SELECT CONVERT(NVARCHAR(10), invdate, 103) as invdate,";
            sqlstr += "Compid,Branchid,InvHdrID,InvHdrIDServer,Storeid,StoreName,InvComments,InvSyncID FROM TInventoryheader WHERE invhdrid=" + invhdrid.ToString();
            InventoryHeader invheader = new InventoryHeader();
            try { invheader = Parse(db.DBFillDataTable(sqlstr, "TINV").Rows[0]); }
            catch { }
            return invheader;
            
        }


        public long GetAtlantisCurrentInventory(bool forcedelete) 
        {
            long result;
            int oldTimeout =  AppGeneralSettings.webServiceProvider.Timeout ;
            AppGeneralSettings.webServiceProvider.Timeout = 3500000;

            try 
            {
                result = AppGeneralSettings.webServiceProvider.AtlGeInventory(AppGeneralSettings.BranchID, forcedelete); 
            }
            catch 
            {
                AppGeneralSettings.webServiceProvider.Timeout = oldTimeout;
                return -1;
            }

            AppGeneralSettings.webServiceProvider.Timeout = oldTimeout;
            return result;
        }


        public DataTable InventoryLists()
        {
            return db.DBFillDataTable("SELECT * FROM TInventoryHeader WHERE InvStatus <> 1 ORDER BY InvHdrID DESC", "DTINVLIST");
        }

        public DataTable InventoryListsOnline()
        {
            return AppGeneralSettings.webServiceProvider.GetInventoryTasks(AppGeneralSettings.BranchID);
        }
        //AppGeneralSettings.WebSyncServiceProvider.ImportPackingListHeaderCType(PackHeader);

        public InventoryHeader Parse(DataRow Dr)
        {
            InventoryHeader invhdr = new InventoryHeader();

            try { invhdr.CompID = short.Parse(Dr["CompID"].ToString()); }
            catch { }
            try { invhdr.Branchid = short.Parse(Dr["Branchid"].ToString()); }
            catch { }
            try { invhdr.InvHdrID = long.Parse(Dr["InvHdrID"].ToString()); }
            catch { }
            try { invhdr.InvHdrIDServer = long.Parse(Dr["InvHdrIDServer"].ToString()); }
            catch { }
            try { invhdr.Storeid = short.Parse(Dr["Storeid"].ToString()); }
            catch { }
            try { invhdr.InvType = short.Parse(Dr["InvType"].ToString()); }
            catch { }
            try { invhdr.StoreName = Dr["StoreName"].ToString(); }
            catch { }
            try { invhdr.InvComments = Dr["InvComments"].ToString(); }
            catch { }
            try { invhdr.InvDate = Dr["InvDate"].ToString(); }
            catch { }
            try { invhdr.InvSyncID = long.Parse(Dr["InvSyncID"].ToString()); }
            catch { }

            return invhdr;
        }

        public long UpdateInventoryHeader(InventoryHeader invhdr)
        {
            if (invhdr.InvHdrID > 0 && db.DBGetNumResultFromSQLSelect("SELECT InvHdrID FROM TInventoryHeader WHERE InvHdrID=" + invhdr.InvHdrID.ToString()) > 0)
                return UpdateRecord(invhdr);
            else
                return InsertRecord(invhdr);
        }

        public long UpdateInventoryHeaderOnline(InventoryHeader invhdr)
        {
            TInventoryHeader hdr = new TInventoryHeader();
            hdr.Branchid = invhdr.Branchid;
            hdr.CompID = invhdr.CompID;
            hdr.InvComments = invhdr.InvComments;
            hdr.InvDate = invhdr.InvDate;
            hdr.Storeid = invhdr.Storeid;
            hdr.InvStatus = invhdr.InvStatus;


            var ret = AppGeneralSettings.webServiceProvider.ImportInventoryHeaderCType(hdr);

            return ret;
        }

        long InsertRecord(InventoryHeader invhdr)
        {
            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;


            long newinvhdrid = 0;
            //if (!(invhdr.InvHdrID > 0))
            //{
            newinvhdrid = db.DBGetNumResultFromSQLSelect("SELECT (MAX(InvHdrID) + 1)  AS NEWID FROM TInventoryHeader");
            if (!(newinvhdrid > 0)) newinvhdrid = 1;
            //}
            //else
            //    newinvhdrid = invhdr.InvHdrID;

            Random randnum = new Random();
            long newInvSyncID = randnum.Next(1000, 100000);


            //TInventoryHeader
            sqlstr.Append("INSERT INTO TInventoryHeader(InvHdrID,InvHdrIDServer,InvSyncID,CompID,BranchID,StoreID,StoreName,CustomerID,CustomerTitle,InvStatus,InvDate,InvComments) VALUES (");
            if (newinvhdrid > 0) sqlstr.Append(newinvhdrid.ToString() + ","); else sqlstr.Append("NULL,");
            if (invhdr.InvHdrIDServer > 0) sqlstr.Append(invhdr.InvHdrIDServer.ToString() + ","); else sqlstr.Append("NULL,");
            if (newInvSyncID > 0) sqlstr.Append(newInvSyncID.ToString() + ","); else sqlstr.Append("NULL,");
            if (invhdr.CompID > 0) sqlstr.Append(invhdr.CompID.ToString() + ","); else sqlstr.Append("NULL,");
            if (invhdr.Branchid > 0) sqlstr.Append(invhdr.Branchid.ToString() + ","); else sqlstr.Append("NULL,");
            if (invhdr.Storeid > 0) sqlstr.Append(invhdr.Storeid.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(AppGeneralSettings.StoreName)) sqlstr.Append("'" + AppGeneralSettings.StoreName + "',"); else sqlstr.Append("NULL,");
            if (invhdr.CustomerID > 0) sqlstr.Append(invhdr.CustomerID.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(invhdr.CustomerTitle)) sqlstr.Append("'" + invhdr.CustomerTitle + "',"); else sqlstr.Append("NULL,");
            if (invhdr.InvStatus > 0) sqlstr.Append(invhdr.InvStatus.ToString() + ","); else sqlstr.Append("0,");
            if (!string.IsNullOrEmpty(invhdr.InvDate)) sqlstr.Append("CONVERT(DATETIME,'" + invhdr.InvDate + "',103),"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(invhdr.InvComments)) sqlstr.Append("'" + invhdr.InvComments + "'"); else sqlstr.Append("NULL");
            sqlstr.Append(")");
            rtrn = db.DBExecuteSQLCmd(sqlstr.ToString());

            if (rtrn > 0)
            {
                // newinvhdrid = db.DBGetNumResultFromSQLSelect("SELECT MAX(InvHdrID) AS NEWID FROM TInventoryHeader");
                return newinvhdrid;
            }
            else
                return -1;

        }


        public string InvHdrTitle(long InvHdrID)
        {
            return db.DBWmsExSelectCmdRStr2Str(" SELECT InvComments FROM TInventoryHeader WHERE InvHdrID=" + InvHdrID.ToString());
        }


        long UpdateRecord(InventoryHeader invhdr)
        {
            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;
            sqlstr.Append("UPDATE TInventoryHeader SET ");
            if (invhdr.Storeid > 0) sqlstr.Append("StoreID=" + invhdr.Storeid.ToString() + ","); else sqlstr.Append("StoreID=NULL,");
            if (invhdr.InvHdrIDServer > 0) sqlstr.Append("InvHdrIDServer=" + invhdr.InvHdrIDServer.ToString() + ","); else sqlstr.Append("InvHdrIDServer=NULL,");
            if (!string.IsNullOrEmpty(invhdr.StoreName)) sqlstr.Append("StoreName = '" + invhdr.StoreName + "',"); else sqlstr.Append("StoreName=NULL,");
            if (invhdr.InvStatus > 0) sqlstr.Append("InvStatus=" + invhdr.InvStatus.ToString() + ","); else sqlstr.Append("InvHdrIDServer=0,");
            if (!string.IsNullOrEmpty(invhdr.InvComments)) sqlstr.Append("InvComments = '" + invhdr.InvComments + "'"); else sqlstr.Append("InvComments=NULL");

            rtrn = db.DBExecuteSQLCmd(sqlstr.ToString());
            return rtrn;
        }


    }

    public class InventoryHandler
    {
        DB db = new DB();

        public string dberror;
        public MInventory InventoryRecord(long invid)
        {
            MInventory inv = new MInventory();
            try { inv = Parse(db.DBFillDataTable("SELECT * FROM TInventory WHERE InvID=" + invid.ToString(), "TINV").Rows[0]); }
            catch { }
            return inv;
        }


        public MInventory InventoryRecordOnline(long invid)
        {
            MInventory inv = new MInventory();
            try
            {
                DataTable dt = AppGeneralSettings.webServiceProvider.GetInventoryRecord(invid);
                inv = Parse(dt.Rows[0]);
            }
            catch { }
            return inv;
        }


        public MInventory Parse(DataRow Dr)
        {
            MInventory inv = new MInventory();

            try { inv.InvID = long.Parse(Dr["InvID"].ToString()); }
            catch { }
            try { inv.InvHdrID = long.Parse(Dr["InvHdrID"].ToString()); }
            catch { }
            try { inv.InvNo = long.Parse(Dr["InvNo"].ToString()); }
            catch { }
            try { inv.ItemID = long.Parse(Dr["ItemID"].ToString()); }
            catch { }
            try { inv.LotID = long.Parse(Dr["LotID"].ToString()); }
            catch { }
            try { inv.MUnitPrimary = short.Parse(Dr["InvMunitPrimary"].ToString()); }
            catch { }
            try { inv.MUnitSecondary = short.Parse(Dr["InvMunitSecondary"].ToString()); }
            catch { }
            try { inv.InvQty = decimal.Parse(Dr["InvQtyPrimary"].ToString()); }
            catch { }
            try { inv.InvQtySecondary = decimal.Parse(Dr["InvQtySecondary"].ToString()); }
            catch { }
            try { inv.ItemCode = Dr["ItemCode"].ToString(); }
            catch { }
            try { inv.LotCode = Dr["LotCode"].ToString(); }
            catch { }
            try { inv.InvDate = Dr["InvDate"].ToString(); }
            catch { }

            return inv;
        }

        public long UpdateInventory(MInventory inv)
        {
            if (!(inv.InvQty > 0)) return -1;

            if (db.DBGetNumResultFromSQLSelect("SELECT InvID FROM TInventory WHERE InvID=" + inv.InvID.ToString()) > 0)
                return UpdateRecord(inv);
            else
                return InsertRecord(inv);
        }

        public long UpdateInventoryOnline(MInventory inv)
        {

            TInventory invRecord = new TInventory();
            invRecord.BranchID = inv.BranchID;
            invRecord.CompID = inv.CompID;
            invRecord.ERPQty = inv.ERPQty;
            invRecord.InvDate = inv.InvDate;
            invRecord.InvHdrID = inv.InvHdrID;
            invRecord.InvHdrIDServer = inv.InvHdrID;
            invRecord.InvID = inv.InvID;
            invRecord.InvQty = inv.InvQty;
            invRecord.InvQtySecondary = inv.InvQtySecondary;
            invRecord.ItemCode = inv.ItemCode;
            invRecord.ItemID = inv.ItemID;
            invRecord.LotCode = inv.LotCode;
            invRecord.LotID = inv.LotID;
            invRecord.MUnitPrimary = inv.MUnitPrimary;
            invRecord.MUnitSecondary = inv.MUnitSecondary;
            invRecord.SerialNumber = inv.SerialNumber;
            invRecord.StoreID = inv.StoreID;

            var response = AppGeneralSettings.webServiceProvider.ImportInventoryOnline(invRecord);

            return response;
        }


        public DataTable InventoryView(long InvHdrID, string sterm, bool getlast10rec)
        {
            DataTable DT = new DataTable();
            string sqlstr = null;

            sqlstr = "SELECT InvID,invno,ItemID,ItemCode,LotCode,LotID,InvQtyPrimary,InvQtySecondary FROM TInventory WHERE InvHdrID=" + InvHdrID.ToString();
            if (!string.IsNullOrEmpty(sterm) && !getlast10rec) sqlstr += " AND (ItemCode LIKE '" + sterm + "%' OR LotCode LIKE '" + sterm + "%')";

            if (getlast10rec)
            {
                sqlstr = "SELECT TOP (10) InvID,ItemID,invno,ItemCode,LotCode,LotID,InvQtyPrimary,InvQtySecondary FROM TInventory WHERE InvHdrID=" + InvHdrID.ToString();
                sqlstr += " ORDER BY invno DESC";
                DT = db.DBFillDataTable(sqlstr, "DTINVENTORY");
            }
            else
            {
                sqlstr += " ORDER BY invno DESC";
                DT = db.DBFillDataTable(sqlstr, "DTINVENTORY");
            }

           
            //if(DT.Rows.Count > 0)
            //{
            //DataColumn idcolumn = new DataColumn();
            //idcolumn.DataType = System.Type.GetType("System.Int32");
            //idcolumn.ColumnName = "ID";
            //idcolumn.AutoIncrement = false;
            //DT.Columns.Add(idcolumn);

            //    for(int i=0;i<DT.Rows.Count;i++)
            //    {
            //        DT.Rows[i]["ID"] = i + 1;
                
            //    }
            //}
            
            return DT;
        }

        public DataTable InventoryViewOnline(long InvHdrID, string sterm, bool getlast10rec)
        {
            return AppGeneralSettings.webServiceProvider.GetInventoryRecords(InvHdrID, sterm, getlast10rec);
        }

        public long DeleteWholeInventory(long InvHdrID)
        {
            db.DBExecuteSQLCmd("DELETE FROM TInventory WHERE InvHdrID=" + InvHdrID.ToString());
            return db.DBExecuteSQLCmd("DELETE FROM TInventoryHeader WHERE InvHdrID=" + InvHdrID.ToString());
        }

        public string GetInventoryCounter(long InvHdrID)
        {

            return db.DBWmsExSelectCmdRN2String("select count(*) from tinventory where invhdrid =" + InvHdrID.ToString());
        
        }

        public InventoryInfo GetInventoryInfoOnline(long InvHdrID)
        {
            return AppGeneralSettings.webServiceProvider.GetInventoryInfo(InvHdrID);
        }

        public long DeleteRecord(long invid)
        {
            return db.DBExecuteSQLCmd("DELETE FROM TInventory WHERE InvID=" + invid.ToString());
        }

        public long DeleteRecordOnline(long invid)
        {
            return AppGeneralSettings.webServiceProvider.DeleteInventoryRecord(invid);
        }

        long InsertRecord(MInventory inv)
        {
            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;
            //sqlstr.Append("INSERT INTO TInventory(InvID,InvHdrID,ItemID,ItemCode,ItemCode,LotID,LotCode,InvQty) VALUES (");
            // if (inv.InvID > 0) sqlstr.Append(inv.InvID.ToString() + ","); else sqlstr.Append("NULL,"); auto
            sqlstr.Append("INSERT INTO TInventory(InvHdrID,ItemID,invno,ItemCode,LotID,LotCode,InvQtyPrimary,InvMunitPrimary,InvQtySecondary,InvMunitSecondary) VALUES (");
            if (inv.InvHdrID > 0) sqlstr.Append(inv.InvHdrID.ToString() + ","); else sqlstr.Append("NULL,");
            if (inv.ItemID > 0) sqlstr.Append(inv.ItemID.ToString() + ","); else sqlstr.Append("NULL,");
            if (inv.InvNo > 0) sqlstr.Append(inv.InvNo.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(inv.ItemCode)) sqlstr.Append("'" + inv.ItemCode + "',"); else sqlstr.Append("NULL,");
            if (inv.LotID > 0) sqlstr.Append(inv.LotID.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(inv.LotCode)) sqlstr.Append("'" + inv.LotCode + "',"); else sqlstr.Append("NULL,");
            if (inv.InvQty > 0) sqlstr.Append(inv.InvQty.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (inv.MUnitPrimary > 0) sqlstr.Append(inv.MUnitPrimary.ToString() + ","); else sqlstr.Append("NULL,");
            if (inv.InvQtySecondary > 0) sqlstr.Append(inv.InvQtySecondary.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (inv.MUnitSecondary > 0) sqlstr.Append(inv.MUnitSecondary.ToString()); else sqlstr.Append("NULL");
            sqlstr.Append(")");
            rtrn = db.DBExecuteSQLCmd(sqlstr.ToString());
            return rtrn;
        }

        long UpdateRecord(MInventory inv)
        {
            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;
            sqlstr.Append("UPDATE TInventory SET ");
            if (inv.ItemID > 0) sqlstr.Append("ItemID=" + inv.ItemID.ToString() + ","); else sqlstr.Append("ItemID=NULL,");
            if (inv.LotID > 0) sqlstr.Append("LotID=" + inv.LotID.ToString() + ","); else sqlstr.Append("LotID=NULL,");
            if (inv.InvQty > 0) sqlstr.Append("InvQtyPrimary=" + inv.InvQty.ToString().Replace(",", ".") + ","); else sqlstr.Append("InvQtyPrimary=NULL,");
            if (inv.MUnitPrimary > 0) sqlstr.Append("InvMunitPrimary=" + inv.MUnitPrimary.ToString() + ","); else sqlstr.Append("InvMunitPrimary=NULL,");
            if (inv.InvQtySecondary > 0) sqlstr.Append("InvQtySecondary=" + inv.InvQtySecondary.ToString().Replace(",", ".") + ","); else sqlstr.Append("InvQtySecondary=NULL,");
            if (inv.MUnitSecondary > 0) sqlstr.Append("InvMunitSecondary=" + inv.MUnitSecondary.ToString() + ","); else sqlstr.Append("InvMunitSecondary=NULL,");
            if (!string.IsNullOrEmpty(inv.ItemCode)) sqlstr.Append("ItemCode = '" + inv.ItemCode + "',"); else sqlstr.Append("ItemCode=NULL,");
            if (!string.IsNullOrEmpty(inv.LotCode)) sqlstr.Append("LotCode = '" + inv.LotCode + "'"); else sqlstr.Append("LotCode=NULL");

            sqlstr.Append(" WHERE InvID=" + inv.InvID.ToString());

            rtrn = db.DBExecuteSQLCmd(sqlstr.ToString());
            return rtrn;
        }

        public string InvHdrTitle(long InvHdrID)
        {
            return db.DBWmsExSelectCmdRStr2Str(" SELECT InvComments FROM TInventoryHeader WHERE InvHdrID=" + InvHdrID.ToString());
        }

    }

    public class ImportData
    {
        DB db = new DB();
        bool newDB;
        public WMSservice.SyncInfo GetSyncInfo()
        {
            WMSservice.SyncInfo syncinfo = new WMSservice.SyncInfo();

            try { syncinfo = AppGeneralSettings.webServiceProvider.GetSyncInfo(); }
            catch { }

            return syncinfo;
        }

        public bool FDownloadFile(string XMLFilename)
        {
            bool Bsuccess = false;
            HttpWebRequest Frequest = null;
            HttpWebResponse Fresponse = null;
            Stream FresponseStream = null;
            FileStream FFilestream = null;
             byte[] Fbuffer = new byte[1024];
            int BytesRead = 0;
            long TotalBytesRead = 0;
            string Filepath;
            string WSfile;
            string WSFileURL;

            Filepath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            WSfile = new System.IO.FileInfo(Filepath).DirectoryName + "\\" + XMLFilename;

            string WSRootPath = null;

            WSRootPath = AppGeneralSettings.webServiceProvider.Url.Replace("SOAReatailWMSMiniProvider.asmx", "");

            WSFileURL = WSRootPath + XMLFilename;

            if (string.IsNullOrEmpty(WSFileURL))
                return false;

            try
            {
                Frequest = (HttpWebRequest)WebRequest.Create(WSFileURL);
                Frequest.Method = "GET";
                Frequest.Timeout = 10000;
                Frequest.SendChunked = true;
                Frequest.TransferEncoding = "UTF-8";

                Fresponse = (HttpWebResponse)Frequest.GetResponse();

                FresponseStream = Fresponse.GetResponseStream();

            }
            catch (Exception ex) { Logger.Flog(">>On Get Response::" + ex.ToString()); return false; }

            try { if (File.Exists(WSfile)) File.Delete(WSfile); }
            catch { }

            try
            {
                do
                {
                    BytesRead = FresponseStream.Read(Fbuffer, 0, Fbuffer.Length);
                    if (BytesRead > 0) FFilestream.Write(Fbuffer, 0, BytesRead);
                    TotalBytesRead += BytesRead;
                } while (BytesRead > 0);
                Bsuccess = true;
            }
            catch (Exception ex) { Logger.Flog(">>On bytesread::" + ex.ToString()); Bsuccess = false; }

            //try 
            //{                
            //   FFilestream = File.Open(WSfile, FileMode.Create, FileAccess.Write, FileShare.None);
            //   BytesRead = FresponseStream.Read(Fbuffer, 0, MAXREAD);


            //   while ( BytesRead > 0)
            //   {
            //       if (BytesRead > 0)
            //       {
            //           FFilestream.Write(Fbuffer, 0, BytesRead);
            //           TotalBytesRead += BytesRead;
            //       }
            //       BytesRead = FresponseStream.Read(Fbuffer, 0, MAXREAD);
            //   }
            //   Bsuccess = true;                                
            //}
            //catch{Bsuccess =false;}


            if (File.Exists(WSfile))
            {
                if (!Bsuccess)
                {
                    try
                    {
                        FresponseStream.Close();
                        Fresponse.Close();
                        FFilestream.Close();
                        File.Delete(WSfile);
                    }
                    catch { }; return false;
                }
                else
                    try
                    {
                        FresponseStream.Close();
                        Fresponse.Close();
                        FFilestream.Close();
                    }
                    catch { }


            }

            return Bsuccess;
        }

        public bool HTTPDownloadFile(string XMLFilename)
        {

            return false;
        }

        public long GetStores()
        {
            DataSet Ds = new DataSet();
            Store store = new Store();
            StoreHandler storehandler = new StoreHandler();

            long raffected = 0;

            try { Ds = AppGeneralSettings.webServiceProvider.SOA_GetStores(); }
            catch { return -10; }


            if (Ds.Tables.Count > 0)
            {
                for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                {
                    store = storehandler.Parse(Ds.Tables[0].Rows[i]);
                    if (store.StoreID > 0)
                        raffected += storehandler.UpdateStore(store);
                }
            }
            return raffected;
        }

        public long GetMunits()
        {
            DataSet Ds = new DataSet();
            MUnits munit = new MUnits();
            MUnitHandler munithandler = new MUnitHandler();


            long raffected = 0;

            try { Ds = AppGeneralSettings.webServiceProvider.GetItemMunits(); }
            catch { return -10; }


            if (Ds.Tables.Count > 0)
            {
                for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                {
                    munit = munithandler.Parse(Ds.Tables[0].Rows[i]);
                    if (munit.MunitID > 0)
                        raffected += munithandler.UpdateMunit(munit);
                }
            }
            return raffected;
        }

        public long GetPartiallyItems(long startpoint, long endpoint)
        {
            ItemHandler itemhandler = new ItemHandler();
            long raffected = 0;

            ERPItem[] erpitemlist = null;
            AppGeneralSettings.webServiceProvider.Timeout = 500000;

            try { erpitemlist = AppGeneralSettings.webServiceProvider.SOA_GetItemsList(AppGeneralSettings.StoreID, startpoint, endpoint); }
            catch (Exception ex) { Logger.Flog(">>GetPartiallyItems.GetDataset():" + ex.ToString()); }


            raffected += itemhandler.DBImportItems(erpitemlist);

            return raffected;

        }
        
        public long GetPartiallyLots(long startpoint, long endpoint)
        {
            LotHandler lothandler = new LotHandler();

            DataSet Ds = new DataSet();
            long raffected = 0;
            ERPLot[] erplotlist = null;
            AppGeneralSettings.webServiceProvider.Timeout = 1000000;
            try { erplotlist = AppGeneralSettings.webServiceProvider.SOA_GetLotsList(AppGeneralSettings.StoreID, startpoint, endpoint); }
            catch (Exception ex) { Logger.Flog(">>GetPartiallyLots.GetDataset():" + ex.ToString()); }


            raffected += lothandler.DBImportLots(erplotlist);  

            return raffected;
        }

        public long ImportItemsData(WMSservice.SyncInfo syncinfo)
        {
            long raffected = 0;

            int step = 100;

            if (syncinfo.MaxItemID > (syncinfo.ItemsRowsCount + 10000))
            {
                step = 1000;
            }

            if (syncinfo.ItemsRowsCount > 0)
            {
                if (syncinfo.ItemsRowsCount > step)
                {
                    for (long k = syncinfo.MinItemID; k <= syncinfo.MaxItemID; k += step)
                    {
                        raffected += GetPartiallyItems(k, k + step);
                    }
                }
                else
                    raffected += GetPartiallyItems(0, 0); //all                                              
            }

            return raffected;
        }

        public long ImportsLotsData(WMSservice.SyncInfo syncinfo)
        {
            long raffected = 0;


            if (syncinfo.LotRowsCount > 0)
            {
                if (syncinfo.LotRowsCount > 500)
                {
                    for (long k = syncinfo.MinLotID; k <= syncinfo.MaxLotID; k += 500)
                    {
                        raffected += GetPartiallyLots(k, k + 500);
                    }
                }
                else
                    raffected += GetPartiallyLots(0, 0); //all     //TEST , MUST BE 0,0                                         
            }

            return raffected;
        }

        public long DELETEALLDATA() {

            db.DBWmsExSelectCmdRStr2Str("DELETE FROM TITEMS");
            db.DBWmsExSelectCmdRStr2Str("DELETE FROM TITEMLOT");
            return 1;
        }

        public long ClearGitemList()
        {
            AppGeneralSettings.webServiceProvider.Clearitemlist();
            return 1;
        }
      
        public long ClearGlotList()
        {
            AppGeneralSettings.webServiceProvider.Clearlotlist();
            return 1;
        }


    }

    public class SyncData
    {
        long rtn;
        bool newDB;
        DB db = new DB();

        public WMSSyncService.SyncInfo CreateItemData(bool fromselecteddate,string selecteddate)
        {
            string lastdate = "";
            string dblastdate = "";
            WMSSyncService.SyncInfo rtn = new WMSSyncService.SyncInfo();
            //AWAKE ORACLE
            try { AppGeneralSettings.WebSyncServiceProvider.AwakeDB(); }
            catch { }
            AppGeneralSettings.WebSyncServiceProvider.Timeout = 1000000;

            dblastdate = GetItemLastSyncDate();
                                 
            try  
            {
                if (dblastdate != "" && dblastdate != DBNull.Value.ToString() && dblastdate.ToUpper() != "NULL")  
                {
                    lastdate = dblastdate;
                }
                else
                {
                    lastdate = "null";
                }
                    AppGeneralSettings.WebSyncServiceProvider.AwakeDB(); 
                rtn = AppGeneralSettings.WebSyncServiceProvider.SOA_CreateItemData(AppGeneralSettings.StoreID, false, lastdate);
                AppGeneralSettings.WebSyncServiceProvider.Timeout = 150000;
                return rtn;
            
            }
            catch(Exception ex) {
                AppGeneralSettings.WebSyncServiceProvider.Timeout = 150000;
                return rtn;
            }
        }

        public WMSSyncService.SyncInfo CreateLotData(bool fromselecteddate, string selecteddate)
        {
            string dblastdate = "";
            string lastdate = "";
            WMSSyncService.SyncInfo rtn = new WMSSyncService.SyncInfo();
            AppGeneralSettings.WebSyncServiceProvider.Timeout = 1000000;
            dblastdate = GetLotLastSyncDate();
            try {
                if (dblastdate != "" && dblastdate != DBNull.Value.ToString() && dblastdate.ToUpper() != "NULL")  
                {
                    lastdate = dblastdate;
                }
                else
                {
                    lastdate = "null";
                }
            rtn = AppGeneralSettings.WebSyncServiceProvider.SOA_CreateLotData(AppGeneralSettings.StoreID, false, lastdate);
            AppGeneralSettings.WebSyncServiceProvider.Timeout = 150000;
            return rtn;
               
            }
            catch {
                
                AppGeneralSettings.WebSyncServiceProvider.Timeout = 150000;
                return rtn; }
        }



        public long SyncLots()
        {

            LotHandler lothandler = new LotHandler();
            DataSet Ds = new DataSet();
            long raffected = 0;


            WMSSyncService.SyncLot[] erplotlist = null;
            try { erplotlist = AppGeneralSettings.WebSyncServiceProvider.SOA_GetNewLots(AppGeneralSettings.StoreID, GetLotLastSyncDate()); }
            catch (Exception ex) { Logger.Flog(">>GetPartiallyItems.GetDataset():" + ex.ToString()); }

            try
            {
                if (erplotlist.Length <= 0) return 0;
            }
            catch 
            {
                return 0;
            }


                for (int i = 0; i < erplotlist.Length; i++)
                {
                    raffected += lothandler.DBSyncLots(erplotlist);
                    CoreTools.SystemIdleTimerReset();
                }

            return raffected;
        }

        public string GetItemLastSyncDate()
        {
            return db.DBWmsExSelectCmdRStr2Str("SELECT CONVERT(nvarchar(10),MAX(ENTRYDATE),103) as ENTRYDATE FROM TItems");
        }

        public string GetLotLastSyncDate()
        {
            return db.DBWmsExSelectCmdRStr2Str("SELECT CONVERT(nvarchar(10),MAX(ENTRYDATE),103) as ENTRYDATE FROM TitemLOT");
        }

        public long ItemsInMobDB()
        {

            return db.DBGetNumResultFromSQLSelect("SELECT count(*) FROM TItems");
        }

        public long GetPartiallyItems(long startpoint, long endpoint)
        {
            ItemHandler itemhandler = new ItemHandler();
            long raffected = 0;

            SyncERPItem[] erpitemlist = null;

            try { erpitemlist = AppGeneralSettings.WebSyncServiceProvider.SOA_GetItemsList(AppGeneralSettings.StoreID,AppGeneralSettings.BranchID,startpoint,endpoint); }
            catch (Exception ex) { Logger.Flog(">>GetPartiallyItems.GetDataset():" + ex.ToString()); }
            
            raffected += itemhandler.DBSyncItems(erpitemlist);

            return raffected;

        }



        public long ClearGLotList() 
        {

            try { AppGeneralSettings.WebSyncServiceProvider.ClearGLotList(AppGeneralSettings.BranchID); }
            catch (Exception ex) { Logger.Flog(">> ClearGLotList" + ex.ToString()); }
          
            return 1;
        
        }

        public long ClearGitemlist()
        {

            try { AppGeneralSettings.WebSyncServiceProvider.ClearGItemList(AppGeneralSettings.BranchID); }
            catch (Exception ex) { Logger.Flog(">> ClearGLotList" + ex.ToString()); }

            return 1;

        }

        public long GetPartiallyLots(long startpoint, long endpoint)
        {
            LotHandler lothandler = new LotHandler();

            DataSet Ds = new DataSet();
            long raffected = 0;
            SyncLot[] erplotlist = null;

            try { erplotlist = AppGeneralSettings.WebSyncServiceProvider.SOA_GetLotsList(AppGeneralSettings.StoreID, AppGeneralSettings.BranchID, startpoint, endpoint); }
            catch (Exception ex) { Logger.Flog(">>GetPartiallyLots.GetDataset():" + ex.ToString()); }


            raffected += lothandler.DBSyncLots(erplotlist);

            return raffected;
        }


    }


    public class OrderHandler
    {
        DB db = new DB();
        public DataTable OrderLists()
        {
            return db.DBFillDataTable("SELECT * FROM Torders WHERE OrderStatus <> 1 ORDER BY OrderID DESC", "DTORDERLIST");
        }

        public Order Parse(DataRow Dr)
        {
            Order myOrder = new Order();

            try { myOrder.OrderID = long.Parse(Dr["OrderID"].ToString()); }
            catch { }
            try { myOrder.CompID = short.Parse(Dr["CompID"].ToString()); }
            catch { }
            try { myOrder.Branchid = short.Parse(Dr["BranchID"].ToString()); }
            catch { }
            try { myOrder.Storeid = short.Parse(Dr["Storeid"].ToString()); }
            catch { }
            try { myOrder.StoreName = Dr["StoreName"].ToString(); }
            catch { }
            try { myOrder.CustomerID = short.Parse(Dr["CustomerID"].ToString()); }
            catch { }
            try { myOrder.OrderComments = Dr["OrderComments"].ToString(); }
            catch { }
            try { myOrder.OrderDate = Dr["OrderDate"].ToString(); }
            catch { }
            try { myOrder.OrderSyncID = long.Parse(Dr["OrderSyncID"].ToString()); }
            catch { }
            try { myOrder.SalesPersonID = long.Parse(Dr["SalesPersonID"].ToString()); }
            catch { }
            try { myOrder.OrderStatus = short.Parse(Dr["OrderStatus"].ToString()); }
            catch { }
            try { myOrder.OrderCode = Dr["OrderCode"].ToString(); }
            catch { }

            return myOrder;
        }

        public long UpdateOrder(Order myorder)
        {
            if (myorder.OrderID > 0 && db.DBGetNumResultFromSQLSelect("SELECT OrderID FROM Torders WHERE OrderID=" + myorder.OrderID.ToString()) > 0)
                return UpdateRecord(myorder);
            else
                return InsertRecord(myorder);
        }

        long InsertRecord(Order myOrder)
        {
            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;


            long newOrderID = 0;
            newOrderID = db.DBGetNumResultFromSQLSelect("SELECT (MAX(OrderID) + 1)  AS NEWID FROM TOrders");
            if (!(newOrderID > 0)) newOrderID = 1;


            Random randnum = new Random();
            long newOrdSyncID = randnum.Next(1000, 100000);

            //TOrders
            sqlstr.Append("INSERT INTO TOrders(OrderID,CompID,BranchID,StoreID,OrderCode,StoreName,CustomerID,CustomerTitle,OrderDate,SalesPersonID,OrderStatus,OrderSyncID,OrderComments) VALUES (");
            if (newOrderID > 0) sqlstr.Append(newOrderID.ToString() + ","); else sqlstr.Append("NULL,");
            if (myOrder.CompID > 0) sqlstr.Append(myOrder.CompID.ToString() + ","); else sqlstr.Append("NULL,");
            if (myOrder.Branchid > 0) sqlstr.Append(myOrder.Branchid.ToString() + ","); else sqlstr.Append("NULL,");
            if (myOrder.Storeid > 0) sqlstr.Append(myOrder.Storeid.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(myOrder.OrderCode)) sqlstr.Append("'" + myOrder.OrderCode + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(myOrder.StoreName)) sqlstr.Append("'" + myOrder.StoreName + "',"); else sqlstr.Append("NULL,");
            if (myOrder.CustomerID > 0) sqlstr.Append(myOrder.CustomerID.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(myOrder.CustomerTitle)) sqlstr.Append("'" + myOrder.CustomerTitle + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(myOrder.OrderDate)) sqlstr.Append("CONVERT(DATETIME,'" + myOrder.OrderDate + "',103),"); else sqlstr.Append("NULL,");
            if (myOrder.SalesPersonID > 0) sqlstr.Append(myOrder.SalesPersonID.ToString() + ","); else sqlstr.Append("NULL,");
            if (myOrder.OrderStatus > 0) sqlstr.Append(myOrder.OrderStatus.ToString() + ","); else sqlstr.Append("0,");
            if (myOrder.OrderSyncID > 0) sqlstr.Append(myOrder.OrderSyncID.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(myOrder.OrderComments)) sqlstr.Append("'" + myOrder.OrderComments + "'"); else sqlstr.Append("NULL");
            sqlstr.Append(")");
            rtrn = db.DBExecuteSQLCmd(sqlstr.ToString());

            if (rtrn > 0)
            {
                // newinvhdrid = db.DBGetNumResultFromSQLSelect("SELECT MAX(InvHdrID) AS NEWID FROM TInventoryHeader");
                return newOrderID;
            }
            else
                return -1;

        }

        long UpdateRecord(Order myOrder)
        {
            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;
            sqlstr.Append("UPDATE TOrders SET ");
            if (myOrder.Storeid > 0) sqlstr.Append("StoreID=" + myOrder.Storeid.ToString() + ","); else sqlstr.Append("StoreID=NULL,");
            if (!string.IsNullOrEmpty(myOrder.StoreName)) sqlstr.Append("StoreName = '" + myOrder.StoreName + "',"); else sqlstr.Append("StoreName=NULL,");
            if (myOrder.OrderStatus > 0) sqlstr.Append("OrderStatus=" + myOrder.OrderStatus.ToString() + ","); else sqlstr.Append("OrderStatus=0,");
            if (!string.IsNullOrEmpty(myOrder.OrderComments)) sqlstr.Append("OrderComments = '" + myOrder.OrderComments + "'"); else sqlstr.Append("OrderComments=NULL");

            rtrn = db.DBExecuteSQLCmd(sqlstr.ToString());
            return rtrn;
        }


    }


    public class ExportData
    {
        DB db = new DB();


        public long ExportPackingListHeader(long PackingListHeaderID)
        {

            DataSet Ds = new DataSet();
            long rtrn = 0;

            PackingListHeader PackHeader = new PackingListHeader();

            string sqlstr = "SELECT * FROM TWMSPACKINGLISTSHEADER WHERE PackingListHeaderID=" + PackingListHeaderID.ToString();
            Ds = db.DBFillDataset(sqlstr, "PACKLISTHDR");


            if (Ds.Tables[0].Rows.Count > 0)
            {
                PackHeader.StoreID = (short)AppGeneralSettings.StoreID;

                PackHeader.Compid = (short)AppGeneralSettings.CompID;
                PackHeader.Branchid = (short)AppGeneralSettings.BranchID;
                PackHeader.Packinglistcomments = Ds.Tables[0].Rows[0]["PackingListComments"].ToString();
                PackHeader.StoreName = Ds.Tables[0].Rows[0]["StoreName"].ToString();
                PackHeader.CustomerCode = Ds.Tables[0].Rows[0]["CustomerCode"].ToString();
                PackHeader.TransCode = AppGeneralSettings.TransCode;
                if (!String.IsNullOrEmpty(Ds.Tables[0].Rows[0]["TransType"].ToString()))
                {
                    PackHeader.TransType = (int)Ds.Tables[0].Rows[0]["TransType"];
                }

                PackHeader.Kindid = AppGeneralSettings.KindID;
                PackHeader.PackingListDate = Ds.Tables[0].Rows[0]["PackingListDate"].ToString();
                PackHeader.StoreID = (int)Ds.Tables[0].Rows[0]["StoreID"];
                try { PackHeader.DSRID = long.Parse(Ds.Tables[0].Rows[0]["Dsrid"].ToString()); }
                catch (Exception ex) { }


                try
                {
                    rtrn = AppGeneralSettings.WebSyncServiceProvider.ImportPackingListHeaderCType(PackHeader);
                }
                catch (Exception ex) { Logger.Flog("ExportData.ExportInventoryHeader>>" + ex.ToString()); rtrn = -10; }

                if (rtrn > 0)
                {
                    return rtrn;
                }
                else
                {
                    Logger.Flog("PACKING LIST EXPORT FAILED  " + sqlstr);
                    return -1;
                }


            }
            else
            {

                Logger.Flog("ExportData.ExportPackingListHeader>> No Data found " + sqlstr);
                return -5;
            }

        }


        public long ExportPackingList(long PackingListHeaderID, long ServerPackingListHeaderID)
        {
            long minid = 0, maxid = 0;
            long rset = 0;
            long raffected = 0;

            DataTable DT = new DataTable();
            DataSet Ds = new DataSet();

            PackingListDetail[] SOAPack;

            string sqlstr = null;

            sqlstr = "SELECT MIN(PackingListDTLID) AS MINPACKID,MAX(PackingListDTLID) AS MAXPACKID FROM TWMSPackingListDetails WHERE PackingListHeaderID=" + PackingListHeaderID.ToString();

            DT = db.DBFillDataTable(sqlstr, "DTMINMAX");
            //InvHdrIDServer
            if (DT.Rows.Count > 0)
            {
                try { minid = long.Parse(DT.Rows[0]["MINPACKID"].ToString()); }
                catch { }
                try { maxid = long.Parse(DT.Rows[0]["MAXPACKID"].ToString()); }
                catch { }

                for (long i = minid; i <= maxid; i += 50)
                {
                    rset = i + 50;
                    sqlstr = "SELECT LOTCODE as ITEMCODE,ItemQtyPrimary,PACKINGLISTHEADERID,ItemQtySecondary,Width,Length,Color,Draft,ItemID,LotID FROM TWMSPackingListDetails WHERE PackingListHeaderID=" + PackingListHeaderID.ToString() + " AND PackingListDTLID BETWEEN " + i.ToString() + " AND " + rset.ToString();
                    Ds = db.DBFillDataset(sqlstr, "DSPACKINGLISTDETAIL");

                    try
                    {
                        if (Ds.Tables[0].Rows.Count > 0)
                        {
                            //   raffected = AppGeneralSettings.webServiceProvider.SOA_ImportInventory(Ds);
                            SOAPack = new PackingListDetail[Ds.Tables[0].Rows.Count];
                            for (int j = 0; j < Ds.Tables[0].Rows.Count; j++)
                            {
                                SOAPack[j] = new PackingListDetail();
                                // SOAInv[j].InvHdrID = invhdrid;
                                // SOAInv[j].InvHdrIDServer = ServerInvHdrID;
                                //  try { SOAPack[j].PackingListDTLID = long.Parse(Ds.Tables[0].Rows[j]["PackingListDTLID"].ToString()); }
                                // catch { }
                                try { SOAPack[j].PackingListHeaderID = ServerPackingListHeaderID; }// long.Parse(Ds.Tables[0].Rows[j]["PackingListHeaderID"].ToString()); }
                                catch { }
                                try { SOAPack[j].ItemID = long.Parse(Ds.Tables[0].Rows[j]["ItemID"].ToString()); }
                                catch { }
                                try { SOAPack[j].ItemCode = Ds.Tables[0].Rows[j]["ItemCode"].ToString(); }
                                catch { }
                                try { SOAPack[j].LotID = long.Parse(Ds.Tables[0].Rows[j]["LotID"].ToString()); }
                                catch { }
                                //   try { SOAPack[j].LotCode = Ds.Tables[0].Rows[j]["LotCode"].ToString(); }
                                //   catch { }
                                try { SOAPack[j].ItemQTYprimary = decimal.Parse(Ds.Tables[0].Rows[j]["ItemQTYprimary"].ToString()); }
                                catch { }
                                try { SOAPack[j].ItemQTYsecondary = decimal.Parse(Ds.Tables[0].Rows[j]["ItemQTYsecondary"].ToString()); }
                                catch { }
                                try { SOAPack[j].Width = decimal.Parse(Ds.Tables[0].Rows[j]["Width"].ToString()); }
                                catch { }
                                try { SOAPack[j].Length = decimal.Parse(Ds.Tables[0].Rows[j]["Length"].ToString()); }
                                catch { }
                                try { SOAPack[j].Color = Ds.Tables[0].Rows[j]["Color"].ToString(); }
                                catch { }
                                try { SOAPack[j].Draft = Ds.Tables[0].Rows[j]["Draft"].ToString(); }
                                catch { }

                                //    try { SOAPack[j].ItemMunitPrimary = int.Parse(Ds.Tables[0].Rows[j]["ItemMunitPrimary"].ToString()); }
                                //    catch { }
                                //   try { SOAPack[j].ItemMunitSecondary = int.Parse(Ds.Tables[0].Rows[j]["ItemMunitSecondary"].ToString()); }
                                //   catch { }
                            }
                            raffected += AppGeneralSettings.WebSyncServiceProvider.ImportPackingListCType(SOAPack);
                        }
                        else
                            Logger.Flog("ExportData.ExportInventory>>" + sqlstr);
                    }
                    catch (Exception ex) { Logger.Flog("ExportData.ExportInventory>>" + ex.ToString()); }

                }

                return raffected;
            }
            else
                return -1;

        }



        public ResultWithMessage ExportInventoryHeader(long invhdrid)
        {

            ResultWithMessage rsmsg = new ResultWithMessage();
            DataSet Ds = new DataSet();

            TInventoryHeader SOAInvHdr = new TInventoryHeader();



            string sqlstr = "SELECT * FROM TInventoryHeader WHERE InvHdrID=" + invhdrid.ToString();
            Ds = db.DBFillDataset(sqlstr, "SAINVHDR");


            try
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    SOAInvHdr.MobInvHdrID = long.Parse(Ds.Tables[0].Rows[0]["InvHdrID"].ToString());

                    if (!(Ds.Tables[0].Rows[0]["InvHdrIDServer"] == DBNull.Value))
                    {
                        SOAInvHdr.InvHdrID = long.Parse(Ds.Tables[0].Rows[0]["InvHdrIDServer"].ToString());
                    }
                    SOAInvHdr.InvSyncID = long.Parse(Ds.Tables[0].Rows[0]["InvSyncID"].ToString());
                    SOAInvHdr.InvComments = Ds.Tables[0].Rows[0]["InvComments"].ToString();

                    SOAInvHdr.CompID = (short)AppGeneralSettings.CompID;
                    SOAInvHdr.Branchid = (short)AppGeneralSettings.BranchID;
                    SOAInvHdr.Storeid = AppGeneralSettings.StoreID;
                    SOAInvHdr.StoreName = AppGeneralSettings.StoreName;

                    rsmsg = AppGeneralSettings.webServiceProvider.UploadInventoryStatusCheck(SOAInvHdr);
                    //////////27-11-2015/////////////
                    if (!rsmsg.posresult) { return rsmsg; }
                    /////////////////////////////////
                    rsmsg.resultno = AppGeneralSettings.webServiceProvider.ImportInventoryHeaderCType(SOAInvHdr);
                    rsmsg.posresult = true;

                    if (rsmsg.resultno == -5)
                    {
                        rsmsg.resultno = MigrateInventory(invhdrid);
                    }

                    if ((rsmsg.resultno > 0) && (SOAInvHdr.InvHdrID == 0))
                    {
                        try { db.DBExecuteSQLCmd("UPDATE TInventoryHeader set InvHdrIDServer = " + rsmsg.resultno.ToString() + " WHERE InvHdrID=" + SOAInvHdr.MobInvHdrID.ToString()); }
                        catch { }

                    }
                }
                else
                    Logger.Flog("ExportData.ExportInventoryHeader>> No Data found " + sqlstr);
            }
            catch (Exception ex) { Logger.Flog("ExportData.ExportInventoryHeader>>" + ex.ToString()); rsmsg.resultno = -10; }
            return rsmsg;
        }

        protected long MigrateInventory(long invhdrid)
        {
            //GET INVENTORYHEADER RECORD
            DB mydb = new DB();
            long rtrn = 0;

            InventoryHeader thisinvheader = new InventoryHeader();
            TInventoryHeader targetinvheader = new TInventoryHeader();
            InventoryHeaderHandler invhandler = new InventoryHeaderHandler();
            thisinvheader = invhandler.InventoryHeaderRecord(invhdrid);

            targetinvheader.InvComments = thisinvheader.InvComments;
            targetinvheader.Branchid = thisinvheader.Branchid;
            targetinvheader.MobInvHdrID = thisinvheader.InvHdrID;
            targetinvheader.CompID = thisinvheader.CompID;
            targetinvheader.Storeid = thisinvheader.Storeid;
            targetinvheader.InvDate = thisinvheader.InvDate;
            targetinvheader.StoreName = AppGeneralSettings.StoreName;

            rtrn = AppGeneralSettings.webServiceProvider.ImportInventoryHeaderCType(targetinvheader);

            if (rtrn > 0)
            {
                try { db.DBExecuteSQLCmd("UPDATE TInventoryHeader set InvHdrIDServer = " + rtrn.ToString() + " WHERE InvHdrID=" + invhdrid.ToString()); }
                catch { }
            }

            return rtrn;





        }

        public long ExportInventory(long invhdrid, long ServerInvHdrID)
        {
            ItemHandler itemhandler = new ItemHandler();
            LotHandler lothandler = new LotHandler();
            WMSservice.ERPItem newitem = new WMSservice.ERPItem();
            WMSservice.ERPLot newlot = new WMSservice.ERPLot();
            long minid = 0, maxid = 0;
            long rset = 0;
            long raffected = 0;
            bool clearprevious = true;

            DataTable DT = new DataTable();
            DataSet Ds = new DataSet();
            
            TInventory[] SOAInv;

            string sqlstr = null;

            sqlstr = "SELECT MIN(InvID) AS MININVID,MAX(InvID) AS MAXINVID FROM TInventory WHERE InvHdrID=" + invhdrid.ToString();

            DT = db.DBFillDataTable(sqlstr, "DTMINMAX");
            //InvHdrIDServer
            if (DT.Rows.Count > 0)
            {
                try { minid = long.Parse(DT.Rows[0]["MININVID"].ToString()); }
                catch { }
                try { maxid = long.Parse(DT.Rows[0]["MAXINVID"].ToString()); }
                catch { }

                for (long i = minid; i <= maxid; i += 50)
                {
                    
                    rset = i + 50;
                    sqlstr = "SELECT * FROM TInventory WHERE InvHdrID=" + invhdrid.ToString() + " AND InvID BETWEEN " + i.ToString() + " AND " + rset.ToString();
                    Ds = db.DBFillDataset(sqlstr, "DSINVENTORY");

                    if (Ds.Tables[0].Rows.Count > 0)
                        {
                            //   raffected = AppGeneralSettings.webServiceProvider.SOA_ImportInventory(Ds);
                            SOAInv = new TInventory[Ds.Tables[0].Rows.Count];
                            for (int j = 0; j < Ds.Tables[0].Rows.Count; j++)
                            {
                                SOAInv[j] = new TInventory();
                                SOAInv[j].InvHdrID = invhdrid;
                                SOAInv[j].InvHdrIDServer = ServerInvHdrID;
                                try { SOAInv[j].InvID = long.Parse(Ds.Tables[0].Rows[j]["InvID"].ToString()); }
                                catch { }
                                try { SOAInv[j].ItemID = long.Parse(Ds.Tables[0].Rows[j]["ItemID"].ToString()); }
                                catch { }
                                try { SOAInv[j].LotID = long.Parse(Ds.Tables[0].Rows[j]["LotID"].ToString()); }
                                catch { }
                                try { SOAInv[j].LotCode = Ds.Tables[0].Rows[j]["LotCode"].ToString(); }
                                catch { }
                                //FIND IF ITEM EXISTS IN LOCALDB

                                try
                                {
                                    if (AppGeneralSettings.webServiceProvider.ItemExists(SOAInv[j].ItemID) < 0){
                                    //Item not exists, insert new item
                                        newitem = itemhandler.ParseItemToERPitem(itemhandler.ItemByID(SOAInv[j].ItemID));
                                        AppGeneralSettings.webServiceProvider.InsertNewItem(newitem);};
                                }
                                catch(Exception ex) { }


                                try
                                {
                                    if (AppGeneralSettings.webServiceProvider.LotExists(SOAInv[j].LotID) < 0)
                                    {//Lot not exists, insert new Lot
                                        newlot = lothandler.ParseLotToERPlot(lothandler.LotByID(SOAInv[j].LotID));
                                        AppGeneralSettings.webServiceProvider.InsertNewLot(newlot);};
                                }
                                catch (Exception ex) { }

                                //
                                try { SOAInv[j].CompID = short.Parse(Ds.Tables[0].Rows[j]["CompID"].ToString()); }
                                catch { }
                                try { SOAInv[j].BranchID = short.Parse(Ds.Tables[0].Rows[j]["BranchID"].ToString()); }
                                catch { }
                                try { SOAInv[j].ItemCode = Ds.Tables[0].Rows[j]["ItemCode"].ToString(); }
                                catch { }


                                try { SOAInv[j].InvQty = decimal.Parse(Ds.Tables[0].Rows[j]["InvQtyPrimary"].ToString()); }
                                catch { }
                                try { SOAInv[j].MUnitPrimary = short.Parse(Ds.Tables[0].Rows[j]["InvMunitPrimary"].ToString()); }
                                catch { }
                                try { SOAInv[j].InvQtySecondary = decimal.Parse(Ds.Tables[0].Rows[j]["InvQtySecondary"].ToString()); }
                                catch { }
                                try { SOAInv[j].MUnitSecondary = short.Parse(Ds.Tables[0].Rows[j]["InvMunitSecondary"].ToString()); }
                                catch { }
                                SOAInv[j].StoreID = AppGeneralSettings.StoreID;
                            }
                            raffected += AppGeneralSettings.webServiceProvider.ImportInventoryCType(SOAInv, clearprevious);
                            clearprevious = false;
                        }
                        else
                            Logger.Flog("ExportData.ExportInventory>>" + sqlstr);

                }

                return raffected;
            }
            else
                return -1;

        }


    }



    public class ReceivesController
    {
        long rtn;
        WMSservice.TransCodeDetail lot;

        public WMSservice.TransCodeDetail GetLotDetails(string zbarcode) 
        {
         lot = new WMSMobileClient.WMSservice.TransCodeDetail();
        try
        {
          lot   =  AppGeneralSettings.webServiceProvider.GetTransCodeDetails(zbarcode);

        }catch{}
           
        return lot;
        
        }

        public long GetTransCodewDetails(long ftrid) 
        {
            rtn = 0;
            WMSSyncService.TransCodeHeader hdr;
            List<WMSSyncService.TransCodeDetail> ldtl = new List<WMSSyncService.TransCodeDetail>();
            try 
            {  
                hdr = AppGeneralSettings.WebSyncServiceProvider.GetTransCodeHeader(ftrid);
                hdr.branchid = AppGeneralSettings.BranchID;
            
            }
            catch (Exception ex)
            {
                AppGeneralSettings.errortmsg = " GetTransCodeHeader " + ex.Message;
                return -1;
            
            }

            try
            {
                rtn = AppGeneralSettings.webServiceProvider.InsertNewReceivesTranscode(ParseTransCodeHeader(hdr));
            }
            catch(Exception ex)
            {
                AppGeneralSettings.errortmsg = "InsertTransCodeHeader " + ex.Message;
                return -1;
            
            }

            //Get header
            try 
            {
                if (rtn > 0) ldtl = new List<WMSSyncService.TransCodeDetail>(AppGeneralSettings.WebSyncServiceProvider.GetTransCodeDetails(ftrid));
           
            
            }
            catch(Exception ex) 
            {
                AppGeneralSettings.errortmsg = "GetTransCodeDetails  " + ex.Message;
                return -1;
            }

            try 
            {

                if (ldtl.Count > 0)
                {
                    AppGeneralSettings.webServiceProvider.InsertTransCodeDetails(ParseTransCodeDetailsList(ldtl).ToArray());
                    AppGeneralSettings.successmsg = "Επιτυχής καταχώρηση ( 1 / "+ldtl.Count.ToString()+")";
                    return 1;

                }
                else 
                {
                  AppGeneralSettings.errortmsg = " Το παραστατικό έχει ήδη καταχωρηθεί !";
                  return -1;
                }

            }
            catch(Exception ex)
            {
                AppGeneralSettings.errortmsg = "  InsertTransCodeDetails " + ex.Message; ;
                return -1;
            }


        }


   

        public List<WMSservice.TransCodeDetail> ParseTransCodeDetailsList(List<WMSSyncService.TransCodeDetail> tdtl) 
        {
            List<WMSservice.TransCodeDetail> lnewdtl = new List<WMSMobileClient.WMSservice.TransCodeDetail>();

            foreach (WMSSyncService.TransCodeDetail t in tdtl) 
            {
                lnewdtl.Add(ParseTransCodeDetails(t));
            
            
            }

            return lnewdtl;
        
        
        }

        public DataTable InsertNewReceiveDTL(WMSservice.TransCodeDetail tdtl) 
        {
            return AppGeneralSettings.webServiceProvider.InsertIntoReceives(tdtl);
        
        }



        public WMSservice.TransCodeDetail ParseTransCodeDetails(WMSSyncService.TransCodeDetail tdtl) 
        {

            WMSservice.TransCodeDetail newdtl = new WMSMobileClient.WMSservice.TransCodeDetail();

            newdtl.FtrID = tdtl.FtrID;
            newdtl.ReceiveID = tdtl.ReceiveID;
            newdtl.itemcode = tdtl.itemcode;
            newdtl.itemdesc = tdtl.itemdesc;
            newdtl.ItemID = tdtl.ItemID;
            newdtl.ItemQtyPrimary = tdtl.ItemQtyPrimary;
            newdtl.ItemQtySecondary = tdtl.ItemQtySecondary;
            newdtl.lotcode = tdtl.lotcode;
            newdtl.LotID  = tdtl.LotID;
            newdtl.munitprimary = tdtl.munitprimary;
            newdtl.munitsecondary = tdtl.munitsecondary;
            newdtl.ReceiveID = tdtl.ReceiveID;
            newdtl.TransID = tdtl.TransID;
            newdtl.zcolor = tdtl.zcolor;
            newdtl.zdraft = tdtl.zdraft;
            newdtl.Zlength = tdtl.Zlength;
            newdtl.Zwidth = tdtl.Zwidth;

            return newdtl;

        
        }

        
        public WMSservice.TransCodeHeader ParseTransCodeHeader(WMSSyncService.TransCodeHeader h) 
        {
            WMSservice.TransCodeHeader hdr = new WMSservice.TransCodeHeader();
            hdr.branchid = h.branchid;
            hdr.branchname = h.branchname;
            hdr.compid = h.compid;
            hdr.fromstoreid = h.fromstoreid;
            hdr.FtrID = h.FtrID;
            hdr.tostoreid = h.tostoreid;
            hdr.tradecode = h.tradecode;
            hdr.transdate = h.transdate;


            return hdr;
        
        
        }


        public DataTable GetTransRemains(long ftrid) 
        {
            return AppGeneralSettings.webServiceProvider.GetTransRemains(ftrid);
        
        }

        public DataTable GetOpenReceives() 
        {
            return AppGeneralSettings.webServiceProvider.GetOpenReceivesList(AppGeneralSettings.BranchID); }
      }
    
    }

    public static class CoreTools
    {
        [DllImport("coredll.dll")]
        public static extern void SystemIdleTimerReset();
    }


