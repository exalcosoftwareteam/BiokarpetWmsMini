using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Xml;
using System.Web;
using System.IO;
using WMSMiniWebService.WMSsyncService;
using System.Web.Hosting;
using WMSMiniWebService.Components;


namespace WMSMiniWebService
{
#region ITEMS,LOTS,MUNITS,BINS


    public class ERPItem
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

    public class ERPLot
    {
        public long LotID { get; set; }
        public long ItemID { get; set; }
        public short CompID { get; set; }
        public string LotCode { get; set; }
        public decimal Width { get; set; }
        public decimal Length { get; set; }
        public string Color { get; set; }
        public string Draft { get; set; }
        public decimal ItemPrimaryQty { get; set; }
        public decimal ItemSecondaryQty { get; set; }
        public string EntryDate { get; set; }
    }

    public class ItemMunits
    {
        DB db = new DB();

        public string MunitDescription(int munitid)
        {
            return db.DBGetStrResultFromSQLSelect("SELECT MUNIT FROM TITEMMUNITS WHERE MUNITID=" + munitid.ToString());
        }

        public short MunitDecimals(int munitid)
        {
            return (short)(db.DBGetNumResultFromSQLSelect("SELECT MUNITDECIMALS FROM TITEMMUNITS WHERE MUNITID=" + munitid.ToString()));
        }

    }

    public class Items
    {
        DB db = new DB();

        public int GetItemPrimaryMUnit(long itemid)
        {
            long rmunit = 0;
            rmunit = db.DBGetNumResultFromSQLSelect("SELECT MUNITPRIMARY  FROM TITEMS WHERE ITEMID=" + itemid.ToString());

            if (rmunit > 0)
                return (int)rmunit;
            else
                return 0;
        }

        public int GetItemSecondaryMUnit(long itemid)
        {
            long rmunit = 0;
            rmunit = db.DBGetNumResultFromSQLSelect("SELECT MUNITSECONDARY  FROM TITEMS WHERE ITEMID=" + itemid.ToString());

            if (rmunit > 0)
                return (int)rmunit;
            else
                return 0;
        }

        public decimal GetItemSecondaryQty(long itemid, decimal primaryqty)
        {
            decimal secqty = 0;
            decimal munitsecrelation = 0;
            int decimals;


            ItemMunits munit = new ItemMunits();

            decimals = MunitDecimals(GetItemSecondaryMUnit(itemid));
            munitsecrelation = GetMunitRelations(itemid);

            secqty = primaryqty * munitsecrelation;

            if (decimals > 0)
            {
                secqty = Math.Round(secqty, decimals);
            }

            return secqty;
        }

        public decimal GetItemPrimaryQty(long itemid, decimal secqty)
        {
            decimal primaryqty = 0;
            decimal munitsecrelation = 0;
            int decimals;


            ItemMunits munit = new ItemMunits();

            decimals = MunitDecimals(GetItemPrimaryMUnit(itemid));
            munitsecrelation = GetMunitRelations(itemid);

            if (!(munitsecrelation > 0)) return -11;

            primaryqty = secqty / munitsecrelation;

            if (decimals > 0)
            {
                primaryqty = Math.Round(primaryqty, decimals);
            }

            return primaryqty;
        }

        public decimal GetMunitRelations(long itemid)
        {
            decimal rmunitrelation = 0;
            rmunitrelation = db.DBGetDecimalResultFromSQLSelect("SELECT MUNITSRELATION  FROM TITEMS WHERE ITEMID=" + itemid.ToString());

            if (rmunitrelation > 0)
                return (decimal)rmunitrelation;
            else
                return 0;
        }

        public short MunitDecimals(int munitid)
        {
            return (short)(db.DBGetNumResultFromSQLSelect("SELECT MUNITDECIMALS FROM TITEMMUNITS WHERE MUNITID=" + munitid.ToString()));
        }

        public TItems ItemInstance(long itemid)
        {
            TItems item = IListItemsData(" ITEMID=" + itemid.ToString(), "")[0];


            return item;
        }

        public TItems ItemInstanceByCode(string itemcode)
        {
            TItems item = new TItems();

            try
            {
                item = IListItemsData(" ITEMCODE='" + itemcode + "'", "")[0];
            }
            catch { }


            return item;
        }

        public List<TItems> IListItemsData(string searchcriteria, string sortcriteria)
        {
            List<TItems> itemsdata = new List<TItems>();

            string sqlstr = "SELECT * FROM VITEMS ";
            if (!string.IsNullOrEmpty(searchcriteria))
            {
                sqlstr += " WHERE " + searchcriteria;
            }

            if (!string.IsNullOrEmpty(sortcriteria))
            {
                sqlstr += " ORDER BY " + searchcriteria;
            }


            IDataReader dr = db.DBReturnDatareaderResults(sqlstr);


            while (dr.Read())
            {
                TItems item = new TItems();

                if (dr["ITEMID"] != DBNull.Value) item.itemid = long.Parse(dr["ITEMID"].ToString());
                if (dr["ITEMCODE"] != DBNull.Value) item.itemcode = dr["ITEMCODE"].ToString();
                if (dr["ITEMDESC"] != DBNull.Value) item.itemdesc = dr["ITEMDESC"].ToString();

                if (dr["GROUPITEMID"] != DBNull.Value) item.groupitemid = int.Parse(dr["GROUPITEMID"].ToString());
                if (dr["MAINITEMCATID"] != DBNull.Value) item.mainitemcatid = int.Parse(dr["MAINITEMCATID"].ToString());

                if (dr["MUNITPRIMARY"] != DBNull.Value) item.munitprimary = short.Parse(dr["MUNITPRIMARY"].ToString());
                if (dr["MUNITPRIMARYDESCR"] != DBNull.Value) item.munitprimarydescr = dr["MUNITPRIMARYDESCR"].ToString();
                if (dr["MUNITPRIMARYDECIM"] != DBNull.Value) item.munitprimarydecimals = short.Parse(dr["MUNITPRIMARYDECIM"].ToString());

                if (dr["MUNITSECONDARY"] != DBNull.Value) item.munitsecondary = short.Parse(dr["MUNITSECONDARY"].ToString());
                if (dr["MUNITSECONDARYDESCR"] != DBNull.Value) item.munitsecondarydescr = dr["MUNITSECONDARYDESCR"].ToString();
                if (dr["MUNITSECONDDECIM"] != DBNull.Value) item.munitsecondarydecimals = short.Parse(dr["MUNITSECONDDECIM"].ToString());

                if (dr["MUNITSRELATION"] != DBNull.Value) item.munitsrelation = decimal.Parse(dr["MUNITSRELATION"].ToString());


                itemsdata.Add(item);
                item = null;
            }

            db.DBDisconnect();
            return itemsdata;
        }

        public DataSet GetItemPartial(long startpoint, long endpoint)
        {
            StringBuilder sqlstr = new StringBuilder();

            sqlstr.Append("SELECT * FROM VITEMS ");

            if (startpoint > 0 && endpoint > 0)
                sqlstr.Append(" WHERE ITEMID BETWEEN " + startpoint.ToString() + " AND " + endpoint.ToString());


            DataSet Ds = new DataSet();
            Ds = db.DBFillDataset(sqlstr.ToString(), "DSITEMS");
            return Ds;
        }

        long SyncInsert(TItems titem)
        {

            StringBuilder sqlstr = new StringBuilder();

            sqlstr.Append("INSERT INTO TITEMS(");

            if (db.dbtype == DBType.ORACLEDB) sqlstr.Append("ITEMID,");

            sqlstr.Append("COMPID,");
            sqlstr.Append("ITEMCODE,");
            sqlstr.Append("ITEMDESC,");
            sqlstr.Append("GROUPITEMID,");
            sqlstr.Append("MAINITEMCATID,");
            sqlstr.Append("SUBITEMCATID,");
            sqlstr.Append("MUNITPRIMARY,");
            sqlstr.Append("MUNITSECONDARY,");
            sqlstr.Append("MUNITSRELATION,");
            sqlstr.Append("ERPITEMID");
            sqlstr.Append(")");
            sqlstr.Append(" VALUES(");

            titem.compid = 1;

            if (db.dbtype == DBType.ORACLEDB)
            {
                long newid = db.DBGetNumResultFromSQLSelect("SELECT SEQ_TITEMS.NEXTVAL FROM DUAL ");
                if (newid > 0) sqlstr.Append(newid.ToString() + ","); else sqlstr.Append("NULL,");
            }

            if (titem.compid > 0) sqlstr.Append(titem.compid.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(titem.itemcode)) sqlstr.Append("'" + titem.itemcode + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(titem.itemdesc)) sqlstr.Append("'" + titem.itemdesc + "',"); else sqlstr.Append("NULL,");

            if (titem.groupitemid > 0) sqlstr.Append(titem.groupitemid.ToString() + ","); else sqlstr.Append("NULL,");
            if (titem.mainitemcatid > 0) sqlstr.Append(titem.mainitemcatid.ToString() + ","); else sqlstr.Append("NULL,");
            if (titem.subitemcatid > 0) sqlstr.Append(titem.subitemcatid.ToString() + ","); else sqlstr.Append("NULL,");

            if (titem.munitprimary > 0) sqlstr.Append(titem.munitprimary.ToString() + ","); else sqlstr.Append("NULL,");

            if (titem.munitsecondary > 0) sqlstr.Append(titem.munitsecondary.ToString() + ","); else sqlstr.Append("NULL,");
            if (titem.munitsrelation > 0) sqlstr.Append(titem.munitsrelation.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (titem.erpitemid > 0) sqlstr.Append(titem.erpitemid.ToString()); else sqlstr.Append("NULL");

            sqlstr.Append(")");

            long r = db.DBExecuteSQLCmd(sqlstr.ToString());
            return r;

        }

        long SyncUpdate(TItems titem)
        {

            if (!(titem.erpitemid > 0)) return 0;

            StringBuilder sqlstr = new StringBuilder();

            sqlstr.Append("UPDATE TITEMS SET ");


            if (!string.IsNullOrEmpty(titem.itemcode)) sqlstr.Append("ITEMCODE='" + titem.itemcode + "',");
            if (!string.IsNullOrEmpty(titem.itemdesc)) sqlstr.Append("ITEMDESC='" + titem.itemcode + "',");

            if (titem.munitprimary > 0) sqlstr.Append("MUNITPRIMARY=" + titem.munitprimary.ToString() + ",");

            if (titem.munitsecondary > 0) sqlstr.Append("MUNITSECONDARY=" + titem.munitsecondary.ToString() + ","); //else sqlstr.Append("MUNITSECONDARY=NULL,");
            if (titem.munitsrelation > 0) sqlstr.Append("MUNITSRELATION=" + titem.ToString().Replace(",", ".") + ",");

            if (titem.groupitemid > 0) sqlstr.Append("GROUPITEMID=" + titem.groupitemid.ToString() + ","); //else sqlstr.Append("GROUPITEMID=NULL,");
            if (titem.mainitemcatid > 0) sqlstr.Append("MAINITEMCATID=" + titem.mainitemcatid.ToString() + ","); //else sqlstr.Append("MAINITEMCATID=NULL,");
            if (titem.subitemcatid > 0) sqlstr.Append("SUBITEMCATID=" + titem.mainitemcatid.ToString() + ","); //else sqlstr.Append("SUBITEMCATID=NULL,");


            sqlstr.Append(" WHERE ERPITEMID=" + titem.erpitemid.ToString());



            long r = db.DBExecuteSQLCmd(sqlstr.ToString());

            return r;

        }

        public bool CheckifExistsERPItemID(long erpitemid)
        {
            return (db.DBGetNumResultFromSQLSelect("SELECT ERPITEMID FROM TITEMS WHERE ERPITEMID=" + erpitemid.ToString()) > 0 ? true : false);
        }

        public long SyncERPItems()
        {
            long rsync = 0;


            TItems erpitem = new TItems();
            Items itemhandler = new Items();
            DB erpdb = new DB(true);
            DataTable DTERPItems = erpdb.DBFillDataTable("SELECT ID,CODE,DESCRIPTION,MU1,MU2,MU2_1,ICTID,IGPID,IGSID FROM MATERIAL WHERE ICTID=23", "DTERPITEMS");

            for (int i = 0; i < DTERPItems.Rows.Count; i++)
            {
                DataRow DR = DTERPItems.Rows[i];

                erpitem.erpitemid = long.Parse(DR["ID"].ToString());

                if (DR["CODE"] != DBNull.Value) erpitem.itemcode = DR["CODE"].ToString();

                if (DR["DESCRIPTION"] != DBNull.Value) erpitem.itemdesc = DR["DESCRIPTION"].ToString();
                if (DR["MU1"] != DBNull.Value) erpitem.munitprimary = short.Parse(DR["MU1"].ToString());

                if (DR["MU2"] != DBNull.Value) erpitem.munitsecondary = short.Parse(DR["MU2"].ToString());
                if (DR["MU2_1"] != DBNull.Value) erpitem.munitsrelation = decimal.Parse(DR["MU2_1"].ToString());

                if (DR["ICTID"] != DBNull.Value) erpitem.groupitemid = int.Parse(DR["ICTID"].ToString());
                if (DR["IGPID"] != DBNull.Value) erpitem.mainitemcatid = int.Parse(DR["IGPID"].ToString());
                if (DR["IGSID"] != DBNull.Value) erpitem.subitemcatid = int.Parse(DR["IGSID"].ToString());

                if (CheckifExistsERPItemID(erpitem.erpitemid))
                {
                    if (SyncUpdate(erpitem) > 0)
                        rsync++;
                }
                else
                {
                    if (SyncInsert(erpitem) > 0)
                        rsync++;
                }

            }
            return rsync;
        }

        public long FInsertItem(ERPItem item)
        {
            StringBuilder sqlstr = new StringBuilder();

            if (string.IsNullOrEmpty(item.EntryDate)) item.EntryDate = DateTime.Today.ToString("dd/MM/yyyy");

            sqlstr.Append("INSERT INTO TItems  (ItemID, CompID, ItemCode, ItemDesc, MUnitPrimary,Transid, MUnitSecondary,ENTRYDATE) VALUES     ( ");
            if (item.ItemID > 0) sqlstr.Append(item.ItemID.ToString() + ","); else sqlstr.Append("NULL,");
            if (item.CompID > 0) sqlstr.Append(item.CompID.ToString() + ","); else sqlstr.Append("2,");
            if (!string.IsNullOrEmpty(item.ItemCode)) sqlstr.Append("'" + item.ItemCode + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(item.ItemDesc)) sqlstr.Append("'" + item.ItemDesc + "',"); else sqlstr.Append("NULL,");
            if (item.MUnitPrimary > 0) sqlstr.Append(item.MUnitPrimary.ToString() + ",99,"); else sqlstr.Append("NULL,99,");
            if (item.MUnitSecondary > 0) sqlstr.Append(item.MUnitSecondary.ToString() + ","); else sqlstr.Append("NULL,");

            if (!string.IsNullOrEmpty(item.EntryDate))
            {
                item.EntryDate = item.EntryDate.Replace("πμ", "am");
                item.EntryDate = item.EntryDate.Replace("μμ", "pm");
                sqlstr.Append("CONVERT(DATETIME,'" + item.EntryDate + "',103)");
            }
            else
                sqlstr.Append("NULL");

            sqlstr.Append(")");
            return db.DBExecuteSQLCmd(sqlstr.ToString());
        }



    }

    public class ItemLots
    {

        public long FInsertLot(ERPLot lot)
        {

            if (string.IsNullOrEmpty(lot.EntryDate)) lot.EntryDate = DateTime.Today.ToString("dd/MM/yyyy");
            StringBuilder sqlstr = new StringBuilder();

            sqlstr.Append("INSERT INTO TItemLot  (LOTID, compid, ItemID, LOTCode,Width,Length,ItemPrimaryQty,ItemSecondaryQty,Color,Transid,Draft,ENTRYDATE) VALUES     ( ");
            if (lot.LotID > 0) sqlstr.Append(lot.LotID.ToString() + ","); else sqlstr.Append("NULL,");
            if (lot.CompID > 0) sqlstr.Append(lot.CompID.ToString() + ","); else sqlstr.Append("2,");
            if (lot.ItemID > 0) sqlstr.Append(lot.ItemID.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(lot.LotCode)) sqlstr.Append("'" + lot.LotCode + "',"); else sqlstr.Append("NULL,");
            if (lot.Width > 0) sqlstr.Append(lot.Width.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (lot.Length > 0) sqlstr.Append(lot.Length.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (lot.ItemPrimaryQty > 0) sqlstr.Append(lot.ItemPrimaryQty.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (lot.ItemSecondaryQty > 0) sqlstr.Append(lot.ItemSecondaryQty.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(lot.Color)) sqlstr.Append("'" + lot.Color.Replace("'","|") + "',99,"); else sqlstr.Append("NULL,99,");
            if (!string.IsNullOrEmpty(lot.Draft)) sqlstr.Append("'" + lot.Draft.Replace("'", "|") + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(lot.EntryDate))
            {
                sqlstr.Append("CONVERT(DATETIME,'" + lot.EntryDate + "',103)");
            }
            else
            {
                sqlstr.Append("NULL");
            }
            sqlstr.Append(")");
            return db.DBExecuteSQLCmd(sqlstr.ToString());
        }


        DB db = new DB();

        public TItemLot ItemLotInstance(long lotid)
        {
            TItemLot itemlot = IListItemLots("LOTID=" + lotid.ToString(), "")[0];
            return itemlot;
        }

        public TItemLot ItemLotInstanceByCode(string lotcode, long itemid)
        {


            string sqlcriteria = " LOTCODE='" + lotcode + "'";
            if (itemid > 0) sqlcriteria += " AND ITEMID=" + itemid.ToString();

            TItemLot itemlot = IListItemLots(sqlcriteria, "")[0];
            return itemlot;
        }

        public List<TItemLot> IListItemLots(string searchcriteria, string sortcriteria)
        {


            List<TItemLot> itemlotsdata = new List<TItemLot>();

            string sqlstr = "SELECT * FROM TITEMLOT ";
            if (!string.IsNullOrEmpty(searchcriteria))
            {
                sqlstr += " WHERE " + searchcriteria;
            }

            if (!string.IsNullOrEmpty(sortcriteria))
            {
                sqlstr += " ORDER BY " + searchcriteria;
            }


            IDataReader dr = db.DBReturnDatareaderResults(sqlstr);


            while (dr.Read())
            {
                TItemLot itemlot = new TItemLot();

                if (dr["LOTID"] != DBNull.Value) itemlot.lotid = long.Parse(dr["LOTID"].ToString());
                if (dr["LOTCODE"] != DBNull.Value) itemlot.lotcode = dr["LOTCODE"].ToString();
                if (dr["ITEMID"] != DBNull.Value) itemlot.itemid = long.Parse(dr["ITEMID"].ToString());

                if (dr["LOTCREATEDATE"] != DBNull.Value) itemlot.lotcreatedate = dr["LOTCREATEDATE"].ToString();
                if (dr["LOTEXPIREDATE"] != DBNull.Value) itemlot.lotexpiredate = dr["LOTEXPIREDATE"].ToString();

                itemlotsdata.Add(itemlot);
                itemlot = null;
            }

            db.DBDisconnect();
            return itemlotsdata;
        }

        public DataSet GetItemLotPartial(long startpoint, long endpoint)
        {
            StringBuilder sqlstr = new StringBuilder();

            sqlstr.Append("SELECT * FROM VITEMLOTS ");

            if (startpoint > 0 && endpoint > 0)
                sqlstr.Append(" WHERE TransID BETWEEN " + startpoint.ToString() + " AND " + endpoint.ToString());


            DataSet Ds = new DataSet();
            Ds = db.DBFillDataset(sqlstr.ToString(), "DSITEMLOTS");
            return Ds;
        }
    }


#endregion

#region INVENTORY
    public class Inventory
    {
        long invID;
        short compID;
        short branchID;
        int storeID;
        long invHdrID;
        long invHdrIDServer;
        long itemID;
        string itemCode;
        long lotID;
        string lotCode;
        string invDate;
        short mUnitPrimary;
        decimal invQty;
        decimal eRPQty;
        short mUnitSecondary;
        decimal invQtySecondary;
        string serialNumber;
        long mobInvID;

        public long InvID { get { return invID; } set { invID = value; } }
        public short CompID { get { return compID; } set { compID = value; } }
        public short BranchID { get { return branchID; } set { branchID = value; } }
        public int StoreID { get { return storeID; } set { storeID = value; } }
        public long InvHdrID { get { return invHdrID; } set { invHdrID = value; } }
        public long InvHdrIDServer { get { return invHdrIDServer; } set { invHdrIDServer = value; } }
        public long ItemID { get { return itemID; } set { itemID = value; } }
        public string ItemCode { get { return itemCode; } set { itemCode = value; } }
        public long LotID { get { return lotID; } set { lotID = value; } }
        public string LotCode { get { return lotCode; } set { lotCode = value; } }
        public string InvDate { get { return invDate; } set { invDate = value; } }
        public short MUnitPrimary { get { return mUnitPrimary; } set { mUnitPrimary = value; } }
        public decimal InvQty { get { return invQty; } set { invQty = value; } }
        public decimal ERPQty { get { return eRPQty; } set { eRPQty = value; } }
        public short MUnitSecondary { get { return mUnitSecondary; } set { mUnitSecondary = value; } }
        public decimal InvQtySecondary { get { return invQtySecondary; } set { invQtySecondary = value; } }
        public string SerialNumber { get { return serialNumber; } set { serialNumber = value; } }
        public long MobInvID { get { return mobInvID; } set { mobInvID = value; } }
    }
    public class InventoryHeader
    {
        public DB db = new DB();


        public DataTable InventoryLists()
        {
            return db.DBFillDataTable("SELECT * FROM TInventoryHeader WHERE InvStatus <> 1", "DTINVLIST");
        }

        public long CreateNewInventory(int compid, int branchid, int storeid)
        {

            TInventoryHeader invhdr = new TInventoryHeader();

            invhdr.CompID = (short)compid;
            invhdr.Branchid = (short)branchid;
            invhdr.Storeid = storeid;
            invhdr.InvComments = "Απογραφή Offline " + DateTime.Now.ToShortDateString();

            //string invcomments = "Απογραφή Offline " + DateTime.Now.ToShortDateString();

            if (storeid > 0) invhdr.InvComments += " A.X. :" + storeid.ToString();

            return InsertRecord(invhdr);

        }

        public TInventoryHeader Parse(DataRow Dr)
        {
            TInventoryHeader invhdr = new TInventoryHeader();

            try { invhdr.InvHdrID = long.Parse(Dr["InvHdrID"].ToString()); }
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
            try { invhdr.MobInvHdrID = long.Parse(Dr["MobInvHdrID"].ToString()); }
            catch { }
            try { invhdr.InvSyncID = long.Parse(Dr["InvSyncID"].ToString()); }
            catch { }

            return invhdr;
        }

        public long UpdateInventoryHeader(TInventoryHeader invhdr)
        {
            if (invhdr.InvHdrID > 0)
                return UpdateRecord(invhdr);
            else
                return InsertRecord(invhdr);
        }

        long InsertRecord(TInventoryHeader invhdr)
        {
            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;
            long invhdrid = 0;
            //sqlstr.Append("INSERT INTO TWMSInventoryHeader(MobInvHdrID,InvSyncID,CompID,BranchID,StoreID,InvDate,InvComments) VALUES (");           

            if (db.dbtype == DBType.ORACLEDB)
            {
                invhdrid = db.DBGetNumResultFromSQLSelect("SELECT SEQ_TWMSINVENTORYHEADER.NEXTVAL FROM DUAL");
                sqlstr.Append("INSERT INTO TWMSInventoryHeader(INVHDRID,MobInvHdrID,InvSyncID,CompID,BranchID,StoreID,Confirmed,StoreName,InvComments) VALUES (");
                sqlstr.Append(invhdrid.ToString() + ",");
            }
            else
            {
                sqlstr.Append("INSERT INTO TWMSInventoryHeader(MobInvHdrID,InvSyncID,CompID,BranchID,StoreID,Confirmed,StoreName,InvComments) VALUES (");
            }

            if (invhdr.MobInvHdrID > 0) sqlstr.Append(invhdr.MobInvHdrID.ToString() + ","); else sqlstr.Append("NULL,");
            if (invhdr.InvSyncID > 0) sqlstr.Append(invhdr.InvSyncID.ToString() + ","); else sqlstr.Append("NULL,");
            if (invhdr.CompID > 0) sqlstr.Append(invhdr.CompID.ToString() + ","); else sqlstr.Append("NULL,");
            if (invhdr.Branchid > 0) sqlstr.Append(invhdr.Branchid.ToString() + ","); else sqlstr.Append("NULL,");
            if (invhdr.Storeid > 0) sqlstr.Append(invhdr.Storeid.ToString() + ",0,"); else sqlstr.Append("NULL,0,");
            //if (!string.IsNullOrEmpty(invhdr.InvDate)) sqlstr.Append("CONVERT(DATETIME,'" + invhdr.InvDate + "',103),"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(invhdr.StoreName)) sqlstr.Append("'" + invhdr.StoreName + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(invhdr.InvComments)) sqlstr.Append("'" + invhdr.InvComments + "'"); else sqlstr.Append("NULL");
            sqlstr.Append(")");
            rtrn = db.DBExecuteSQLCmd(sqlstr.ToString());

            if (rtrn > 0) return db.DBGetNumResultFromSQLSelect("SELECT IDENT_CURRENT('TWMSInventoryHeader')");
            else
            {
                if (!(rtrn > 0)) db.f_sqlerrorlog(1, "InsertRecord(TInventoryHeader invhdr)", sqlstr.ToString().Replace("'", "|"));
                return -1;
            }

        }

        long UpdateRecord(TInventoryHeader invhdr)
        {
            //check if invhdrid exists
            string sqlcheck;

            sqlcheck = "select count(invhdrid) from  TWMSInventoryHeader  WHERE invhdrid =" + invhdr.InvHdrID.ToString();



            if (db.DBGetNumResultFromSQLSelectWithZero(sqlcheck) == 0) return -5;     
            
            //
            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;
            sqlstr.Append("UPDATE TWMSInventoryHeader SET ");
            if (invhdr.Storeid > 0) sqlstr.Append("StoreID=" + invhdr.Storeid.ToString() + ","); else sqlstr.Append("StoreID=NULL,");
            if (invhdr.MobInvHdrID > 0) sqlstr.Append("MobInvHdrID=" + invhdr.MobInvHdrID.ToString() + ","); else sqlstr.Append("MobInvHdrID=NULL,");
            if (!string.IsNullOrEmpty(invhdr.StoreName)) sqlstr.Append("StoreName = '" + invhdr.StoreName + "',"); else sqlstr.Append("StoreName=NULL,");
            if (!string.IsNullOrEmpty(invhdr.InvComments)) sqlstr.Append("InvComments = '" + invhdr.InvComments + "'"); else sqlstr.Append("InvComments=NULL");
            sqlstr.Append(" WHERE InvHdrID=" + invhdr.InvHdrID.ToString());
            rtrn = db.DBExecuteSQLCmd(sqlstr.ToString());

            if (!(rtrn > 0)) db.f_sqlerrorlog(0, "InventoryHeader.UpdateRecord", sqlstr.ToString());
            if (rtrn > 0)
                return invhdr.InvHdrID;
            else
                return -1;

        }


    }
    public class InventoryEntity
    {
        int compID;
        int branchID;
        int storeID;
        long invHdrID;
        long invHdrIDServer;
        long itemID;
        string itemCode;
        long lotID;
        string lotCode;
        string invDate;
        int mUnitPrimary;
        decimal invQty;
        decimal eRPQty;
        int mUnitSecondary;
        decimal invQtySecondary;
        string serialNumber;

        public int CompID { get { return compID; } set { compID = value; } }
        public int BranchID { get { return branchID; } set { branchID = value; } }
        public int StoreID { get { return storeID; } set { storeID = value; } }
        public long InvHdrID { get { return invHdrID; } set { invHdrID = value; } }
        public long InvHdrIDServer { get { return invHdrIDServer; } set { invHdrIDServer = value; } }
        public long ItemID { get { return itemID; } set { itemID = value; } }
        public string ItemCode { get { return itemCode; } set { itemCode = value; } }
        public long LotID { get { return lotID; } set { lotID = value; } }
        public string LotCode { get { return lotCode; } set { lotCode = value; } }
        public string InvDate { get { return invDate; } set { invDate = value; } }
        public int MUnitPrimary { get { return mUnitPrimary; } set { mUnitPrimary = value; } }
        public decimal InvQty { get { return invQty; } set { invQty = value; } }
        public decimal ERPQty { get { return eRPQty; } set { eRPQty = value; } }
        public int MUnitSecondary { get { return mUnitSecondary; } set { mUnitSecondary = value; } }
        public decimal InvQtySecondary { get { return invQtySecondary; } set { invQtySecondary = value; } }
        public string SerialNumber { get { return serialNumber; } set { serialNumber = value; } }
    }
    public class WMSInvOffline
    {

        DB db = new DB();
        
        InventoryHeader invhdrhandler = new InventoryHeader();


        public WMSInvOffline()
        { }


        public DataSet GetStores()
        {
            DataSet DS = new DataSet();
            DS = db.DBFillDataset("SELECT * FROM TStores", "DSSTORES");
            return DS;
        }

        public DataSet GetInventoryTasks(int compid, int branchid)
        {
            StringBuilder sqlstr = new StringBuilder();

            sqlstr.Append("SELECT TWMSInventoryHeader.InvHdrID,TWMSInventoryHeader.CompID,TWMSInventoryHeader.BranchID,TWMSInventoryHeader.InvDate,TWMSInventoryHeader.Confirmed, ");
            sqlstr.Append("CONVERT(NVARCHAR, DAY(TWMSInventoryHeader.InvDate)) + '/' + CONVERT(NVARCHAR, MONTH(TWMSInventoryHeader.InvDate)) + '/' + RIGHT(CONVERT(NVARCHAR,YEAR(TWMSInventoryHeader.InvDate)), 2) AS InvShortDate,");
            sqlstr.Append("ISNULL(TWMSInventoryHeader.InvComments,'...') AS InvComments ");
            sqlstr.Append("FROM TWMSInventoryHeader ");
            sqlstr.Append("WHERE (TWMSInventoryHeader.Confirmed IS NULL OR TWMSInventoryHeader.Confirmed = 0) ");

            if (compid > 0)
                sqlstr.Append("AND  TWMSInventoryHeader.CompID= " + compid.ToString());

            if (branchid > 0)
                sqlstr.Append(" AND  TWMSInventoryHeader.BranchID= " + branchid.ToString());

            DataSet ds = db.DBFillDataset(sqlstr.ToString(), "DSINVLIST");


            if (ds.Tables[0].Rows.Count > 0)
            {
                DataColumn DTColumn = new DataColumn();

                DTColumn.DataType = System.Type.GetType("System.Int32");
                DTColumn.ColumnName = "ID";
                DTColumn.AutoIncrement = false;
                ds.Tables[0].Columns.Add(DTColumn);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["ID"] = i + 1;
                }

                ds.AcceptChanges();

            }
            return ds;
        }

        public DataSet GetItemsMunits()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("SELECT  MunitID, CompID, MUnit, MunitEN, MunitDecimals, MunitDecimalsTSp, SuggestedUseAs FROM  TItemMunits");
            return db.DBFillDataset(sqlstr.ToString(), "DSITemMunits");
        }

        public DataSet GetItems(int storeid)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("SELECT ITEMID, ITEMCODE, ITEMDESC,0 as InvQTY,MUNITPRIMARY,MUNITSECONDARY FROM TITEMS");
            DataSet Ds = new DataSet();
            Ds = db.DBFillDataset(sqlstr.ToString(), "DSITEMS");
            return Ds;
        }

        public DataSet GetItems(int storeid, long startid, long endid)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("SELECT TITEMS.ITEMID, TITEMS.ITEMCODE, TITEMS.ITEMDESC,0 as InvQTY,TITEMS.MUNITPRIMARY,TITEMS.MUNITSECONDARY, ");
            sqlstr.Append("TItemMunits.MUnit AS MunitDesc1,TItemMunits_a.MUnit AS MunitDesc2 ");
            sqlstr.Append("FROM TITEMS LEFT OUTER JOIN TItemMunits ON TITEMS.MUNITPRIMARY = TItemMunits.MunitID ");
            sqlstr.Append("LEFT OUTER JOIN TItemMunits TItemMunits_a ON TITEMS.MUNITSECONDARY = TItemMunits_a.MunitID ");
            if (startid > 0 && endid > 0)
                sqlstr.Append(" WHERE TITEMS.TransID BETWEEN " + startid.ToString() + " AND " + endid.ToString());

            sqlstr.Append(" ORDER BY TITEMS.TransID");
            DataSet Ds = new DataSet();
            Ds = db.DBFillDataset(sqlstr.ToString(), "DSITEMS");
            return Ds;
        }

        public List<ERPItem> GetItemsList(int storeid, int startid, int endid)
        {
            List<ERPItem> itemlist = new List<ERPItem>();


            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("SELECT TITEMS.ITEMID, TITEMS.ITEMCODE,CONVERT(CHAR(10), TITEMS.ENTRYDATE,103) as ENTRYDATE, TITEMS.ITEMDESC,0 as InvQTY,TITEMS.MUNITPRIMARY,TITEMS.MUNITSECONDARY, ");
            sqlstr.Append("TItemMunits.MUnit AS MunitDesc1,TItemMunits_a.MUnit AS MunitDesc2 ");
            sqlstr.Append("FROM TITEMS LEFT OUTER JOIN TItemMunits ON TITEMS.MUNITPRIMARY = TItemMunits.MunitID ");
            sqlstr.Append("LEFT OUTER JOIN TItemMunits TItemMunits_a ON TITEMS.MUNITSECONDARY = TItemMunits_a.MunitID ");
            if (startid > 0 && endid > 0)
                sqlstr.Append(" WHERE TITEMS.TransID BETWEEN " + startid.ToString() + " AND " + endid.ToString());

            sqlstr.Append(" ORDER BY TITEMS.TransID");
            DataTable dt = db.DBFillDataTable(sqlstr.ToString(), "DTITEMS");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ERPItem item = ItemParse(dt.Rows[i]);
                if (item.ItemID > 0) itemlist.Add(item);
            }
            return itemlist;

        }

        public ERPItem ItemParse(DataRow Dr)
        {
            ERPItem item = new ERPItem();
            try { item.ItemID = long.Parse(Dr["ItemID"].ToString()); }
            catch { }
            try { item.CompID = short.Parse(Dr["CompID"].ToString()); }
            catch { }
            try { item.MUnitPrimary = int.Parse(Dr["MUnitPrimary"].ToString()); }
            catch { }
            try { item.MUnitSecondary = int.Parse(Dr["MUnitSecondary"].ToString()); }
            catch { }
            try { item.MUnitsRelation = decimal.Parse(Dr["MUnitsRelation"].ToString()); }
            catch { }
            try { item.EntryDate =  Dr["EntryDate"].ToString(); }
            catch { }
            try { item.ItemCode = Dr["ItemCode"].ToString(); }
            catch { }
            try { item.ItemDesc = Dr["ItemDesc"].ToString(); }
            catch { }
            try { item.MUnitDesc1 = Dr["MUnitDesc1"].ToString(); }
            catch { }
            try { item.MUnitDesc2 = Dr["MUnitDesc2"].ToString(); }
            catch { }

            return item;
        }

        public DataSet GetLots(int storeid)
        {
            StringBuilder sqlstr = new StringBuilder();

            sqlstr.Append(" SELECT  LOTID, LOTCODE, ITEMID,");
            sqlstr.Append(" CONVERT(CHAR(10), LOTCreateDate,103) as INDATE,");
            sqlstr.Append(" CONVERT(CHAR(10), LOTExpireDate,103) as EXPDATE,");
            sqlstr.Append(" CONVERT(CHAR(10), EntryDate,103) as ENTRYDATE,");
            sqlstr.Append(" WIDTH, LENGTH,COLOR,DRAFT,");
            sqlstr.Append("ITEMPRIMARYQTY AS LOTQTY,ITEMPRIMARYQTY,ItemSecondaryQty ");
            sqlstr.Append(" FROM TITEMLOT ");
            DataSet Ds = new DataSet();
            Ds = db.DBFillDataset(sqlstr.ToString(), "DSLOTS");
            return Ds;
        }


        public decimal GetERPLotSecondQTY(int branchid, long lotid)
        {
            return db.DBGetDecimalResultFromSQLSelect("select secondaryqty from ATLDETAILITEMQTYS where branchid =" + branchid.ToString() + " and parid = " + lotid.ToString());

        }



        public DataSet GetLots(int storeid, long startid, long endid)
        {
            StringBuilder sqlstr = new StringBuilder();

            sqlstr.Append(" SELECT  LOTID, LOTCODE, ITEMID,");
            sqlstr.Append(" CONVERT(CHAR(10), LOTCreateDate,103) as INDATE,");
            sqlstr.Append(" CONVERT(CHAR(10), LOTExpireDate,103) as EXPDATE,");
            sqlstr.Append(" CONVERT(CHAR(10), EntryDate,103) as ENTRYDATE,");
            sqlstr.Append(" WIDTH, LENGTH,");
            sqlstr.Append(" ITEMPRIMARYQTY AS LOTQTY,ITEMPRIMARYQTY,ItemSecondaryQty,COLOR,DRAFT ");
            sqlstr.Append(" FROM TITEMLOT ");
            if (startid > 0 && endid > 0)
                sqlstr.Append(" WHERE TransID BETWEEN " + startid.ToString() + " AND " + endid.ToString());

            sqlstr.Append(" ORDER BY TransID");
            DataSet Ds = new DataSet();
            Ds = db.DBFillDataset(sqlstr.ToString(), "DSLOTS");
            return Ds;
        }

        public List<ERPLot> GetLotsList(int storeid, long startid, long endid)
        {
            StringBuilder sqlstr = new StringBuilder();

            sqlstr.Append(" SELECT  LOTID, LOTCODE, ITEMID,");
            sqlstr.Append(" CONVERT(CHAR(10), LOTCreateDate,103) as INDATE,");
            sqlstr.Append(" CONVERT(CHAR(10), LOTExpireDate,103) as EXPDATE,");
            sqlstr.Append(" CONVERT(CHAR(10), EntryDate,103) as ENTRYDATE,");
            sqlstr.Append(" WIDTH, LENGTH,COLOR,DRAFT,");
            sqlstr.Append("ITEMPRIMARYQTY AS LOTQTY,ITEMPRIMARYQTY,ItemSecondaryQty ");
            sqlstr.Append(" FROM TITEMLOT ");
            if (startid > 0 && endid > 0)
                sqlstr.Append(" WHERE TransID BETWEEN " + startid.ToString() + " AND " + endid.ToString());

            sqlstr.Append(" ORDER BY TransID");
            DataTable DT = new DataTable();
            DT = db.DBFillDataTable(sqlstr.ToString(), "DSLOTS");

            List<ERPLot> lots = new List<ERPLot>();

            foreach (DataRow dr in DT.Rows)
            {
                lots.Add(LotParse(dr));
            }


            return lots;
        }

        public ERPLot LotParse(DataRow Dr)
        {
            ERPLot lot = new ERPLot();

            try { lot.LotID = long.Parse(Dr["LOTID"].ToString()); }
            catch { }
            try { lot.ItemID = long.Parse(Dr["ITEMID"].ToString()); }
            catch { }
            //try { lot.CompID = short.Parse(Dr["CompID"].ToString()); }
            //catch { }
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
         //   try { lot.ItemPrimaryQty = decimal.Parse(Dr["LOTQTY"].ToString()); }
        //    catch { }
        //    try { lot.ItemSecondaryQty = decimal.Parse(Dr["ItemSecondaryQty"].ToString()); }
        //    catch { }
            try { lot.EntryDate = Dr["ENTRYDATE"].ToString().Replace("πμ", "am").Replace("μμ", "pm"); }
            catch { }

            return lot;
        }

        //public long CreateStoresXMLFile()
        //{
        //    return CreateXMLFile(GetStores(), AppXMLFiles.XMLStores);
        //}

        //public long CreateItemsMunitsXMLFile(int storeid)
        //{
        //    return CreateXMLFile(GetItemsMunits(), AppXMLFiles.XMLMUnits);
        //}

        //public long CreateItemsXMLFile(int storeid)
        //{

        //    return CreateXMLFile(GetItems(storeid), AppXMLFiles.XMLItems);
        //}

        //public long CreateLotXMLFile(int storeid)
        //{
        //    return CreateXMLFile(GetLots(storeid), AppXMLFiles.XMLLots);
        //}

        //public long CreateInvHeadersXMLFile(int compid, int branchid)
        //{
        //    return CreateXMLFile(GetInventoryTasks(compid, branchid), AppXMLFiles.XMLInvHeaders);
        //}

        //private long CreateXMLFile(DataSet Ds, string XMLFName)
        //{

        //    string fullpathXMLFile;


        //    fullpathXMLFile = HttpContext.Current.Request.PhysicalApplicationPath + XMLFName;

        //    long itmcnt = 0;
        //    //string  XMLFname = Server.MapPath("DSXMLItems.xml");
        //    try { File.Delete(fullpathXMLFile); }
        //    catch { }

        //    try { itmcnt = Ds.Tables[0].Rows.Count; }
        //    catch { itmcnt = 0; }

        //    if (itmcnt > 0)
        //    {
        //        try
        //        {
        //            FileStream XMLstream = new FileStream(fullpathXMLFile, System.IO.FileMode.OpenOrCreate);

        //            Ds.WriteXml(XMLstream, XmlWriteMode.IgnoreSchema);
        //            XMLstream.Close();
        //        }
        //        catch { itmcnt = -1; }//error occured
        //    }

        //    return itmcnt;
        //}

        public long CreateNewInventory(int compid, int branchid, int storeid)
        {
            return invhdrhandler.CreateNewInventory(compid, branchid, storeid);
        }

        public long UploadInventoryHeader(DataSet ds)
        {
            DataSet Ds = new DataSet();
            Ds = ds;
            long rtrn = 0;

            if (Ds.Tables.Count > 0)
            {
                try { rtrn = invhdrhandler.UpdateInventoryHeader(invhdrhandler.Parse(Ds.Tables[0].Rows[0])); Ds.Dispose(); return rtrn; }
                catch (Exception ex) { invhdrhandler.db.f_sqlerrorlog(0, "UploadInventoryHeader", ex.ToString(), Ds.Tables.Count.ToString(), "WebService", "WebService"); return -1; }
            }
            else
                return -9;
        }

        public long UploadInventory(DataSet ds)
        {
            InventoryHandler invhandler = new InventoryHandler();
            long raffected = 0;
            DataSet Ds = new DataSet();
            Ds = ds;
            if (Ds.Tables.Count > 0)
            {
                try
                {
                    for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                    {
                        try { raffected += invhandler.UpdateInventory(invhandler.Parse(Ds.Tables[0].Rows[i])); }
                        catch { }
                    }
                }
                catch { raffected = -1; }
            }
            else
                raffected = -9;

            return raffected;

        }

        public string InventoryComments(long invhdrid)
        {
            return db.DBGetStrResultFromSQLSelect("SELECT TWMSInventoryHeader.InvComments FROM TWMSInventoryHeader WHERE TWMSInventoryHeader.InvHdrID = " + invhdrid.ToString());
        }

        public long InventoryEntry(InventoryEntity inventoryentry)
        {
            StringBuilder sqlstr = new StringBuilder();


            sqlstr.Append("INSERT INTO TWMSInventory(CompID,BranchID,StoreID,InvHdrID,ItemID,ItemCode,LotID,LotCode,MUnitPrimary,InvQty,ERPQty,MUnitSecondary,InvQtySecondary,SerialNumber) VALUES(");
            if (inventoryentry.CompID > 0) sqlstr.Append(inventoryentry.CompID.ToString() + ","); else sqlstr.Append("NULL,");
            if (inventoryentry.BranchID > 0) sqlstr.Append(inventoryentry.BranchID.ToString() + ","); else sqlstr.Append("NULL,");
            if (inventoryentry.StoreID > 0) sqlstr.Append(inventoryentry.StoreID.ToString() + ","); else sqlstr.Append("NULL,");
            if (inventoryentry.InvHdrID > 0) sqlstr.Append(inventoryentry.InvHdrID.ToString() + ","); else sqlstr.Append("NULL,");
            if (inventoryentry.ItemID > 0) sqlstr.Append(inventoryentry.ItemID.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(inventoryentry.ItemCode)) sqlstr.Append("'" + inventoryentry.ItemCode + "',"); else sqlstr.Append("NULL,");
            if (inventoryentry.LotID > 0) sqlstr.Append(inventoryentry.LotID.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(inventoryentry.LotCode)) sqlstr.Append("'" + inventoryentry.LotCode + "',"); else sqlstr.Append("NULL,");
            if (inventoryentry.MUnitPrimary > 0) sqlstr.Append(inventoryentry.MUnitPrimary.ToString() + ","); else sqlstr.Append("NULL,");
            if (inventoryentry.InvQty > 0) sqlstr.Append(inventoryentry.InvQty.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (inventoryentry.ERPQty > 0) sqlstr.Append(inventoryentry.ERPQty.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (inventoryentry.MUnitSecondary > 0) sqlstr.Append(inventoryentry.MUnitSecondary.ToString() + ","); else sqlstr.Append("NULL,");
            if (inventoryentry.InvQtySecondary > 0) sqlstr.Append(inventoryentry.InvQtySecondary.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(inventoryentry.SerialNumber)) sqlstr.Append("'" + inventoryentry.SerialNumber + "'"); else sqlstr.Append("NULL");
            sqlstr.Append(")");
            return db.DBExecuteSQLCmd(sqlstr.ToString());
        }

        public long InventoryStatusUpdate(long invhdrid, int status)
        {
            return db.DBExecuteSQLCmd("UPDATE TWMSInventoryHeader SET Confirmed=" + status.ToString() + " WHERE InvHdrID=" + invhdrid.ToString());
        }

        public SyncInfo GetSyncInfo()
        {
            SyncInfo syncinfo = new SyncInfo();

            try
            {
                syncinfo.ItemsRowsCount = db.DBGetNumResultFromSQLSelect("SELECT COUNT(*) AS ITEMCNT FROM TITEMS");
            }
            catch { }
            try
            {
                syncinfo.MaxItemID = db.DBGetNumResultFromSQLSelect("SELECT MAX(TransID) AS MaxItemID FROM TITEMS");
            }
            catch { }
            try
            {
                syncinfo.MinItemID = db.DBGetNumResultFromSQLSelect("SELECT MIN(TransID) AS MaxItemID FROM TITEMS");

            }
            catch { }

            try
            {
                syncinfo.LotRowsCount = db.DBGetNumResultFromSQLSelect("SELECT COUNT(*) AS LOTCNT FROM TITEMLot");
            }
            catch { }
            try
            {
                syncinfo.MaxLotID = db.DBGetNumResultFromSQLSelect("SELECT MAX(TransID) AS LOTid FROM TITEMLot");

            }
            catch { }
            try
            {
                syncinfo.MinLotID = db.DBGetNumResultFromSQLSelect("SELECT MIN(TransID) AS LOTid FROM TITEMLot");

            }
            catch { }


            return syncinfo;

        }

    }
    public class InventoryHandler
    {
        public DB db = new DB();

        public TInventory Parse(DataRow Dr)
        {
            TInventory inv = new TInventory();

            try { inv.InvID = long.Parse(Dr["InvID"].ToString()); }
            catch { }
            try { inv.InvHdrID = long.Parse(Dr["InvHdrID"].ToString()); }
            catch { }
            try { inv.InvHdrIDServer = long.Parse(Dr["InvHdrIDServer"].ToString()); }
            catch { }
            try { inv.ItemID = long.Parse(Dr["ItemID"].ToString()); }
            catch { }
            try { inv.LotID = long.Parse(Dr["LotID"].ToString()); }
            catch { }
            try { inv.MUnitPrimary = short.Parse(Dr["MUnitPrimary"].ToString()); }
            catch { }
            try { inv.MUnitSecondary = short.Parse(Dr["MUnitSecondary"].ToString()); }
            catch { }
            try { inv.InvQty = decimal.Parse(Dr["InvQty"].ToString()); }
            catch { }
            try { inv.InvQtySecondary = decimal.Parse(Dr["InvQtySecondary"].ToString()); }
            catch { }
            try { inv.ItemCode = Dr["ItemCode"].ToString(); }
            catch { }
            try { inv.LotCode = Dr["LotCode"].ToString(); }
            catch { }
            try { inv.InvDate = Dr["InvDate"].ToString(); }
            catch { }

            if (inv.InvHdrIDServer > 0)
                inv.InvHdrID = inv.InvHdrIDServer;

            return inv;
        }


        public ResultWithMessage CheckIfInventoryCanUpload(TInventoryHeader invhdr) 
        {

            ResultWithMessage resmsg = new ResultWithMessage();
            //check if   inventory exists

            if (db.DBGetNumResultFromSQLSelect("SELECT isnull(Confirmed,0) as Confirmed FROM TWMSInventoryHeader WHERE InvHdrID =" + invhdr.InvHdrID.ToString()) > 0) 
            { 
                resmsg.posresult = false;
                resmsg.errormessage = "Η απογραφή έχει οριστικοποιηθεί!";
                resmsg.resultno = -1;
                return resmsg;
            }

            if (db.DBGetNumResultFromSQLSelect("SELECT readytoupload  FROM Tbranches WHERE branchid =" + invhdr.Branchid.ToString()) > 0)
            {
                resmsg.posresult = false;
                resmsg.errormessage = "Το υποκατάστημα είναι σε κατάσταση ενημέρωσης του Atlantis !";
                resmsg.resultno = -1;
                return resmsg;
            }
            else 
            {

                resmsg.posresult = true;
                resmsg.resultno = 1;
                return resmsg;
            
            }

        
        }

        public long UpdateInventory(TInventory inv)
        {
            try
            {
                if (inv.InvID > 0)
                {
                    return UpdateRecord2(inv);
                }
                else 
                {
                    return InsertRecord(inv);              
                }

            }
            catch (Exception ex) { db.f_sqlerrorlog(0, "UpdateInventory>>" + inv.ItemCode, ex.ToString(), ">>", "Webservice", ">>"); }
            return -1;
        }

 

        public DataTable InventoryView(long InvHdrID)
        {
            DataTable DT = new DataTable();

            string sqlstr = "SELECT InvID,ItemID,ItemCode,LotID,LotCode,InvQty,ERPQty FROM TInventory ";
            DT = db.DBFillDataTable(sqlstr, "DTINVENTORY");
            return DT;
        }

        long InsertRecord(TInventory inv)
        {
            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;

            sqlstr.Append("INSERT INTO TWMSInventory(InvHdrID,MobInvID,StoreID,ItemID,ItemCode,LotID,LotCode,InvQty,MUnitPrimary,InvQtySecondary,MUnitSecondary) VALUES (");
            if (inv.InvHdrIDServer > 0) sqlstr.Append(inv.InvHdrIDServer.ToString() + ","); else sqlstr.Append("NULL,");
            if (inv.MobInvID > 0) sqlstr.Append(inv.MobInvID.ToString() + ","); else sqlstr.Append("NULL,");
            if (inv.StoreID > 0) sqlstr.Append(inv.StoreID.ToString() + ","); else sqlstr.Append("NULL,");
            if (inv.ItemID > 0) sqlstr.Append(inv.ItemID.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(inv.ItemCode)) sqlstr.Append("'" + inv.ItemCode + "',"); else sqlstr.Append("NULL,");
            if (inv.LotID > 0) sqlstr.Append(inv.LotID.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(inv.LotCode)) sqlstr.Append("'" + inv.LotCode + "',"); else sqlstr.Append("NULL,");
            if (inv.InvQty > 0) sqlstr.Append(inv.InvQty.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (inv.MUnitPrimary > 0) sqlstr.Append(inv.MUnitPrimary.ToString() + ","); else sqlstr.Append("NULL,");
            if (inv.InvQtySecondary > 0) sqlstr.Append(inv.InvQtySecondary.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (inv.MUnitSecondary > 0) sqlstr.Append(inv.MUnitSecondary.ToString()); else sqlstr.Append("NULL");

            sqlstr.Append(")");
            try { rtrn = db.DBExecuteSQLCmd(sqlstr.ToString()); }
            catch (Exception ex) { db.f_sqlerrorlog(0, "InsertRecord>>", ex.ToString().Replace("'", "|"), ">>", "Webservice", ">>"); }

            if (!(rtrn > 0))
                db.f_sqlerrorlog(0, "InsertRecord>>" + inv.ItemCode, sqlstr.ToString().Replace("'", "|"), ">>", "Webservice", ">>");
            return rtrn;
        }


        long UpdateRecord(TInventory inv)
        {
            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;
            sqlstr.Append("UPDATE TWMSInventory SET ");
            if (inv.ItemID > 0) sqlstr.Append("ItemID=" + inv.ItemID.ToString() + ","); else sqlstr.Append("ItemID=NULL,");
            if (inv.LotID > 0) sqlstr.Append("LotID=" + inv.LotID.ToString() + ","); else sqlstr.Append("LotID=NULL,");
            if (inv.InvQty > 0) sqlstr.Append("InvQty=" + inv.InvQty.ToString().Replace(",", ".") + ","); else sqlstr.Append("InvQty=NULL,");
            if (inv.MUnitPrimary > 0) sqlstr.Append("MUnitPrimary=" + inv.MUnitPrimary.ToString() + ","); else sqlstr.Append("MUnitPrimary=NULL,");
            if (inv.InvQtySecondary > 0) sqlstr.Append("InvQtySecondary=" + inv.InvQtySecondary.ToString().Replace(",", ".") + ","); else sqlstr.Append("InvQtySecondary=NULL,");
            if (inv.MUnitSecondary > 0) sqlstr.Append("MUnitSecondary=" + inv.MUnitSecondary.ToString() + ","); else sqlstr.Append("MUnitSecondary=NULL,");
            if (!string.IsNullOrEmpty(inv.ItemCode)) sqlstr.Append("ItemCode = '" + inv.ItemCode + "',"); else sqlstr.Append("ItemCode=NULL,");
            if (!string.IsNullOrEmpty(inv.LotCode)) sqlstr.Append("LotCode = '" + inv.LotCode + "'"); else sqlstr.Append("LotCode=NULL");
            sqlstr.Append(" WHERE MobInvID=" + inv.InvID.ToString() + " AND InvHdrID=" + inv.InvHdrIDServer.ToString());

            rtrn = db.DBExecuteSQLCmd(sqlstr.ToString());
            return rtrn;
        }

        //19/12/2024
        //NOT SURE IF THE ABOVE IS USED
        long UpdateRecord2(TInventory inv)
        {
            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;
            sqlstr.Append("UPDATE TWMSInventory SET ");
            if (inv.InvQty > 0) sqlstr.Append("InvQty=" + inv.InvQty.ToString().Replace(",", ".") + ","); else sqlstr.Append("InvQty=NULL,");
            if (inv.InvQtySecondary > 0) sqlstr.Append("InvQtySecondary=" + inv.InvQtySecondary.ToString().Replace(",", ".") ); else sqlstr.Append("InvQtySecondary=NULL");
            sqlstr.Append(" WHERE InvID=" + inv.InvID.ToString());

            rtrn = db.DBExecuteSQLCmd(sqlstr.ToString());
            return rtrn;
        }


        public long InventoryHeaderExists(long InvHdrID) 
        {

            if (db.DBGetNumResultFromSQLSelect("SELECT count(InvHdrID) FROM TWMSInventoryHeader WHERE InvHdrID=" + InvHdrID.ToString()) > 0) { return 1; }
            else { return -1; }
        
        }


        public DataTable GetInventoryTasks(int branchid)
        {
            string sqlstr;
            sqlstr = "SELECT InvHdrID as InvHdrID, CONVERT(NVARCHAR(10), invdate, 103) as InvDate,InvComments as InvComments,";
            sqlstr += "invRecords FROM VINVENTORYHEADERSTATS WHERE Branchid=" + branchid.ToString();
            sqlstr += " AND ISNULL(CONFIRMED,0) < 2 ORDER BY InvHdrID DESC";

            return db.DBFillDataTable(sqlstr, "TINV");
        }

        public DataTable GetInventoryRecords(long invHDRid,string itemfilter,bool last10items)
        {
            StringBuilder sqlstr = new StringBuilder();
            string top10rows = last10items ? " TOP(10) " : "";

            sqlstr.Append(" SELECT "+top10rows+" InvID,ItemID,ItemCode,LotCode,LotID,InvQty as InvQtyPrimary,InvQtySecondary FROM TWMSInventory WHERE InvHdrID= "+invHDRid.ToString()+" ");
            if (!string.IsNullOrEmpty(itemfilter) && !last10items)
            {
                sqlstr.Append(" AND (ItemCode LIKE '%" + itemfilter + "%' OR LotCode LIKE '%" + itemfilter + "%')");
            }
 
            sqlstr.Append(" ORDER BY InvID DESC");

            return db.DBFillDataTable(sqlstr.ToString(), "DTINVENTORY");
        }

        public DataTable GetInventoryRecord(long invid)
        {
            StringBuilder sqlstr = new StringBuilder();

            sqlstr.Append(" SELECT  InvID,InvHdrID,ItemID,ItemCode,LotCode,LotID,InvQty as InvQtyPrimary,InvQtySecondary, ");
            sqlstr.Append(" Munitprimary as InvMunitPrimary,MunitSecondary,InvDate as InvMunitSecondary");
            
            sqlstr.Append(" FROM TWMSInventory WHERE InvID= " + invid.ToString() + " ");

            return db.DBFillDataTable(sqlstr.ToString(), "DTINVENTORY");
        }


        public long DeleteInventoryRecord(long invid)
        {

            return db.DBExecuteSQLCmd("DELETE FROM TWMSInventory WHERE InvID = " + invid.ToString());
        }

    }

    public class InventoryHeaderHandler
    {
        public DB db = new DB();


        public DataTable InventoryLists()
        {
            return db.DBFillDataTable("SELECT * FROM TInventoryHeader WHERE InvStatus <> 1", "DTINVLIST");
        }

        public long CreateNewInventory(int compid, int branchid, int storeid)
        {

            TInventoryHeader invhdr = new TInventoryHeader();

            invhdr.CompID = (short)compid;
            invhdr.Branchid = (short)branchid;
            invhdr.Storeid = storeid;
            invhdr.InvComments = "Απογραφή Offline " + DateTime.Now.ToShortDateString();

            //string invcomments = "Απογραφή Offline " + DateTime.Now.ToShortDateString();

            if (storeid > 0) invhdr.InvComments += " A.X. :" + storeid.ToString();

            return InsertRecord(invhdr);

        }

        public TInventoryHeader Parse(DataRow Dr)
        {
            TInventoryHeader invhdr = new TInventoryHeader();

            try { invhdr.InvHdrID = long.Parse(Dr["InvHdrID"].ToString()); }
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
            try { invhdr.MobInvHdrID = long.Parse(Dr["MobInvHdrID"].ToString()); }
            catch { }
            try { invhdr.InvSyncID = long.Parse(Dr["InvSyncID"].ToString()); }
            catch { }


            return invhdr;
        }

        public long UpdateInventoryHeader(TInventoryHeader invhdr)
        {
            if (db.DBGetNumResultFromSQLSelect("SELECT InvHdrID FROM TWMSInventoryHeader WHERE MobInvHdrID=" + invhdr.MobInvHdrID.ToString()) > 0)
                return UpdateRecord(invhdr);
            else
                return InsertRecord(invhdr);
        }

        long InsertRecord(TInventoryHeader invhdr)
        {
            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;

            //sqlstr.Append("INSERT INTO TWMSInventoryHeader(MobInvHdrID,InvSyncID,CompID,BranchID,StoreID,InvDate,InvComments) VALUES (");           
            sqlstr.Append("INSERT INTO TWMSInventoryHeader(MobInvHdrID,InvSyncID,CompID,BranchID,StoreID,Confirmed,InvComments) VALUES (");
            if (invhdr.MobInvHdrID > 0) sqlstr.Append(invhdr.MobInvHdrID.ToString() + ","); else sqlstr.Append("NULL,");
            if (invhdr.InvSyncID > 0) sqlstr.Append(invhdr.InvSyncID.ToString() + ","); else sqlstr.Append("NULL,");
            if (invhdr.CompID > 0) sqlstr.Append(invhdr.CompID.ToString() + ","); else sqlstr.Append("NULL,");
            if (invhdr.Branchid > 0) sqlstr.Append(invhdr.Branchid.ToString() + ","); else sqlstr.Append("NULL,");
            if (invhdr.Storeid > 0) sqlstr.Append(invhdr.Storeid.ToString() + ",0"); else sqlstr.Append("NULL,0");
            //if (!string.IsNullOrEmpty(invhdr.InvDate)) sqlstr.Append("CONVERT(DATETIME,'" + invhdr.InvDate + "',103),"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(invhdr.InvComments)) sqlstr.Append("'" + invhdr.InvComments + "'"); else sqlstr.Append("NULL");
            sqlstr.Append(")");
            rtrn = db.DBExecuteSQLCmd(sqlstr.ToString());

            if (rtrn > 0) return db.DBGetNumResultFromSQLSelect("SELECT IDENT_CURRENT('TWMSInventoryHeader')");
            else
            {
                if (!(rtrn > 0)) db.f_sqlerrorlog(0, "InventoryHeader.InsertRecord", sqlstr.ToString().Replace("'", "|"), "", "WebService", "");
                return -1;
            }

        }

        long UpdateRecord(TInventoryHeader invhdr)
        {
            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;
            sqlstr.Append("UPDATE TWMSInventoryHeader SET ");
            if (invhdr.Storeid > 0) sqlstr.Append("StoreID=" + invhdr.Storeid.ToString() + ","); else sqlstr.Append("StoreID=NULL,");
            if (invhdr.MobInvHdrID > 0) sqlstr.Append("MobInvHdrID=" + invhdr.MobInvHdrID.ToString() + ","); else sqlstr.Append("MobInvHdrID=NULL,");
            if (!string.IsNullOrEmpty(invhdr.InvComments)) sqlstr.Append("InvComments = '" + invhdr.InvComments + "'"); else sqlstr.Append("InvComments=NULL");
            sqlstr.Append(" WHERE InvSyncID=" + invhdr.InvSyncID.ToString());
            rtrn = db.DBExecuteSQLCmd(sqlstr.ToString());

            if (!(rtrn > 0)) db.f_sqlerrorlog(0, "InventoryHeader.UpdateRecord", sqlstr.ToString(), "", "WebService", "");
            if (rtrn > 0)
                // return db.DBGetNumResultFromSQLSelect("SELECT InvHdrID FROM TWMSInventoryHeader WHERE MobInvHdrID=" + invhdr.MobInvHdrID.ToString());
                return invhdr.InvHdrID;
            else
                return -1;

        }




    }



    #endregion

#region OTHERS
public class DBGeneralOperations
{

public string CheckDBConnection()
{
    DB db = new DB();
    try
    {
        return db.DBGetNumResultFromSQLSelect("SELECT COUNT(*) AS RCNT FROM TDual").ToString();
    }
    catch (Exception ex) {return db.SQLErrText; }
}

public long FindCodeByID(string table, string id)
{
    DB db = new DB();

    StringBuilder sqlstr = new StringBuilder();

    switch (table)
    {
        case TablesCollection.TItems:
            {
                sqlstr.Append("SELECT ITEMCODE FROM TITEMS WHERE ITEMID=" + id.ToString());
                break;
            }
        case TablesCollection.TItemLot:
            {
                sqlstr.Append("SELECT LOTCODE FROM TITEMLOT WHERE LOTID=" + id.ToString());
                break;
            }
        default:
            break;
    }
    return db.DBGetNumResultFromSQLSelect(sqlstr.ToString());

}


}

public struct AppXMLFiles
{
public static string XMLItems = "XMLItems.xml";
public static string XMLLots = "XMLLot.xml";
public static string XMLStores = "XMLStores.xml";
public static string XMLMUnits = "XMLMUnits.xml";
public static string XMLInvHeaders = "XMLInvHeaders.xml";
}




#endregion





#region Receives

public class ReceivesController
{ 
    DB db = new DB();
    Items items = new Items();
    ItemLots lots = new ItemLots();
    StringBuilder sqlstr = new StringBuilder();

    public void WriteToLog(string message)
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

    public long InsertNewTranscode(TransCodeHeader t)
    {

        
        sqlstr.Length = 0;
        
        if ( db.DBGetNumResultFromSQLSelect("SELECT COUNT(*) FROM TWMSRECEIVES WHERE FTRID = "+t.FtrID.ToString()) > 0 )return -1;

        sqlstr.Append("INSERT INTO TWMSRECEIVES(");
        sqlstr.Append("FTRID,");
        sqlstr.Append("CONFIRMED,");
        sqlstr.Append("RECEIVEDATE,");
        sqlstr.Append("COMPID,");
        sqlstr.Append("BRANCHID,");
        sqlstr.Append("SRCDOCUMENT,");
        sqlstr.Append("STOREIDFROM,");
        sqlstr.Append("STOREIDTO,");
        sqlstr.Append("TRANSDATE");
        sqlstr.Append(")");
        sqlstr.Append(" VALUES(");


        if (t.FtrID > 0) sqlstr.Append(t.FtrID.ToString() + ","); else sqlstr.Append("NULL,");
             sqlstr.Append("0,CONVERT(DATETIME,GETDATE(),103),");
        if (t.compid > 0) sqlstr.Append(t.compid.ToString() + ","); else sqlstr.Append("NULL,");
        if (t.branchid > 0) sqlstr.Append(t.branchid.ToString() + ","); else sqlstr.Append("NULL,");
        if (!string.IsNullOrEmpty(t.tradecode)) sqlstr.Append("'"+t.tradecode.ToString().Replace("'","|") + "',"); else sqlstr.Append("NULL,");
        if (t.fromstoreid > 0) sqlstr.Append(t.fromstoreid.ToString() + ","); else sqlstr.Append("NULL,");
        if (t.tostoreid > 0) sqlstr.Append(t.tostoreid.ToString() + ","); else sqlstr.Append("NULL,");
        if (!string.IsNullOrEmpty(t.transdate)) sqlstr.Append("CONVERT(DATETIME,'" + t.transdate + "',103)" + ")"); else sqlstr.Append("NULL)");

        
       return db.DBExecuteSQLCmd(sqlstr.ToString());
 
    }


    public long InsertNewTranscodeDetail(TransCodeDetail tdtl)
    {
        Log mylog = new Log();
        sqlstr.Length = 0;
        ERPItem item;
        ERPLot itemlot;
        mylog.WriteToLog("About to insert zwidth = " + tdtl.Zwidth.ToString());
        //CHECK IF ITEM AND LOT EXIST IN TABLE

        if (db.DBGetNumResultwithZero("SELECT COUNT(*) FROM TITEMS WHERE ITEMID = " + tdtl.ItemID.ToString()) == 0) 
        {
            item = new ERPItem();
            item.ItemID = tdtl.ItemID;
            item.ItemCode = tdtl.itemcode;
            item.ItemDesc = tdtl.itemdesc;
            item.MUnitPrimary = tdtl.munitprimary;
            item.MUnitSecondary = tdtl.munitsecondary;
            item.CompID = 2;
            
            items.FInsertItem(item);


        }

        if (db.DBGetNumResultwithZero("SELECT COUNT(*) FROM TITEMLOT WHERE LOTID = " + tdtl.LotID.ToString()) == 0)
        {
            itemlot = new ERPLot();
            itemlot.Color = tdtl.zcolor;
            itemlot.Draft = tdtl.zdraft;
            itemlot.ItemID = tdtl.ItemID;
            itemlot.LotID = tdtl.LotID;
            itemlot.Length = tdtl.Zlength;
            itemlot.Width = tdtl.Zwidth;
            itemlot.LotCode = tdtl.lotcode;
            itemlot.CompID = 2;
            lots.FInsertLot(itemlot);

        }
        //


        sqlstr.Append("INSERT INTO TWMSTRANSCODEDETAILS(");
        sqlstr.Append("RECEIVEID,");
        sqlstr.Append("FTRID,");
        sqlstr.Append("ITEMID,");
        sqlstr.Append("ITEMLOTID,");
        sqlstr.Append("LOTCODE,");
        sqlstr.Append("ITEMCODE,");

        sqlstr.Append("WIDTH,");
        sqlstr.Append("LENGTH,");
        sqlstr.Append("COLOR,");
        sqlstr.Append("DRAFT,");

        sqlstr.Append("ITEMMUNITPRIMARY,");
        sqlstr.Append("ITEMMUNITSECONDARY,");
        sqlstr.Append("ITEMPRIMARYQTY,");
        sqlstr.Append("ITEMSECONDARYQTY)");
        sqlstr.Append(" VALUES (");
                
        
        if (tdtl.ReceiveID > 0) sqlstr.Append(tdtl.ReceiveID.ToString() + ","); else sqlstr.Append("NULL,");
        if (tdtl.FtrID > 0) sqlstr.Append(tdtl.FtrID.ToString() + ","); else sqlstr.Append("NULL,");
        if (tdtl.ItemID > 0) sqlstr.Append(tdtl.ItemID.ToString() + ","); else sqlstr.Append("NULL,");
        if (tdtl.LotID > 0) sqlstr.Append(tdtl.LotID.ToString() + ","); else sqlstr.Append("NULL,");
        if (!string.IsNullOrEmpty(tdtl.lotcode)) sqlstr.Append("'"+tdtl.lotcode.ToString() + "',"); else sqlstr.Append("NULL,");
        if (!string.IsNullOrEmpty(tdtl.itemcode)) sqlstr.Append("'" + tdtl.itemcode.ToString() + "',"); else sqlstr.Append("NULL,");

        if (tdtl.Zwidth > 0) sqlstr.Append(tdtl.Zwidth.ToString().Replace(",",".") + ","); else sqlstr.Append("NULL,");
        if (tdtl.Zlength > 0) sqlstr.Append(tdtl.Zlength.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
        if (!string.IsNullOrEmpty(tdtl.zcolor)) sqlstr.Append("'" + tdtl.zcolor.ToString() + "',"); else sqlstr.Append("NULL,");
        if (!string.IsNullOrEmpty(tdtl.zdraft)) sqlstr.Append("'" + tdtl.zdraft.ToString() + "',"); else sqlstr.Append("NULL,");


        if (tdtl.munitprimary > 0) sqlstr.Append(tdtl.munitprimary.ToString() + ","); else sqlstr.Append("NULL,");
        if (tdtl.munitsecondary > 0) sqlstr.Append(tdtl.munitsecondary.ToString() + ","); else sqlstr.Append("NULL,");
        if (tdtl.ItemQtyPrimary > 0) sqlstr.Append(tdtl.ItemQtyPrimary.ToString().Replace(",",".") + ","); else sqlstr.Append("NULL,");
        if (tdtl.ItemQtySecondary > 0) sqlstr.Append(tdtl.ItemQtySecondary.ToString().Replace(",", ".") + ")"); else sqlstr.Append("NULL)");


        return db.DBExecuteSQLCmd(sqlstr.ToString());

    }


    public long GetReceiveIDfromFTRID(long ftrid) 
    {
      return  db.DBGetNumResultFromSQLSelect("select receiveid from twmsreceives where ftrid = " + ftrid.ToString());
    
    }

    public DataTable InsertNewReceiveDTL(TransCodeDetail tdtl)
    {
        sqlstr.Length = 0;
 


        if (db.DBGetNumResultwithZero("SELECT CONFIRMED FROM TWMSRECEIVES WHERE FTRID = " + tdtl.FtrID.ToString()) == 1) return new DataTable("NULLTABLE");
        

        if (db.DBGetNumResultwithZero("SELECT COUNT(*) FROM TITEMS WHERE ITEMID = " + tdtl.ItemID.ToString()) == 0)
        {
            ERPItem item = new ERPItem();
            item.ItemID = tdtl.ItemID;
            item.ItemCode = tdtl.itemcode;
            item.ItemDesc = tdtl.itemdesc;
            item.MUnitPrimary = tdtl.munitprimary;
            item.MUnitSecondary = tdtl.munitsecondary;
            item.CompID = 2;

            items.FInsertItem(item);


        }

        if (db.DBGetNumResultwithZero("SELECT COUNT(*) FROM TITEMLOT WHERE LOTID = " + tdtl.LotID.ToString()) == 0)
        {
            ERPLot itemlot = new ERPLot();
            itemlot.Color = tdtl.zcolor;
            itemlot.Draft = tdtl.zdraft;
            itemlot.ItemID = tdtl.ItemID;
            itemlot.LotID = tdtl.LotID;
            itemlot.Length = tdtl.Zlength;
            itemlot.Width = tdtl.Zwidth;
            itemlot.LotCode = tdtl.lotcode;
            itemlot.CompID = 2;
            lots.FInsertLot(itemlot);

        }


        sqlstr.Append("INSERT INTO TWMSRECEIVESDTL(");
        sqlstr.Append("RECEIVEID,");
        sqlstr.Append("FTRID,");
        sqlstr.Append("ITEMID,");
        sqlstr.Append("ITEMLOTID,");
        sqlstr.Append("LOTCODE,");
        sqlstr.Append("ITEMCODE,");

        sqlstr.Append("WIDTH,");
        sqlstr.Append("LENGTH,");
        sqlstr.Append("COLOR,");
        sqlstr.Append("DRAFT,");

        sqlstr.Append("ITEMMUNITPRIMARY,");
        sqlstr.Append("ITEMMUNITSECONDARY,");
        sqlstr.Append("ITEMPRIMARYQTY,");
        sqlstr.Append("ITEMSECONDARYQTY)");
        sqlstr.Append(" VALUES (");



        if (tdtl.ReceiveID > 0) sqlstr.Append(tdtl.ReceiveID.ToString() + ","); else sqlstr.Append("NULL,");
        if (tdtl.FtrID > 0) sqlstr.Append(tdtl.FtrID.ToString() + ","); else sqlstr.Append("NULL,");
        if (tdtl.ItemID > 0) sqlstr.Append(tdtl.ItemID.ToString() + ","); else sqlstr.Append("NULL,");
        if (tdtl.LotID > 0) sqlstr.Append(tdtl.LotID.ToString() + ","); else sqlstr.Append("NULL,");
        if (!string.IsNullOrEmpty(tdtl.lotcode)) sqlstr.Append("'" + tdtl.lotcode.ToString() + "',"); else sqlstr.Append("NULL,");
        if (!string.IsNullOrEmpty(tdtl.itemcode)) sqlstr.Append("'" + tdtl.itemcode.ToString() + "',"); else sqlstr.Append("NULL,");


        if (tdtl.Zwidth > 0) sqlstr.Append(tdtl.Zwidth.ToString().Replace(",",".") + ","); else sqlstr.Append("NULL,");
        if (tdtl.Zlength > 0) sqlstr.Append(tdtl.Zlength.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");

        if (!string.IsNullOrEmpty(tdtl.zcolor)) sqlstr.Append("'" + tdtl.zcolor.ToString() + "',"); else sqlstr.Append("NULL,");
        if (!string.IsNullOrEmpty(tdtl.zdraft)) sqlstr.Append("'" + tdtl.zdraft.ToString() + "',"); else sqlstr.Append("NULL,");
 
        
        
        if (tdtl.munitprimary > 0) sqlstr.Append(tdtl.munitprimary.ToString() + ","); else sqlstr.Append("NULL,");
        if (tdtl.munitsecondary > 0) sqlstr.Append(tdtl.munitsecondary.ToString() + ","); else sqlstr.Append("NULL,");
        if (tdtl.ItemQtyPrimary > 0) sqlstr.Append(tdtl.ItemQtyPrimary.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
        if (tdtl.ItemQtySecondary > 0) sqlstr.Append(tdtl.ItemQtySecondary.ToString().Replace(",", ".") + ")"); else sqlstr.Append("NULL)");

        Log log = new Log();
        log.WriteToLog(sqlstr.ToString());


        db.DBExecuteSQLCmd(sqlstr.ToString());



        return GetTransRemains(tdtl.FtrID);

    }

    public TransCodeDetail GetTransCodeDetail(string lotcode) 
    {

        sqlstr.Length = 0;
    sqlstr.Append(" SELECT ");
    sqlstr.Append(" TITEMS.ITEMID,");
    sqlstr.Append(" TITEMLOT.LOTID AS ITEMLOTID,");
    sqlstr.Append(" TITEMS.ITEMCODE,");
    sqlstr.Append(" TITEMS.ITEMDESC,");
    sqlstr.Append(" TITEMLOT.WIDTH,");
    sqlstr.Append(" TITEMLOT.LENGTH,");
    sqlstr.Append(" TITEMLOT.DRAFT,");
    sqlstr.Append(" TITEMLOT.COLOR");
    sqlstr.Append(" FROM TITEMLOT,TITEMS");
    sqlstr.Append(" WHERE TITEMLOT.ITEMID = TITEMS.ITEMID");
    sqlstr.Append(" AND TITEMLOT.LOTCODE = '"+lotcode+"'");


        TransCodeDetail tdtl = new TransCodeDetail();
        IDataReader dr = db.DBReturnDatareaderResults(sqlstr.ToString());

            if (dr != null)
            {
                while (dr.Read())
                {


                    tdtl.lotcode = lotcode; 
                     if (dr["ITEMID"] != DBNull.Value) tdtl.ItemID = long.Parse(dr["ITEMID"].ToString());
                    if (dr["ITEMLOTID"] != DBNull.Value) tdtl.LotID = long.Parse(dr["ITEMLOTID"].ToString());
                    if (dr["ITEMCODE"] != DBNull.Value) tdtl.itemcode = dr["ITEMCODE"].ToString();
                    if (dr["ITEMDESC"] != DBNull.Value) tdtl.itemdesc = dr["ITEMDESC"].ToString();
                    if (dr["WIDTH"] != DBNull.Value) tdtl.Zwidth =  decimal.Parse(dr["WIDTH"].ToString());
                    if (dr["LENGTH"] != DBNull.Value) tdtl.Zlength = decimal.Parse(dr["LENGTH"].ToString());
                    if (dr["COLOR"] != DBNull.Value) tdtl.zcolor = dr["COLOR"].ToString();
                    if (dr["DRAFT"] != DBNull.Value) tdtl.zdraft = dr["DRAFT"].ToString();
                }
            }

    return tdtl;
    
    }

    public DataTable GetTransList(short branchid)
    {
        sqlstr.Length = 0;

        sqlstr.Append(" select TWMSReceives.receiveid,TWMSReceives.ftrid,TWMSReceives.Branchid,SrcDocument,TBranches.branchname, ");
        sqlstr.Append("CONVERT(NVARCHAR, DAY(transdate)) + '/' + CONVERT(NVARCHAR, MONTH(transdate)) + '/' + RIGHT(CONVERT(NVARCHAR,YEAR(transdate)), 2) AS transdate ");
        sqlstr.Append("  from TWMSReceives,TBranches  where ");
        sqlstr.Append(" TWMSReceives.Branchid = TBranches.Branchid ");
        sqlstr.Append(" and TWMSReceives.Branchid = "+branchid.ToString());
        sqlstr.Append(" and isnull(Confirmed,0) = 0 ");


        return db.DBFillDataTable(sqlstr.ToString(), "DTRECEIVES");
 
    
    }


    public DataTable GetTransRemains(long ftrid) 
    {

                sqlstr.Length = 0;

        //        sqlstr.Append(" SELECT * FROM ( SELECT      isnull(v1.LotCode,v2.LotCode) as LotCode,  ISNULL(CAST(v1.qty AS INTEGER),0) as qty, CAST(isnull(v2.scannedqty,0) AS INTEGER) as scannedqty");
        //sqlstr.Append(" FROM         (SELECT     FtrID, LotCode, SUM(ItemPrimaryQty) AS qty");
        //sqlstr.Append(" FROM          dbo.TWMSTransCodeDetails");
        //sqlstr.Append(" WHERE ftrid = "+ftrid.ToString()+" GROUP BY FtrID, LotCode) AS v1 FULL OUTER JOIN");
        //sqlstr.Append(" (SELECT     FtrID, LotCode, SUM(ItemPrimaryQty) AS scannedqty");
        //sqlstr.Append(" FROM          dbo.TWMSReceivesDtl");
        //sqlstr.Append(" WHERE ftrid = " + ftrid.ToString() + "GROUP BY FtrID, LotCode) AS v2 ON v1.FtrID = v2.FtrID AND v1.LotCode = v2.LotCode ) Vjob where qty<>scannedqty ");

        sqlstr.Append(" SELECT * FROM ( SELECT isnull(v1.LotCode, v2.LotCode) as LotCode, ");
        sqlstr.Append(" ISNULL(CAST(v1.qty AS INTEGER), 0) as qty, CAST(isnull(v2.scannedqty, 0) AS INTEGER) ");
        sqlstr.Append(" as scannedqty, v1.width, v1.length, v1.color, v1.draft, v1.itemid FROM ");
        sqlstr.Append(" ( SELECT FtrID, LotCode, SUM(ItemPrimaryQty) AS qty, width, length, color, draft, itemid ");
        sqlstr.Append(" FROM dbo.TWMSTransCodeDetails WHERE ftrid =  " + ftrid.ToString() + " ");
        sqlstr.Append(" GROUP BY FtrID, LotCode, width, length, color, draft, itemid ) AS v1 ");
        sqlstr.Append(" FULL OUTER JOIN ( SELECT FtrID, LotCode, SUM(ItemPrimaryQty) AS scannedqty, width, length, color, draft, itemid ");
        sqlstr.Append(" FROM dbo.TWMSReceivesDtl WHERE ftrid =  " + ftrid.ToString() + " GROUP BY FtrID, LotCode, width, length, color, draft, itemid ) AS v2 ON v1.FtrID = v2.FtrID ");
        sqlstr.Append(" AND v1.width = v2.width AND v1.length = v2.length AND ISNULL(v1.draft,'') = ISNULL(v2.draft,'') AND ");
        sqlstr.Append(" v1.color = v2.color AND v1.itemid = v2.itemid ) Vjob WHERE qty <> scannedqty");


        return db.DBFillDataTable(sqlstr.ToString(), "DTREMAINS");

 
    
    }



}




#endregion








}
