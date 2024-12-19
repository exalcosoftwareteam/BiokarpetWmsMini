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
    public partial class FrmSelectInventoryHeaderOnline : Form
    {
        InventoryHeaderHandler invhdrhandler = new InventoryHeaderHandler();

        long iInvHdrID=0;

        public FrmSelectInventoryHeaderOnline()
        {
            InitializeComponent();
            GetInventoryTasks() ;
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
                GoInventory();
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
            SetInvHeaderID();
        }

        private void DGInvHeaderList_CurrentCellChanged(object sender, EventArgs e)
        {
            try { DGInvHeaderList.Select(DGInvHeaderList.CurrentRowIndex);}
            catch { }
 
               SetInvHeaderID();
        }

        

        #endregion

        protected void GetInventoryTasks()
        {
            DataTable DT = new DataTable();
            try
            {
                 DT = invhdrhandler.InventoryListsOnline();

                if (DT.Rows.Count > 0)
                {


                    DGInvHeaderList.DataSource = DT;

                    DataGridTableStyle DGListStyle = new DataGridTableStyle();
                    DGListStyle.MappingName = "TINV";

                    DataGridTextBoxColumn col1 = new DataGridTextBoxColumn();
                    DataGridTextBoxColumn col2 = new DataGridTextBoxColumn();
                    DataGridTextBoxColumn col3 = new DataGridTextBoxColumn();
                    DataGridTextBoxColumn col4 = new DataGridTextBoxColumn();


                    col1.MappingName = "InvHdrID";
                    col1.HeaderText = "#";
                    col1.Width = 23;
                    DGListStyle.GridColumnStyles.Add(col1);

                    col2.MappingName = "InvDate";
                    col2.HeaderText = "Ημ/νία";
                    col2.Format = "dd/MM/yy";
                    col2.Width = 60;
                    DGListStyle.GridColumnStyles.Add(col2);

                    col3.MappingName = "InvComments";
                    col3.HeaderText = "Παρατηρήσεις";
                    col3.Width = 60;
                    DGListStyle.GridColumnStyles.Add(col3);

                    col4.MappingName = "invRecords";
                    col4.HeaderText = "Πλήθος";
                    col4.Width = 50;
                    DGListStyle.GridColumnStyles.Add(col4);

                    


                    DGInvHeaderList.TableStyles.Clear();
                    
                    DGInvHeaderList.TableStyles.Add(DGListStyle);

                    int cnt = 0;

                    if (Program.iInvHeader.InvHdrID > 0)
                    {

                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            DGInvHeaderList.UnSelect(i);
                            try
                            {
                                if (Program.iInvHeader.InvHdrID == long.Parse(DT.Rows[i]["InvHdrID"].ToString()))
                                {
                                    DGInvHeaderList.Select(i);
                                    DGInvHeaderList.CurrentRowIndex = i;
                                    iInvHdrID = long.Parse(DT.Rows[i]["InvHdrID"].ToString());
                                }
                            }
                            catch { }
                       }

                    }
                    else {
                        DGInvHeaderList.CurrentRowIndex = 0;
                        DGInvHeaderList.Select(0);
                    }

                    
                }
            }
            catch { }

            if (!(DT.Rows.Count > 0))
            {
                ShowMessageBox("Δεν έχουν οριστεί εργασίες απογραφής. Προσθέστε μία...");
                return;
            }
            else
                SetInvHeaderID();

         
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
            if (BtnNewInventory.Image != Properties.Resources.add_on) BtnNewInventory.Image = Properties.Resources.add_on;
        }

        protected void DisableNew()
        {
            if (BtnNewInventory.Image != Properties.Resources.add) BtnNewInventory.Image = Properties.Resources.add;
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

        protected void SetInvHeaderID()
        {
            int rcnt = 0;
            iInvHdrID = 0;
            try { rcnt = ((DataTable)DGInvHeaderList.DataSource).Rows.Count; }
            catch { }
            if (rcnt > 0)
            {
                if (!(DGInvHeaderList.CurrentRowIndex > 0))                                    
                    DGInvHeaderList.CurrentRowIndex = 0;

                iInvHdrID = long.Parse(DGInvHeaderList[DGInvHeaderList.CurrentRowIndex, 0].ToString());
            }

            if (iInvHdrID > 0)
            {
                Program.iInvHeader.InvHdrID = iInvHdrID;               
            }
        }

        protected void GoBack()
        {
            Cursor.Current = Cursors.WaitCursor;
            //WMSForms.FrmMain.Show();            
            this.Close();
            Cursor.Current = Cursors.Default;
        }

        protected void NewInventory()
        {
            if (MessageBox.Show("Νέα Απογραφή;", "Επιβεβαίωαση", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                WMSForms.FrmInventoryHeader = new FrmInventoryHeader();
                WMSForms.FrmInventoryHeader.Show();
                this.Close();

            }

        }

        protected void ViewInventory()
        {
            Cursor.Current = Cursors.WaitCursor;

            if (!(Program.iInvHeader.InvHdrID > 0))
            {
                ShowMessageBox("Επιλέξτε πρώτα εργασία απογραφής από τη λίστα...");
                return;
            }
            else
            {
                WMSForms.FrmInventoryViewOnline = new FrmInventoryViewOnline();
                WMSForms.FrmInventoryViewOnline.Show();
                this.Close();
            }

            Cursor.Current = Cursors.Default;
        }

        protected void GoInventory()
        {
            Cursor.Current = Cursors.WaitCursor;

            if (!(Program.iInvHeader.InvHdrID > 0))
            {
                ShowMessageBox("Επιλέξτε πρώτα εργασία απογραφής από τη λίστα...");
                return;
            }
            else
            {
                if (AppGeneralSettings.OnlineMode)
                {
                    WMSForms.FrmInventoryOnline = new FrmInventoryOnline(0);
                    WMSForms.FrmInventoryOnline.Show();
                }
                else {
                    WMSForms.FrmInventory = new FrmInventory(0);
                    WMSForms.FrmInventory.Show();            
                }



                this.Close();
            }

            Cursor.Current = Cursors.Default;
        }
               
        protected void GoSync()
        {
            Cursor.Current = Cursors.WaitCursor;

            WMSForms.FrmOfflineSettings = new FrmOfflineSettings();
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

                BtnNewInventory.Location = new Point(BtnNewInventory.Location.X, PBMenuBar.Location.Y + oldbtny);
                PBBtnEnter.Location = new Point(PBBtnEnter.Location.X, PBMenuBar.Location.Y + oldbtny);


                PBMenuBar.SizeMode = PictureBoxSizeMode.StretchImage;
                PBBtnBck.SizeMode = PictureBoxSizeMode.StretchImage;
                BtnView.SizeMode = PictureBoxSizeMode.StretchImage;
                BtnNewInventory.SizeMode = PictureBoxSizeMode.StretchImage;
                PBBtnEnter.SizeMode = PictureBoxSizeMode.StretchImage;

                PBMenuBar.Width = Screen.PrimaryScreen.Bounds.Width;
            }
            else 
            {

                PBMenuBar.Location = new Point(PBMenuBar.Location.X, Screen.PrimaryScreen.Bounds.Height - PBMenuBar.Height - 25);

                PBBtnBck.Location = new Point(PBBtnBck.Location.X, PBMenuBar.Location.Y + oldbtny);
                BtnView.Location = new Point(BtnView.Location.X, PBMenuBar.Location.Y + oldbtny);

                BtnNewInventory.Location = new Point(BtnNewInventory.Location.X, PBMenuBar.Location.Y + oldbtny);
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
            NewInventory();
        }

        private void PBBtnEnter_Click(object sender, EventArgs e)
        {
            SetInvHeaderID();
            GoInventory();
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            EnableView();

            ViewInventory();
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

        private void button1_Click(object sender, EventArgs e)
        {
     
        } 
        


   }

      

      
       

        
      

       

        

       
    }

        

