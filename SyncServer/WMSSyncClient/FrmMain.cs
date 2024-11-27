using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.Threading;

using WMSSyncClient.components;
using WMSSyncClient.WMSSyncService;

 
namespace WMSSyncClient
{
   
    public partial class FrmMain : Form
    {

        AppSettings appsetting = new AppSettings();
        SyncItems syncitems = new SyncItems();

        bool goon = false;

        delegate  long FAsyncSyncMUnits();
        //delegate void FAsyncSyncStores();
        delegate void FAsyncSyncItems();
        delegate void FAsyncSyncLots();
        delegate void FAsyncPrepareData();

        delegate void SetTextCallback(string text);

        bool DDLStoreFocus = false;
       

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

            FGetSettings();

            SyncItemsSelection.syncMunits = true;
            SyncItemsSelection.syncItems = true;
            SyncItemsSelection.syncLots = true;

            CkBmunits.Checked = true;
            CkBItems.Checked = true;
            CkBLots.Checked = true;

        }

        #region "Form Events"

        private void DDLStore_SelectedIndexChanged(object sender, EventArgs e)
        {
                       
        }

        private void DDLStore_SelectionChangeCommitted(object sender, EventArgs e)
        {          
            appsetting.StoreID = 0;
            if (DDLStoreFocus) BtnSetStore.Text = "Αλλαγή";
        }

        private void DDLStore_Enter(object sender, EventArgs e)
        {
            DDLStoreFocus = true;
        }

        private void DDLStore_Leave(object sender, EventArgs e)
        {
            DDLStoreFocus = false;
        }

        private void TBWSVCRootFolder_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnSyncData_Click(object sender, EventArgs e)
        {

            if (!(appsetting.StoreID > 0) )
            {
                MessageBox.Show("Παρακαλώ επιλέξτε Α.Χ. και πιέστε το κουμπί <<Αλλαγή>> για επιβεβαίωση!");
                return;
            }
            if (MessageBox.Show("Να ξεκινήσει η διαδικασία του συγχρονισμού δεδομένων;", "Ερώτηση", MessageBoxButtons.YesNo,
                     MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes) { BtnSyncData.Text = "Λήψη Δεδομένων"; return; }


            PGBar.Value = 0;
           
            BtnSyncData.Text = "Παρακαλώ Περιμένετε...";

           
            LBSyncItems.Text = ".......................Συγχρονισμός  Ειδών";
            LBSyncLots.Text = ".................Συγχρονισμός Παρτίδων";

            ImgSyncItems.Visible = false;
            ImgSyncLots.Visible = false;

            syncitems = null;
            syncitems = new SyncItems();

            BtnCancel.Enabled = true;
            BtnSyncData.Enabled = false;

            BWorker.RunWorkerAsync();

            //if (CkBItems.Checked)
            //    BWorker.RunWorkerAsync();
            //else if (CkBLots.Checked)
            //    BWorker2.RunWorkerAsync();
           
          
        }

        private void BtnSetStore_Click(object sender, EventArgs e)
        {
            if (BtnSetStore.Text == "Αλλαγή")
            {
                SetStore();
                BtnSyncData.Enabled = true;
            }
            else
            {
                BtnSyncData.Enabled = false;
                GetStores();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            BWorker.CancelAsync();            
        }

        private void BtnCancel2_Click(object sender, EventArgs e)
        {
            BWorker2.CancelAsync();        
        }

        private void BWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (BWorker.CancellationPending) e.Cancel = true;   
            PrepareSyncData();                                                            
            BWorker.ReportProgress(10);

            if (BWorker.CancellationPending) e.Cancel = true;   
            
            if (SyncItemsSelection.syncMunits) SyncMunits();
            
            BWorker.ReportProgress(20);

            if (BWorker.CancellationPending) e.Cancel = true;   
            
            if (SyncItemsSelection.syncItems) SyncItems();

                         
            BWorker.ReportProgress(100);
        }

        private void BWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
                      
            if (BWorker2.CancellationPending) e.Cancel = true;

            if (SyncItemsSelection.syncLots) SyncLots();

            BWorker2.ReportProgress(100);
        }

        private void BWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            PGBar.Value = e.ProgressPercentage;
            

        }

        private void BWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            PGLotBar.Value = e.ProgressPercentage;
        }


        private void BWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Η εργασία ακυρώθηκε από το χρήστη");
            }

            ImgSyncItems.Visible = true;
            LBSyncItems.Visible = true;
            if (syncitems.ItemsTransfered > 0)
            {
                if (syncitems.ItemsTransfered > syncitems.syncinfo.ItemsRowsCount)
                    syncitems.ItemsTransfered = syncitems.syncinfo.ItemsRowsCount;
                LBSyncItems.Text += " " + syncitems.ItemsTransfered.ToString() + " Από " + syncitems.syncinfo.ItemsRowsCount.ToString();
                ImgSyncItems.Image = Properties.Resources.ok;
                ImgSyncItems.Visible = true;
            }
            else
            {
                ImgSyncItems.Visible = true;
                LBSyncItems.Text += " 0 " + " Από " + syncitems.syncinfo.ItemsRowsCount.ToString();
                ImgSyncItems.Image = Properties.Resources.error;
            }


           
            BtnSyncData.Text = "Λήψη Δεδομένων";

            BtnSyncData.Enabled = true;
            BtnCancel.Enabled = false;
            
            //
            BWorker2.RunWorkerAsync();
        }

        private void BWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Η εργασία ακυρώθηκε από το χρήστη");
            }

            LBSyncLots.Visible = true;
            ImgSyncLots.Visible = true;
            if (syncitems.LotsTransfered > 0)
            {

                if (syncitems.LotsTransfered > syncitems.syncinfo.LotRowsCount)
                    syncitems.LotsTransfered = syncitems.syncinfo.LotRowsCount;

                LBSyncLots.Text += " " + syncitems.LotsTransfered.ToString() + " Από " + syncitems.syncinfo.LotRowsCount.ToString();

                ImgSyncLots.Image = Properties.Resources.ok;
                ImgSyncLots.Visible = true;
            }
            else
            {
                LBSyncLots.Text += " 0 " + " Από " + syncitems.syncinfo.ItemsRowsCount.ToString();

                ImgSyncLots.Visible = true;
                ImgSyncLots.Image = Properties.Resources.error;
            }

            BtnSyncData.Text = "Λήψη Δεδομένων";

            BtnCancel2.Enabled = false;
            BtnSyncData.Enabled = true;
        }

        #endregion

        protected void TransferData()
        {                                          
            PrepareSyncData();

            SyncMunits();
            //AsyncSyncItems();
            //SyncItems();            
            //AsyncSyncLots();
            //SyncLots();                       
        }

        protected void ChangeButtonText(string s)
        {
            BtnSyncData.Text = s;
        }

        protected int FGetSettings()
        {
            try
            {
                TBWSVCRootFolder.Text =  appsetting.GWSyncSVCService.Url.ToString();
                if (appsetting.GWSyncSVCService.ConnectionStatus() > 0)
                {

                    ImgOkWebserviceCon.Visible = true;

                    LBwebserviceVersion.Text = "Web Service Version :" +  appsetting.GWSyncSVCService.SOA_WebServiceVersion();
                }
                else
                {
                    LBWebserviceStatus.Visible = true;
                    LBWebserviceStatus.Text = "Πρόβλημα  επικοινωνίας με την Υπηρεσία WEBService";
                    BtnSyncData.Enabled = false;
                    BtnSyncData.BackColor = Color.Gray;
                    return -1;
                }
            }
            catch { }

            try {
                if (appsetting.GWSyncSVCService.ConnectionStatus() == 1)
                    ImgOkWebserviceCon.Image = Properties.Resources.ok;

            }
            catch { ImgOkWebserviceCon.Image = Properties.Resources.error; return -1; }

            LBStoreID.Text = "#" + appsetting.StoreID.ToString();
            //

            Store st = new Store();
            st.StoreID = (short) appsetting.StoreID;
            st.StoreName = appsetting.StoreName;
            ArrayList item = new ArrayList();

            item.Add(st);
            DDLStore.DataSource = item;
            DDLStore.ValueMember = "StoreID";
            DDLStore.DisplayMember = "StoreName";

            DDLStore.Enabled = false;
            BtnSyncData.Focus();
          
            return 1;
        }

        protected void PrepareSyncData()
        {
            syncitems.LotsTransfered = 0;
            syncitems.ItemsTransfered = 0;
            syncitems.FPrepareTemporaryData(appsetting.StoreID);
        }

        protected void SyncMunits()
        {
          syncitems.FGetMunits();          
        }

        protected void SyncItems()
        {           
            long ItemInsRows = 0;                        
            if (syncitems.syncinfo.ItemsRowsCount > 0)
                ItemInsRows = syncitems.FGetItemsData(appsetting.StoreID);                                   
        }

        protected void SyncLots()
        {
            long LotInsRows=0;
           
            if (syncitems.syncinfo.LotRowsCount > 0)
                LotInsRows = syncitems.FGetLotsData(appsetting.StoreID);                            
        }
       
        protected void GetStores()
        {
            List<Store> erpstores = new List<Store>();

            try { erpstores = syncitems.StoresFromWebService(); }
            catch{}

            if (erpstores.Count > 0)
            {
                try
                {
                    Store erpstore = new Store();
                    erpstore.StoreID = 9999;
                    erpstore.StoreName = "ΟΛΟΙ ΟΙ ΑΠΟΘ. ΧΩΡΟΙ (ΠΡΟΣΟΧΗ ΣΤΗ ΧΡΗΣΗ ΤΟΥ)";

                    erpstores.Add(erpstore);
                }
                catch { }

             

                DDLStore.DataSource = null;
                DDLStore.Items.Clear();
             

                DDLStore.DataSource = erpstores;

                DDLStore.ValueMember = "StoreID";
                DDLStore.DisplayMember = "StoreName";
                
                if (appsetting.StoreID > 0)
                    DDLStore.SelectedValue = appsetting.StoreID;

                LBStoreID.Text = ">#" + DDLStore.SelectedValue;
                DDLStore.Enabled = true;
            }
        }

        protected void SetStore()
        {
            int fstoreid=0;
            string fstorename=null;

            try { fstoreid =int.Parse(DDLStore.SelectedValue.ToString()); }
            catch { }
            try 
            {
                fstorename = DDLStore.GetItemText(DDLStore.Items[DDLStore.SelectedIndex]);
                
            }
            catch { }

            if (fstoreid > 0)
            {
                appsetting.StoreID = fstoreid;
                appsetting.SetPermanentStoreID(fstoreid, fstorename);
                LBStoreID.Text = "#" + fstoreid.ToString();
            }
            BtnSetStore.Text = "( o )";
        }

        private void CkBmunits_CheckedChanged(object sender, EventArgs e)
        {
            SyncItemsSelection.syncMunits = CkBmunits.Checked;
        }

        private void CkBItems_CheckedChanged(object sender, EventArgs e)
        {
            SyncItemsSelection.syncItems = CkBItems.Checked;
        }

        private void CkBLots_CheckedChanged(object sender, EventArgs e)
        {
            SyncItemsSelection.syncLots = CkBLots.Checked;
        }

       

      
       

       

      
     
        
    
    }
}
