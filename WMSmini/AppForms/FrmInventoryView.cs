using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WMSMobileClient.Components;

namespace WMSMobileClient
{
    public partial class FrmInventoryView : Form
    {
        InventoryHandler invhandler = new InventoryHandler();

        string isearchterm = null;
        bool iGetLastrecs = false;

        long iinvid=0;

        public FrmInventoryView()
        {            
            InitializeComponent();

            GetInventoryItemsList();

            this.Text += "-" + invhandler.InvHdrTitle(Program.iInvHeader.InvHdrID);
        }

        #region "FormEvents"

        private void FrmInventoryView_Load(object sender, EventArgs e)
        {
            FixResolutionIssues();
        }

        private void FrmInventoryView_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Escape))
                GoBack();
            else
                EnableBackKey(e);
        }

        private void DGInventorytemsList_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Escape))
                GoBack();            
        }


        private void DGInventorytemsList_CurrentCellChanged(object sender, EventArgs e)
        {

            try { DGInventorytemsList.Select(DGInventorytemsList.CurrentRowIndex); }
            catch { }
            if (LBMsgBox.Visible)
                HideMessageBox();
        }

        private void TBSearch_GotFocus(object sender, EventArgs e)
        {
            //TBSearch.BackColor = Color.Lavender;
            PBoxSearch.Image = Properties.Resources.textbox_focus;
        }

        private void TBSearch_LostFocus(object sender, EventArgs e)
        {
            //TBSearch.BackColor = Color.White;
            PBoxSearch.Image = Properties.Resources.textbox_xsmall;
        }

        private void CkBoxLastRecords_CheckStateChanged(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void TBSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                PerformSearch();
            else if (e.KeyCode == Keys.Escape)
                GoBack();
        }

        private void TBSearch_TextChanged(object sender, EventArgs e)
        {
            if (CkBoxLastRecords.Checked) CkBoxLastRecords.Checked = false;
        }

        #endregion

        protected void GetInventoryItemsList()
        {
            DataTable DT = new DataTable();
           
           
            long invid=0;

            iinvid = 0;

            if (!(Program.iInvHeader.InvHdrID > 0))          
                GoBack();                        

            
            Cursor.Current = Cursors.WaitCursor;

           
            try
            {
                DT = invhandler.InventoryView(Program.iInvHeader.InvHdrID,isearchterm,iGetLastrecs);

               
                if (DT.Rows.Count > 0)
                {
                    DGInventorytemsList.DataSource = DT;

                    DataGridTableStyle DGListStyle = new DataGridTableStyle();
                    DGInventorytemsList.TableStyles.Clear();

                    DataGridTextBoxColumn col1 = new DataGridTextBoxColumn();
                    DataGridTextBoxColumn col2 = new DataGridTextBoxColumn();
                    DataGridTextBoxColumn col3 = new DataGridTextBoxColumn();
                    DataGridTextBoxColumn col4 = new DataGridTextBoxColumn();
                    DataGridTextBoxColumn col5 = new DataGridTextBoxColumn();
                    DataGridTextBoxColumn col6 = new DataGridTextBoxColumn();

                    col1.MappingName = "InvID";
                    col1.HeaderText = "#";
                    col1.Width = 0;
                    DGListStyle.GridColumnStyles.Add(col1);
                   
                    
                    col6.MappingName = "InvNo";
                    col6.HeaderText = "#";
                    col6.Width = 30;
                    DGListStyle.GridColumnStyles.Add(col6);


                    col2.MappingName = "ItemCode";
                    col2.HeaderText = "Κωδ. Είδους";
                    col2.Width = 25;
                    DGListStyle.GridColumnStyles.Add(col2);

                    col3.MappingName = "LotCode";
                    col3.HeaderText = "Παρτίδα";
                    col3.Width = 90;
                    DGListStyle.GridColumnStyles.Add(col3);

                    col4.MappingName = "InvQtyPrimary";
                    col4.HeaderText = "K.MM";
                    col4.Width = 80;
                    DGListStyle.GridColumnStyles.Add(col4);

                    col5.MappingName = "InvQtySecondary";
                    col5.HeaderText = "Δ.ΜΜ";
                    col5.Width = 80;
                    DGListStyle.GridColumnStyles.Add(col5);

                    DGInventorytemsList.TableStyles.Add(DGListStyle);

                    DGInventorytemsList.Select(DGInventorytemsList.CurrentRowIndex);

                    invid = long.Parse(DGInventorytemsList[DGInventorytemsList.CurrentRowIndex, 0].ToString());
                    if (invid > 0) iinvid = invid;

                    BtnDelete.Visible = true;                   
                }
                else
                {
                    //ImgButtonDelete.Visible = false;
                    ShowMessageBox("Δεν υπάρχουν εγγραφές σε αυτή την απογραφή!");
                }
            }
            catch (Exception Ex) { }
           
               Cursor.Current = Cursors.Default;
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

        protected void GoBack()
        {
            WMSForms.FrmSelectInventoryHeader = new FrmSelectInventoryHeader();
            WMSForms.FrmSelectInventoryHeader.Show();
            this.Close();
        }

        protected void GoExport()
        {
            WMSForms.FrmExportInventory = new FrmExportInventory();
            WMSForms.FrmExportInventory.Show();
            this.Close();
        }

        protected void GoInventory()
        {
            try { iinvid = long.Parse(DGInventorytemsList[DGInventorytemsList.CurrentRowIndex, 0].ToString()); }
            catch { }
            WMSForms.FrmInventory = new FrmInventory(iinvid);
            WMSForms.FrmInventory.Show();
            this.Close();
        }
     
        protected void DeleteInventory()
        {
            if (MessageBox.Show("Είστε Βέβαιοι για την Διαγραφή της απογραφής;\n Όλες οι εγγραφές θα διαγραφούν!", "Ερώτηση", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                return;
            if (invhandler.DeleteWholeInventory(Program.iInvHeader.InvHdrID) > 0)
            {
                Program.iInvHeader.InvHdrID = 0;
                GoBack();
            }
        }       

        protected void PerformSearch()
        {
            isearchterm = TBSearch.Text.Trim() ;

            if (CkBoxLastRecords.Checked)
            {
                iGetLastrecs = true;
                isearchterm = null;
                TBSearch.Text = "";
            }
            else
                iGetLastrecs = false;

            GetInventoryItemsList();
        }

        private void PBBtnBck_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteInventory();
        }

        private void BtnSync_Click(object sender, EventArgs e)
        {
            GoExport();
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            GoInventory();
        }

        protected void FixResolutionIssues()
        {

            string oldpos = PBMenuBar.Location.X.ToString() + "X" + PBMenuBar.Location.Y.ToString();
            int oldbtny = PBBtnBck.Location.Y - PBMenuBar.Location.Y;

            if (Screen.PrimaryScreen.Bounds.Width > 240)
            {
                PBMenuBar.Location = new Point(PBMenuBar.Location.X, Screen.PrimaryScreen.Bounds.Height - PBMenuBar.Height - 30);

                PBBtnBck.Location = new Point(PBBtnBck.Location.X, PBMenuBar.Location.Y + oldbtny);
                BtnView.Location = new Point(BtnView.Location.X, PBMenuBar.Location.Y + oldbtny);
                BtnSync.Location = new Point(BtnSync.Location.X, PBMenuBar.Location.Y + oldbtny);
                BtnDelete.Location = new Point(BtnDelete.Location.X, PBMenuBar.Location.Y + oldbtny);
                //PBBtnEnter.Location = new Point(PBBtnEnter.Location.X, PBMenuBar.Location.Y + oldbtny);


                PBMenuBar.SizeMode = PictureBoxSizeMode.StretchImage;
                PBBtnBck.SizeMode = PictureBoxSizeMode.StretchImage;
                BtnView.SizeMode = PictureBoxSizeMode.StretchImage;
                BtnDelete.SizeMode = PictureBoxSizeMode.StretchImage;

                BtnSync.SizeMode = PictureBoxSizeMode.StretchImage;


                PBMenuBar.Width = Screen.PrimaryScreen.Bounds.Width;
            }
            else {

                PBMenuBar.Location = new Point(PBMenuBar.Location.X, Screen.PrimaryScreen.Bounds.Height - PBMenuBar.Height - 25);

                PBBtnBck.Location = new Point(PBBtnBck.Location.X, PBMenuBar.Location.Y + oldbtny);
                BtnView.Location = new Point(BtnView.Location.X, PBMenuBar.Location.Y + oldbtny);
                BtnSync.Location = new Point(BtnSync.Location.X, PBMenuBar.Location.Y + oldbtny);
                BtnDelete.Location = new Point(BtnDelete.Location.X, PBMenuBar.Location.Y + oldbtny);
            
            
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

        private void DGInventorytemsList_DoubleClick(object sender, EventArgs e)
        {
            GoInventory();
        }

        

      

      

     
      
       

            
    }
}