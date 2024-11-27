using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WMSMobileClient.Components;
using WMSMobileClient.WMSservice;
using System.Data.SqlServerCe;

namespace WMSMobileClient
{
    public partial class FrmOfflineSettings : Form
    {
        WMSMobileClient.Components.ImportData importdata = new WMSMobileClient.Components.ImportData();
        WMSMobileClient.Components.SyncData syncdata = new WMSMobileClient.Components.SyncData();
        WMSSyncService.SyncInfo synciteminfo = new WMSSyncService.SyncInfo();
        WMSSyncService.SyncInfo synclotinfo = new WMSSyncService.SyncInfo();
        ItemHandler itemhandler = new ItemHandler();
        static int iimpitemrows = 0;
        int caller;
        static int iimplotrows = 0;
        public bool emptydb = false;
        public FrmOfflineSettings()
        {
            InitializeComponent();
            InitValues();
        }

        protected void InitValues() 
        {
            LBSyncInfo.Text = "Βρέθηκαν : ";
            LBSyncInfo.Visible = false;
            LBImportitemsrows.Text = "";
            LBImportitemsrows.Visible = false;
            LBImportlotrows.Text = "";
            LBImportlotrows.Visible = false;
            ImgSyncItems.Visible = false;
            ImgSyncLots.Visible = false;
            iimplotrows = 0;
            iimpitemrows = 0;
        
        }

        public FrmOfflineSettings(int ObjectCaller)
        {
            InitializeComponent();

            caller = ObjectCaller;
        }


        protected void GoBack()
        {

            if (caller == 1)
            {
                WMSForms.FrmPackingListHeader = new FrmPackingListHeader();
                WMSForms.FrmPackingListHeader.Show();
                this.Close();
            }
            else
            {
                WMSForms.FrmSelectInventoryHeader = new FrmSelectInventoryHeader();
                WMSForms.FrmSelectInventoryHeader.Show();
                this.Close();
            }
        }



        private void FrmOfflineSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Escape))
            {
                GoBack();
            }
            else
                EnableBackKey(e);


        }

        private void BtnSyncData_Click(object sender, EventArgs e)
        {
            InitValues();
            if (MessageBox.Show("ΠΡΟΣΟΧΗ ! τα δεδομένα ΘΑ ΔΙΑΓΡΑΦΟΥΝ ! και Θα μεταφερθούν απο την αρχή - πολύωρη διαδικασία. Είστε βέβαιοι ?", "Ερώτηση", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                ImportData();
            }
        }

        protected void FixResolutionIssues()
        {

            string oldpos = PBMenuBar.Location.X.ToString() + "X" + PBMenuBar.Location.Y.ToString();
            int oldbtny = PBBtnBck.Location.Y - PBMenuBar.Location.Y;

            if (Screen.PrimaryScreen.Bounds.Width > 240)
            {
                PBMenuBar.Location = new Point(PBMenuBar.Location.X, Screen.PrimaryScreen.Bounds.Height - PBMenuBar.Height - 30);

                PBBtnBck.Location = new Point(PBBtnBck.Location.X, PBMenuBar.Location.Y + oldbtny);


                PBMenuBar.SizeMode = PictureBoxSizeMode.StretchImage;
                PBBtnBck.SizeMode = PictureBoxSizeMode.StretchImage;

                PBMenuBar.Width = Screen.PrimaryScreen.Bounds.Width;
            }
            else
            {
                PBMenuBar.Location = new Point(PBMenuBar.Location.X, Screen.PrimaryScreen.Bounds.Height - PBMenuBar.Height - 25);

                PBBtnBck.Location = new Point(PBBtnBck.Location.X, PBMenuBar.Location.Y + oldbtny);


            }
        }
        protected void EnableBackKey(KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(AppGeneralSettings.BackButton))
            {
                if (AppGeneralSettings.BackButton == "ESC" && e.KeyCode == Keys.Escape)
                    GoBack();
                else if (AppGeneralSettings.BackButton == "F1" && e.KeyCode == Keys.F1)
                    GoBack();
            }
        }

        protected void ImportData()
        {

            long improws = 0;

            ImgSyncLots.Visible = false;

            LBImportitemsrows.Visible = false;
            LBImportlotrows.Visible = false;

            ImgSyncItems.Visible = false;
            ImgSyncLots.Visible = false;

           
            Cursor.Current = Cursors.WaitCursor;

            SyncInfo syncinfo = importdata.GetSyncInfo();
            //CLEARS DATABASE  !!!!!
            importdata.ClearGlotList();
            importdata.ClearGlotList();
            importdata.DELETEALLDATA();
            /////////////////////////
            LBSyncInfo.Text = "";
            if (syncinfo.ItemsRowsCount > 0) LBSyncInfo.Text += " [ " + syncinfo.ItemsRowsCount.ToString() + "] Είδη";
            if (syncinfo.LotRowsCount > 0) LBSyncInfo.Text += " - [ " + syncinfo.LotRowsCount.ToString() + "] Παρτίδες"; ;
            LBSyncInfo.Visible = true;
            Application.DoEvents();

            importdata.GetStores();
            importdata.GetMunits();

            improws = ImportItems(syncinfo);

            ImgSyncItems.Visible = true;
            if (improws > 0)
            {
                LBImportitemsrows.Visible = true;
                LBImportitemsrows.Text = "( " + improws.ToString() + " ) Είδη...";
                ImgSyncItems.Image = Properties.Resources.ok;

            }
            else
            {
                ImgSyncItems.Image = Properties.Resources.error;
            }
            Application.DoEvents();
            //improws = importdata.ImportLotsFromXML();
            LBImportlotrows.Visible = true;
            improws = ImportLots(syncinfo);
            //improws = importdata.ImportsLotsData(syncinfo);
            ImgSyncLots.Visible = true;
            if (improws > 0)
            {
                LBImportlotrows.Visible = true;
                LBImportlotrows.Text = "( " + improws.ToString() + " ) Παρτίδες...";
                ImgSyncLots.Image = Properties.Resources.ok;
            }
            else
                ImgSyncLots.Image = Properties.Resources.error;

           
            Cursor.Current = Cursors.Default;
        }

        protected void SyncData()
        {

            long improws = 0;

            ImgSyncLots.Visible = false;

            if (!emptydb)
            {
                cb_checkifexists.Checked = true;
                AppGeneralSettings.CheckIFItemOrLotExists = 1;
                emptydb = true;
                Application.DoEvents();
            }

            LBImportitemsrows.Visible = false;
            LBImportlotrows.Visible = false;
            ImgSyncItems.Visible = false;
            ImgSyncLots.Visible = false;

            BtnSyncData.Text = "Παρακαλώ περιμένετε...";
            Cursor.Current = Cursors.WaitCursor;

            //CREATE DATA AND GET INFO


            CreateDataAndGetInfo();
            // 0,4310sec per row



            LBSyncInfo.Visible = true;

            improws = SyncItems(synciteminfo);

            ImgSyncItems.Visible = true;
            if (improws > 0)
            {

                LBImportitemsrows.Visible = true;
                LBImportitemsrows.Text = "( " + improws.ToString() + " ) Είδη...";
                ImgSyncItems.Image = Properties.Resources.ok;
                Application.DoEvents();
            }
            else
                ImgSyncItems.Image = Properties.Resources.error;

            LBImportlotrows.Visible = true;
            improws = SyncLots(synclotinfo);

            ImgSyncLots.Visible = true;
            if (improws > 0)
            {
                LBImportlotrows.Visible = true;
                LBImportlotrows.Text = "( " + improws.ToString() + " ) Παρτίδες...";
                ImgSyncLots.Image = Properties.Resources.ok;
                Application.DoEvents();
            }
            else
                ImgSyncLots.Image = Properties.Resources.error;

            BtnSyncData.Text = "Ενημέρωση Δεδομένων";

            Cursor.Current = Cursors.Default;

        }

        private void PBBtnBck_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private void PBBtnBck_GotFocus(object sender, EventArgs e)
        {
            PBBtnBck.Image = Properties.Resources.buttonback_on;
        }

        private void PBBtnBck_MouseDown(object sender, MouseEventArgs e)
        {
            PBBtnBck.Image = Properties.Resources.buttonback_on;
        }

        private void PBBtnBck_LostFocus(object sender, EventArgs e)
        {
            PBBtnBck.Image = Properties.Resources.buttonback;
        }

        private void FrmOfflineSettings_Load(object sender, EventArgs e)
        {
            FixResolutionIssues();
        }
        protected void CreateDataAndGetInfo()
        {
            string fromdate = "";
            double minutes;

            
            fromdate = dateTimePicker1.Value.ToString("dd/MM/yyyy");

            if (emptydb) { fromdate = "null";  }

            syncdata.ClearGitemlist();
            syncdata.ClearGLotList();
            if (emptydb)
            {
                synciteminfo = syncdata.CreateItemData(true, fromdate);
                synclotinfo = syncdata.CreateLotData(true, fromdate);
                emptydb = false;
            }
            else
            {


                if (cb_fromdate.Checked)
                {
                    synciteminfo = syncdata.CreateItemData(true, fromdate);
                    synclotinfo = syncdata.CreateLotData(true, fromdate);

                }
                else
                {
                    synciteminfo = syncdata.CreateItemData(false, fromdate);
                    synclotinfo = syncdata.CreateLotData(false, fromdate);
                }
            }


            if (synciteminfo.ItemsRowsCount > 0) LBSyncInfo.Text += " [" + synciteminfo.ItemsRowsCount.ToString() + "] Είδη -";
            if (synclotinfo.LotRowsCount > 0) LBSyncInfo.Text += " [" + synclotinfo.LotRowsCount.ToString() + "] Παρτίδες";

            // minutes = ((itemcount + lotcount) * 0.4310) / 60;
            // minutes = Math.Round(minutes, 0);
            LBSyncInfo.Visible = true;
            //  lb_etimatedtime.Text += "Χρόνος συγχρονισμού κατά προσέγγιση : " + minutes.ToString() + " Λεπτά";
            Application.DoEvents();


        }
     
        protected long ImportItems(SyncInfo syncinfo)
        {
            long rowsaffected = 0;
            DateTime syncStart;
            int syncrecordsamnt = 100;

            if (AppGeneralSettings.SyncRecordsAmount > 0) syncrecordsamnt = AppGeneralSettings.SyncRecordsAmount;

            if (syncinfo.ItemsRowsCount > 0)
            {
                syncStart = DateTime.Now;
                LBImportitemsrows.Visible = true;

                if (syncinfo.ItemsRowsCount > syncrecordsamnt)
                {
                    for (long k = 0; k < syncinfo.ItemsRowsCount; k += syncrecordsamnt)
                    {
                        iimpitemrows += (int)importdata.GetPartiallyItems((int)k, (int)k + syncrecordsamnt);
                        rowsaffected = iimpitemrows;
                        LBImportitemsrows.Text = "Εισαγωγή " + iimpitemrows.ToString() + " από " + syncinfo.ItemsRowsCount.ToString();
                        Application.DoEvents();

                        if (k + syncrecordsamnt >= syncinfo.ItemsRowsCount)
                        {
                            importdata.ClearGlotList();

                        }
                    }
                }
                else
                {
                    iimpitemrows += (int)importdata.GetPartiallyItems(0, 0); //all      //TEST MUST BE 0,0                              
                    rowsaffected = iimpitemrows;
                    LBImportitemsrows.Text = "Εισαγωγή " + iimpitemrows.ToString() + " από " + syncinfo.ItemsRowsCount.ToString();
                }
            }



            ImgSyncItems.Visible = true;
            if (iimpitemrows > 0)
                ImgSyncItems.Image = Properties.Resources.ok;
            else
                ImgSyncItems.Image = Properties.Resources.error;

            return rowsaffected;
        }

        protected long ImportLots(SyncInfo syncinfo)
        {
            long rowsaffected = 0;
            DateTime syncStart;
            int syncrecordsamnt = 100;

            if (AppGeneralSettings.SyncRecordsAmount > 0) syncrecordsamnt = AppGeneralSettings.SyncRecordsAmount;

            if (syncinfo.LotRowsCount > 0)
            {
                syncStart = DateTime.Now;
                LBImportitemsrows.Visible = true;

                if (syncinfo.LotRowsCount > syncrecordsamnt)
                {
                    for (long k = 0; k < syncinfo.LotRowsCount; k += syncrecordsamnt)
                    {


                        iimplotrows += (int)importdata.GetPartiallyLots((int)k, (int)k + syncrecordsamnt);
                        rowsaffected = iimplotrows;
                        LBImportlotrows.Text = "Εισαγωγή " + iimplotrows.ToString() + " από " + syncinfo.LotRowsCount.ToString();
                        Application.DoEvents();
                        if (k + syncrecordsamnt >= syncinfo.LotRowsCount)
                        {
                            importdata.ClearGlotList();

                        }
                    }
                }
                else
                {
                    iimpitemrows += (int)importdata.GetPartiallyLots(0, 0); //all      //TEST MUST BE 0,0                              
                    rowsaffected = iimpitemrows;
                    LBImportlotrows.Text = "Εισαγωγή " + iimplotrows.ToString() + " από " + syncinfo.LotRowsCount.ToString();
                }
            }



            ImgSyncLots.Visible = true;
            if (iimplotrows > 0)
                ImgSyncLots.Image = Properties.Resources.ok;
            else
                ImgSyncLots.Image = Properties.Resources.error;

            return rowsaffected;
        }

        protected long SyncItems(WMSSyncService.SyncInfo syncinfo)
        {
            long rowsaffected = 0;
            DateTime syncStart;
            int syncrecordsamnt = 100;

            if (AppGeneralSettings.SyncRecordsAmount > 0) syncrecordsamnt = AppGeneralSettings.SyncRecordsAmount;

            syncStart = DateTime.Now;
            LBImportitemsrows.Visible = true;

            if (syncinfo.ItemsRowsCount > 0)
            {
                syncStart = DateTime.Now;
                LBImportitemsrows.Visible = true;

                if (syncinfo.ItemsRowsCount > syncrecordsamnt)
                {
                    for (long k = 0; k < syncinfo.ItemsRowsCount; k += syncrecordsamnt)
                    {

                        iimpitemrows += (int)syncdata.GetPartiallyItems((int)k, (int)k + syncrecordsamnt);
                        rowsaffected = iimpitemrows;
                        LBImportitemsrows.Text = "Εισαγωγή " + iimpitemrows.ToString() + " από " + syncinfo.ItemsRowsCount.ToString();
                        Application.DoEvents();
                        if (k + syncrecordsamnt >= syncinfo.ItemsRowsCount)
                        {
                            syncdata.ClearGitemlist();

                        }
                    }
                }
                else
                {
                    iimpitemrows += (int)syncdata.GetPartiallyItems(0, 0); //all      //TEST MUST BE 0,0                              
                    rowsaffected = iimpitemrows;
                    LBImportitemsrows.Text = "Εισαγωγή " + iimpitemrows.ToString() + " από " + syncinfo.ItemsRowsCount.ToString();
                }
            }

            if (rowsaffected > 0)
            {
                ImgSyncItems.Image = Properties.Resources.ok;
            }
            else if (rowsaffected == 0)
            {
                LBImportitemsrows.Text = "Δεν βρέθηκαν νέα είδη ";
                ImgSyncItems.Image = Properties.Resources.error;
            }
            else if (rowsaffected < 0)
            {
                LBImportitemsrows.Text = "Σφάλμα (" + rowsaffected.ToString() + ")";
            }
            return rowsaffected;

        }

        protected long SyncLots(WMSSyncService.SyncInfo syncinfo)
        {
            long rowsaffected = 0;
            DateTime syncStart;
            int syncrecordsamnt = 100;

            if (AppGeneralSettings.SyncRecordsAmount > 0) syncrecordsamnt = AppGeneralSettings.SyncRecordsAmount;

            if (syncinfo.LotRowsCount > 0)
            {
                syncStart = DateTime.Now;
                LBImportitemsrows.Visible = true;

                if (syncinfo.LotRowsCount > syncrecordsamnt)
                {
                    for (long k = 0; k < syncinfo.LotRowsCount; k += syncrecordsamnt)
                    {


                        iimplotrows += (int)syncdata.GetPartiallyLots((int)k, (int)k + syncrecordsamnt);
                        rowsaffected = iimplotrows;
                        LBImportlotrows.Text = "Εισαγωγή " + iimplotrows.ToString() + " από " + syncinfo.LotRowsCount.ToString();
                        Application.DoEvents();

                        //check if this is the final loop , if it is clear the list
                        if (k + syncrecordsamnt >= syncinfo.LotRowsCount)
                        {
                            syncdata.ClearGLotList();

                        }


                    }
                    

                }
                else
                {
                    iimpitemrows += (int)syncdata.GetPartiallyLots(0, 0); //all      //TEST MUST BE 0,0                              
                    rowsaffected = iimpitemrows;
                    LBImportlotrows.Text = "Εισαγωγή " + iimplotrows.ToString() + " από " + syncinfo.LotRowsCount.ToString();
                }
            }

            
            ImgSyncLots.Visible = true;
            if (iimplotrows > 0)
                ImgSyncLots.Image = Properties.Resources.ok;
            else
                ImgSyncLots.Image = Properties.Resources.error;
            if (rowsaffected > syncinfo.LotRowsCount)
            {
                return syncinfo.LotRowsCount;
            }
            else
            {
                return rowsaffected;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitValues();
            BtnSyncData.Text = "Παρακαλώ περιμένετε...";
            if (CheckIfDataExists()) SyncData();
            else
            {
   
                importdata.GetMunits();
                importdata.GetStores();
                SyncData();
            }
        }

        private bool CheckIfDataExists()
        {
            if (syncdata.ItemsInMobDB() > 0)
            {
                emptydb = false;
                return true;
            }
            else {
                emptydb = true;
                return false; }
        }

        public long SyncItemsProvider()
        {

            
            DataSet Ds = new DataSet();
            long raffected = 0;
            

            WMSSyncService.SyncERPItem[] erpitemlist = null;
            AppGeneralSettings.WebSyncServiceProvider.Timeout = 20000;
            try { erpitemlist = AppGeneralSettings.WebSyncServiceProvider.SOA_GetNewItems(AppGeneralSettings.StoreID, syncdata.GetItemLastSyncDate()); }
            catch (Exception ex) { Logger.Flog(">>GetPartiallyItems.GetDataset():" + ex.ToString()); }
            try
            {
                if (erpitemlist.Length <= 0) return 0;
            }
            catch { }
                     

            //raffected = DBSyncItems(erpitemlist);
          

            return raffected;
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {

        }

        private void cb_fromdate_CheckStateChanged(object sender, EventArgs e)
        {
            if (cb_fromdate.Checked) dateTimePicker1.Enabled = true; else dateTimePicker1.Enabled = false;

        }

        private void cb_checkifexists_CheckStateChanged(object sender, EventArgs e)
        {
            if (cb_checkifexists.Checked) AppGeneralSettings.CheckIFItemOrLotExists = 1; else AppGeneralSettings.CheckIFItemOrLotExists = 0;
        }



    }
}