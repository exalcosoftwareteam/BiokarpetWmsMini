
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Reflection;
using WMSMobileClient;
using WMSMobileClient.WMSservice;

namespace WMSMobileClient.Components
{
    public static class WMSForms
    {
        //public static FrmMain FrmMain;
        public static FrmInventory FrmInventory;
        public static FrmSelectInventoryHeader FrmSelectInventoryHeader;
        public static FrmSettings FrmSettings;
        public static FrmInventoryHeader FrmInventoryHeader;
        public static FrmInventoryView FrmInventoryView;
        public static FrmOfflineSettings FrmOfflineSettings;
        public static FrmDBSettings FrmDBSettings;
        public static FrmExportInventory FrmExportInventory;
        public static FrmSelectPackingList FrmSelectPackingList;
        public static FrmPackingListHeader FrmPackingListHeader;
        public static FrmPackingList FrmPackingList;
        public static FrmPackingListView FrmPackingListView;
        public static FrmExportPackingList FrmExportPackingList;
        public static FrmReceivesHeader FrmReceivesHeader;      
        public static FrmCreateTradeCode FrmCreateTradeCode;
        public static FrmReceiveView FrmReceiveView;
    }

    public static class  AppGeneralSettings
    {

        public static int SyncRecordsAmount;
        public static short CompID;
        public static short BranchID;
        public static short KindID;
        public static string CustomerCode;
        public static string StoreName;
        public static short TransType;
        public static int StoreID;
        public static short CheckIFItemOrLotExists = 0;
        public static long DSRID;
        public static string DBFile = "WMSMINIDB";
        public static string DBPasswd = "bidata";
        public static string TransCode = "";
        public static bool UseLot = true;
        public static bool UseBins = false;
        public static string ItemCodePrefix;
        public static bool RemovePrefixOnScanning;
        public static char WildChar = '.';
        public static string BackButton="F1";
        public static string errortmsg = "";
        public static string successmsg = "";
        public static string SERVERIP;
        public static bool ALTERINVEXPORT;
        public static ReceivesController receives = new ReceivesController();
        //public static SOARetailWMSMiniProvider.SOARetailWMSMiniProvider webServiceProvider;
        public static WMSservice.WebService webServiceProvider = new WebService();
        public static WMSSyncService.WMSSyncService WebSyncServiceProvider = new WMSSyncService.WMSSyncService();
    }

    public  struct AppXMLFiles
    {
        public static string XMLItems = "XMLItems.xml";
        public static string XMLLots = "XMLLot.xml";
    }

    public  class AppSettings
    {
         short compid;
         short branchid;
         short kindid;
         short transtype;
         int customerid;
         int storeid;
         int inventorymunit;
         int packmunit;
         int m2munit;
         long dsrid;
         bool uselot;
         bool usebins;
         string websvcurl;
         string serverip;
         short ALTERINVEXPORT;
         string websyncsvcurl;
         string transcode;
         string dbfile = "WMSMINIDB";
         string dbpass = "bidata";
         string configfile = "MyConfig.xml";
        string customerCode;
         string itemcodeprefix;
         bool removeprefixonScanning;
         char wildchar;
         string backbutton;

         public string ErrorMsg;
         public string UpdateURL;

         WMSservice.WebService webServiceProvider;
         WMSSyncService.WMSSyncService webSyncServiceProvider;


        #region Properties

        public  short CompID
        { get { return compid; } set { compid = value; } }

        public short KindID
        { get { return kindid; } set { kindid = value; } }

        public short TransType
        { get { return transtype; } set { transtype = value; } }

        public short BranchID
        { get { return branchid; } set { branchid = value; } }

        public  int StoreID
        { get { return storeid; } set { storeid = value; } }

        public  string CustomerCode
        { get { return customerCode; } set { customerCode = value; } }

        public int InventoryMunit
        { get { return inventorymunit; } set { inventorymunit = value; } }


         public int PackMunit
        { get { return packmunit; } set { packmunit = value; } }
        

        public int M2Uunit
        { get { return m2munit; } set { m2munit = value; } }

        public bool UseLot
        { get { return uselot; } set { uselot = value; } }

        public bool UseBins
        { get { return usebins; } set { usebins = value; } }

        public  string DBFile
        { get { return dbfile; } }

        public  string DBPasswd
        { get { return dbpass; } }

        public string ItemCodePrefix
        { get { return itemcodeprefix; } set { itemcodeprefix = value; } }
        
        public string SERVERIP
        { get { return serverip; } set { serverip = value; } }
        
        public bool RemovePrefixOnScanning
        { get { return removeprefixonScanning; } set { removeprefixonScanning = value; } }

        public long DSRID
        { get { return dsrid; } set { dsrid = value; } }

        public char WildChar
        { get { return wildchar; } }

        public string BackButton
        { get { return backbutton; } }

        public string WebSvcUrl
        { get { return websvcurl; } set { websvcurl = value; } }

         public string WebSyncSvcUrl
        { get { return websyncsvcurl; } set { websyncsvcurl = value; } }
        

        public string TransCode
        { get { return transcode; } set { transcode = value; } }

        public WMSservice.WebService WEBServiceProvider
        { get { return webServiceProvider; } }

        public WMSSyncService.WMSSyncService WEBSyncServiceProvider
        { get { return webSyncServiceProvider; } }

        #endregion


        public  AppSettings()
        {
            webServiceProvider = new WMSservice.WebService();
            webSyncServiceProvider = new WMSSyncService.WMSSyncService();

            SettingsFromFile();
            WebServiceSetup();
            HandleDbChanges();



            if (compid > 0) AppGeneralSettings.CompID = compid;
            if (branchid > 0) AppGeneralSettings.BranchID = branchid;
            if (storeid > 0) AppGeneralSettings.StoreID = storeid;
            if (kindid > 0) AppGeneralSettings.KindID = kindid;
            if (transtype > 0) AppGeneralSettings.TransType = transtype;
            AppGeneralSettings.CustomerCode = CustomerCode;
            AppGeneralSettings.TransCode = transcode;
            AppGeneralSettings.UseLot = uselot;

           
            
            if (AppGeneralSettings.StoreID > 0)
            {
                DB mydb = new DB();
                AppGeneralSettings.StoreName = mydb.DBWmsExSelectCmdRStr2Str("SELECT STORENAME FROM TSTORES WHERE STOREID=" + AppGeneralSettings.StoreID.ToString());
            }

            AppGeneralSettings.webServiceProvider = webServiceProvider;
            AppGeneralSettings.webServiceProvider.Url = websvcurl;

            AppGeneralSettings.WebSyncServiceProvider = webSyncServiceProvider;
            AppGeneralSettings.WebSyncServiceProvider.Url = websyncsvcurl;


            AppGeneralSettings.UseBins = usebins;

            AppGeneralSettings.WildChar = wildchar;
            AppGeneralSettings.ItemCodePrefix = itemcodeprefix;
            AppGeneralSettings.RemovePrefixOnScanning = removeprefixonScanning;
            AppGeneralSettings.BackButton = backbutton;
            AppGeneralSettings.DSRID = DSRID;
            AppGeneralSettings.SERVERIP = serverip;
            try { UpdateURL = websyncsvcurl.Replace("WMSSyncService.asmx", "") + "Download/WMSRetailClientCAB.CAB"; }
            catch { }
        }

        private void HandleDbChanges()
        {
            string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\dbchanges.txt";
            StreamReader readtext = new StreamReader(filepath);
            string dbchanges = readtext.ReadLine();
            readtext.Close();

            DB mydb = new DB();
            if (dbchanges.Length > 5)
            {
                try
                {
                    mydb.DBExecuteSQLCmd(dbchanges);
                    StreamWriter reader = new StreamWriter(filepath);
                    reader.Write("0");
                    reader.Close();
                }
                catch
                {
                    StreamWriter reader = new StreamWriter(filepath);
                    reader.Write("0");
                    reader.Close();

                }

            }
        }

        private   void SettingsFromFile()
        {
            string XMLConfFile;
            string XMLFilePath;

            XMLFilePath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            XMLConfFile = new FileInfo(XMLFilePath).DirectoryName + "\\" + configfile;


            XmlTextReader XMLConftReader = new System.Xml.XmlTextReader(XMLConfFile);
            XmlNameTable MyXmlNT   = XMLConftReader.NameTable;
         
            while (XMLConftReader.Read())
            {
                switch  (XMLConftReader.Name)
                    {
                    case "COMPID":
                            try { compid = short.Parse(XMLConftReader.ReadString()); }
                            catch { }
                            break;

                    case "BRANCHID":
                            try { branchid = short.Parse(XMLConftReader.ReadString()); }
                            catch { }
                            break;
                        
                    case "STOREID":
                            try { storeid = int.Parse(XMLConftReader.ReadString()); }
                            catch { }
                            break;

                    case "INVENTORYMUNIT":
                            try { inventorymunit = int.Parse(XMLConftReader.ReadString()); }
                            catch { }
                            break;
                    case "PACKMUNIT":
                            try { packmunit = int.Parse(XMLConftReader.ReadString()); }
                            catch { }
                            break;

                    case "M2MUNIT":
                            try { m2munit = int.Parse(XMLConftReader.ReadString()); }
                            catch { }
                            break;
                    case "USELOT":
                            try { uselot = (int.Parse(XMLConftReader.ReadString()) == 1 ? true : false); }
                            catch { uselot = false; }
                            break;
                    case "USEBINS":
                            try { usebins = (int.Parse(XMLConftReader.ReadString()) == 1 ? true : false); }
                            catch { usebins = false; }
                            break;
                    case "WMSService":
                            try { websvcurl = XMLConftReader.ReadString(); }
                            catch { }
                            break;
                    case "WMSSYNCSERVICE":
                            try { websyncsvcurl = XMLConftReader.ReadString(); }
                            catch { }
                            break;
                    case "ITEMCODEPREFIX":
                            try { itemcodeprefix = XMLConftReader.ReadString(); }
                            catch { }
                            break;
                    case "REMOVEPREFIXONSCANNING":
                            try { removeprefixonScanning = (XMLConftReader.ReadString() == "1" ? true : false); }
                            catch { }
                            break;
                    case "WILDCHAR":
                            try { wildchar = (char) XMLConftReader.ReadString()[0]; }
                            catch { }
                            break;
                    case "BACKBUTTON":
                            try { backbutton = XMLConftReader.ReadString(); }
                            catch { }
                            break;

                    case "KINDID":
                            try { kindid = short.Parse(XMLConftReader.ReadString()); }
                            catch { }
                            break;
                   
                   case "TRANSTYPE":
                            try { transtype = short.Parse(XMLConftReader.ReadString()); }
                            catch { }
                            break;

                   case "CUSTOMERCODE":
                            try { customerCode = XMLConftReader.ReadString(); }
                            catch { }
                            break;
                   case "TRANSCODE":
                            try { transcode = XMLConftReader.ReadString(); }
                            catch { }
                            break;
                   case "DSRID":
                            try { DSRID = long.Parse(XMLConftReader.ReadString()); }
                            catch { }
                            break;
                   case "SERVERIP":
                            try { serverip =XMLConftReader.ReadString(); }
                            catch { }
                            break;

                   case "ALTERINVEXPORT":
                            try { 
                                ALTERINVEXPORT = short.Parse(XMLConftReader.ReadString());
                                if (ALTERINVEXPORT > 0) { AppGeneralSettings.ALTERINVEXPORT = true; } else { AppGeneralSettings.ALTERINVEXPORT = false; }
                            }
                            catch { }
                            break;    

                        
                    }



            }

            XMLConftReader.Close();
            
           
        }

        private  void WebServiceSetup()
        {
            if (!string.IsNullOrEmpty(websvcurl))
                WEBServiceProvider.Url = websvcurl;

            if (!string.IsNullOrEmpty(websyncsvcurl))
                WEBSyncServiceProvider.Url = websyncsvcurl;

            WEBServiceProvider.Timeout = 60000;
            WEBSyncServiceProvider.Timeout = 60000;
        }

        public  long SaveSettings()
        {
            int fcompid, fbranchid, fstoreid;
            int finventorymunit, fm2munit,fpackmunit;
            string fwebsvcurl;
            string fwebsyncserviceurl;
            string fwildchar;
            string fitemcodeprefix="";            
            StringBuilder XmlStr = new StringBuilder();

            string XMLFilePath,XMLConfFile;

            if (compid > 0) fcompid = compid; else fcompid = 1;
            if (branchid > 0) fbranchid = branchid; else fbranchid = 0;
            if (storeid > 0) fstoreid = storeid; else fstoreid = 0;
            if (inventorymunit > 0) finventorymunit = inventorymunit; else finventorymunit = 0;
            if (m2munit > 0) fm2munit = m2munit; else fm2munit = 0;
            if (packmunit > 0) fpackmunit = packmunit; else fpackmunit = 0;
            if (compid > 0) AppGeneralSettings.CompID = compid;
            if (branchid > 0) AppGeneralSettings.BranchID = branchid;
            if (storeid > 0) AppGeneralSettings.StoreID = storeid;
            if (customerid > 0) AppGeneralSettings.CustomerCode = CustomerCode;
            if (DSRID > 0) AppGeneralSettings.DSRID = DSRID;
            if (wildchar != null) AppGeneralSettings.WildChar = wildchar;
            


            if (backbutton != null) AppGeneralSettings.BackButton = backbutton;

            AppGeneralSettings.RemovePrefixOnScanning = removeprefixonScanning;

            if (!string.IsNullOrEmpty(itemcodeprefix))
            {
                fitemcodeprefix = itemcodeprefix;
                AppGeneralSettings.ItemCodePrefix = itemcodeprefix;
            }

            fwildchar = AppGeneralSettings.WildChar.ToString();

            serverip = AppGeneralSettings.SERVERIP.ToString();
            
            
            if (!string.IsNullOrEmpty(websvcurl)) fwebsvcurl = websvcurl; else fwebsvcurl = " ";
            if (!string.IsNullOrEmpty(websyncsvcurl)) fwebsyncserviceurl = websyncsvcurl; else fwebsyncserviceurl = " ";

            XmlStr.AppendLine("<?xml version='1.0'?>");
            XmlStr.AppendLine("<AppConfig>");
            XmlStr.AppendLine("<COMPID>" + fcompid.ToString() + "</COMPID>");
            XmlStr.AppendLine("<BRANCHID>" + fbranchid.ToString() + "</BRANCHID>");
            XmlStr.AppendLine("<STOREID>" + fstoreid.ToString() + "</STOREID>");           
            XmlStr.AppendLine("<INVENTORYMUNIT>" + finventorymunit.ToString() + "</INVENTORYMUNIT>");
            XmlStr.AppendLine("<PACKMUNIT>" + fpackmunit.ToString() + "</PACKMUNIT>");
            XmlStr.AppendLine("<M2MUNIT>" + fm2munit.ToString() + "</M2MUNIT>");
            XmlStr.AppendLine("<USELOT>" + (uselot ? "1" : "0") + "</USELOT>");
            XmlStr.AppendLine("<USEBINS>" + (usebins ? "1" : "0") + "</USEBINS>");
            XmlStr.AppendLine("<WMSService>" + fwebsvcurl + "</WMSService>");
            XmlStr.AppendLine("<WMSSYNCSERVICE>" + fwebsyncserviceurl + "</WMSSYNCSERVICE>");
            XmlStr.AppendLine("<ITEMCODEPREFIX>" + fitemcodeprefix + "</ITEMCODEPREFIX>");
            XmlStr.AppendLine("<REMOVEPREFIXONSCANNING>" + (removeprefixonScanning==true ? "1" : "0") + "</REMOVEPREFIXONSCANNING>");
            XmlStr.AppendLine("<WILDCHAR>" + fwildchar + "</WILDCHAR>");
            XmlStr.AppendLine("<BACKBUTTON>" + backbutton + "</BACKBUTTON>");
            XmlStr.AppendLine("<KINDID>" + kindid.ToString() + "</KINDID>");
            XmlStr.AppendLine("<TRANSTYPE>" + transtype.ToString() + "</TRANSTYPE>");
            XmlStr.AppendLine("<CUSTOMERCODE>" + customerCode.ToString() + "</CUSTOMERCODE>");
            XmlStr.AppendLine("<TRANSCODE>" + transcode + "</TRANSCODE>");
            XmlStr.AppendLine("<DSRID>" + DSRID.ToString() + "</DSRID>");
            XmlStr.AppendLine("<SERVERIP>" + SERVERIP.ToString() + "</SERVERIP>");
            if (AppGeneralSettings.ALTERINVEXPORT)
            {
                XmlStr.AppendLine("<ALTERINVEXPORT>1</ALTERINVEXPORT>");
            }
            else 
            {
                XmlStr.AppendLine("<ALTERINVEXPORT>0</ALTERINVEXPORT>");
            }
            XmlStr.AppendLine("</AppConfig>");

            XMLFilePath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            XMLConfFile = new System.IO.FileInfo(XMLFilePath).DirectoryName + "\\" + configfile;


            try {File.Delete(XMLConfFile);} catch{}

            XmlDocument XmlConf = new XmlDocument();

            try
            {
                XmlConf.LoadXml(XmlStr.ToString());
                XmlConf.Save(XMLConfFile);
                return 1;
            }
            catch (Exception ex) { ErrorMsg = ex.ToString();return -1; }

            //return 0;
        }

        
    }

}
