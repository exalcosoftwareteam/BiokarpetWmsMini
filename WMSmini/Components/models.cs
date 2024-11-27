
namespace WMSMobileClient.Components
{

    public class OrderDetail
    {
        long orderDtlID;
        long orderid;
        long itemID;
        short orderMunitPrimary;
        short orderMunitSecondary;
        decimal orderQtySecondary;
        decimal orderQtyPrimary;


        public long OrderDTLID { get { return orderDtlID; } set { orderDtlID = value; } }
        public long OrderID { get { return orderid; } set { orderid = value; } }
        public long ItemID { get { return itemID; } set { itemID = value; } }
        public short OrderMunitPrimary { get { return orderMunitPrimary; } set { orderMunitPrimary = value; } }
        public short OrderMunitSecondary { get { return orderMunitSecondary; } set { orderMunitSecondary = value; } }
        public decimal OrderQtySecondary { get { return orderQtySecondary; } set { orderQtySecondary = value; } }
        public decimal OrderQtyPrimary { get { return orderQtyPrimary; } set { orderQtyPrimary = value; } }
    }

    public class PackingList
    {
        long packingListDTLID;
        long packingListHeaderID;
        long orderid;
        long orderDtlID;
        long itemID;
        long lotID;
        decimal width;
        decimal length;
        string color;
        string draft;
        string packDate;
        short itemMunitPrimary;
        short itemMunitSecondary;
        decimal itemQtySecondary;
        decimal itemQtyPrimary;
        string itemCode;
        string lotCode;
        string itemDesc;


        public long PackingListDTLID { get { return packingListDTLID; } set { packingListDTLID = value; } }
        public long PackingListHeaderID { get { return packingListHeaderID; } set { packingListHeaderID = value; } }
        public long OrderID { get { return orderid; } set { orderid = value; } }
        public long OrderDTLID { get { return orderDtlID; } set { orderDtlID = value; } }
        public long ItemID { get { return itemID; } set { itemID = value; } }
        public long LotID { get { return lotID; } set { lotID = value; } }
        public string ItemCode { get { return itemCode; } set { itemCode = value; } }
        public string ItemDesc { get { return itemDesc; } set { itemDesc = value; } }
        public string LotCode { get { return lotCode; } set { lotCode = value; } }
        public string PackDate { get { return packDate; } set { packDate = value; } }
        public short ItemMunitPrimary { get { return itemMunitPrimary; } set { itemMunitPrimary = value; } }
        public short ItemMunitSecondary { get { return itemMunitSecondary; } set { itemMunitSecondary = value; } }
        public decimal ItemQtySecondary { get { return itemQtySecondary; } set { itemQtySecondary = value; } }
        public decimal ItemQtyPrimary { get { return itemQtyPrimary; } set { itemQtyPrimary = value; } }
        public decimal Width { get { return width; } set { width = value; } }
        public decimal Length { get { return length; } set { length = value; } }
        public string Draft { get { return draft; } set { draft = value; } }
        public string Color { get { return color; } set { color = value; } }
    }

    public class PackingHeader
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
        public long Dsrid { get { return dsrid; } set { dsrid = value; } }
    }

    public class MInventory
    {
        long invID;
        short compID;
        short branchID;
        short storeID;
        long invHdrID;
        long itemID;
        long invno;
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

        public long InvID { get { return invID; } set { invID = value; } }
        public long InvNo { get { return invno; } set { invno = value; } }
        public short CompID { get { return compID; } set { compID = value; } }
        public short BranchID { get { return branchID; } set { branchID = value; } }
        public short StoreID { get { return storeID; } set { storeID = value; } }
        public long InvHdrID { get { return invHdrID; } set { invHdrID = value; } }
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

    public class Item
    {

        long itemID;
        short compID;
        string itemCode;
        string itemDesc;
        string entrydate;
        int mUnitPrimary;
        int mUnitSecondary;
        decimal mUnitsRelation;
        string mUnitDesc1;
        string mUnitDesc2;

        public long ItemID { get { return itemID; } set { itemID = value; } }
        public short CompID { get { return compID; } set { compID = value; } }
        public string ItemCode { get { return itemCode; } set { itemCode = value; } }
        public string ItemDesc { get { return itemDesc; } set { itemDesc = value; } }
        public int MUnitPrimary { get { return mUnitPrimary; } set { mUnitPrimary = value; } }
        public int MUnitSecondary { get { return mUnitSecondary; } set { mUnitSecondary = value; } }
        public decimal MUnitsRelation { get { return mUnitsRelation; } set { mUnitsRelation = value; } }
        public string MUnitDesc1 { get { return mUnitDesc1; } set { mUnitDesc1 = value; } }
        public string MUnitDesc2 { get { return mUnitDesc2; } set { mUnitDesc2 = value; } }
        public string EntryDate { get { return entrydate; } set { entrydate = value; } }


    }

    public class Lot
    {
        long lotID;
        long itemID;
        short compID;
        string lotCode;
        string draft;
        string color;
        decimal width;
        decimal length;
        decimal itemPrimaryQty;
        decimal itemSecondaryQty;
        decimal erpqty;
        decimal erpqty2;
        string entrydate;

        public long LotID { get { return lotID; } set { lotID = value; } }
        public long ItemID { get { return itemID; } set { itemID = value; } }
        public short CompID { get { return compID; } set { compID = value; } }
        public string LotCode { get { return lotCode; } set { lotCode = value; } }
        public string Color { get { return color; } set { color = value; } }
        public string Draft { get { return draft; } set { draft = value; } }
        public decimal Width { get { return width; } set { width = value; } }
        public decimal Length { get { return length; } set { length = value; } }
        public decimal ItemPrimaryQty { get { return itemPrimaryQty; } set { itemPrimaryQty = value; } }
        public decimal ItemSecondaryQty { get { return itemSecondaryQty; } set { itemSecondaryQty = value; } }
        public decimal ErpQty { get { return erpqty; } set { erpqty = value; } }
        public decimal ErpQty2 { get { return erpqty2; } set { erpqty2 = value; } }
        public string EntryDate { get { return entrydate; } set { entrydate = value; } }
    }

    public class InventoryHeader
    {
        long invHdrID;
        short compID;
        short branchid;
        short storeid;
        string storeName;
        string invDate;
        short invType;
        string invComments;
        short confirmed;
        long customerID;
        string customerTitle;
        long invHdrIDServer;
        short invStatus;
        long invSyncID;


        public long InvHdrID { get { return invHdrID; } set { invHdrID = value; } }
        public long InvHdrIDServer { get { return invHdrIDServer; } set { invHdrIDServer = value; } }
        public short CompID { get { return compID; } set { compID = value; } }
        public short Branchid { get { return branchid; } set { branchid = value; } }
        public short Storeid { get { return storeid; } set { storeid = value; } }
        public string StoreName { get { return storeName; } set { storeName = value; } }
        public string InvDate { get { return invDate; } set { invDate = value; } }
        public short InvType { get { return invType; } set { invType = value; } }
        public string InvComments { get { return invComments; } set { invComments = value; } }
        public short Confirmed { get { return confirmed; } set { confirmed = value; } }
        public long CustomerID { get { return customerID; } set { customerID = value; } }
        public short InvStatus { get { return invStatus; } set { invStatus = value; } }
        public string CustomerTitle { get { return customerTitle; } set { customerTitle = value; } }
        public long InvSyncID { get { return invSyncID; } set { invSyncID = value; } }

    }

    public class Order
    {
        long orderid;
        short compID;
        short branchid;
        short storeid;
        string storeName;
        string orderComments;
        short confirmed;
        long customerID;
        string orderDate;
        string customerTitle;
        short orderStatus;
        long orderSyncID;
        long salesPersonID;
        string orderCode;



        public long OrderID { get { return orderid; } set { orderid = value; } }
        public short CompID { get { return compID; } set { compID = value; } }
        public short Branchid { get { return branchid; } set { branchid = value; } }
        public short Storeid { get { return storeid; } set { storeid = value; } }
        public string StoreName { get { return storeName; } set { storeName = value; } }
        public string OrderCode { get { return orderCode; } set { orderCode = value; } }
        public string OrderComments { get { return orderComments; } set { orderComments = value; } }
        public string OrderDate { get { return orderDate; } set { orderDate = value; } }
        public short Confirmed { get { return confirmed; } set { confirmed = value; } }
        public long CustomerID { get { return customerID; } set { customerID = value; } }
        public short OrderStatus { get { return orderStatus; } set { orderStatus = value; } }
        public string CustomerTitle { get { return customerTitle; } set { customerTitle = value; } }
        public long OrderSyncID { get { return orderSyncID; } set { orderSyncID = value; } }
        public long SalesPersonID { get { return salesPersonID; } set { salesPersonID = value; } }
    }

}