using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WMSMobileClient.Components;

namespace WMSMobileClient
{
    public partial class FrmReceiveView : Form{
 
       long ireceiveid;
        long iftrid;
        DataTable DT;
        public WMSservice.TransCodeDetail lot; 
        public FrmReceiveView()

        {
            InitializeComponent();

            GetReceivesItemList();

     //       this.Text += "-" + invhandler.InvHdrTitle(Program.iInvHeader.InvHdrID);
        }

        public FrmReceiveView(long thisreceiveid,long thisftrid)
        {
            ireceiveid = thisreceiveid;
            iftrid = thisftrid;
            InitializeComponent();

            GetReceivesItemList();
        }


        #region "FormEvents"

        private void FrmInventoryView_Load(object sender, EventArgs e)
        {
            FixResolutionIssues();
            tb_lotcode.Focus();
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

        #endregion


        private void DrawDG(DataTable DT) 
        {
                if (DT.Rows.Count > 0)
                {

                    DGInventorytemsList.DataSource = DT;

                    DataGridTableStyle DGListStyle = new DataGridTableStyle();
                    DGListStyle.MappingName = "DTREMAINS";
                    DGInventorytemsList.TableStyles.Clear();




                    DataGridTextBoxColumn col1 = new DataGridTextBoxColumn();
                    DataGridTextBoxColumn col2 = new DataGridTextBoxColumn();
                    DataGridTextBoxColumn col3 = new DataGridTextBoxColumn();

                    col1.MappingName = "LotCode";
                    col1.HeaderText = "Παρτίδα";
                    col1.Width = 110;
                    DGListStyle.GridColumnStyles.Add(col1);


                    col2.MappingName = "qty";
                    col2.HeaderText = "QTY";
                    col2.Width = 30;
                    DGListStyle.GridColumnStyles.Add(col2);


                    col3.MappingName = "scannedqty";
                    col3.HeaderText = "Scan";
                    col3.Width = 40;
                    DGListStyle.GridColumnStyles.Add(col3);



                    DGInventorytemsList.TableStyles.Add(DGListStyle);

                    DGInventorytemsList.Select(DGInventorytemsList.CurrentRowIndex);

                    BtnDelete.Visible = true;
                }
                else
                {
                    //ImgButtonDelete.Visible = false;
                    ShowMessageBox("Δεν υπάρχουν εγγραφές !");
                }

        
        }

        protected void GetReceivesItemList()
        {
            DT = new DataTable();
           
            Cursor.Current = Cursors.WaitCursor;
            try { DrawDG(AppGeneralSettings.receives.GetTransRemains(iftrid)); }
            catch { }
            
           
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
            WMSForms.FrmReceivesHeader = new FrmReceivesHeader();
            WMSForms.FrmReceivesHeader.Show();
            this.Close();
        }

  

        private void PBBtnBck_Click(object sender, EventArgs e)
        {
            GoBack();
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


        private void tb_lotcode_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter)
            {
                HideMessageBox();
                HandleTBLot();


            }
               
        
        }

        private void HandleTBLot() 
        {
           // tb_qty.Focus();

            try
            {
                lot = AppGeneralSettings.receives.GetLotDetails(tb_lotcode.Text);

                //GET LOT GET ITEM FROM ERP/////////////////////////////////
                if (lot.LotID == 0 ) 
                {
                //GET LOT GET ITEM FROM ERP
                 WMSSyncService.SyncLot newlot =  AppGeneralSettings.WebSyncServiceProvider.SOA_GetNewLotbyCode(tb_lotcode.Text);
                 WMSSyncService.SyncERPItem newitem = AppGeneralSettings.WebSyncServiceProvider.SOA_GetNewItem(newlot.ItemID);
                lot.LotID = newlot.LotID;
                //lot.lotcode = tb_lotcode.Text;
                lot.lotcode = tb_lotcode.Text.Length > 10 ? tb_lotcode.Text :  newlot.LotCode;
                    
                lot.itemcode = newitem.ItemCode;
                lot.itemdesc = newitem.ItemDesc;
                lot.ItemID = newlot.ItemID;
                lot.Zlength = newlot.Length;
                lot.Zwidth = newlot.Width;
                lot.zcolor = newlot.Color;
                lot.zdraft = newlot.Draft;
                lot.munitprimary = newitem.MUnitPrimary;
                lot.munitsecondary = newitem.MUnitSecondary;
                    /////////////////////////////////////////////////
                
                }
            }
            catch { }
            lot.FtrID = iftrid;
            lot.ReceiveID = ireceiveid;
            lb_itemcode.Text = lot.itemcode;
            lb_itemdesc.Text = lot.itemdesc;
            lb_widthlength.Text = "Μ Χ Π =" + lot.Zlength.ToString() + " X " + lot.Zwidth.ToString();
            lb_draft.Text = "Σ:" + lot.zdraft;
            lb_color.Text = "X:" + lot.zcolor;
            tb_qty.Text = "1";
            tb_qty.SelectAll();
            tb_qty.Focus();
        
        }


        private void ClearForm()
        {
            tb_lotcode.Text = "";
            tb_qty.Text = "";
            lb_barcode.Text = "";
            lb_widthlength.Text = "";
            lb_color.Text = "";
            lb_draft.Text = "";
            lb_itemdesc.Text = "";
            lb_itemcode.Text = "";
            lot = new WMSservice.TransCodeDetail();
            tb_lotcode.Focus();
        }

        private void tb_qty_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lot.ItemQtyPrimary = int.Parse(tb_qty.Text);

                try 
                {
                    DT = AppGeneralSettings.receives.InsertNewReceiveDTL(lot);
                    if (DT.Rows.Count == 0)
                    {
                        GoBack();
                        return;
                    }
                    DrawDG(DT);
                    ShowMessageBox("Επιτυχής καταχώρηση");
                
                }
                catch(Exception ex) { ShowMessageBox("Συνέβη σφάλμα!"); }

                ClearForm();

            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                HandleTBLot();


            }
        }

       
      //      PBoxPackDate.Image = Properties.Resources.textbox_small;


        private void tb_lotcode_GotFocus(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.textbox_small_focus;
        }

        private void tb_lotcode_LostFocus(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.textbox_small;

        }

        private void tb_qty_GotFocus(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.textbox_small_focus;
        }

        private void tb_qty_LostFocus(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.textbox_small;
        }
 

 
        }
 
}