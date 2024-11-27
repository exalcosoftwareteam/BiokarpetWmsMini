using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WMSMobileClient;
using WMSMobileClient.Components;

namespace WMSMobileClient
{
    public partial class FrmPackingListHeader : Form
    {
        bool iDateEntered = false;
        bool iCommentsEntered = false;
        int dsrid;
        DB mydb = new DB();
        PackingHeader packlistheader = new PackingHeader();
        PackingListHeaderHandler packlisthandler = new PackingListHeaderHandler();

        public FrmPackingListHeader()
        {
            InitializeComponent();
            GetDate();
           // TBtranstype.Text = AppGeneralSettings.DSRID.ToString();
        }


        private void FrmInventoryHeader_Load(object sender, EventArgs e)
        {
            FixResolutionIssues();
            fillDsridCompobox();
        }

        private void PBSoftKeyb_Click(object sender, EventArgs e)
        {
            if (OnScreenKeyboard.Enabled)
                OnScreenKeyboard.Enabled = false;
            else
                OnScreenKeyboard.Enabled = true;
        }

        #region "Form Events"
    
        private void ΤΒΙnvDate_GotFocus(object sender, EventArgs e)
        {            
            PBoxPackDate.Image = Properties.Resources.textbox_small_focus;
            DisableEnter();
        }

        private void ΤΒΙnvDate_LostFocus(object sender, EventArgs e)
        {            
            PBoxPackDate.Image = Properties.Resources.textbox_small;
        }

        private void ΤΒΙnvDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckDate();
                TBPackHeaderComments.Focus();
            }
            if (e.KeyCode == Keys.Escape)
                GoBack();
        }

        private void ΤΒΙnvDate_TextChanged(object sender, EventArgs e)
        {
            iDateEntered = true;
            iCommentsEntered = false;
            HideMessageBox();
        }

        private void TBInvHeaderComments_GotFocus(object sender, EventArgs e)
        {            
           // PBoxPackHeaderComments.Image = Properties.Resources.textbox_focus;
            DisableEnter();
        }

        private void TBInvHeaderComments_LostFocus(object sender, EventArgs e)
        {            
           // PBoxPackHeaderComments.Image = Properties.Resources.textbox;
        }

        private void TBInvHeaderComments_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TBcustomercode.Focus();
                EnableEnter();
            }
            if (e.KeyCode == Keys.Escape)
                GoBack();
        }


        private void PBoxInvDate_Click(object sender, EventArgs e)
        {
            ΤΒPackListDate.Focus();
        }

        private void PBoxInvHeaderComments_Click(object sender, EventArgs e)
        {
            TBPackHeaderComments.Focus();
        }

        private void FrmInventoryHeader_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Escape))
                GoBack();
            
           
        }

        private void PBBtnBck_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }
      
        #endregion

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

                //PBoxPackHeaderComments.Width = Screen.PrimaryScreen.Bounds.Width - 10;
            }
            else
            {
               // PBoxPackHeaderComments.Width = 200;
              //  PBoxPackHeaderComments.Height = 35;
              //  PBoxPackHeaderComments.SizeMode = PictureBoxSizeMode.StretchImage;
                //              

                PBoxPackDate.Width = 100;
                PBoxPackDate.Height = 35;
                PBoxPackDate.SizeMode = PictureBoxSizeMode.StretchImage;

                PBMenuBar.Location = new Point(PBMenuBar.Location.X, Screen.PrimaryScreen.Bounds.Height - PBMenuBar.Height - 25);

                PBBtnBck.Location = new Point(PBBtnBck.Location.X, PBMenuBar.Location.Y + oldbtny);
                BtnSave.Location = new Point(BtnSave.Location.X, PBMenuBar.Location.Y + oldbtny);
            }
        }

        protected void GetDate()
        {
            ΤΒPackListDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        protected void EnableEnter()
        {
            if (BtnSave.Image != Properties.Resources.buttonsave_on) BtnSave.Image = Properties.Resources.buttonsave_on;
        }

        protected void DisableEnter()
        {
            if (BtnSave.Image != Properties.Resources.buttonsave) BtnSave.Image = Properties.Resources.buttonsave;
        }

        protected void CheckDate()
        {

        }

        protected void CheckComments()
        {

        }

        protected void ShowMessageBox(string msg)
        {
            PBoxMessage.Visible = true;
            LBMsgBox.Visible = true;
            if (!string.IsNullOrEmpty(msg)) LBMsgBox.Text = msg;
        }

        protected void HideMessageBox()
        {
            
            if (PBoxMessage.Visible) PBoxMessage.Visible = false;
            if (LBMsgBox.Visible) LBMsgBox.Visible = false;
        }

        protected void SaveChanges()
        {

            AppGeneralSettings.DSRID = dsrid;
            
            if (!(TBPackHeaderComments.Text.Length > 0))
                TBPackHeaderComments.Text = "...";
            try { packlistheader.Branchid = (short)AppGeneralSettings.BranchID; }
            catch { }
            try { packlistheader.Compid = (short)AppGeneralSettings.CompID; }
            catch { }
            try { packlistheader.StoreID = (short)AppGeneralSettings.StoreID; }
            catch { }
            try { packlistheader.Dsrid = AppGeneralSettings.DSRID; }
            catch { }
            try { packlistheader.PackingListDate = ΤΒPackListDate.Text; }
            catch { }

            // customer code and transtype from here
            try { packlistheader.CustomerCode = TBcustomercode.Text; }
            catch { }
            try { packlistheader.TransType = int.Parse(TBtranstype.Text); }
            catch { }

            //
            if (dsrid == 0) {
                MessageBox.Show("Παρακαλώ εισάγεται σειρά παραστατικού !");
                return;
            }


            try { packlistheader.Packinglistcomments = TBPackHeaderComments.Text; }
            catch { }

            long newpackhdrid = packlisthandler.UpdatePackingListHeader(packlistheader);
            if (newpackhdrid > 0)
            {
                Program.iPackHeader.PackingListHeaderID = newpackhdrid;
                Program.iPackHeader.Compid = packlistheader.Compid;
                Program.iPackHeader.Branchid = packlistheader.Branchid;
                Program.iPackHeader.StoreID = packlistheader.StoreID;
                Program.iPackHeader.CustomerCode = packlistheader.CustomerCode;
                Program.iPackHeader.TransType = packlistheader.TransType;
                Program.iPackHeader.Dsrid = packlistheader.Dsrid; 
                GoBack();
            }

            else
            {
                ShowMessageBox("Αποτυχία δημιουργίας νέας διακίνησης!");
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

        protected void GoBack()
        {
            FrmSelectPackingList FrmSelectPackingList = new FrmSelectPackingList();
            FrmSelectPackingList.Show();
            this.Close();
        }

        private void BtnSave_GotFocus(object sender, EventArgs e)
        {
            EnableEnter();
        }

        private void BtnSave_LostFocus(object sender, EventArgs e)
        {
            DisableEnter();
        }

        private void BtnSave_MouseDown(object sender, MouseEventArgs e)
        {
            EnableEnter();
        }


        private void fillDsridCompobox()
        {
            DataTable dtstores = new DataTable();
            dtstores = mydb.DBFillDataTable("SELECT * from TStores WHERE STOREID =" + AppGeneralSettings.StoreID.ToString(), "tstores");

            try
            {
                cb_dsrid.Items.Add(dtstores.Rows[0]["DSRIDTOCENTRAL"] + " ΠΡΟΣ ΚΕΝΤΡΙΚΟ");
                cb_dsrid.Items.Add(dtstores.Rows[0]["DSRIDTOBRANCH"] + " ΠΡΟΣ ΥΠΟΚ/ΜΑ");
            }
            catch 
            {
                MessageBox.Show("Παρακαλώ επιλέξτε υποκατάστημα στις ρυθμίσεις !!");
                FrmSettings FrmSettings = new FrmSettings();
                FrmSettings.Show();
                this.Close();
            }
                

                    
        }

        private void cb_dsrid_SelectedValueChanged(object sender, EventArgs e)
        {dsrid =int.Parse(cb_dsrid.SelectedItem.ToString().Substring(0, 4));}


        private void TBcustomercode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cb_dsrid.Focus();
            }
        }

        private void TBPackHeaderComments_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TBcustomercode.Focus();
            }

        }

        private void cb_dsrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
            BtnSave.Focus();
            }

        }







    }
}