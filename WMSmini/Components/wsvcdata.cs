using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using WMSMobileClient;
using WMSMobileClient.Components;
using WMSMobileClient.WMSservice;
using WMSMobileClient.WMSSyncService;
namespace WMSMobileClient.Components
{
    class wsvcdata
    {
        public string wsvcerror = null;

        public long CheckConnection()
        {
            long RTRN = -1;
            try
            {
                RTRN = long.Parse(AppGeneralSettings.webServiceProvider.CheckDBConnection());
                return RTRN;
            }
            catch  {  }

            return RTRN;
        }

        public WMSservice.TItems ItemInfoByCode(string itemcode)
        {
            wsvcerror = null;

            WMSMobileClient.WMSservice.TItems serviceitem = new WMSMobileClient.WMSservice.TItems();

            try
            { 
                serviceitem = AppGeneralSettings.webServiceProvider.ItemInfoByCode(itemcode); 
            }
            catch (Exception ex) { wsvcerror = ex.ToString(); }

            return serviceitem;
        }

        public TItems ItemInfo(long itemid)
        {
            wsvcerror = null;

            TItems iteminfo = new TItems();

            try { iteminfo = AppGeneralSettings.webServiceProvider.ItemInfo(itemid);}
            catch (Exception ex) { wsvcerror = ex.ToString(); }

            return iteminfo;
        }
      
        public TItemLot LotInfoByCode(string lotcode,long itemid)
        {
            wsvcerror = null;

            TItemLot  lot = new TItemLot();

            try { lot = AppGeneralSettings.webServiceProvider.LotInfoByCode(lotcode, itemid); }
            catch (Exception ex) { wsvcerror = ex.ToString(); }

            return lot;            
        }


        #region List to Datatable

        private DataTable ToDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }
            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                tb.Rows.Add(values);
            }
            return tb;
        }

        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        public static Type GetCoreType(Type t)
        {

            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                    return t;
                else
                    return Nullable.GetUnderlyingType(t);
            }
            else
                return t;
        }
       
        #endregion       

    }

}
