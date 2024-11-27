using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WMSMiniWebService
{

    public class WMSservice : IWMSservice
    {
                
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }


        public TItems ItemInfo(long itemid)
        {
            Items itemhandler = new Items();
            return itemhandler.ItemInstance(itemid);
        }
        public TItems ItemInfoByCode(string itemcode)
        {
            Items itemhandler = new Items();
            return itemhandler.ItemInstanceByCode(itemcode);
        }

        public TItemLot LotInfo(long lotid)
        {
            ItemLots lothandler = new ItemLots();
            return lothandler.ItemLotInstance(lotid);
        }

        public TItemLot LotInfoByCode(string lotcode, long itemid)
        {
            ItemLots lothandler = new ItemLots();
            return lothandler.ItemLotInstanceByCode(lotcode, itemid);
        }

        public List<TWmsBinItemsQty> BinitemsQty(string criteria, string sortcriteria)
        {
            WMSBinitemsqty test = new WMSBinitemsqty();

            return test.WmsBinItemsQtyData(criteria, sortcriteria);
        }


        public long BinIDByCode(short branchid, string bincode)
        {
            WMSBins wmsbins = new WMSBins();

            return wmsbins.BinIDByCode(branchid, bincode);
        }

        public TWmsBins GetBinInfo(long binid)
        {
            WMSBins wmsbins = new WMSBins();

            return wmsbins.BinInfo(binid);
        }

        public long GetMinBinIDByType(short branchid, short bintype)
        {
            WMSBins wmsbins = new WMSBins();

            return wmsbins.GetMinBinIDByType(branchid, bintype);
        }

        public List<TWmsBins> BinList(short branchid, short bintype)
        {
            WMSBins wmsbins = new WMSBins();
            return wmsbins.BinList(branchid, bintype);
        }

        public List<TWmsTransTypes> TransTypeInfo(int transtypeid)
        {
            WMSTransTypes wmstranstypedata = new WMSTransTypes();
            return wmstranstypedata.IListTransTypes(" TRANSTYPEID=" + transtypeid.ToString(), "");
        }

        public TWmsTransTypes GetTransTypeInfo(int transtypeid)
        {
            WMSTransTypes wmstranstypedata = new WMSTransTypes();

            return wmstranstypedata.WmsTransTypeInstance(transtypeid);
        }

        public List<TWmsBinItemsQty> GetItemQtyAvailableByBinCode(short branchid, long itemid, long itemlotid, long packitemid)
        {
            WMSBinitemsqty binitems = new WMSBinitemsqty();

            return binitems.GetItemQtyAvailableByBinCode(branchid, itemid, itemlotid, packitemid);
        }

        public long UpdateOrderLinesStatus(long orerid)
        {
            Orders orders = new Orders();
            return orders.UpdateOrderLinesStatus(orerid);

        }

        public long NewPackage(TPackage package)
        {
            Packages packages = new Packages();
            return packages.Insert(package);
        }

        public List<TWmsBinItemsQty> PaleteItemsList(short branchid, string packitemno, long packitemid)
        {
            WMSBinitemsqty binitemsqty = new WMSBinitemsqty();
            return binitemsqty.PaleteItemsList(branchid, packitemno, packitemid);
        }

        public long UpdatePaleteWeight(short compid, short branchid, short whbuildid, long packitemid, decimal paleteweight, bool printpaletelabel)
        {
            WMSBinitemsqty binitemsqty = new WMSBinitemsqty();

            return binitemsqty.UpdatePaleteWeight(compid, branchid, whbuildid, packitemid, paleteweight, printpaletelabel);
        }

        public long OrderFirstPalete(long orderid)
        {
            WMSBinitemsqty binitemsqty = new WMSBinitemsqty();

            return binitemsqty.OrderFirstPalete(orderid);
        }

        public string CustomerPalete(long customerid, long orderid)
        {
            WMSBinitemsqty binitemsqty = new WMSBinitemsqty();
            return binitemsqty.CustomerPalete(customerid, orderid);
        }

        public List<TOrderDetails> OrderDetailsList(long orderid)
        {
            Orders orderdtl = new Orders();
            return orderdtl.OrderDetails(orderid);
        }

        public long SyncERPItems()
        {
            Items itemhandler = new Items();
            return itemhandler.SyncERPItems();
        }

        public List<TWmsBinItemsQty> GetOrderItemQtyAvailableByBinCode(short branchid, long orderid, long orderdtlid, bool uselot)
        {
            WMSBinitemsqty binitems = new WMSBinitemsqty();

            return binitems.GetOrderItemQtyAvailableByBinCode(branchid, orderid, orderdtlid, uselot);
        }

        public SyncInfo GetSyncInfo()
        {
            InventoryHandler invhandler = new InventoryHandler();
            return invhandler.GetSyncInfo();
        }

        public long ImportInventoryHeaderCType(TInventoryHeader invhdr)
        {
            InventoryHeader invhdrhandler = new InventoryHeader();
            return invhdrhandler.UpdateInventoryHeader(invhdr);
        }
    }
}
