using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WMSMobileClient;
using System.Net;
using System.Xml;
using System.Web;
using WMSMobileClient.Components;
using WMSMobileClient.WMSservice;
using System.IO;


namespace WMSMobileClient
{
    public partial class FrmSettings : Form
    {
        Boolean justopened = false;
        bool iHitEnter = false;
        CompactDB cdp = new CompactDB();
        AppSettings settings = new AppSettings();

        public FrmSettings()
        {
            justopened = true;
            InitializeComponent();
            FixResolutionIssues();

            GetSettings();

            GetMobileDeviceIPAddress();
            ReadStoresFromXML();
            justopened = false;
        }

        protected void GetMobileDeviceIPAddress()
        {
            IPHostEntry host;

            host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily.ToString() == "InterNetwork")
                    {
                        LBIpAddress.Text  = "ip: " + ip.ToString();
                    }
                }
            
        }

        #region Events
        private void FrmSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && iHitEnter)
            {
                SaveChanges();
            }
            if (e.KeyCode == Keys.Escape)
            {
                GotoMenu();
            }

        }

        private void TBCompID_GotFocus(object sender, EventArgs e)
        {            
            PBoxCompID.Image = Properties.Resources.textbox_small_focus;
            DisableEnter();
        }

        private void TBCompID_LostFocus(object sender, EventArgs e)
        {                        
            PBoxCompID.Image = Properties.Resources.textbox_small;
        }

        private void TBBranchID_GotFocus(object sender, EventArgs e)
        {            
            PBoxBranchID.Image = Properties.Resources.textbox_small_focus;
            DisableEnter();
        }

        private void TBBranchID_LostFocus(object sender, EventArgs e)
        {            
            PBoxBranchID.Image = Properties.Resources.textbox_small;
        }

        private void TBStoreID_GotFocus(object sender, EventArgs e)
        {

            PBoxStoreID.Image = Properties.Resources.textbox_small_focus;
            DisableEnter();
        }



        private void TBStoreID_LostFocus(object sender, EventArgs e)
        {
            PBoxStoreID.Image = Properties.Resources.textbox_small;
        }

        private void TBWebService_GotFocus(object sender, EventArgs e)
        {
            PBoxWebService.Image = Properties.Resources.textbox_focus;
            DisableEnter();
        }

        private void TBWebService_LostFocus(object sender, EventArgs e)
        {
             PBoxWebService.Image = Properties.Resources.textbox;
        }


                         
        private void TBWebService_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                TBCompID.Focus();
            if (e.KeyCode == Keys.Escape)
                GotoMenu();
        }

        private void TBCompID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                TBBranchID.Focus();
            if (e.KeyCode == Keys.Escape)
                GotoMenu();
        }


        private void TBBranchID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                TBStoreID.Focus();
            if (e.KeyCode == Keys.Escape)
                GotoMenu();
        }

        private void TBStoreID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                iHitEnter = true;                                
            }
            if (e.KeyCode == Keys.Escape)
                GotoMenu();
        }


        private void PBoxEnter_MouseMove(object sender, MouseEventArgs e)
        {
            BtnSave.Image = Properties.Resources.ENTER;
        }

        private void PBBtnBck_Click(object sender, EventArgs e)
        {
            GotoMenu();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void PBBtnBck_GotFocus(object sender, EventArgs e)
        {
            PBBtnBck.Image = Properties.Resources.buttonback_on;
        }

        private void PBBtnBck_LostFocus(object sender, EventArgs e)
        {
            PBBtnBck.Image = Properties.Resources.buttonback;
        }

        private void PBBtnBck_MouseDown(object sender, MouseEventArgs e)
        {
            PBBtnBck.Image = Properties.Resources.buttonback_on;
        }

        private void PBBtnBck_MouseUp(object sender, MouseEventArgs e)
        {
            PBBtnBck.Image = Properties.Resources.buttonback;
        }

        private void BtnSave_GotFocus(object sender, EventArgs e)
        {
            BtnSave.Image = Properties.Resources.buttonsave_on;
        }

        private void BtnSave_LostFocus(object sender, EventArgs e)
        {
            BtnSave.Image = Properties.Resources.buttonsave;
        }

        private void BtnSave_MouseDown(object sender, MouseEventArgs e)
        {
            BtnSave.Image = Properties.Resources.buttonsave_on;
        }

        private void BtnSave_MouseUp(object sender, MouseEventArgs e)
        {
            BtnSave.Image = Properties.Resources.buttonsave;
        }

        private void PBoxEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SaveChanges();
            if (e.KeyCode == Keys.Escape)
                GotoMenu();
        }


        private void PBoxWebService_Click(object sender, EventArgs e)
        {
            TBWebService.Focus();
        }

        private void PBoxCompID_Click(object sender, EventArgs e)
        {
            TBCompID.Focus();
        }

        private void PBoxStoreID_Click(object sender, EventArgs e)
        {
            TBStoreID.Focus();
        }

        private void PBoxBranchID_Click(object sender, EventArgs e)
        {
            TBBranchID.Focus();
        }

        
        private void BtnCheckWSConnection_Click(object sender, EventArgs e)
        {
            CheckWSConnection();

        }

        private void BtnCheckWSConnection_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && iHitEnter)
            {
                SaveChanges();
            }
            if (e.KeyCode == Keys.Escape)
            {
                GotoMenu();
            }
        }




        private void LLbUpdate_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("Έτοιμη για αναβάθμιση;", "Ερώτηση", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                try { System.Diagnostics.Process.Start("iexplore", settings.UpdateURL); }
                catch { }
                Application.Exit();


                //try
                //{
                //    if (System.IO.File.Exists("\\My Documents\\WMSRetailClientCAB.CAB"))
                //    {
                //        Application.Exit();
                //        System.Diagnostics.Process.Start("\\My Documents\\WMSRetailClientCAB.CAB", "");
                //    }
                //    else
                //        MessageBox.Show(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.ToString());
                //}
            }
        }

        #endregion

        protected void GetSettings()
        {
            DisableEnter();
            TBBranchID.Text = AppGeneralSettings.BranchID.ToString();
            TBCompID.Text = AppGeneralSettings.CompID.ToString();
            TBStoreID.Text = AppGeneralSettings.StoreID.ToString();
            TBWebService.Text = AppGeneralSettings.webServiceProvider.Url.ToString();
            TBSyncWebService.Text = AppGeneralSettings.WebSyncServiceProvider.Url.ToString();

            if (AppGeneralSettings.ALTERINVEXPORT)
            {
                cb_alterinvexport.Checked = true;
            }else
            {
                cb_alterinvexport.Checked = false;
            }

            if (AppGeneralSettings.OnlineMode)
            {
                cb_onlinemode.Checked = true;
            }else
            {
                cb_onlinemode.Checked = false;
            }

            

            LLbUpdate.Text = "Download Update";
           
        }

        protected void GotoMenu()
        {          
            //WMSForms.FrmMain.Show();
            this.Close();
            
        }

        protected void SaveChanges()
        {
            try { if (int.Parse(TBCompID.Text) > 0 && AppGeneralSettings.CompID != int.Parse(TBCompID.Text)) AppGeneralSettings.CompID = short.Parse(TBCompID.Text); }
            catch { if (TBCompID.Text.Length > 0) ErrorMessage(""); return; }

            try { if (int.Parse(TBBranchID.Text) > 0 && AppGeneralSettings.BranchID != int.Parse(TBBranchID.Text)) AppGeneralSettings.BranchID = short.Parse(TBBranchID.Text); }
            catch { ErrorMessage(""); return; }

            try { if (int.Parse(TBStoreID.Text) > 0) AppGeneralSettings.StoreID = int.Parse(TBStoreID.Text); }
            catch { ErrorMessage(""); return; }

            settings.WEBServiceProvider.Url = TBWebService.Text;
            settings.WebSvcUrl = TBWebService.Text;
            settings.WEBSyncServiceProvider.Url = TBSyncWebService.Text;
            settings.WebSyncSvcUrl = TBSyncWebService.Text;


            if (AppGeneralSettings.CompID > 0) settings.CompID = AppGeneralSettings.CompID;
            if (AppGeneralSettings.BranchID > 0) settings.BranchID = AppGeneralSettings.BranchID;
            if (AppGeneralSettings.StoreID > 0) settings.StoreID = AppGeneralSettings.StoreID;
            if (AppGeneralSettings.KindID > 0) settings.KindID = AppGeneralSettings.KindID;
            if (AppGeneralSettings.TransType > 0) settings.TransType = AppGeneralSettings.TransType;
            if (AppGeneralSettings.DSRID > 0) settings.DSRID = AppGeneralSettings.DSRID;
            AppGeneralSettings.ALTERINVEXPORT = cb_alterinvexport.Checked;
            AppGeneralSettings.OnlineMode = cb_onlinemode.Checked;


            if (settings.SaveSettings() > 0)
            {
                GotoMenu();
            }
            else
            {
                MessageBox.Show("Πρόβλημα με την αποθήκευση των ρυθμίσεων");
            }

        }

        protected void EnableEnter()
        {
            if (BtnSave.Image != Properties.Resources.buttonsave_on) BtnSave.Image = Properties.Resources.buttonsave_on;            
        }

        protected void DisableEnter()
        {
            if (BtnSave.Image != Properties.Resources.buttonsave) BtnSave.Image = Properties.Resources.buttonsave;
        }

        protected void ErrorMessage(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
                MessageBox.Show("Μη έγκυρος αριθμός!", "Σφάλμα!", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            else
                MessageBox.Show(msg, "Σφάλμα!", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
        }       

        protected void GoDBSettings()
        {
            Cursor.Current = Cursors.WaitCursor;
            
            WMSForms.FrmDBSettings = new FrmDBSettings();
            WMSForms.FrmDBSettings.Show();
            this.Close();

            Cursor.Current = Cursors.Default;
        }

        protected void CheckWSConnection()
        {
            int oldwmssynctimeout = 0;
            int oldwmstimeout = 0;
            Cursor.Current = Cursors.WaitCursor;

            long RTRN = -1;
            try
            {
                oldwmstimeout = AppGeneralSettings.webServiceProvider.Timeout;
                AppGeneralSettings.webServiceProvider.Timeout = 1500;
                RTRN = long.Parse(AppGeneralSettings.webServiceProvider.CheckDBConnection());
                AppGeneralSettings.webServiceProvider.Timeout = oldwmstimeout;
                if (RTRN == -10)
                {
                    PBoxWSConStatus.Image = Properties.Resources.ok;
                    MessageBox.Show("Πρόβλημα με τη σύνδεση στη Β.Δ.!");
                }
                else if (RTRN > 0)
                    PBoxWSConStatus.Image = Properties.Resources.ok;
                else
                    PBoxWSConStatus.Image = Properties.Resources.error;
            }
            catch (Exception ex){ PBoxWSConStatus.Image = Properties.Resources.error; }
            PBoxWSConStatus.Visible = true;


            try 
            {
                oldwmssynctimeout = AppGeneralSettings.WebSyncServiceProvider.Timeout;
                AppGeneralSettings.WebSyncServiceProvider.Timeout = 1500;
                RTRN = AppGeneralSettings.WebSyncServiceProvider.ConnectionStatus();
                AppGeneralSettings.WebSyncServiceProvider.Timeout = oldwmssynctimeout;
                if (RTRN == -10)
                {
                    PBoxWSConStatus.Image = Properties.Resources.ok;
                    MessageBox.Show("Πρόβλημα με τη σύνδεση στη Β.Δ.!");
                }
                else if (RTRN > 0)
                    PBoxWSSyncConStatus.Image = Properties.Resources.ok;
                else
                    PBoxWSSyncConStatus.Image = Properties.Resources.error;
            }
            catch (Exception ex){ PBoxWSSyncConStatus.Image = Properties.Resources.error; }
            PBoxWSSyncConStatus.Visible = true;


            Cursor.Current = Cursors.Default;


        }


        protected void FixResolutionIssues()
        {

            string oldpos = PBMenuBar.Location.X.ToString() + "X" + PBMenuBar.Location.Y.ToString();
            int oldbtny = PBBtnBck.Location.Y - PBMenuBar.Location.Y;

            if (Screen.PrimaryScreen.Bounds.Width > 240)
            {
                PBMenuBar.Location = new Point(PBMenuBar.Location.X, Screen.PrimaryScreen.Bounds.Height - PBMenuBar.Height - 30);

                PBBtnBck.Location = new Point(PBBtnBck.Location.X, PBMenuBar.Location.Y + oldbtny);
                BtnSave.Location = new Point(BtnSave.Location.X, PBMenuBar.Location.Y + oldbtny);


                PBMenuBar.SizeMode = PictureBoxSizeMode.StretchImage;
                PBBtnBck.SizeMode = PictureBoxSizeMode.StretchImage;
                BtnSave.SizeMode = PictureBoxSizeMode.StretchImage;

                PBMenuBar.Width = Screen.PrimaryScreen.Bounds.Width;


                BtnDBSettings.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {

                PBoxWebService.Width = 205;
                //PBoxWebService.Height = 35;
                PBoxWebService.SizeMode = PictureBoxSizeMode.StretchImage;
                //

                PBoxSyncWebService.Width = 205;
                //PBoxWebService.Height = 35;
                PBoxSyncWebService.SizeMode = PictureBoxSizeMode.StretchImage;


               // PBoxStoreID.Width = 85;
               //PBoxStoreID.Height = 35;
                PBoxStoreID.SizeMode = PictureBoxSizeMode.StretchImage;
                //
                //PBoxCompID.Width = 85;
               //PBoxCompID.Height = 35;
                PBoxCompID.SizeMode = PictureBoxSizeMode.StretchImage;

 
                //PBoxBranchID.Width = 85;
                //PBoxBranchID.Height = 35;
                PBoxBranchID.SizeMode = PictureBoxSizeMode.StretchImage;


                PBMenuBar.Location = new Point(PBMenuBar.Location.X, Screen.PrimaryScreen.Bounds.Height - PBMenuBar.Height - 25);
                PBBtnBck.Location = new Point(PBBtnBck.Location.X, PBMenuBar.Location.Y + oldbtny);
                BtnSave.Location = new Point(BtnSave.Location.X, PBMenuBar.Location.Y + oldbtny);

            }
        }

        private void BtnDBSettings_Click(object sender, EventArgs e)
        {
            BtnDBSettings.Image = Properties.Resources.db_on;
            GoDBSettings();
        }

        private void BtnDBSettings_MouseDown(object sender, MouseEventArgs e)
        {
            BtnDBSettings.Image = Properties.Resources.db_on;
        }

        private void BtnDBSettings_MouseUp(object sender, MouseEventArgs e)
        {
            BtnDBSettings.Image = Properties.Resources.db;
        }

        private void BtnDBSettings_GotFocus(object sender, EventArgs e)
        {
            BtnDBSettings.Image = Properties.Resources.db_on;
        }

        private void BtnDBSettings_LostFocus(object sender, EventArgs e)
        {
            BtnDBSettings.Image = Properties.Resources.db;
        }



        private void PBSoftKeyb_Click(object sender, EventArgs e)
        {
            if (OnScreenKeyboard.Enabled)
                OnScreenKeyboard.Enabled = false;
            else
                OnScreenKeyboard.Enabled = true;
        }


        
        private void ReadStoresFromXML()
        {
            long rtrn;
            string XMLFilePath, XMLConfFile, test,storename;
            XmlDocument doc = new XmlDocument();
            //  HttpContext.Current.Request.PhysicalApplicationPath + XMLFName

            XMLFilePath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            XMLConfFile = new FileInfo(XMLFilePath).DirectoryName + "\\" + "STORES.XML";


            XmlTextReader XMLConftReader = new System.Xml.XmlTextReader(XMLConfFile);
            XmlNameTable MyXmlNT = XMLConftReader.NameTable;
            StringBuilder sqlstr = new StringBuilder();
            DataSet Xmltable = new DataSet();
            DataTable dt = new DataTable();
            Xmltable.ReadXml(XMLConfFile);
            dt = Xmltable.Tables[0];
            CompactDB cdp = new CompactDB();

            cdp.CreateTableTstores();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbStores.Items.Add(dt.Rows[i]["STORENAME"].ToString());
                sqlstr.Append("INSERT INTO TStores  (StoreID,Branchid,SERVERIP,StoreName,DSRIDTOCENTRAL,DSRIDTOBRANCH) VALUES (");
                if (!string.IsNullOrEmpty(dt.Rows[i]["STOREID"].ToString())) sqlstr.Append(dt.Rows[i]["STOREID"].ToString() + ","); else sqlstr.Append("NULL,");
                if (!string.IsNullOrEmpty(dt.Rows[i]["BRANCHID"].ToString())) sqlstr.Append(dt.Rows[i]["BRANCHID"].ToString() + ","); else sqlstr.Append("NULL,");
                if (!string.IsNullOrEmpty(dt.Rows[i]["SERVERIP"].ToString())) sqlstr.Append("'" + dt.Rows[i]["SERVERIP"].ToString() + "',"); else sqlstr.Append("NULL,");
                if (!string.IsNullOrEmpty(dt.Rows[i]["STORENAME"].ToString())) sqlstr.Append("'" + dt.Rows[i]["STORENAME"].ToString() + "',"); else sqlstr.Append("NULL,");
                if (!string.IsNullOrEmpty(dt.Rows[i]["DSRIDTOCENTRAL"].ToString())) sqlstr.Append(dt.Rows[i]["DSRIDTOCENTRAL"].ToString() + ","); else sqlstr.Append("NULL,");
                if (!string.IsNullOrEmpty(dt.Rows[i]["DSRIDTOBRANCH"].ToString())) sqlstr.Append(dt.Rows[i]["DSRIDTOBRANCH"].ToString()); else sqlstr.Append("NULL");
                sqlstr.Append(")");
                rtrn = cdp.db.DBExecuteSQLCmd(sqlstr.ToString());
                if (rtrn < 0)
                {
                    
                    MessageBox.Show("Πρόβλημα στην εισαγωγή των Υποκαταστημάτων");
                    return;
                }
                sqlstr = new StringBuilder();

            }
            if(AppGeneralSettings.StoreID > 0 )
            {
              storename = cdp.db.DBWmsExSelectCmdRStr2Str("SELECT STORENAME FROM TSTORES WHERE STOREID=" + AppGeneralSettings.StoreID.ToString());
              cbStores.SelectedItem = storename;
              AppGeneralSettings.StoreName = storename;
            }


        }

        private void cbStores_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cdp.db.DBWmsExSelectCmdRN2String("create table   TsysErrorLog (SysErrLogID int IDENTITY (100,1) PRIMARY KEY, ErrorCode nvarchar (100), ErrorText nvarchar (250),LogDate datetime not null default getdate())");
            MessageBox.Show("ok");
        }

        private void cbStores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (justopened) return;
            string storename;
            try
            {
                storename = cbStores.SelectedItem.ToString();
                AppGeneralSettings.StoreID = int.Parse(cdp.db.DBWmsExSelectCmdRN2String("SELECT STOREID FROM TSTORES WHERE STORENAME='" + storename + "'"));
                AppGeneralSettings.BranchID = short.Parse(cdp.db.DBWmsExSelectCmdRN2String("SELECT BRANCHID FROM TSTORES WHERE STORENAME='" + storename + "'"));
                TBStoreID.Text = AppGeneralSettings.StoreID.ToString();
                TBBranchID.Text = AppGeneralSettings.BranchID.ToString();
                //AppGeneralSettings.SERVERIP = cdp.db.DBWmsExSelectCmdRStr2Str("SELECT SERVERIP FROM TSTORES WHERE STORENAME='" + storename + "'");
                //TBWebService.Text = "http://" + AppGeneralSettings.SERVERIP + "/WMSminiWebService/WebService.asmx";
                //TBSyncWebService.Text = "http://" + AppGeneralSettings.SERVERIP + "/WMSSyncService/WMSSyncService.asmx";
            }
            catch { }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }



      
    }

}
 