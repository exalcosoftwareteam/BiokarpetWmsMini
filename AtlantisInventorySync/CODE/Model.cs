using System;
using System.Collections.Generic;
using System.Text;

namespace AtlantisInventorySync
{
    public class ItemDetailsQTY
    {
        public short branchid { get; set; }
        public  long iteid { get; set; }
        public long parid { get; set; }
        public decimal initqty1 { get; set; }
        public decimal initqty2 { get; set; }
        public decimal primaryqty { get; set; }
        public decimal secondaryqty { get; set; }
        public decimal qtyinstock1 { get; set; }
        public decimal qtyinstock2 { get; set; }

    }



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
        public long igpid { get; set; }
       public long ictid { get; set; }

       public string zsgbdescr { get; set; }
       public string zcomposition { get; set; }

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

        public string zsetdescr { get; set; }
        public string zdescription { get; set; }
        public string zbarcode2 { get; set; }


    }




}
