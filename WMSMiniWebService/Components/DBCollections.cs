using System;
using System.Collections.Generic;
using System.Text;

namespace WMSMiniWebService
{


    public struct TablesCollection
    {
        public const string TItems = "TITEMS";
        public const string TItemLot = "TITEMLOT";
    }

    public struct TableIds
    {
        public const string ItemID = "ITEMID";
        public const string LotID = "LOTID";

    }


    public struct WSVCReturnValues
    {
        public const long generalerror = -1;
        public const long dbconnectionfailed = -10;
        public const long serviceunreachable = -20;
        public const long notenouphqty = -101;
    }

    public struct DBOperationsReturnvalues
    {
        public const long notenouphqty = -101;
    }
}
