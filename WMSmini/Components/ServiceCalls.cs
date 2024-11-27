using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WMSMobileClient.WMSservice;
using WMSMobileClient.Components;

namespace WMSMobileClient
{
    public static class ServiceCalls
    {

        public static long ImportInventory(List<TInventory> inv, bool prev)
        {
            return AppGeneralSettings.webServiceProvider.ImportInventoryCTypeList(inv.ToArray(), prev);
        }

        public static long ImportInventory(TInventory inv, bool prev)
        {
            return AppGeneralSettings.webServiceProvider.ImportInventoryAlter(inv, prev);
        }

    }


}
