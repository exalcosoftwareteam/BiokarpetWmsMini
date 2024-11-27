using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WMSMiniWebService
{
    // NOTE: If you change the interface name "IService1" here, you must also update the reference to "IService1" in Web.config.
    [ServiceContract]
    public interface IWMSservice
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);
        [OperationContract]
        TItems ItemInfo(long itemid);

        [OperationContract]
        TItems ItemInfoByCode(string itemcode);


        [OperationContract]
        TItemLot LotInfo(long lotid);

        [OperationContract]
        TItemLot LotInfoByCode(string lotcode, long itemid);

        [OperationContract]
        long UpdateOrderLinesStatus(long orerid);


        [OperationContract]
        List<TWmsBinItemsQty> BinitemsQty(string criteria, string sortcriteria);

        [OperationContract]
        long BinIDByCode(short branchid, string bincode);


        [OperationContract]
        TWmsBins GetBinInfo(long binid);

        [OperationContract]
        long GetMinBinIDByType(short branchid, short bintype);

        [OperationContract]
        List<TWmsBins> BinList(short branchid, short bintype);

        [OperationContract]
        List<TWmsTransTypes> TransTypeInfo(int transtypeid);


        [OperationContract]
        TWmsTransTypes GetTransTypeInfo(int transtypeid);

        [OperationContract]
        List<TWmsBinItemsQty> GetItemQtyAvailableByBinCode(short branchid, long itemid, long itemlotid, long packitemid);

        [OperationContract]
        long NewPackage(TPackage package);

        [OperationContract]
        List<TWmsBinItemsQty> PaleteItemsList(short branchid, string packitemno, long packitemid);

        [OperationContract]
        long UpdatePaleteWeight(short compid, short branchid, short whbuildid, long packitemid, decimal paleteweight, bool printpaletelabel);

        [OperationContract]
        long OrderFirstPalete(long orderid);

        [OperationContract]
        string CustomerPalete(long customerid, long orderid);

        [OperationContract]
        List<TOrderDetails> OrderDetailsList(long orderid);


        [OperationContract]
        long SyncERPItems();


        [OperationContract]
        List<TWmsBinItemsQty> GetOrderItemQtyAvailableByBinCode(short branchid, long orderid, long orderdtlid, bool uselot);

        [OperationContract]
        SyncInfo GetSyncInfo();

        [OperationContract]
        long ImportInventoryHeaderCType(TInventoryHeader invhdr);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
