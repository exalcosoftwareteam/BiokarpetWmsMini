using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Reflection;
using System.Diagnostics;

using System.Xml;

namespace WMSSyncClient.components
{
    public enum ConfigFileType
    {
        WebConfig,
        AppConfig
    }

    public static class SyncItemsSelection
    {
        public static bool syncMunits { get; set; }
        public static bool syncItems { get; set; }
        public static bool syncLots { get; set; }
    }


    public class AppConfigManager : System.Configuration.AppSettingsReader
    {
        public string docName = String.Empty;
        private XmlNode node = null;
        private string ConfSection=null;

        private int _configType;

        public int ConfigType
        {
            get
            {
                return _configType;
            }
            set
            {
                _configType = value;
            }
        }

        public   AppConfigManager() {}
        public AppConfigManager(string confsection)
        {
            ConfSection = confsection;
        }
        //WMSSyncClient.Properties.Settings

        public bool SetValue(string key, string value)
        {
            XmlDocument cfgDoc = new XmlDocument();
            loadConfigDoc(cfgDoc);
            // retrieve the appSettings node 
            
            if (!string.IsNullOrEmpty(ConfSection))
                node = cfgDoc.SelectSingleNode("//" + ConfSection);
            else
                node = cfgDoc.SelectSingleNode("//appSettings");

            if (node == null)
            {
                throw new System.InvalidOperationException("appSettings section not found");
            }

            try
            {
                // XPath select setting "add" element that contains this key    
                //XmlElement addElem = (XmlElement)node.SelectSingleNode("//add[@key='" + key + "']");
                XmlElement addElem = (XmlElement)node.SelectSingleNode("//setting[@name='" + key + "']");
                
                if (addElem != null)
                {
                    addElem.LastChild.InnerText = value;
                    //addElem.SetAttribute("value", value);
                }
                // not found, so we need to add the element, key and value
                //else
                //{
                //    XmlElement entry = cfgDoc.CreateElement("setting");
                //    entry.SetAttribute("name", key);
                //    entry.SetAttribute("value", value);
                //    node.AppendChild(entry);
                //}
                //save it
                saveConfigDoc(cfgDoc, docName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void saveConfigDoc(XmlDocument cfgDoc, string cfgDocPath)
        {
            try
            {
                XmlTextWriter writer = new XmlTextWriter(cfgDocPath, null);
                writer.Formatting = Formatting.Indented;
                cfgDoc.WriteTo(writer);
                writer.Flush();
                writer.Close();
                return;
            }
            catch
            {
                throw;
            }
        }

        public bool removeElement(string elementKey)
        {
            try
            {
                XmlDocument cfgDoc = new XmlDocument();
                loadConfigDoc(cfgDoc);
                // retrieve the appSettings node 

                if (!string.IsNullOrEmpty(ConfSection))
                    node = cfgDoc.SelectSingleNode("//" + ConfSection);
                else
                    node = cfgDoc.SelectSingleNode("//appSettings");

                if (node == null)
                {
                    throw new System.InvalidOperationException("appSettings section not found");
                }
                // XPath select setting "add" element that contains this key to remove   
                node.RemoveChild(node.SelectSingleNode("//add[@key='" + elementKey + "']"));

                saveConfigDoc(cfgDoc, docName);
                return true;
            }
            catch
            {
                return false;
            }
        }


        private XmlDocument loadConfigDoc(XmlDocument cfgDoc)
        {
            // load the config file 
            if (Convert.ToInt32(ConfigType) == Convert.ToInt32(ConfigFileType.AppConfig))
            {

                docName = (( Assembly.GetEntryAssembly()).GetName()).Name;
                docName += ".exe.config";
            }
            //else
            //{
            //    docName = System.Web.HttpContext.Current.Server.MapPath("web.config");
            //}
            cfgDoc.Load(docName);
            return cfgDoc;
        }
    }

    class AppSettings
    {
        int storeid;
        int branchid;
        int compid;
        string storename;

        public WMSSyncClient.WMSSyncService.WMSSyncService GWSyncSVCService; 
       
        public AppSettings()
        {
            try 
            { 
                GWSyncSVCService = new WMSSyncClient.WMSSyncService.WMSSyncService();
                GWSyncSVCService.Url = Properties.Settings.Default.WSVC.ToString();
                GWSyncSVCService.Timeout = 100000;
            }
            catch { }

         

            try { storeid = int.Parse(Properties.Settings.Default.STOREID.ToString()); }
            catch { }
            try { storename = Properties.Settings.Default.STORENAME; }
            catch { }
            try { branchid = int.Parse(Properties.Settings.Default.BRANCHID.ToString()); }
            catch { }
            try { compid = int.Parse(Properties.Settings.Default.COMPID.ToString()); }
            catch { }

        }

        public int StoreID
        { 
            get { return storeid; }
            set { storeid = value; }
        }

        public string StoreName { get { return storename; } set { storename = value; } }
        public int BranchID
        { get { return branchid; } }

        public int CompID
        { get { return compid; } }

        public void SetPermanentStoreID(int newstoreid,string storename)
        {
            storeid = newstoreid;


            AppConfigManager AppconfigHandler = new AppConfigManager("applicationSettings");

            AppconfigHandler.ConfigType = Convert.ToInt32(ConfigFileType.AppConfig);

            AppconfigHandler.SetValue("STOREID",newstoreid.ToString());
            AppconfigHandler.SetValue("STORENAME", storename);
            
        }



    }
}
