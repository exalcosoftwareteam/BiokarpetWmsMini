using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using WMSSyncService;


namespace WMSSyncService 
{
    public class TOrderDetails
    {
        public int id { get; set; }
        public long orderdtlid { get; set; }
        public short compid { get; set; }
        public long orderid { get; set; }
        public long itemid { get; set; }
        public long itemlotid { get; set; }
        public decimal itemqtyprimary { get; set; }
        public short munitprimary { get; set; }
        public long itemqtysecondary { get; set; }
        public short munitsecondary { get; set; }
        public string dateentry { get; set; }
        public string orderdate { get; set; }
        public string requesteddelivdate { get; set; }
        public string promiseddelivdate { get; set; }
        public short orderdtlstatus { get; set; }
        public decimal priceperunit { get; set; }
        public short moneytype { get; set; }
        public decimal itemqtyprice { get; set; }
        public decimal itemlength { get; set; }
        public int itemcolorid { get; set; }
        public bool painted { get; set; }
        public int itemcolorid2 { get; set; }
        public string orderdtlcomments { get; set; }
        public int packtype { get; set; }
        public int hardness { get; set; }
        public long accerpitemid { get; set; }
        public long erporderdtlid { get; set; }
        public decimal prdiscount { get; set; }
        public decimal specialprice { get; set; }
        public long weborderdtlid { get; set; }
        public long weborderid { get; set; }
        public string weekshipping { get; set; }
        public string dbreccreatehost { get; set; }
        public string dbreccreatedate { get; set; }
        public int dbreccreateuser { get; set; }
        public string dbrecmoddate { get; set; }
        public int dbrecmoduser { get; set; }
        public string dbrecmodhost { get; set; }
        public short servbranchid { get; set; }

        public string itemcode { get; set; }
        public string itemdesc { get; set; }
        public string colordesc { get; set; }
        public short munitdecimals { get; set; }
        public string ordercode { get; set; }
        public long customerid { get; set; }
        public string customertitle { get; set; }
        public decimal itemrequestedqty { get; set; }
        public decimal itempqtyfree { get; set; }
        public decimal itemdoneqty { get; set; }
        public decimal shippedpqty { get; set; }
        public decimal itemqtybalance { get; set; }
        public decimal itemqtyshippbalance { get; set; }

    }


    public class PackingListHeader
    {
        long packingListHeaderID;
        long packinglistserverid;
        string packinglistcomments;
        short packingliststatus;
        string customercode;
        string customertitle;
        string packinglistdate;
        short compid;
        short branchid;
        int storeID;
        string storename;
        short kindid;
        int transtype;
        string transcode;
        long orderid;
        long orderdtlid;
        long dsrid;


        public long PackingListHeaderID { get { return packingListHeaderID; } set { packingListHeaderID = value; } }
        public long Packinglistserverid { get { return packinglistserverid; } set { packinglistserverid = value; } }
        public string Packinglistcomments { get { return packinglistcomments; } set { packinglistcomments = value; } }
        public short PackingListStatus { get { return packingliststatus; } set { packingliststatus = value; } }
        public string CustomerCode { get { return customercode; } set { customercode = value; } }
        public string CustomerTitle { get { return customertitle; } set { customertitle = value; } }
        public string PackingListDate { get { return packinglistdate; } set { packinglistdate = value; } }
        public short Compid { get { return compid; } set { compid = value; } }
        public short Branchid { get { return branchid; } set { branchid = value; } }
        public int StoreID { get { return storeID; } set { storeID = value; } }
        public string StoreName { get { return storename; } set { storename = value; } }
        public short Kindid { get { return kindid; } set { kindid = value; } }
        public int TransType { get { return transtype; } set { transtype = value; } }
        public string TransCode { get { return transcode; } set { transcode = value; } }
        public long OrderID { get { return orderid; } set { orderid = value; } }
        public long OrderDtlID { get { return orderdtlid; } set { orderdtlid = value; } }
        public long DSRID { get { return dsrid; } set { dsrid = value; } }




    }
    public class SyncLot
    {
        public long LotID { get; set; }
        public long ItemID { get; set; }
        public short CompID { get; set; }
        public short BranchID { get; set; }
        public short StoreID { get; set; }
        public string LotCode { get; set; }
        public decimal Width { get; set; }
        public decimal Length { get; set; }
        public string Color { get; set; }
        public string Draft { get; set; }
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
            set { minitemid = value; }
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
        public short storeID { get; set; }
        public string storeName { get; set; }
        public short compID { get; set; }
    }
    public class PackingListDetail
    {

        long packingListHeaderID;
        long packingListDTLID;
        long itemid;
        long lotid;
        string itemcode;
        string lotcode;
        decimal itemqtyprimary;
        decimal itemqtysecondary;
        int itemmunitprimary;
        int itemmunitsecondary;
        string color;
        string draft;
        decimal width;
        decimal length;


        public long PackingListHeaderID { get { return packingListHeaderID; } set { packingListHeaderID = value; } }
        public long PackingListDTLID { get { return packingListDTLID; } set { packingListDTLID = value; } }
        public long ItemID { get { return itemid; } set { itemid = value; } }
        public long LotID { get { return lotid; } set { lotid = value; } }
        public string ItemCode { get { return itemcode; } set { itemcode = value; } }
        public string LotCode { get { return lotcode; } set { lotcode = value; } }
        public decimal ItemQTYprimary { get { return itemqtyprimary; } set { itemqtyprimary = value; } }
        public decimal ItemQTYsecondary { get { return itemqtysecondary; } set { itemqtysecondary = value; } }
        public int ItemMunitPrimary { get { return itemmunitprimary; } set { itemmunitprimary = value; } }
        public int ItemMunitSecondary { get { return itemmunitsecondary; } set { itemmunitsecondary = value; } }
        public string Color { get { return color; } set { color = value; } }
        public string Draft { get { return draft; } set { draft = value; } }
        public decimal Width { get { return width; } set { width = value; } }
        public decimal Length { get { return length; } set { length = value; } }
    }

    public class SyncERPItem
    {
        public long ItemID { get; set; }
        public short CompID { get; set; }
        public short BranchID { get; set; }
        public short StoreID { get; set; }
        public string ItemCode { get; set; }
        public string ItemDesc { get; set; }
        public int MUnitPrimary { get; set; }
        public int MUnitSecondary { get; set; }
        public decimal MUnitsRelation { get; set; }
        public string MUnitDesc1 { get; set; }
        public string MUnitDesc2 { get; set; }
        public string EntryDate { get; set; }
    }


    public class CarpetTrans
    {
        public long TRANSID { get; set; }
        public short ERPCompID { get; set; }
        public short ERPBRACHID { get; set; }
        public long AlpisStoreTransID { get; set; }
        public long WMSTRANSID { get; set; }
        public string AlpisStoreTransDate { get; set; }
        public long ERPTransSeriesID { get; set; }
        public long ERPCustomerID { get; set; }
        public long ERPSupplierID { get; set; }
        public long ERPFromStoreID { get; set; }
        public long ERPToStoreID { get; set; }
        public long ERPItemID { get; set; }
        public long ERPItemMUnit { get; set; }
        public long ERPItemSMUnit { get; set; }
        public decimal ERPItemQty { get; set; }
        public decimal ERPITEMSQTY { get; set; }
        public decimal ERPItemUnitPrice { get; set; }
        public decimal ERPItemQtyPrice { get; set; }
        public string EXTRASERVICE { get; set; }
        public string ERPTRANSCODE { get; set; }
        public long DOCTYPE { get; set; }
        public short isnew { get; set; }
    }
    public class TransCodeHeader
    {

        public long FtrID { get; set; }
        public short compid { get; set; }
        public short branchid { get; set; }
        public string tradecode { get; set; }
        public int fromstoreid { get; set; }
        public int tostoreid { get; set; }
        public string branchname { get; set; }
        public string transdate { get; set; }
    }

    public class TransCodeDetail
    {

        public long FtrID { get; set; }
        public long TransID { get; set; }
        public long ReceiveID { get; set; }

        public long ItemID { get; set; }
        public long LotID { get; set; }

        public int munitprimary { get; set; }
        public int munitsecondary { get; set; }

        public Decimal ItemQtyPrimary { get; set; }
        public Decimal ItemQtySecondary { get; set; }


        public Decimal Zwidth { get; set; }
        public Decimal Zlength { get; set; }



        public string zcolor  { get; set; }
        public string zdraft { get; set; }

        public string lotcode { get; set; }
        public string itemcode { get; set; }
        public string itemdesc { get; set; }




    }
}