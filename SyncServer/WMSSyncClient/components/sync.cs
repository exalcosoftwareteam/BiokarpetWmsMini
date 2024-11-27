using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using WMSSyncClient.WMSSyncService;

namespace WMSSyncClient.components
{
    public class Store
    {
        short storeid;
        short compID;
        short branchid;
        string storeName;

        public short StoreID { get { return storeid; } set { storeid = value; } }
        public short CompID { get { return compID; } set { compID = value; } }
        public short BranchID { get { return branchid; } set { branchid = value; } }
        public string StoreName { get { return storeName; } set { storeName = value; } }

    }

    public class MUnits
    {
        int munitID;
        short compID;
        string mUnit;
        short munitDecimals;

        public int MunitID { get { return munitID; } set { munitID = value; } }
        public short CompID { get { return compID; } set { compID = value; } }
        public short MunitDecimals { get { return munitDecimals; } set { munitDecimals = value; } }
        public string MUnit { get { return mUnit; } set { mUnit = value; } }
    }
   
    public class Stock
    {
        public short CompID;
        public int BranchID;
        public int StoreID;
        public long ItemID;
        public long LotID;
        public decimal ItemPrimaryQty;
        public decimal ItemSecondaryQty;
    }

    public class MUnitHandler
    {
        DB db = new DB();

        public MUnits Parse(ERPMunit erpmunit)
        {
            MUnits munit = new MUnits();
            munit.MunitID = erpmunit.munitID;
            munit.CompID = erpmunit.compID;
            munit.MunitDecimals = erpmunit.munitDecimals;
            munit.MUnit = erpmunit.mUnit;

            
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

    public class SyncItems
    {
        public long ItemsTransfered { get; set; }
        public long LotsTransfered { get; set; }
        public int noninsertedItems { get;set; }

        AppSettings appsettings = new AppSettings();

        public SyncInfo syncinfo = new SyncInfo();
        DB dbtrans = new DB();

        
        public SyncItems()
        {
            ItemsTransfered = 0;
            LotsTransfered = 0;
        }

        public void FPrepareTemporaryData(int storeid)
        {
            syncinfo = appsettings.GWSyncSVCService.SOA_CreateTemporaryData(storeid);
        }

        public List<Store> StoresFromWebService()
        {
            List<Store> erpstores = new List<Store>();

            ERPStore[] ERPstoresarray = null;

            try { ERPstoresarray = appsettings.GWSyncSVCService.SOA_GetStores((short)appsettings.CompID); }
            catch { }

            for (int i = 0; i < ERPstoresarray.Length; i++)
            {
                Store st = new Store();
                st.StoreID = ERPstoresarray[i].storeID;
                st.CompID = ERPstoresarray[i].compID;
                st.StoreName = ERPstoresarray[i].storeName;

                erpstores.Add(st);
            }

            return erpstores;
        }

        public List<ERPMunit> MunitsFromWebService()
        {
            List<ERPMunit> munits = new List<ERPMunit>();
            ERPMunit[] munitsarray = appsettings.GWSyncSVCService.SOA_GetMUnits();

            for (int i = 0; i < munitsarray.Length; i++)
            {
                munits.Add(munitsarray[i]);
            }

            return munits;
        }

        public long FGetMunits()
        {
            MUnits munit = new MUnits();
            MUnitHandler munithandler = new MUnitHandler();

            List<ERPMunit> ERPmunits = new List<ERPMunit>();
            long r = 0;
            try { ERPmunits = MunitsFromWebService(); }
            catch { }

            for (int i = 0; i < ERPmunits.Count; i++)
            {
                munit = munithandler.Parse(ERPmunits[i]);
                r += munithandler.UpdateMunit(munit);
            }

                return r;
        }
        
        public long FGetItemsData( int storeid)
        {
            long raffected=0;
            ItemsTransfered = 0;
            int blkrows = 500;

            if (syncinfo.ItemsRowsCount > 0)
            {
                if (syncinfo.ItemsRowsCount > blkrows)
                {
                    for (long k = syncinfo.MinItemRowid; k <= syncinfo.MaxItemRowid; k += blkrows)
                    {
                        raffected += FGetPartiallyItems(storeid, k, (k-1) + blkrows);                                              
                    }
                }
                else
                    raffected += FGetPartiallyItems(storeid,0, 0); //all                                              
            }

            ItemsTransfered += raffected;

            return raffected;
        }

        public long FGetLotsData(int storeid)
        {
            long raffected = 0;
            long startpoint = 0;//, endpoint = 0;

            int blkrows = 500;


            LotsTransfered = 0;
            if (syncinfo.LotRowsCount > 0)
            {
                if (syncinfo.LotRowsCount > blkrows)
                {
                    startpoint = syncinfo.MinLotRowID;
                    for (long k = syncinfo.MinLotRowID; k <= syncinfo.MaxLotRowID; k += blkrows)
                    {
                        raffected += FGetPartiallyLots(storeid, k, (k - 1) + blkrows);
                    }
                }
                else
                    raffected += FGetPartiallyLots(storeid, 0, 0);
            }

            LotsTransfered = raffected;
            return raffected;
        }


        private long FGetPartiallyItems(int storeid,long startpoint, long endpoint)
        {            
            SyncERPItem[] ERPitemList = null;

            ERPitemList = appsettings.GWSyncSVCService.SOA_GetInventoryItems(storeid, startpoint, endpoint);
                        
            long raffected=0;


            for (int i = 0; i < ERPitemList.Length; i++)
            {
                SyncERPItem erpitem = new SyncERPItem();
                erpitem = ERPitemList[i];

                if (FInsertItem(erpitem) > 0)
                        raffected++;             
            }
                       
            return raffected;
        }      
       

        private long FInsertItem( SyncERPItem item)
        {
            if (ItemExists(item.ItemID)) return FUpdateItem(item);

            StringBuilder sqlstr = new StringBuilder();

            if (string.IsNullOrEmpty(item.EntryDate)) item.EntryDate = DateTime.Now.ToShortDateString();

            sqlstr.Append("INSERT INTO TItems  (ItemID, CompID, ItemCode, ItemDesc, MUnitPrimary, MUnitSecondary,ENTRYDATE) VALUES     ( ");
            if (item.ItemID > 0) sqlstr.Append(item.ItemID.ToString() + ","); else sqlstr.Append("NULL,");
            if (item.CompID > 0) sqlstr.Append(item.CompID.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(item.ItemCode)) sqlstr.Append("'" + item.ItemCode + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(item.ItemDesc)) sqlstr.Append("'" + item.ItemDesc + "',"); else sqlstr.Append("NULL,");
            if (item.MUnitPrimary > 0) sqlstr.Append(item.MUnitPrimary.ToString() + ","); else sqlstr.Append("NULL,");
            if (item.MUnitSecondary > 0) sqlstr.Append(item.MUnitSecondary.ToString() + ","); else sqlstr.Append("NULL,");

            if (!string.IsNullOrEmpty(item.EntryDate))
            {
                item.EntryDate = item.EntryDate.Replace("πμ", "am");
                item.EntryDate=item.EntryDate.Replace("μμ", "pm");
                try { if (DateTime.Parse(item.EntryDate) > DateTime.Parse("1/1/1900")) sqlstr.Append("CONVERT(DateTime,'" + item.EntryDate + "',103)"); else sqlstr.Append("NULL");  }
                 catch 
                {
                    sqlstr.Append("NULL");
                
                }
                
            }
            else
                sqlstr.Append("NULL");
            
            sqlstr.Append(")");
            return dbtrans.DBExecuteSQLCmd(sqlstr.ToString());
        }

        private long FUpdateItem(SyncERPItem item)
        {
            StringBuilder sqlstr = new StringBuilder();
            if (!(item.ItemID > 0) ) return 0;

            sqlstr.Append("UPDATE    TItems SET  ");
            if (item.MUnitPrimary > 0) sqlstr.Append("MUnitPrimary=" + item.MUnitPrimary.ToString() + ","); else sqlstr.Append("MUnitPrimary=NULL,");
            if (item.MUnitSecondary > 0) sqlstr.Append("MUnitSecondary=" + item.MUnitSecondary.ToString() ); else sqlstr.Append("MUnitSecondary=NULL");            
            sqlstr.Append(" WHERE ITEMID=" + item.ItemID.ToString());
            return dbtrans.DBExecuteSQLCmd(sqlstr.ToString());

        }

        private bool ItemExists(long itemid)
        {
            bool ex = false;
            try { if (dbtrans.DBGetNumResultFromSQLSelect ("SELECT ITEMID FROM TITEMS WHERE ITEMID=" + itemid.ToString()) > 0) ex = true; }
            catch { }
            return ex;
        }

        private long FGetPartiallyLots(int storeid,long startpoint, long endpoint)
        {
            SyncLot[] lostList = null;

            lostList = appsettings.GWSyncSVCService.SOA_GetInventoryLots(storeid, startpoint, endpoint);


            long raffected = 0;

            for (int i = 0; i < lostList.Length; i++)
            {                              
               if (FInsertLot(lostList[i]) > 0)
                  raffected++;                
            }
                
            return raffected;
        }
     

        private long FInsertLot(SyncLot lot)
        {            
            if (LotExists(lot.LotID)) return FUpdateLot(lot);

            if (string.IsNullOrEmpty(lot.EntryDate)) lot.EntryDate = DateTime.Now.ToShortDateString();
            StringBuilder sqlstr = new StringBuilder();

            sqlstr.Append("INSERT INTO TItemLot  (LOTID, compid, ItemID, LOTCode,Width,Length,ItemPrimaryQty,ItemSecondaryQty,Color,Draft,ENTRYDATE) VALUES     ( ");
            if (lot.LotID > 0) sqlstr.Append(lot.LotID.ToString() + ","); else sqlstr.Append("NULL,");
            if (lot.CompID > 0) sqlstr.Append(lot.CompID.ToString() + ","); else sqlstr.Append("NULL,");
            if (lot.ItemID > 0) sqlstr.Append(lot.ItemID.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(lot.LotCode)) sqlstr.Append("'" + lot.LotCode + "',"); else sqlstr.Append("NULL,");
            if (lot.Width > 0) sqlstr.Append(lot.Width.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (lot.Length > 0) sqlstr.Append(lot.Length.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");          
            if (lot.ItemPrimaryQty > 0) sqlstr.Append(lot.ItemPrimaryQty.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (lot.ItemSecondaryQty > 0) sqlstr.Append(lot.ItemSecondaryQty.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(lot.Color)) sqlstr.Append("'" + lot.Color + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(lot.Draft)) sqlstr.Append("'" + lot.Draft + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(lot.EntryDate))
                try { if (DateTime.Parse(lot.EntryDate) > DateTime.Parse("1/1/1900")) sqlstr.Append("CONVERT(DateTime,'" + lot.EntryDate + "',103)"); else sqlstr.Append("NULL"); }
                catch { }
                
            else
                sqlstr.Append("NULL");

            sqlstr.Append(")");
            return dbtrans.DBExecuteSQLCmd(sqlstr.ToString());
        }

        private long FUpdateLot(SyncLot lot)
        {
            StringBuilder sqlstr = new StringBuilder();           

            sqlstr.Append("UPDATE    TItemLot SET  ");
            if (lot.Width > 0) sqlstr.Append("Width=" + lot.Width.ToString().Replace(",", ".") + ","); else sqlstr.Append("Width=0,");
            if (lot.Length > 0) sqlstr.Append("Length=" + lot.Length.ToString().Replace(",", ".") + ","); else sqlstr.Append("Length=0,");
            if (!string.IsNullOrEmpty(lot.Draft)) sqlstr.Append("Draft='" + lot.Draft + "',"); else sqlstr.Append("Draft=NULL,");
            if (!string.IsNullOrEmpty(lot.Color)) sqlstr.Append("Color='" + lot.Color + "',"); else sqlstr.Append("Color=NULL,");
            if (lot.ItemPrimaryQty > 0) sqlstr.Append("ItemPrimaryQty=" + lot.ItemPrimaryQty.ToString().Replace(",", ".") + ","); else sqlstr.Append("ItemPrimaryQty=0,");
            if (lot.ItemSecondaryQty > 0) sqlstr.Append("ItemSecondaryQty=" + lot.ItemSecondaryQty.ToString().Replace(",", ".")); else sqlstr.Append("ItemSecondaryQty=0 ");
            sqlstr.Append(" WHERE LOTID=" + lot.LotID.ToString());
            long rtrn = dbtrans.DBExecuteSQLCmd(sqlstr.ToString());
            return rtrn;
        }

        private bool LotExists(long lotid)
        {
            bool ex=false;
            try { if (dbtrans.DBGetNumResultFromSQLSelect("SELECT LOTID FROM TITEMLOT WHERE LOTID=" + lotid.ToString()) > 0) ex = true; }
            catch { }
            return ex;
        }
    }

    
}
