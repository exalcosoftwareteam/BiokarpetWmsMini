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
    public partial class FrmInventoryHeader : Form
    {
        bool iDateEntered = false;
        bool iCommentsEntered = false;

        InventoryHeader invhdr = new InventoryHeader();
        InventoryHeaderHandler invhdrhandler = new InventoryHeaderHandler();

        public FrmInventoryHeader()
        {
            InitializeComponent();
            GetDate();
            ShowMessageBox("ΠΡΟΣΟΧΗ , πρέπει να είστε online για να δημιουργήσετε νέα απογραφή");
        }


        private void FrmInventoryHeader_Load(object sender, EventArgs e)
        {
            FixResolutionIssues();
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
            PBoxInvDate.Image = Properties.Resources.textbox_small_focus;
            DisableEnter();
        }

        private void ΤΒΙnvDate_LostFocus(object sender, EventArgs e)
        {            
            PBoxInvDate.Image = Properties.Resources.textbox_small;
        }

        private void ΤΒΙnvDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckDate();
                TBInvHeaderComments.Focus();
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
            PBoxInvHeaderComments.Image = Properties.Resources.textbox_focus;
            DisableEnter();
        }

        private void TBInvHeaderComments_LostFocus(object sender, EventArgs e)
        {            
            PBoxInvHeaderComments.Image = Properties.Resources.textbox;
        }

        private void TBInvHeaderComments_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckComments();

                BtnSave.Focus();
                EnableEnter();
            }
            if (e.KeyCode == Keys.Escape)
                GoBack();
        }

        private void TBInvHeaderComments_TextChanged(object sender, EventArgs e)
        {
            iCommentsEntered = true;
            iDateEntered = false;
            HideMessageBox();
        }

        private void PBoxInvDate_Click(object sender, EventArgs e)
        {
            ΤΒΙnvDate.Focus();
        }

        private void PBoxInvHeaderComments_Click(object sender, EventArgs e)
        {
            TBInvHeaderComments.Focus();
        }

        private void FrmInventoryHeader_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Escape))
                GoBack();
            else if (e.KeyCode == Keys.Enter)
            {
                EnableEnter();
                SaveChanges();
            }
            else
                EnableBackKey(e);
           
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

                PBoxInvHeaderComments.Width = Screen.PrimaryScreen.Bounds.Width - 10;
            }
            else
            {
                PBoxInvHeaderComments.Width = 200;
                PBoxInvHeaderComments.Height = 35;
                PBoxInvHeaderComments.SizeMode = PictureBoxSizeMode.StretchImage;
                //              

                PBoxInvDate.Width = 100;
                PBoxInvDate.Height = 35;
                PBoxInvDate.SizeMode = PictureBoxSizeMode.StretchImage;

                PBMenuBar.Location = new Point(PBMenuBar.Location.X, Screen.PrimaryScreen.Bounds.Height - PBMenuBar.Height - 25);

                PBBtnBck.Location = new Point(PBBtnBck.Location.X, PBMenuBar.Location.Y + oldbtny);
                BtnSave.Location = new Point(BtnSave.Location.X, PBMenuBar.Location.Y + oldbtny);
            }
        }

        protected void GetDate()
        {
            ΤΒΙnvDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
            long result;
            


            if (!(TBInvHeaderComments.Text.Length > 0))
                TBInvHeaderComments.Text = "...";
            try { invhdr.Branchid = (short)AppGeneralSettings.BranchID; }
            catch { }
            try { invhdr.CompID = (short)AppGeneralSettings.CompID; }
            catch { }
            try { invhdr.Storeid = (short)AppGeneralSettings.StoreID; }
            catch { }
            try { invhdr.InvDate = ΤΒΙnvDate.Text; }
            catch { }
            try { invhdr.InvComments = TBInvHeaderComments.Text; }
            catch { }


            Cursor.Current = Cursors.WaitCursor;
            ShowMessageBox("Παρακαλώ περιμένετε ... ");
            Application.DoEvents();
            if (cb_forcedelete.Checked)
            {
                if (!(MessageBox.Show("Έχετε επιλέξει να γίνει επανενημέρωση του καταλόγου Atlantis,είστε βέβαιοι ?", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
                {
                    HideMessageBox();
                    Cursor.Current = Cursors.Default;
                    return;
                }
               result= invhdrhandler.GetAtlantisCurrentInventory(true);
            }
            else
            {
               result= invhdrhandler.GetAtlantisCurrentInventory(false);
            }

            if (result < 0)
            {

                ShowMessageBox("Συνέβη κάποιο σφάλμα,Ελέγξτε την συνδεσιμότητα");
                Cursor.Current = Cursors.Default;
                return;

            }
            HideMessageBox();
            Cursor.Current = Cursors.Default;
 
            long newinvhdrid= invhdrhandler.UpdateInventoryHeader(invhdr) ;



            if (newinvhdrid > 0)
            {
                Program.iInvHeader.InvHdrID = newinvhdrid;
                Program.iInvHeader.CompID = invhdr.CompID;
                Program.iInvHeader.Branchid = invhdr.Branchid;
                Program.iInvHeader.Storeid = invhdr.Storeid;
                GoBack();
            }

            else
            {
                ShowMessageBox("Αποτυχία δημιουργίας νέας απογραφής!");
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
            FrmSelectInventoryHeader FrmSelectInventoryHeader = new FrmSelectInventoryHeader();
            FrmSelectInventoryHeader.Show();
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

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {

        }

       

        

       

                          

    }
}