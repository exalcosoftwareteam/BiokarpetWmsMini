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
    public partial class FrmReceivesHeader : Form
    {
        InventoryHeaderHandler invhdrhandler = new InventoryHeaderHandler();
        ReceivesController rcontroller = new ReceivesController();
        long IFtrid=0;
        long ireceiveid = 0;

        public FrmReceivesHeader()
        {
            InitializeComponent();
            GetReceiveTasks();
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
            if (e.KeyCode == Keys.Enter)
            {
                Goentry();
            }
            else if (e.KeyCode == Keys.Escape) 
            { 
                GoBack();
            }
        }




        private void Goentry() 
        {
            try
            {
                ireceiveid = long.Parse(DGReceivesList[DGReceivesList.CurrentRowIndex,0].ToString());
                IFtrid = long.Parse(DGReceivesList[DGReceivesList.CurrentRowIndex, 1].ToString());

                WMSForms.FrmReceiveView = new FrmReceiveView(ireceiveid, IFtrid);
                WMSForms.FrmReceiveView.Show();
                this.Close();
            }
            catch (Exception ex)
            {
            
            
            }
        }
      


        private void DGInvHeaderList_GotFocus(object sender, EventArgs e)
        {
            DisableEnter();
            DisableNew();
            DisableView();           
        }

       


        private void DGInvHeaderList_CurrentCellChanged(object sender, EventArgs e)
        {
            ireceiveid = long.Parse(DGReceivesList[DGReceivesList.CurrentRowIndex, 0].ToString());
        }

        

        #endregion

        protected void GetReceiveTasks()
        {
            DataTable DT = new DataTable();
            try
            {
 
                DT = rcontroller.GetOpenReceives();

                if (DT.Rows.Count > 0)
                {
                    DGReceivesList.DataSource = DT;

                    DataGridTableStyle DGListStyle = new DataGridTableStyle();


                    DGListStyle.MappingName = "DTRECEIVES";
                    DGReceivesList.TableStyles.Clear();

                    DataGridTextBoxColumn col1 = new DataGridTextBoxColumn();
                    DataGridTextBoxColumn col2 = new DataGridTextBoxColumn();
                    DataGridTextBoxColumn col3 = new DataGridTextBoxColumn();
                    DataGridTextBoxColumn col4 = new DataGridTextBoxColumn();


                    col4.MappingName = "receiveid";
                    col4.HeaderText = "receiveid";
                    col4.Width = 0;
                    DGListStyle.GridColumnStyles.Add(col4);


                    col1.MappingName = "ftrid";
                    col1.HeaderText = "#";
                    col1.Width = 60;
                    DGListStyle.GridColumnStyles.Add(col1);

                    col2.MappingName = "SrcDocument";
                    col2.HeaderText = "Παραστατικό";
                    col2.Width = 80;
                    DGListStyle.GridColumnStyles.Add(col2);

                    col3.MappingName = "transdate";
                    col3.HeaderText = "Ημ/νία";
                    col3.Width = 70;
                    col3.Format = "dd/MM/yy";
                    DGListStyle.GridColumnStyles.Add(col3);

                    DGReceivesList.TableStyles.Add(DGListStyle);

                   // bool isselected;

                    DGReceivesList.CurrentRowIndex = 0;
                    DGReceivesList.Select(0);

                    
                }
            }
            catch(Exception ex) { }

            if (!(DT.Rows.Count > 0))
            {
                ShowMessageBox("Δεν έχουν οριστεί εργασίες παραλαβής. Προσθέστε μία...");
                return;
            }
            else
            {
                //SetFtrID();

            }
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

 

        protected void GoBack()
        {
            Cursor.Current = Cursors.WaitCursor;
            //WMSForms.FrmMain.Show();            
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
        

        private void PBBtnEnter_Click(object sender, EventArgs e)
        {
            Goentry();
        }


        private void BtnView_MouseDown(object sender, MouseEventArgs e)
        {           
            EnableView();
        }

        private void BtnView_GotFocus(object sender, EventArgs e)
        {           
            EnableView();
        }



        private void TBPackHeaderComments_KeyDown(object sender, KeyEventArgs e)
        {
            handleftrid();
        }

        private void btn_newtask_Click(object sender, EventArgs e)
        {
            if (tb_ftrid.Text.Length > 0)
            {
                handleftrid();
            }
        }

        private void tb_ftrid_TextChanged(object sender, EventArgs e)
        {
            HideMessageBox();
        }

        private void tb_ftrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  handleftrid();
        }



        private void handleftrid()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (rcontroller.GetTransCodewDetails(long.Parse(tb_ftrid.Text)) > 0)
            {

                tb_ftrid.Text = "";
                GetReceiveTasks();
                ShowMessageBox( AppGeneralSettings.successmsg);
             }
            else
            {
                LBMsgBox.Text = AppGeneralSettings.errortmsg;
                ShowMessageBox(AppGeneralSettings.errortmsg);
                tb_ftrid.SelectAll();
            }

            Cursor.Current = Cursors.Default;
        }

        private void FrmReceivesHeader_Load(object sender, EventArgs e)
        {
            FixResolutionIssues();

            tb_ftrid.Focus();
        }
 
 
 

   }

      

      
       

        
      

       

        

       
    }

        

