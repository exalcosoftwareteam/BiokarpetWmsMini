using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WMSMobileClient.Components;

namespace WMSMobileClient
{
    public partial class FrmPackingListView : Form
    {
        PackingListDetailHandler packdtlid = new PackingListDetailHandler();

        string isearchterm = null;
        bool iGetLastrecs = false;

        long ipackdtlid = 0;

        public FrmPackingListView()
        {            
            InitializeComponent();

            GetPackingItemsList();
            this.Text += "-" + packdtlid.PackingListHeaderTitle(Program.iPackHeader.PackingListHeaderID);
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
            try { DGPackingItemsList.Select(DGPackingItemsList.CurrentRowIndex); }
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

        protected void GetPackingItemsList()
        {
            DataTable DT = new DataTable();
           
           
            long packinglistid=0;


            if (!(Program.iPackHeader.PackingListHeaderID > 0))          
                GoBack();                        

            
            Cursor.Current = Cursors.WaitCursor;

           
            try
            {
                DT = packdtlid.PackingListView(Program.iPackHeader.PackingListHeaderID, isearchterm, iGetLastrecs);

               
                if (DT.Rows.Count > 0)
                {
                    DGPackingItemsList.DataSource = DT;

                    DataGridTableStyle DGListStyle = new DataGridTableStyle();
                    DGPackingItemsList.TableStyles.Clear();

                    DataGridTextBoxColumn col1 = new DataGridTextBoxColumn();
                    DataGridTextBoxColumn col2 = new DataGridTextBoxColumn();
                    DataGridTextBoxColumn col3 = new DataGridTextBoxColumn();
                    DataGridTextBoxColumn col4 = new DataGridTextBoxColumn();
                    DataGridTextBoxColumn col5 = new DataGridTextBoxColumn();


                    col1.MappingName = "PackingListDTLID";
                    col1.HeaderText = "#";
                    col1.Width = 0;
                    DGListStyle.GridColumnStyles.Add(col1);

                    col2.MappingName = "ItemCode";
                    col2.HeaderText = "Κωδ. Είδους";
                    col2.Width = 100;
                    DGListStyle.GridColumnStyles.Add(col2);

                    col3.MappingName = "LOTCode";
                    col3.HeaderText = "Παρτίδα";
                    col3.Width = 70;
                    DGListStyle.GridColumnStyles.Add(col3);

                    col4.MappingName = "ItemQtyPrimary";
                    col4.HeaderText = "K.MM";
                    col4.Width = 70;
                    DGListStyle.GridColumnStyles.Add(col4);

                    col5.MappingName = "ItemQtySecondary";
                    col5.HeaderText = "Δ.ΜΜ";
                    col5.Width = 70;
                    DGListStyle.GridColumnStyles.Add(col5);

                    DGPackingItemsList.TableStyles.Add(DGListStyle);

                    DGPackingItemsList.Select(DGPackingItemsList.CurrentRowIndex);

                    packinglistid = long.Parse(DGPackingItemsList[DGPackingItemsList.CurrentRowIndex, 0].ToString());
                    if (packinglistid > 0) ipackdtlid = packinglistid;

                    BtnDelete.Visible = true;                   
                }
                else
                {
                    //ImgButtonDelete.Visible = false;
                    ShowMessageBox("Δεν υπάρχουν εγγραφές σε αυτήν την διακίνηση!");
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
            WMSForms.FrmSelectPackingList = new FrmSelectPackingList();
            WMSForms.FrmSelectPackingList.Show();
            this.Close();
        }

        protected void GoExport()
        {
            WMSForms.FrmExportPackingList = new FrmExportPackingList();
            WMSForms.FrmExportPackingList.Show();
            this.Close();
        }

        protected void GoPackingList()
        {
            try { ipackdtlid = long.Parse(DGPackingItemsList[DGPackingItemsList.CurrentRowIndex, 0].ToString()); }
            catch { }
            WMSForms.FrmPackingList = new FrmPackingList(ipackdtlid);
            WMSForms.FrmPackingList.Show();
            this.Close();
        }
     
        protected void DeletePackingList()
        {
            if (MessageBox.Show("Είστε Βέβαιοι για την Διαγραφή της διακίνησης;\n Όλες οι εγγραφές θα διαγραφούν!", "Ερώτηση", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                return;
            if ( packdtlid.DeleteWholePackingList(Program.iPackHeader.PackingListHeaderID) > 0)
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

            GetPackingItemsList();
        }

        private void PBBtnBck_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeletePackingList();
        }

        private void BtnSync_Click(object sender, EventArgs e)
        {
            GoExport();
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            GoPackingList();
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

        private void DGPackingItemsList_DoubleClick(object sender, EventArgs e)
        {
            GoPackingList();
        }



      

      

     
      
       

            
    }
}