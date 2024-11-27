using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using WMSSyncService;


namespace WMSSyncService
{


    public class SyncERPItem
    {
        public long ItemID { get; set; }
        public short CompID { get; set; }
        public string ItemCode { get; set; }
        public string ItemDesc { get; set; }
        public int MUnitPrimary { get; set; }
        public int MUnitSecondary { get; set; }
        public decimal MUnitsRelation { get; set; }
        public string MUnitDesc1 { get; set; }
        public string MUnitDesc2 { get; set; }
        public string EntryDate { get; set; }
    }

    public class SyncLot
    {
        public long LotID { get; set; }
        public long ItemID { get; set; }
        public short CompID { get; set; }
        public string LotCode { get; set; }
        public decimal Width { get; set; }
        public decimal Length { get; set; }
        public string Draft { get; set; }
        public string Color { get; set; }
        public decimal ItemPrimaryQty { get; set; }
        public decimal ItemSecondaryQty { get; set; }
        public string EntryDate { get; set; }
    }

    public class SyncInfo
    {
        long minitemid; long maxitemid;
        long minitemrowid; long maxitemrowid;
        long itemsrowscount;
        long minlotid; long maxlotid;
        long minlotrowid; long maxlotrowid;
        long lotrowscount;
        string comments;

        public long MinItemID
        {
            get { return minitemid; }
            set {minitemid = value;}
        }
        public long MaxItemID
        {
            get { return maxitemid; }
            set { maxitemid = value; }
        }
        public long MinItemRowid
        {
            get { return minitemrowid; }
            set { minitemrowid = value; }
        }
        public long MaxItemRowid
        {
            get { return maxitemrowid; }
            set { maxitemrowid = value; }
        }
        public long ItemsRowsCount
        {
            get { return itemsrowscount; }
            set { itemsrowscount = value; }
        }

        public long MinLotID
        {
            get { return minlotid; }
            set { minlotid = value; }
        }
        public long MaxLotID
        {
            get { return maxlotid; }
            set { maxlotid = value; }
        }
        public long MinLotRowID
        {
            get { return minlotrowid; }
            set { minlotrowid = value; }
        }
        public long MaxLotRowID
        {
            get { return maxlotrowid; }
            set { maxlotrowid = value; }
        }


        public long LotRowsCount
        {
            get { return lotrowscount; }
            set { lotrowscount = value; }
        }
        public string Comments
        {
            get { return comments; }
            set { comments = value; }
        }
    }

    public class ERPMunit
    {
        public int munitID { get; set; }
        public short compID { get; set; }
        public string mUnit { get; set; }
        public short munitDecimals { get; set; }
    }

    public class ERPStore
    {
        public short storeID { get;set; }
        public string storeName { get; set; }
        public short compID {get;set;}
    }

    public class SyncData
    {
        OraDB db = new OraDB();
        public SyncInfo syncinfo = new SyncInfo();

        public SyncData() { }

        public int DBConnectionState()
        {
            int rtrn = 0;
            try { rtrn = int.Parse(db.DBWmsExSelectCmdRN2String("SELECT 1 FROM DUAL")); }
            catch { }
            return rtrn;
        }

        public List<ERPStore> FGetStores(short compid)
        {
            DataTable DT = new DataTable();
            StringBuilder sqlstr = new StringBuilder();
            List<ERPStore> stores = new List<ERPStore>();


            sqlstr.Append("SELECT  COMID AS COMPID,CODEID AS STOREID,NAME AS STORENAME   FROM STORE");
            if (compid > 0) sqlstr.Append(" WHERE COMID=" + compid.ToString());

            sqlstr.Append(" ORDER BY NAME");

            DT= db.DBFillDataTable(sqlstr.ToString(),"DSSTORE");

            if (DT.Rows.Count > 0)
            {
                foreach (DataRow dr in DT.Rows)
                {
                    ERPStore store = new ERPStore();
                    if (dr["COMPID"] != DBNull.Value) store.compID = short.Parse(dr["COMPID"].ToString());
                    if (dr["STOREID"] != DBNull.Value) store.storeID = short.Parse(dr["STOREID"].ToString());

                    store.storeName = dr["STORENAME"].ToString();

                    stores.Add(store);
                }
            }

            return stores;
        }

        public List<ERPMunit> FGetMunits()
        {
            DataTable DT = new DataTable();
            StringBuilder sqlstr = new StringBuilder();

            List<ERPMunit> munits = new List<ERPMunit>();
            

            sqlstr.Append("SELECT  CODEID AS MUNITID,DESCR AS MUNIT,DECIMALS  AS MUNITDECIMALS   FROM MESUNIT");  
       

            DT = db.DBFillDataTable(sqlstr.ToString(), "DSSTORE");

            if (DT.Rows.Count > 0)
            {
                foreach (DataRow dr in DT.Rows)
                {
                    ERPMunit munit = new ERPMunit();
                    if (dr["MUNITID"] != DBNull.Value) munit.munitID = int.Parse(dr["MUNITID"].ToString());
                    munit.mUnit = dr["MUNIT"].ToString();
                    if (dr["MUNITDECIMALS"] != DBNull.Value) munit.munitDecimals = short.Parse(dr["MUNITDECIMALS"].ToString());

                    munits.Add(munit);
                }
            }

            return munits;
        }

        public string FCreateInvItemsData(int StoreID,  bool GetZeroQty)
        {            
            StringBuilder sqlstr = new StringBuilder();
            string rtrn = null;
            long affectedrows = 0;

            string SrcTableData = "ZVBRAPOGRAFH";
            string tblname = "ZSTORESINVERNTORYITEMS_";
            bool wherecl = false;

            if (StoreID > 0)
                tblname += StoreID.ToString();


            db.DBExecuteSQLCmd("DROP TABLE " + tblname);

            sqlstr.Append("CREATE TABLE " + tblname + " AS ");  
          
            if (StoreID == 9999 || StoreID==0)
                sqlstr.Append(" SELECT DISTINCT 0 AS ID,COMPID,0 AS BRANCHID,0 AS STOREID,ITEMID,ITEMCODE,ITEMDESC,MUNITPRIMARY,MUNITSECONDARY,ENTRYDATE FROM  " + SrcTableData + " ");
            else
                sqlstr.Append(" SELECT DISTINCT 0 AS ID,COMPID,BRANCHID,STOREID,ITEMID,ITEMCODE,ITEMDESC,MUNITPRIMARY,MUNITSECONDARY,ENTRYDATE FROM  " + SrcTableData + " ");

            if (StoreID > 0 && StoreID !=9999) //9999 means all stores
            {
                sqlstr.Append("WHERE STOREID=" + StoreID.ToString());
                wherecl = true;                
            }
            if (StoreID != 9999)
            {
                if (!GetZeroQty)
                {
                    if (wherecl) sqlstr.Append(" AND NVL(QUANT1,0) <> 0"); else sqlstr.Append(" WHERE NVL(QUANT1,0) <> 0");
                }
            }

            sqlstr.Append(" ORDER BY ITEMID");

            affectedrows = db.DBExecuteSQLCmd(sqlstr.ToString());
            if (affectedrows > 0)
                db.DBExecuteSQLCmd("UPDATE " + tblname + " SET ID=ROWNUM");

            try { syncinfo.MinItemID = long.Parse(db.DBWmsExSelectCmdRN2String("SELECT MIN(ITEMID) AS MINITEMID FROM " + tblname)); }
            catch { }
            try { syncinfo.MaxItemID = long.Parse(db.DBWmsExSelectCmdRN2String("SELECT MAX(ITEMID) AS MAXITEMID FROM " + tblname)); }
            catch { }
            try { syncinfo.ItemsRowsCount = long.Parse(db.DBWmsExSelectCmdRN2String("SELECT COUNT(DISTINCT ITEMID) AS CNTITEMS FROM " + tblname)); }
            catch { }

            syncinfo.MinItemRowid = 1;
            syncinfo.MaxItemRowid = syncinfo.ItemsRowsCount;
            return rtrn = affectedrows.ToString();
        }

        public string FCreateInvLotsData(int StoreID, bool GetZeroQty)
        {
            StringBuilder sqlstr = new StringBuilder();
            string rtrn = null;
            long affectedrows = 0;
            bool wherecl = false;

            string SrcTableData = "ZVBRAPOGRAFH";
            string tblname = "ZSTORESINVERNTORYLOTS_";
           

            if (StoreID > 0)
                tblname += StoreID.ToString();

            db.DBExecuteSQLCmd("DROP TABLE " + tblname);

            sqlstr.Append("CREATE TABLE " + tblname + " ");


            if (StoreID == 9999 || StoreID == 0)
                sqlstr.Append(" AS SELECT DISTINCT 0 AS ID,COMPID,0 AS BRANCHID,0 AS STOREID,LOTID,LOTCODE,ITEMID,WIDTH,LENGTH,DRAFT,COLOR,0 AS QUANT1,0 AS QUANT2,ZENTRYDATE FROM  " + SrcTableData + " ");
            else
                sqlstr.Append(" AS SELECT DISTINCT 0 AS ID,COMPID,BRANCHID,STOREID,LOTID,LOTCODE,ITEMID,WIDTH,LENGTH,DRAFT,COLOR,QUANT1,QUANT2,ZENTRYDATE FROM  " + SrcTableData + " ");


            if (StoreID > 0 && StoreID != 9999) //all stores
            {
                sqlstr.Append("WHERE STOREID=" + StoreID.ToString());
                wherecl = true;                     
            }

            if (StoreID != 9999)
            {
                if (!GetZeroQty)
                {
                    if (wherecl) sqlstr.Append(" AND NVL(QUANT1,0) <> 0"); else sqlstr.Append(" WHERE NVL(QUANT1,0) <> 0");
                }
            }
            sqlstr.Append(" ORDER BY LOTID");

            affectedrows = db.DBExecuteSQLCmd(sqlstr.ToString());

            if (affectedrows > 0)
                db.DBExecuteSQLCmd("UPDATE " + tblname  + " SET ID=ROWNUM");

            try { syncinfo.MinLotID = long.Parse(db.DBWmsExSelectCmdRN2String("SELECT MIN(LOTID) AS MINLOTID FROM " + tblname)); }
            catch { }
            try { syncinfo.MaxLotID = long.Parse(db.DBWmsExSelectCmdRN2String("SELECT MAX(LOTID) AS MAXLOTID FROM " + tblname)); }
            catch { }
            try { syncinfo.LotRowsCount = long.Parse(db.DBWmsExSelectCmdRN2String("SELECT COUNT(LOTID) AS CNTLOTID FROM " + tblname)); }
            catch { }

            syncinfo.MinLotRowID = 1;
            syncinfo.MaxLotRowID = syncinfo.LotRowsCount;

            return rtrn = affectedrows.ToString();
        }

        public string FCreateInvStatusData(int StoreID,bool GetZeroQty)
        {           
            StringBuilder sqlstr = new StringBuilder();
            string rtrn = null;
            long affectedrows=0;
            bool wherecl = false;

            string SrcTableData = "ZVBRAPOGRAFH";
            string tblname = "ZSTORESINVERNTORYSTATUS_";
            
            if (StoreID > 0)
                tblname += StoreID.ToString();


            db.DBExecuteSQLCmd("DROP TABLE " + tblname);

            sqlstr.Append("CREATE TABLE " + tblname + " ");          
            //sqlstr.Append(" ");
            //
            sqlstr.Append("AS SELECT ROWNUM AS ID,STOREID,COMPID,BRANCHID,STORENAME,LOTID,LOTCODE,ITEMID,ITEMCODE,ITEMDESC,DESCRIPTION,MUNITPRIMARY,MUNITSECONDARY,");
            sqlstr.Append("MUNIT,MUNIT2,WIDTH,LENGTH,DRAFT,COLOR,QUANT1,QUANT2 ");
            sqlstr.Append(" FROM  " + SrcTableData + " ");

            if (StoreID > 0 && StoreID != 9999)
            {
                sqlstr.Append("WHERE STOREID=" + StoreID.ToString());
                wherecl = true;
            }
            if (StoreID != 9999)
            {
                if (!GetZeroQty)
                {
                    if (wherecl) sqlstr.Append(" AND NVL(QUANT1,0) <> 0"); else sqlstr.Append(" WHERE NVL(QUANT1,0) <> 0");
                }
            }
            sqlstr.Append(" ORDER BY LOTID");
            affectedrows = db.DBExecuteSQLCmd(sqlstr.ToString());

            return rtrn = affectedrows.ToString();
        }
             
        public DataSet FgetCurrentInventoryByLot(int StoreID, long StartLotID, long EndLotID)
        {
            StringBuilder sqlstr = new StringBuilder();

            DataSet DT = new DataSet();           
            string tblname = "ZSTORESINVERNTORYSTATUS_";

            if (StoreID > 0)
                tblname += StoreID.ToString();

            sqlstr.Append("SELECT ID,STOREID,BRANCHID,LOTID,LOTCODE,");
            sqlstr.Append("ITEMID,ITEMCODE,ITEMDESC,WIDTH,LENGTH,DRAFT,COLOR,QUANT1,QUANT2 ");
            sqlstr.Append("FROM " + tblname + " ");
            sqlstr.Append("WHERE  STOREID=" + StoreID.ToString() + " ");
            if (StartLotID > 0 && EndLotID > 0)
                sqlstr.Append("AND (ID BETWEEN " + StartLotID.ToString() + " AND " + EndLotID.ToString() +")");
            sqlstr.Append("ORDER BY ID");


            DT = db.DBFillDataset(sqlstr.ToString(),"DSINV");
            return DT;
        }

        public long FClearData(int StoreID)

        {
            long rtrn=0;
            string tblname = "ZSTORESINVERNTORYITEMS_";
            if (StoreID > 0)
                tblname += StoreID.ToString();

            rtrn += db.DBExecuteSQLCmd("DROP TABLE " + tblname);


            tblname = "ZSTORESINVERNTORYLOTS_";
            if (StoreID > 0)
                tblname += StoreID.ToString();

            rtrn += db.DBExecuteSQLCmd("DROP TABLE " + tblname);

            tblname = "ZSTORESINVERNTORYSTATUS_";
            if (StoreID > 0)
                tblname += StoreID.ToString();

            rtrn += db.DBExecuteSQLCmd("DROP  TABLE " + tblname);

            return rtrn;
        }

        #region sync items

        public List<SyncERPItem> FGetListItems(int StoreID, long StartID, long EndID)
        {
            StringBuilder sqlstr = new StringBuilder();
            List<SyncERPItem> ListItems = new List<SyncERPItem>();

            DataTable DT = new DataTable();

            string tblname = "ZSTORESINVERNTORYITEMS_";
            Boolean WhereCl = false;

            if (StoreID > 0)
            {
                tblname += StoreID.ToString();
            }

            sqlstr.Append("SELECT DISTINCT ITEMID,COMPID,MUNITPRIMARY,MUNITSECONDARY,ITEMCODE,ITEMDESC,ENTRYDATE FROM " + tblname);

            if ((StartID > 0 && EndID > 0) || StoreID > 0)
                sqlstr.Append("  ");

            if (StoreID > 0 && StoreID != 9999)
            {
                sqlstr.Append(" WHERE STOREID=" + StoreID.ToString());
                WhereCl = true;
            }

            if (StartID > 0 && EndID > 0)
            {
                if (WhereCl) sqlstr.Append(" AND "); else sqlstr.Append(" WHERE ");

                sqlstr.Append(" ID BETWEEN " + StartID.ToString() + " AND " + EndID.ToString());
            }
            //else if (!string.IsNullOrEmpty(lastsyncdate))
            //{
            //    if (WhereCl) sqlstr.Append(" AND "); else sqlstr.Append(" WHERE ");
            //    sqlstr.Append(" ENTRYDATE <= TO_DATE('" + lastsyncdate + "','DD/YY/YYYY'");
            //}

            //sqlstr.Append(" ORDER BY ID");
            DT = db.DBFillDataTable(sqlstr.ToString(), "DSITEMS");


            for (int i = 0; i < DT.Rows.Count; i++)
            {
                ListItems.Add(ItemParse(DT.Rows[i]));
            }
            
            return ListItems;
        }
       
        public List<SyncLot> FGetListLots(int StoreID, long StartID, long EndID)
        {
            StringBuilder sqlstr = new StringBuilder();
            List<SyncLot> ListLots = new List<SyncLot>();

            DataTable DT = new DataTable();

            string tblname = "ZSTORESINVERNTORYLOTS_";
            Boolean WhereCl = false;

            if (StoreID > 0)
            {
                tblname += StoreID.ToString();
            }

            sqlstr.Append("SELECT * FROM " + tblname);

            if ((StartID > 0 && EndID > 0) || StoreID > 0)
                sqlstr.Append("  ");

            if (StoreID > 0 && StoreID != 9999)
            {
                sqlstr.Append(" WHERE STOREID=" + StoreID.ToString());
                WhereCl = true;
            }

            
            if (StartID > 0 && EndID > 0)
            {
                if (WhereCl) sqlstr.Append(" AND "); else sqlstr.Append(" WHERE ");

                sqlstr.Append(" ID BETWEEN " + StartID.ToString() + " AND " + EndID.ToString());
            }
            //else if (!string.IsNullOrEmpty(lastsyncdate))
            //{
            //    if (WhereCl) sqlstr.Append(" AND "); else sqlstr.Append(" WHERE ");
            //    sqlstr.Append(" ZENTRYDATE <= TO_DATE('" + lastsyncdate + "','DD/YY/YYYY'");
            //}

            sqlstr.Append(" ORDER BY ID");
            DT = db.DBFillDataTable(sqlstr.ToString(), "DSLOTS");

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                ListLots.Add(FParseLot(DT.Rows[i]));
            }
            
            return ListLots;
        }

        public SyncERPItem ItemParse(DataRow Dr)
        {
            SyncERPItem item = new SyncERPItem();
            try { item.ItemID = long.Parse(Dr["ITEMID"].ToString()); }
            catch { }
            try { item.CompID = short.Parse(Dr["COMPID"].ToString()); }
            catch { }
            try { item.MUnitPrimary = int.Parse(Dr["MUNITPRIMARY"].ToString()); }
            catch { }
            try { item.MUnitSecondary = int.Parse(Dr["MUNITSECONDARY"].ToString()); }
            catch { }

            try { item.ItemCode = Dr["ITEMCODE"].ToString(); }
            catch { }
            try { item.ItemDesc = Dr["ITEMDESC"].ToString().Replace("'","").Replace("\"",""); }
            catch { }
            //try { item.MUnitDesc1 = Dr["MUnitDesc1"].ToString(); }
            //catch { }
            //try { item.MUnitDesc2 = Dr["MUnitDesc2"].ToString(); }
            //catch { }
            try { item.EntryDate = Dr["ENTRYDATE"].ToString(); }
            catch { }

            return item;
        }

        private SyncLot FParseLot(DataRow Dr)
        {
            SyncLot lot = new SyncLot();

            try { lot.LotID = long.Parse(Dr["LOTID"].ToString()); }
            catch { }
            try { lot.ItemID = long.Parse(Dr["ITEMID"].ToString()); }
            catch { }
            try { lot.CompID = short.Parse(Dr["COMPID"].ToString()); }
            catch { }
            try { lot.LotCode = Dr["LOTCODE"].ToString(); }
            catch { }
            try { lot.Width = decimal.Parse(Dr["WIDTH"].ToString()); }
            catch { }
            try { lot.Length = decimal.Parse(Dr["LENGTH"].ToString()); }
            catch { }
            try { lot.Color = Dr["COLOR"].ToString(); }
            catch { }
            try { lot.Draft = Dr["DRAFT"].ToString(); }
            catch { }
            try { lot.ItemPrimaryQty = decimal.Parse(Dr["QUANT1"].ToString()); }
            catch { }
            try { lot.ItemSecondaryQty = decimal.Parse(Dr["QUANT2"].ToString()); }
            catch { }
            try { lot.EntryDate = Dr["ZENTRYDATE"].ToString().Replace("πμ", "am"); lot.EntryDate = lot.EntryDate.Replace("μμ", "pm"); }
            catch { }
           
            return lot;
        }       

        #endregion
    }

  
}