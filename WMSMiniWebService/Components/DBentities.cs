



using System;
namespace WMSMiniWebService
{

    public class TPackage
    {
        public long packitemid { get; set; }
        public short compid { get; set; }
        public short branchid { get; set; }
        public string packitemno { get; set; }
        public string packdate { get; set; }
        public int packmachineid { get; set; }
        public long customerid { get; set; }
        public long prodorderid { get; set; }
        public int packtype { get; set; }
        public short inwarehouse { get; set; }
        public long orderid { get; set; }
    }

    public class TItemsMunits
    {
        public int munitid { get; set; }
        public short compid { get; set; }
        public string munit { get; set; }
        public string muniten { get; set; }
        public short munitdecimals { get; set; }
        public short munitdecimalstsp { get; set; }
        public short suggesteduseas { get; set; }
    }

    public class TItems
    {
        public long itemid { get; set; }
        public short compid { get; set; }
        public string itemcode { get; set; }
        public long productid { get; set; }
        public string itemdesc { get; set; }
        public int groupitemid { get; set; }
        public int mainitemcatid { get; set; }
        public int subitemcatid { get; set; }
        public int manufactureid { get; set; }
        public decimal stocklevelqty { get; set; }
        public decimal stocklevelmunit { get; set; }
        public short munitprimary { get; set; }
        public string munitprimarydescr { get; set; }
        public short munitprimarydecimals { get; set; }
        public short munitsecondary { get; set; }
        public string munitsecondarydescr { get; set; }
        public short munitsecondarydecimals { get; set; }
        public decimal munitsrelation { get; set; }
        public short munitputaway { get; set; }
        public decimal munitsputawayrelation { get; set; }
        public short munitweight { get; set; }
        public decimal munitweightrelation { get; set; }
        public int wmszone { get; set; }
        public short replenishmenttype { get; set; }
        public long erpitemid { get; set; }
        public string erpitemcode { get; set; }
        public int accountclassid { get; set; }
        public bool haslength { get; set; }
        public bool hascolor { get; set; }
        public long stocklevelqtywcolor { get; set; }
        public decimal replitemlength { get; set; }
        public decimal standardlength { get; set; }
        public bool enabled { get; set; }
        public bool autoprodschedule { get; set; }
        public string dimensions { get; set; }
        public string dbreccreatedate { get; set; }
        public int dbreccreateuser { get; set; }
        public string dbreccreatehost { get; set; }
        public string dbrecmoddate { get; set; }
        public int dbrecmoduser { get; set; }
        public string dbrecmodhost { get; set; }

    }

    public class TItemLot
    {
        public long lotid { get; set; }
        public short compid { get; set; }
        public long itemid { get; set; }
        public string lotcode { get; set; }
        public string lotdesc { get; set; }
        public string lotcreatedate { get; set; }
        public string lotexpiredate { get; set; }
        public long prodorderid { get; set; }
        public decimal prodorderlotqty { get; set; }
        public long erplotid { get; set; }
        public string dbreccreatedate { get; set; }
        public int dbreccreateuser { get; set; }
        public string dbrecmoddate { get; set; }
        public int dbrecmoduser { get; set; }
        public string dbreccreatehost { get; set; }
        public string dbrecmodhost { get; set; }
    }

    public class TWmsBinItemsQty
    {
        public long binitemsqtyid { get; set; }
        public short compid { get; set; }
        public short branchid { get; set; }
        public long whbinid { get; set; }
        public long whbinidto { get; set; }
        public short whbintype { get; set; }
        public string whbincode { get; set; }
        public long itemid { get; set; }
        public long itemlotid { get; set; }
        public long packitemid { get; set; }
        public decimal itemqtyprimary { get; set; }
        public short munitprimary { get; set; }
        public string munitprimarydesc { get; set; }
        public decimal munitsrelation { get; set; }
        public short munitsecondary { get; set; }
        public string munitsecondarydesc { get; set; }
        public decimal itemqtysecondary { get; set; }
        public decimal packitemqty { get; set; }
        public short munitpack { get; set; }
        public int itemcolorid { get; set; }
        public int itemcolorid2 { get; set; }
        public decimal itemlength { get; set; }
        public string packitemno { get; set; }
        public long resrvstoretransid { get; set; }
        public long orderdtlid { get; set; }
        public long orderid { get; set; }
        public long routeid { get; set; }
        public string dbreccreatedate { get; set; }
        public int dbreccreateuser { get; set; }
        public string dbrecmoddate { get; set; }
        public int dbrecmoduser { get; set; }
        public string dbreccreatehost { get; set; }
        public string dbrecmodhost { get; set; }
        public short transtype { get; set; }
        public short transex { get; set; }
        public bool checkavailability { get; set; }
        public bool createbintransrecord { get; set; }
        public string comments { get; set; }
        public long packitemweight { get; set; }
        public string itemcode { get; set; }
        public string ordercode { get; set; }
    }

    public class TWmsBinStoretrans
    {
        public long binstoretransid { get; set; }
        public long whbinid { get; set; }
        public short wmstranstype { get; set; }
        public long itemid { get; set; }
        public decimal itemqtyprimary { get; set; }
        public short munitprimary { get; set; }
        public decimal itempackqty { get; set; }
        public short munitpack { get; set; }
        public long storetransid { get; set; }
        public long storetransdtlid { get; set; }
        public long orderid { get; set; }
        public long orderdtlid { get; set; }
        public short transex { get; set; }
        public string terminal { get; set; }
        public long itemlotid { get; set; }
        public decimal itemqtysecondary { get; set; }
        public short munitsecondary { get; set; }
        public decimal itemlength { get; set; }
        public int itemcolorid { get; set; }
        public int itemcolorid2 { get; set; }
        public string wmstransdate { get; set; }
        public long whbinidfrom { get; set; }
        public long whbinidto { get; set; }
        public long packitemid { get; set; }
        public string packitemno { get; set; }
        public short compid { get; set; }
        public short branchid { get; set; }
        public string dbreccreatehost { get; set; }
        public string dbrecmodhost { get; set; }
        public long packid { get; set; }
        public int prodphaseid { get; set; }
        public int prodprocessid { get; set; }
        public long packitemidto { get; set; }
        public string packitemnoto { get; set; }
        public string dbreccreatedate { get; set; }
        public int dbreccreateuser { get; set; }
        public string dbrecmoddate { get; set; }
        public int dbrecmoduser { get; set; }
    }


    public class TOrders
    {
        public long orderid { get; set; }
        public short compid { get; set; }
        public string ordercode { get; set; }
        public long customerid { get; set; }
        public long contactid { get; set; }
        public string dateentry { get; set; }
        public string orderdate { get; set; }
        public string requesteddelivdate { get; set; }
        public string promiseddelivdate { get; set; }
        public int salespersonid { get; set; }
        public short orderstatus { get; set; }
        public int salesmid { get; set; }
        public short branchid { get; set; }
        public string shippcountry { get; set; }
        public string shippcity { get; set; }
        public string shippaddress { get; set; }
        public string shippzip { get; set; }
        public string shippphone { get; set; }
        public long erporderid { get; set; }
        public bool printed { get; set; }
        public int packtype { get; set; }
        public long weborderid { get; set; }
        public string customerordercode { get; set; }
        public string weekshipping { get; set; }
        public int hardness { get; set; }
        public decimal exrate { get; set; }
        public short moneytype { get; set; }
        public short servbranchid { get; set; }
        public short munitbackorder { get; set; }
        public string ordercomments { get; set; }
        public string dbreccreatedate { get; set; }
        public int dbreccreateuser { get; set; }
        public string dbrecmoddate { get; set; }
        public int dbrecmoduser { get; set; }
        public string dbreccreatehost { get; set; }
        public string dbrecmodhost { get; set; }

    }

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

    public class TWmsBinTypes
    {
        public int bintypeid { get; set; }
        public string bintypedesc { get; set; }
    }

    public class TWmsTransTypes
    {
        public int wmstranstypeid { get; set; }
        public string wmstranstypedesc { get; set; }
        public string wmstranstypeen { get; set; }
        public short transex { get; set; }
        public int wmsbindefault { get; set; }
        public bool wmsinternaltrans { get; set; }
        public int wmsbinfrom { get; set; }
        public int wmsbinto { get; set; }
        public bool showbinqtys { get; set; }
        public bool showallbins { get; set; }

    }

    public class TWmsBins
    {
        public int whbinid { get; set; }
        public short compid { get; set; }
        public short branchid { get; set; }
        public int whzoneid { get; set; }
        public string whbincode { get; set; }
        public short whbintype { get; set; }
        public string friendlycode { get; set; }
    }



    public class TSysEventLog
    {
        public long logid { get; set; }
        public short compid { get; set; }
        public string appusername { get; set; }
        public string dberrorcode { get; set; }
        public string dberrortext { get; set; }
    }


    public class ResultWithMessage
    {
        public bool posresult { get; set; }
        public long resultno { get; set; }
        public string successmessage { get; set; }
        public string errormessage { get; set; }

    }

    public class TInventoryHeader
    {
        long invHdrID;
        short compID;
        short branchid;
        int storeid;
        string storeName;
        string invDate;
        short invType;
        string invComments;
        short confirmed;
        long customerID;
        string customerTitle;
        short invStatus;
        long mobInvHdrID;
        long invSyncID;

        public long InvHdrID { get { return invHdrID; } set { invHdrID = value; } }
        public short CompID { get { return compID; } set { compID = value; } }
        public short Branchid { get { return branchid; } set { branchid = value; } }
        public int Storeid { get { return storeid; } set { storeid = value; } }
        public string StoreName { get { return storeName; } set { storeName = value; } }
        public string InvDate { get { return invDate; } set { invDate = value; } }
        public short InvType { get { return invType; } set { invType = value; } }
        public string InvComments { get { return invComments; } set { invComments = value; } }
        public short Confirmed { get { return confirmed; } set { confirmed = value; } }
        public long CustomerID { get { return customerID; } set { customerID = value; } }
        public short InvStatus { get { return invStatus; } set { invStatus = value; } }
        public string CustomerTitle { get { return customerTitle; } set { customerTitle = value; } }
        public long MobInvHdrID { get { return mobInvHdrID; } set { mobInvHdrID = value; } }
        public long InvSyncID { get { return invSyncID; } set { invSyncID = value; } }
    }

    public class TInventory
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

    public class SyncInfo
    {
        long minitemid; long maxitemid;
        long itemsrowscount;
        long minlotid; long maxlotid;
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

    public class InventoryInfo
    {
        public int InvCount { get; set; }
        public string LastBarcode { get; set; }
        public DateTime LastInvdate { get; set; } 
    
    }




}