using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WMSMobileClient;
using WMSMobileClient.Components;
using WMSMobileClient.WMSSyncService;


namespace WMSMobileClient
{
    public partial class FrmExportPackingList : Form
    {
        public FrmExportPackingList()
        {
            InitializeComponent();
           
        }


        private void FrmExportInventory_Load(object sender, EventArgs e)
        {
            FixResolutionIssues();
            
        }

     
        #region Form Events
        private void PBBtnBck_Click(object sender, EventArgs e)
        {
            GoBack();
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

        private void FrmExportInventory_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Escape))
            {
                GoBack();
            }


        }

        private void BtnSyncData_Click(object sender, EventArgs e)
        {
            AppGeneralSettings.TransType = 0;
            AppGeneralSettings.CustomerCode = null;


           if (MessageBox.Show("Η διακίνηση θα αποσταλεί στο Atlantis ,θέλετε να συνεχίσετε;", "Ερώτηση", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {

                ExportData();

                }
               
                        

        }

        
        
        #endregion

    

        protected void CheckInvHdrID()
        {
            if (!(Program.iInvHeader.InvHdrID > 0))          
                GoBack();  
        }

        protected void ExportData()
        {
            long exprows = 0;
            long PackingListHeaderID=0;
            ExportData exportdata = new ExportData();
            PackingListHeaderHandler packheader = new PackingListHeaderHandler();

            BtnSyncData.Text = "Παρακαλώ περιμένετε...";
            Cursor.Current = Cursors.WaitCursor;

            if (packheader.CheckPackingListStatus(Program.iPackHeader.PackingListHeaderID) > 0) 
            {
                if (MessageBox.Show("Η διακίνηση έχει ήδη αποσταλεί , θέλετε να την ξαναστείλετε;", "Ερώτηση", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No) {
                    Cursor.Current = Cursors.Default;
                    return;
                                    
                }
         
            
            
            }
            Application.DoEvents();

            PackingListHeaderID = exportdata.ExportPackingListHeader(Program.iPackHeader.PackingListHeaderID);

            if (!(PackingListHeaderID > 0))
            {
                if (PackingListHeaderID == -10)
                    MessageBox.Show("Πρόβλημα Επικοινωνίας με την υπηρεσία!");
                else
                    MessageBox.Show("Πρόβλημα με την εξαγωγή Δεδομένων!");

                BtnSyncData.Text = "Εξαγωγή Διακίνησης";
                ImgExportInventory.Visible = false;
                LBExpInvRows.Visible = false;

                Cursor.Current = Cursors.Default;

                return;
            }


            exprows = ExportPackingList(Program.iPackHeader.PackingListHeaderID, PackingListHeaderID);

            ImgExportInventory.Visible = true;
            LBExpInvRows.Visible = true;
            LBExportInventory.Visible = true;

            if (exprows > 0)
            {
                ImgExportInventory.Image = Properties.Resources.ok;

                //LBExpInvRows.Text = "Εξαγωγή (" + exprows.ToString() + " ) εγγραφές";
                packheader.SetPackingListHeaderStatus(Program.iPackHeader.PackingListHeaderID,1);
                
            }
            else
            {
                ImgExportInventory.Image = Properties.Resources.error;
               // LBExpInvRows.Text = "Εξαγωγή ( 0 ) εγγραφές";
            }

            BtnSyncData.Text="Εξαγωγή Διακίνησης";

            Cursor.Current = Cursors.Default;
        }

        protected void FixResolutionIssues()
        {

            string oldpos = PBMenuBar.Location.X.ToString() + "X" + PBMenuBar.Location.Y.ToString();
            int oldbtny = PBBtnBck.Location.Y - PBMenuBar.Location.Y;

            PBoxTransType.Height = 35;
            PBoxTransType.SizeMode = PictureBoxSizeMode.StretchImage;

            PBoxCustomerCode.Height = 35;
            PBoxCustomerCode.SizeMode = PictureBoxSizeMode.StretchImage;


            if (Screen.PrimaryScreen.Bounds.Width > 240)
            {
                PBMenuBar.Location = new Point(PBMenuBar.Location.X, Screen.PrimaryScreen.Bounds.Height - PBMenuBar.Height - 30);

                PBBtnBck.Location = new Point(PBBtnBck.Location.X, PBMenuBar.Location.Y + oldbtny);


                PBMenuBar.SizeMode = PictureBoxSizeMode.StretchImage;
                PBBtnBck.SizeMode = PictureBoxSizeMode.StretchImage;

                PBMenuBar.Width = Screen.PrimaryScreen.Bounds.Width;


            }
            else {

                PBMenuBar.Location = new Point(PBMenuBar.Location.X, Screen.PrimaryScreen.Bounds.Height - PBMenuBar.Height - 25);

                PBBtnBck.Location = new Point(PBBtnBck.Location.X, PBMenuBar.Location.Y + oldbtny);
            
                  }
        }

        protected void GoBack()
        {
            WMSForms.FrmPackingListView = new FrmPackingListView();
            WMSForms.FrmPackingListView.Show();
            this.Close();
        }

        private void TBTransType_GotFocus(object sender, EventArgs e)
        {

            PBoxTransType.Image = Properties.Resources.textbox_focus;          
        }

        private void TBCustomerCode_GotFocus(object sender, EventArgs e)
        {
            PBoxCustomerCode.Image = Properties.Resources.textbox_focus;    
        }

        private void TBTransType_LostFocus(object sender, EventArgs e)
        {
            PBoxTransType.Image = Properties.Resources.textbox;
        }

        private void TBCustomerCode_LostFocus(object sender, EventArgs e)
        {
            PBoxCustomerCode.Image = Properties.Resources.textbox;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WMSForms.FrmCreateTradeCode = new FrmCreateTradeCode();
            WMSForms.FrmCreateTradeCode.Show();
            this.Close();
        }

        public long ExportPackingList(long PackingListHeaderID, long ServerPackingListHeaderID)
        {
            DB db = new DB();
            int i = 0;
            int step = 50;
            long raffected = 0;
            int dsi;
            List<PackingListDetail> Pdtl = new List<PackingListDetail>();
            List<PackingListDetail> ppartial = new List<PackingListDetail>();
            PackingListDetail p = new PackingListDetail();

            DataTable DT = new DataTable();

            string sqlstr = null;
            sqlstr = "SELECT LOTCODE as ITEMCODE,ItemQtyPrimary,PACKINGLISTHEADERID,ItemQtySecondary,Width,Length,Color,Draft,ItemID,LotID FROM TWMSPackingListDetails WHERE PackingListHeaderID=" + PackingListHeaderID.ToString();

            DT = db.DBFillDataTable(sqlstr, "DTMINMAX");

            if (DT == null) return -1;
            if (DT.Rows.Count == 0) return -1;

            for (i = 0; i < DT.Rows.Count; i++)
            {
                p = new PackingListDetail();

                p.PackingListHeaderID = ServerPackingListHeaderID;
                if (DT.Rows[i]["ItemID"] != DBNull.Value) p.ItemID = long.Parse(DT.Rows[i]["ItemID"].ToString());
                if (DT.Rows[i]["ItemCode"] != DBNull.Value) p.ItemCode = DT.Rows[i]["ItemCode"].ToString();
                if (DT.Rows[i]["LotID"] != DBNull.Value) p.LotID = long.Parse(DT.Rows[i]["LotID"].ToString());
                if (DT.Rows[i]["ItemQTYprimary"] != DBNull.Value) p.ItemQTYprimary = decimal.Parse(DT.Rows[i]["ItemQTYprimary"].ToString());
                if (DT.Rows[i]["ItemQTYsecondary"] != DBNull.Value) p.ItemQTYsecondary = decimal.Parse(DT.Rows[i]["ItemQTYsecondary"].ToString());
                if (DT.Rows[i]["Width"] != DBNull.Value) p.Width = decimal.Parse(DT.Rows[i]["Width"].ToString());
                if (DT.Rows[i]["Length"] != DBNull.Value) p.Length = decimal.Parse(DT.Rows[i]["Length"].ToString());
                if (DT.Rows[i]["Color"] != DBNull.Value) p.Color = DT.Rows[i]["Color"].ToString();
                if (DT.Rows[i]["Draft"] != DBNull.Value) p.Draft = DT.Rows[i]["Draft"].ToString();

                Pdtl.Add(p);

            }

            dsi = 0;
            LBExpInvRows.Visible = true;
            while (dsi < Pdtl.Count)
            {
                int count = Pdtl.Count - dsi > step ? step : Pdtl.Count - dsi;
                ppartial = Pdtl.GetRange(dsi, count);
                dsi += step;
                raffected += AppGeneralSettings.WebSyncServiceProvider.ImportPackingListCType(ppartial.ToArray());
                LBExpInvRows.Text = "Εισαγωγή " + raffected.ToString() + " από " + Pdtl.Count.ToString();
                Application.DoEvents();
            }


            return raffected;
        }

    }
}