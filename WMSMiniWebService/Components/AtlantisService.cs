using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.IO;
using System.Text;

namespace WMSMiniWebService
{
    public class AtlantisWebService
    {
        public static WMSsyncService.WMSSyncService atlantiswebserviceprovider = new WMSMiniWebService.WMSsyncService.WMSSyncService();
         Items itemfunctions = new Items();
         ItemLots lotfunctions = new ItemLots();
        String sqlstr = "";
        DB mydb = new DB();

        public AtlantisWebService() 
        {
            Appsetings();
        
        }

 
        public void Appsetings()
        {
            
        atlantiswebserviceprovider.Url = ConfigurationManager.AppSettings["WMSsyncServiceURL"].ToString();
        
        }


        public int GetItemFromERP(long itemid) 
        {
            try
            {
                ERPItem thisitem = ParseSyncERPtoErpITEM(atlantiswebserviceprovider.SOA_GetNewItem(itemid));
                itemfunctions.FInsertItem(thisitem);
            }catch(Exception ex)
            {
                mydb.WriteToLog(ex.Message.ToString());
            }
                return 1;
  
        
        }


        public int GetLotFromERP(long lotid)
        {
            ERPLot newlot = new ERPLot();
            try
            {
                newlot = ParseSyncERPtoErpLot(atlantiswebserviceprovider.SOA_GetNewLot(lotid));
            }
            catch (Exception ex) 
            {
                mydb.WriteToLog(ex.Message.ToString());
            }
            lotfunctions.FInsertLot(newlot);
            return 1;


        }

        protected ERPItem ParseSyncERPtoErpITEM(WMSsyncService.SyncERPItem newerpitem)
        {
        ERPItem newitem = new ERPItem();

            newitem.CompID = newerpitem.CompID;
            newitem.EntryDate = newerpitem.EntryDate;
            newitem.ItemCode = newerpitem.ItemCode;
            newitem.ItemDesc = newerpitem.ItemDesc;
            newitem.ItemID = newerpitem.ItemID;
            newitem.MUnitPrimary = newerpitem.MUnitPrimary;
            newitem.MUnitSecondary = newerpitem.MUnitSecondary;
            newitem.MUnitsRelation = newerpitem.MUnitsRelation;
            return newitem;

        }

        protected ERPLot ParseSyncERPtoErpLot(WMSsyncService.SyncLot newerplot)
        {
            ERPLot newlot = new ERPLot();

            newlot.Color = newerplot.Color;
            newlot.CompID = newerplot.CompID;
            newlot.Draft = newerplot.Draft;
            newlot.EntryDate = newerplot.EntryDate;
            newlot.ItemID = newerplot.ItemID;
            newlot.ItemPrimaryQty = newerplot.ItemPrimaryQty;
            newlot.ItemSecondaryQty = newerplot.ItemSecondaryQty;
            newlot.Length = newerplot.Length;
            newlot.LotCode = newerplot.LotCode;
            newlot.LotID = newerplot.LotID;
            newlot.Width = newerplot.Width;

            return newlot;

        }





    }
}
