using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using WMSMobileClient;
using WMSMobileClient.Components;

namespace WMSMobileClient
{
    public partial class FrmSelectPackingList : Form
    {
        PackingListHeaderHandler packhdrhandler = new PackingListHeaderHandler();

        long iPackHdrID=0;


        public FrmSelectPackingList()
        {
            InitializeComponent();
            GetPackingLists();
        }

        #region "FormEvents"
           


        private void PBoxMessage_Click(object sender, EventArgs e)
        {
            HideMessageBox();
        }

        

        private void PBoxEnter_MouseMove()
        {
            EnableEnter();
        }

        private void DGInvHeaderList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                GoBack();
        }

        

       

      
        private void FrmSelectInventoryHeader_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode ==Keys.Escape))
            {
                GoBack();
            }
            else if ((e.KeyCode == Keys.Enter))
            {
                GoPackingList();
            }
          

        }

        private void DGInvHeaderList_GotFocus(object sender, EventArgs e)
        {
            DisableEnter();
            DisableNew();
            DisableView();           
        }

        

        private void DGInvHeaderList_LostFocus(object sender, EventArgs e)
        {
            SetPackHeaderID();
        }

        private void DGInvHeaderList_CurrentCellChanged(object sender, EventArgs e)
        {

            try { DGPackHeaderList.Select(DGPackHeaderList.CurrentRowIndex); }
            catch { }
           SetPackHeaderID();
        }

        

        #endregion

        protected void GetPackingLists()
        {
            DataTable DT = new DataTable();
            try
            {
                DT = packhdrhandler.PackingLists();

                if (DT.Rows.Count > 0)
                {
                    DGPackHeaderList.DataSource = DT;

                    DataGridTableStyle DGListStyle = new DataGridTableStyle();
                    DGPackHeaderList.TableStyles.Clear();

                    DataGridTextBoxColumn col1 = new DataGridTextBoxColumn();
                    DataGridTextBoxColumn col2 = new DataGridTextBoxColumn();
                    DataGridTextBoxColumn col3 = new DataGridTextBoxColumn();
                    DataGridTextBoxColumn col4 = new DataGridTextBoxColumn();
                    DataGridTextBoxColumn col5 = new DataGridTextBoxColumn();


                    col1.MappingName = "PackingListHeaderID";
                    col1.HeaderText = "#";
                    col1.Width = 0;
                    DGListStyle.GridColumnStyles.Add(col1);

                    col2.MappingName = "PackingListDate";
                    col2.HeaderText = "Ημ/νία";
                    col2.Format = "dd/MM/yy";
                    col2.Width = 55;
                    DGListStyle.GridColumnStyles.Add(col2);

                    col3.MappingName = "CustomerCode";
                    col3.HeaderText = "Πελάτης";
                    col3.Width = 50;
                    DGListStyle.GridColumnStyles.Add(col3);

                    col4.MappingName = "DSRID";
                    col4.HeaderText = "Κίνηση";
                    col4.Width = 50;
                    DGListStyle.GridColumnStyles.Add(col4);

                    
                    col5.MappingName = "PackingListComments";
                    col5.HeaderText = "Παρατηρήσεις";
                    col5.Width = 120;
                    DGListStyle.GridColumnStyles.Add(col5);





                    DGPackHeaderList.TableStyles.Add(DGListStyle);

                    if (Program.iPackHeader.PackingListHeaderID > 0)
                    {

                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            DGPackHeaderList.UnSelect(i);
                            try
                            {
                                if (Program.iPackHeader.PackingListHeaderID == long.Parse(DT.Rows[i]["PackingListHeaderID"].ToString()))
                                {

                                    DGPackHeaderList.Select(i);
                                    DGPackHeaderList.CurrentRowIndex = i;
                                    iPackHdrID = long.Parse(DT.Rows[i]["PackingListHeaderID"].ToString());
                                }

                            }

                            catch { }
                        }

                    }
                    else {
                        DGPackHeaderList.CurrentRowIndex = 0;
                        DGPackHeaderList.Select(0);

                            }
                }
            }
            catch { }

            if (!(DT.Rows.Count > 0))
            {
                ShowMessageBox("Δεν υπάρχουν εκκρεμείς Διακινήσεις. Προσθέστε μία...");
                return;
            }
            else
                SetPackHeaderID();
        
        }


        protected void EnableEnter()
        {
            if (PBBtnEnter.Image != Properties.Resources.enterselected_on) PBBtnEnter.Image = Properties.Resources.enterselected_on;
        }

        protected void DisableEnter()
        {
            if (PBBtnEnter.Image != Properties.Resources.enterselected) PBBtnEnter.Image = Properties.Resources.enterselected;
        }

        protected void EnableNew()
        {
            if (BtnNewPackHeader.Image != Properties.Resources.add_on) BtnNewPackHeader.Image = Properties.Resources.add_on;
        }

        protected void DisableNew()
        {
            if (BtnNewPackHeader.Image != Properties.Resources.add) BtnNewPackHeader.Image = Properties.Resources.add;
        }

        protected void EnableView()
        {
            if (BtnView.Image != Properties.Resources.view_on) BtnView.Image = Properties.Resources.view_on;
        }

        protected void DisableView()
        {
            if (BtnView.Image != Properties.Resources.view) BtnView.Image = Properties.Resources.view;
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

        protected void SetPackHeaderID()
        {
            int rcnt = 0;
            iPackHdrID = 0;
            try { rcnt = ((DataTable)DGPackHeaderList.DataSource).Rows.Count; }
            catch { }
            if (rcnt > 0)
            {
                if (!(DGPackHeaderList.CurrentRowIndex > 0))                                    
                    DGPackHeaderList.CurrentRowIndex = 0;

                iPackHdrID = long.Parse(DGPackHeaderList[DGPackHeaderList.CurrentRowIndex, 0].ToString());
            }

            if (iPackHdrID > 0)
            {
                Program.iPackHeader.PackingListHeaderID = iPackHdrID;               
            }
        }

        protected void GoBack()
        {
            Cursor.Current = Cursors.WaitCursor;
            //WMSForms.FrmMain.Show();            
            this.Close();
            Cursor.Current = Cursors.Default;
        }

        protected void NewPackingListHeader()
        {
            if (MessageBox.Show("Νέα Διακίνηση;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                WMSForms.FrmPackingListHeader = new FrmPackingListHeader();
                WMSForms.FrmPackingListHeader.Show();
                this.Close();

            }

        }

        protected void ViewPackingList()
        {
            Cursor.Current = Cursors.WaitCursor;

            if (!(Program.iPackHeader.PackingListHeaderID > 0))
            {
                ShowMessageBox("Επιλέξτε πρώτα Διακίνηση από τη λίστα...");
                return;
            }
            else
            {
                WMSForms.FrmPackingListView = new FrmPackingListView();
                WMSForms.FrmPackingListView.Show();
                this.Close();
            }

            Cursor.Current = Cursors.Default;
        }

        protected void GoPackingList()
        {
            Cursor.Current = Cursors.WaitCursor;

            if (!(Program.iPackHeader.PackingListHeaderID > 0))
            {
                ShowMessageBox("Επιλέξτε πρώτα διακίνηση από τη λίστα...");
                return;
            }
            else
            {
                WMSForms.FrmPackingList = new FrmPackingList();
                WMSForms.FrmPackingList.Show();
                this.Close();
            }

            Cursor.Current = Cursors.Default;
        }
               
        protected void GoSync()
        {
            Cursor.Current = Cursors.WaitCursor;

            WMSForms.FrmOfflineSettings = new FrmOfflineSettings(1);
            WMSForms.FrmOfflineSettings.Show();
            this.Close();

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
                BtnView.Location = new Point(BtnView.Location.X, PBMenuBar.Location.Y + oldbtny);

                BtnNewPackHeader.Location = new Point(BtnNewPackHeader.Location.X, PBMenuBar.Location.Y + oldbtny);
                PBBtnEnter.Location = new Point(PBBtnEnter.Location.X, PBMenuBar.Location.Y + oldbtny);


                PBMenuBar.SizeMode = PictureBoxSizeMode.StretchImage;
                PBBtnBck.SizeMode = PictureBoxSizeMode.StretchImage;
                BtnView.SizeMode = PictureBoxSizeMode.StretchImage;
                BtnNewPackHeader.SizeMode = PictureBoxSizeMode.StretchImage;
                PBBtnEnter.SizeMode = PictureBoxSizeMode.StretchImage;

                PBMenuBar.Width = Screen.PrimaryScreen.Bounds.Width;
            }
            else 
            {

                PBMenuBar.Location = new Point(PBMenuBar.Location.X, Screen.PrimaryScreen.Bounds.Height - PBMenuBar.Height - 25);

                PBBtnBck.Location = new Point(PBBtnBck.Location.X, PBMenuBar.Location.Y + oldbtny);
                BtnView.Location = new Point(BtnView.Location.X, PBMenuBar.Location.Y + oldbtny);

                BtnNewPackHeader.Location = new Point(BtnNewPackHeader.Location.X, PBMenuBar.Location.Y + oldbtny);
                PBBtnEnter.Location = new Point(PBBtnEnter.Location.X, PBMenuBar.Location.Y + oldbtny);
            
            
            
            }
        }

        private void BtnSync_Click(object sender, EventArgs e)
        {

        }

        private void PBBtnBck_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private void PBBtnBck_MouseDown(object sender, MouseEventArgs e)
        {
            PBBtnBck.Image = Properties.Resources.buttonback_on;
        }
        
        private void BtnNewInventory_Click(object sender, EventArgs e)
        {
            NewPackingListHeader();
        }

        private void PBBtnEnter_Click(object sender, EventArgs e)
        {
            SetPackHeaderID();
            GoPackingList();
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            EnableView();

            ViewPackingList();
        }

        private void BtnView_MouseDown(object sender, MouseEventArgs e)
        {           
            EnableView();
        }

        private void BtnView_GotFocus(object sender, EventArgs e)
        {           
            EnableView();
        }

        private void BtnNewInventory_GotFocus(object sender, EventArgs e)
        {
            EnableNew();
        }

        private void BtnNewInventory_MouseDown(object sender, MouseEventArgs e)
        {
            EnableNew();
        }

        private void PBSync_Click(object sender, EventArgs e)
        {
            GoSync();
        }

        private void FrmSelectInventoryHeader_Load(object sender, EventArgs e)
        {
            FixResolutionIssues();
            
        } 
        

        private void PBoxEnter_Click(object sender, EventArgs e)
        {
        
        }


   }

      

      
       

        
      

       

        

       
    }

        

