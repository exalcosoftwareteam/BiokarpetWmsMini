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
    public partial class FrmInventoryOnline : Form
    {

        bool iLotCodeEntered = false;
        bool iItemCodeEntered = false;
        bool iQtyEntered = false;
        bool iLotChecked = false;
        short caller = 0;
        Item item = new Item();
        Lot lot = new Lot();

        MInventory inv = new MInventory();
        LotHandler lothandler = new LotHandler();
        InventoryHandler InvHandler = new InventoryHandler();
        ItemHandler itemhandler = new ItemHandler();

        MUnits InvMunitPrimary = new MUnits();
        MUnits InvMunitSecondary = new MUnits();


        public FrmInventoryOnline(long parminvid)
        {
            InitializeComponent();
            inv.InvNo = int.Parse(InvHandler.GetInventoryCounter(Program.iInvHeader.InvHdrID)) + 1;
            lbcounter.Text = "#" + (inv.InvNo - 1).ToString();

            if (parminvid > 0)
            {
                GetInventoryRecord(parminvid);
                caller = 1;
            }
        }

        private void FrmInventory_Load(object sender, EventArgs e)
        {
            
             FixResolutionIssues();

            EnableUseLot();

            CheckInventoryHeaderID();
            GetInvMunitSettings();
            
        }
             
        #region Form Events"

        private void FrmInventory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                GoBack();
                return;
            }
            else if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                if (iQtyEntered || BtnSave.Focus())
                    SaveChanges();
            }
            else
                EnableBackKey(e);       

        }

        private void TBLotCode_GotFocus(object sender, EventArgs e)
        {            
            PBoxLotCode.Image = Properties.Resources.textbox_focus;          
        }

        private void TBLotCode_LostFocus(object sender, EventArgs e)
        {            
            PBoxLotCode.Image = Properties.Resources.textbox;

            if (TBLotCode.Text.Length > 0) iLotCodeEntered = true;
        }
       
        private void TBItemCode_GotFocus(object sender, EventArgs e)
        {            
            PBoxITemCode.Image = Properties.Resources.textbox_focus;                       
         
        }

        private void TBItemCode_LostFocus(object sender, EventArgs e)
        {           
            PBoxITemCode.Image = Properties.Resources.textbox;
            if (TBLotCode.Text.Length > 0) iItemCodeEntered = true;
        }

        private void TBQty_GotFocus(object sender, EventArgs e)
        {            
            PBoxQty.Image = Properties.Resources.textbox_small_focus;
            CheckQty();
            CheckItemCode();
        }

        private void TBQty_LostFocus(object sender, EventArgs e)
        {            
            PBoxQty.Image = Properties.Resources.textbox_small;
            CheckQty();            
        }

        private void PBoxLotCode_Click(object sender, EventArgs e)
        {
            TBLotCode.Focus();
        }

        private void PBoxITemCode_Click(object sender, EventArgs e)
        {
            TBItemCode.Focus();
        }

        private void PBoxQty_Click(object sender, EventArgs e)
        {
            TBQty.Focus();

        }

                 
        private void TBQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (TBQty.Text.Length > 6)
                {
                    TBQty.Text = "";
                    return;

                }

                if (CheckQty())
                {                                        
                    BtnSaveFake.Enabled = true;
                    BtnSaveFake.Focus();
                    iQtyEntered = true;
                }
                SaveChanges();
                //BtnDelete.Focus();
            }
            if (e.KeyCode == Keys.Escape)
                GoBack();
        }

        private void TBLotCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!iLotChecked)
                {
                    CheckLotCode();
                }
            }else if (e.KeyCode == Keys.Escape)
                GoBack();
        }

        private void TBLotCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (!iLotChecked)
                {
                    CheckLotCode();
                }
            }
            else if (e.KeyChar == (char)27) GoBack();
                 
 

        }

        private void TBItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (CheckItemCode())
                    TBQty.Focus();
                else
                    TBItemCode.Focus();
            }
            if (e.KeyCode == Keys.Escape)
                GoBack();
        }

       

        private void PBoxMessage_Click(object sender, EventArgs e)
        {
            HideMessageBox();
        }


        private void TBLotCode_TextChanged(object sender, EventArgs e)
        {
            iLotCodeEntered = true;
            iQtyEntered = false;
            iItemCodeEntered = false;
            iLotChecked = false;
            HideMessageBox();
            
        }

        private void TBItemCode_TextChanged(object sender, EventArgs e)
        {
            iItemCodeEntered = true;
            iLotCodeEntered = false;
            iQtyEntered = false;
            HideMessageBox();
        }

        private void TBQty_TextChanged(object sender, EventArgs e)
        {
            iQtyEntered = true;
            iItemCodeEntered = false;
            iLotCodeEntered = false;
            HideMessageBox();
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

                BtnDelete.Location = new Point(BtnDelete.Location.X, PBMenuBar.Location.Y + oldbtny);

                PBMenuBar.SizeMode = PictureBoxSizeMode.StretchImage;
                PBBtnBck.SizeMode = PictureBoxSizeMode.StretchImage;
                BtnSave.SizeMode = PictureBoxSizeMode.StretchImage;
                BtnDelete.SizeMode = PictureBoxSizeMode.StretchImage;
                PBMenuBar.Width = Screen.PrimaryScreen.Bounds.Width;


            }
            else
            {
                {
                    PBoxLotCode.Width = 200;
                    PBoxLotCode.Height = 35;
                    PBoxLotCode.SizeMode = PictureBoxSizeMode.StretchImage;
                    //
                    PBoxITemCode.Width = 200;
                    PBoxITemCode.Height = 35;
                    PBoxITemCode.SizeMode = PictureBoxSizeMode.StretchImage;
                  
                    PBoxQty.Width = 100;
                    PBoxQty.Height = 35;
                    PBoxQty.SizeMode = PictureBoxSizeMode.StretchImage;

                    PBMenuBar.Location = new Point(PBMenuBar.Location.X, Screen.PrimaryScreen.Bounds.Height - PBMenuBar.Height - 25);

                    PBBtnBck.Location = new Point(PBBtnBck.Location.X, PBMenuBar.Location.Y + oldbtny);
                    BtnSave.Location = new Point(BtnSave.Location.X, PBMenuBar.Location.Y + oldbtny);

                    BtnDelete.Location = new Point(BtnDelete.Location.X, PBMenuBar.Location.Y + oldbtny);


                }
            }
        }


        protected void GetInventoryRecord(long pinvid)
        {
            decimal tempqty;
            LBMunitQty.Visible = true;
            LBAlterMunit.Visible = true;
            LBDimensions.Visible = true;

            inv = InvHandler.InventoryRecord(pinvid);
            if (inv.InvID > 0)
            {
                BtnDelete.Visible = true;               
                TBLotCode.Text = inv.LotCode;
                TBItemCode.Text = inv.ItemCode;

                if (inv.ItemID > 0)
                {
                    item = itemhandler.ItemByID(inv.ItemID);
                    LBItemDesc.Visible = true;
                    LBItemDesc.Text = item.ItemDesc;
                    
                }
                if (inv.LotID > 0) 
                {
                    lot = lothandler.LotByID(inv.LotID);
                   
                    LBDimensions.Text = Math.Round(lot.Width, 2).ToString() + " X " + Math.Round(lot.Length, 2).ToString();
                }

                if (InvMunitPrimary.MunitID == item.MUnitPrimary)
                {
                    if (!(inv.MUnitPrimary > 0)) inv.MUnitPrimary = (short)item.MUnitPrimary;
                }
                else if (InvMunitPrimary.MunitID == item.MUnitSecondary)
                {
                    if (!(inv.MUnitSecondary > 0)) inv.MUnitSecondary = (short)item.MUnitSecondary;
                }

               // 05/02/2013 if ((lot.Width > 0)
                if ((lot.Width > 0) && (item.MUnitPrimary == 12))
                {

                    TBQty.Text = Math.Round(inv.InvQtySecondary, 0).ToString();
                    tempqty = inv.InvQtySecondary * (lot.Width * lot.Length);
                    LBAlterQty.Text = tempqty.ToString();

                }
                else
                {

                    TBQty.Text = Math.Round(inv.InvQty, 0).ToString();

                }


                
                //if (inv.InvQtySecondary > 0) LBAlterQty.Text = Math.Round(inv.InvQtySecondary, 2).ToString();
                 
            }
            TBQty.Focus();
        }

        protected void GetInvMunitSettings()
        {
            AppSettings appsettings = new AppSettings();
            MUnitHandler munithandler = new MUnitHandler();
           
            if (appsettings.InventoryMunit > 0) InvMunitPrimary = munithandler.MunitInfo(appsettings.InventoryMunit);
            if (appsettings.M2Uunit > 0) InvMunitSecondary = munithandler.MunitInfo(appsettings.M2Uunit);

        }

        protected void CheckInventoryHeaderID()
        {
            if (!(Program.iInvHeader.InvHdrID > 0))
            {
                GoSelectInventoryTask();
            }
            else
            {
                this.Text += "-" + InvHandler.InvHdrTitle(Program.iInvHeader.InvHdrID);
                inv.InvHdrID = Program.iInvHeader.InvHdrID;
            }
           
        }

        
        protected void ShowMessageBox(string msg, bool iserror)
        {
            if (iserror)
            {

                PBoxMessage.Image = Properties.Resources.msgbox_error;
                LBMsgBox.ForeColor = Color.FromArgb(244, 235, 163);
                LBMsgBox.BackColor = Color.FromArgb(204, 51, 51);
            }
            else
            {
                LBMsgBox.ForeColor = Color.FromArgb(204, 51, 51);
                LBMsgBox.BackColor = Color.FromArgb(244, 235, 163);
                PBoxMessage.Image = Properties.Resources.msgbox;
            }

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
            if (caller == 0)
            {
                WMSForms.FrmSelectInventoryHeader = new FrmSelectInventoryHeader();
                WMSForms.FrmSelectInventoryHeader.Show();
                this.Close();
            }
            else {

                WMSForms.FrmInventoryView = new FrmInventoryView();
                WMSForms.FrmInventoryView.Show();
                this.Close();
            
            
            }         
        }

        protected void GoSelectInventoryTask()
        {
            WMSForms.FrmSelectInventoryHeader = new FrmSelectInventoryHeader();
            WMSForms.FrmSelectInventoryHeader.Show();
            this.Close();
        }

        protected void EnableUseLot()
        {
            if (!AppGeneralSettings.UseLot)
            {
                TBLotCode.Visible = false;
                PBoxLotCode.Visible = false;
                LBLotCode.Visible = false;

                LBItemDesc.Location = LBItemCode.Location;
                LBItemDesc.Width = PBoxITemCode.Width;

                TBItemCode.Location = TBLotCode.Location;
                PBoxITemCode.Location = PBoxLotCode.Location;
                LBItemCode.Location = LBLotCode.Location;


            }
        }

        protected void InitEntry()
        {
            iLotCodeEntered = false;
            iItemCodeEntered = false;
            iLotChecked = false;

            iQtyEntered = false;

            LBAlterMunit.Visible = false;
            LBAlterQty.Visible = false;

            LBAlterMunit.Text = "";
            LBAlterQty.Text = "";

            TBLotCode.Text = "";
            TBItemCode.Text = "";
            LBItemDesc.Text = "";
            LBDimensions.Text = "";
            lbcolor.Text = "";
            lbdraft.Text = "";
            lb_erpqty.Text = "";


            BtnDelete.Visible = false;
          
            TBQty.Text = "";
            lot = null;
            item = null;
            inv = null;

            inv = new MInventory();
            lot = new Lot();
            item = new Item();
            inv.InvNo = int.Parse(InvHandler.GetInventoryCounter(Program.iInvHeader.InvHdrID)) + 1;
            lbcounter.Text = "#" + (inv.InvNo - 1).ToString();
            
            DisableSave();

            inv.InvHdrID = Program.iInvHeader.InvHdrID;
        }

        protected void CheckLotCode()
        {
            if (TBLotCode.Text.Length > 0 && iLotCodeEntered)
            {
                lot = new Lot();
                iLotCodeEntered = false;
                lot = lothandler.LotByCodeOnline(TBLotCode.Text.Trim(), Program.iInvHeader.InvHdrID);
                if (lot.LotID > 0)
                {
                    inv.LotID = lot.LotID;
                    inv.LotCode = lot.LotCode;
                    lbcolor.Text = lot.Color;
                    lbdraft.Text = "Σχέδιο : " + lot.Draft;
                    //lb_erpqty.Text = "Σ "+ lot.ErpQty2.ToString();
                    if (lot.Width > 0)
                    {
                        LBAlterMunit.Visible = true;
                        LBAlterQty.Visible = true;
                        LBDimensions.Visible = true;
                        LBDimensions.Text = Math.Round(lot.Width, 2).ToString() + " X " + Math.Round(lot.Length, 2).ToString();
                    }
                    else
                    {
                        LBAlterMunit.Visible = false;
                        LBAlterQty.Visible = false;
                        LBDimensions.Visible = false;
                    }
                    if (lot.ItemID > 0)
                    {
                        inv.ItemID = lot.ItemID;
                        item = itemhandler.ItemByID(lot.ItemID);
                        if (item.ItemID > 0)
                        {
                            TBItemCode.Text = item.ItemCode;
                            LBItemDesc.Text = item.ItemDesc;

                            inv.MUnitPrimary = (short) item.MUnitPrimary;
                            inv.MUnitSecondary = (short) item.MUnitSecondary;
                            inv.ItemCode = item.ItemCode;

                        

                            if (InvMunitPrimary.MunitID > 0)
                                LBMunitQty.Text = InvMunitPrimary.MUnit;
                            else
                                if (!string.IsNullOrEmpty(item.MUnitDesc1)) LBMunitQty.Text = item.MUnitDesc1;

                            if (InvMunitSecondary.MunitID > 0)
                                LBAlterMunit.Text = InvMunitSecondary.MUnit;
                            else

                                LBAlterMunit.Text = item.MUnitDesc2;


                            if ((lot.Width > 0) && (item.MUnitPrimary == 12))
                            {
                                LBAlterMunit.Text = item.MUnitDesc1;
                                LBAlterMunit.Visible = true;
                                LBAlterQty.Visible = true;
                                LBDimensions.Visible = true;
                                LBDimensions.Text = Math.Round(lot.Width, 2).ToString() + " X " + Math.Round(lot.Length, 2).ToString();
                            }
                            else
                            {
                                LBAlterMunit.Text = item.MUnitDesc2;
                                LBAlterMunit.Visible = false;
                                LBAlterQty.Visible = false;
                                LBDimensions.Visible = false;
                            }



                            TBQty.Text = "1";
                            TBQty.SelectAll();
                            TBQty.Focus();
                        }
                    }
                }
                else
                {
                    inv.LotCode = TBLotCode.Text.Trim();
                    ShowMessageBox("H Παρτίδα δεν βρέθηκε!",true);
                    TBItemCode.Focus();
                }
            }
        }

        protected bool CheckItemCode()
        {
            string enteredcode = null;

            iLotChecked = true;
            if (TBItemCode.Text.Length > 0 && iItemCodeEntered)
            {
                //               
                enteredcode = TBItemCode.Text;               
                //check if used wild char
                if ((AppGeneralSettings.RemovePrefixOnScanning) && (!string.IsNullOrEmpty(AppGeneralSettings.ItemCodePrefix)) && enteredcode.Length > 4)
                {
                    enteredcode = enteredcode.Remove(0, AppGeneralSettings.ItemCodePrefix.Length);
                    TBItemCode.Text = enteredcode;
                }
                //

                iItemCodeEntered = false;
                item = itemhandler.ItemByCode(TBItemCode.Text.Trim());
                if (item.ItemID > 0)
                {
                    inv.ItemID = item.ItemID;
                    inv.ItemCode = item.ItemCode;
                    LBItemDesc.Text = item.ItemDesc;


                    inv.MUnitPrimary = (short)item.MUnitPrimary;
                    inv.MUnitSecondary = (short)item.MUnitSecondary;

                    if (InvMunitPrimary.MunitID > 0)
                        LBMunitQty.Text = InvMunitPrimary.MUnit;
                    else
                        if (!string.IsNullOrEmpty(item.MUnitDesc1)) LBMunitQty.Text = item.MUnitDesc1;

                    if (InvMunitSecondary.MunitID > 0)
                        LBAlterMunit.Text = InvMunitSecondary.MUnit;
                    else
                        LBAlterMunit.Text = item.MUnitDesc2;

                    return true;
                }
                else
                {
                    inv.ItemCode = TBItemCode.Text.Trim();
                    ShowMessageBox("Το είδος δεν βρέθηκε!",true);
                    return false;
                }
            }

            return false;
        }

        protected bool CheckQty()
        {
            if (TBQty.Text.Length > 0 && iQtyEntered)
            {
                iQtyEntered = false;
                try { inv.InvQty = decimal.Parse(TBQty.Text); }
                catch { inv.InvQty = 0; }

                 if (!(inv.InvQty > 0))
                {
                    ShowMessageBox("Η ποσότητα δεν είναι έγκυρη!",true);
                    return false;
                }

                if ((lot.Width > 0) && (item.MUnitPrimary == 12))
                {
                    LBAlterQty.Text = Math.Round((inv.InvQty * (lot.Width * lot.Length)),2).ToString();
                }
                else
                {
                    LBAlterMunit.Visible = false;
                    LBAlterQty.Visible = false;
                }


                if (InvMunitPrimary.MunitID > 0)
                {
                    if (InvMunitPrimary.MunitID == item.MUnitPrimary && (lot.Width > 0) & item.MUnitSecondary > 0)
                    {
                        inv.MUnitSecondary = (short)item.MUnitSecondary;
                    }
                    else if (InvMunitPrimary.MunitID == item.MUnitSecondary && (lot.Width > 0))
                    {
                        inv.MUnitPrimary = (short)item.MUnitPrimary;
                    }
                }


                if ((lot.Width > 0) && (item.MUnitPrimary == 12))
                {

                    inv.InvQtySecondary = inv.InvQty;
                    inv.InvQty = inv.InvQty * (lot.Width * lot.Length);

                }

                #region changed  05/02/2013


                //CHANGED  05/02/2013 
                //if (InvMunitPrimary.MunitID > 0)
                //{
                //    if (InvMunitPrimary.MunitID == item.MUnitPrimary && (lot.Width > 0) & item.MUnitSecondary > 0)
                //    {
                //        inv.MUnitSecondary = (short)item.MUnitSecondary;
                //    }
                //    else if (InvMunitPrimary.MunitID == item.MUnitSecondary && (lot.Width > 0))
                //    {
                //        inv.MUnitPrimary = (short)item.MUnitPrimary;
                //    }
                //}

                //if (lot.Width > 0)
                //{

                //    inv.InvQtySecondary = inv.InvQty;
                //    inv.InvQty = inv.InvQty * (lot.Width * lot.Length);

                //}
               #endregion

               return true;
            }
            return false;
        }

        protected void EnableSaveChanges()
        {
            BtnSave.Image = Properties.Resources.buttonsave_on;
            BtnSaveFake.Enabled =  true;
        }

        protected void DisableSave()
        {
            BtnSave.Image = Properties.Resources.buttonsave;
        }

        protected void SaveChanges()
        {
            //if (TBLotCode.Text.Length > 0  && TBQty.Text.Length > 0)
            //{
                inv.CompID = AppGeneralSettings.CompID;
                inv.BranchID = AppGeneralSettings.BranchID;
                inv.StoreID =(short) AppGeneralSettings.StoreID;

                if (InvHandler.UpdateInventoryOnline(inv) > 0)
                {
                    InitEntry();
                    TBLotCode.Focus();
                }
                else
                {
                    ShowMessageBox("Πρόβλημα με την καταχώρηση! ",true);
                    TBLotCode.Focus();
                }                     
        }
       
        protected void DeleteRecord()
        {
            if (MessageBox.Show("Είστε Βέβαιοι για την Διαγραφή της Εγγραφής;", "Ερώτηση", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                return;
            InvHandler.DeleteRecord(inv.InvID);
            InitEntry();
        }

        private void PBBtnBck_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void BtnSave_GotFocus(object sender, EventArgs e)
        {
            if (BtnSave.Image != Properties.Resources.buttonsave_on) BtnSave.Image = Properties.Resources.buttonsave_on;

        }

        private void BtnSave_LostFocus(object sender, EventArgs e)
        {
            if (BtnSave.Image != Properties.Resources.buttonsave) BtnSave.Image = Properties.Resources.buttonsave;

        }

        private void BtnSave_MouseDown(object sender, MouseEventArgs e)
        {
            if (BtnSave.Image != Properties.Resources.buttonsave_on) BtnSave.Image = Properties.Resources.buttonsave_on;

        }

        private void BtnSave_MouseUp(object sender, MouseEventArgs e)
        {
            if (BtnSave.Image != Properties.Resources.buttonsave) BtnSave.Image = Properties.Resources.buttonsave;

        }

        private void PBBtnBck_GotFocus(object sender, EventArgs e)
        {
            if (PBBtnBck.Image != Properties.Resources.buttonback_on) PBBtnBck.Image = Properties.Resources.buttonback_on;

        }

        private void PBBtnBck_LostFocus(object sender, EventArgs e)
        {
            if (PBBtnBck.Image != Properties.Resources.buttonback) PBBtnBck.Image = Properties.Resources.buttonback;
        }

        private void PBBtnBck_MouseDown(object sender, MouseEventArgs e)
        {
            if (PBBtnBck.Image != Properties.Resources.buttonback_on) PBBtnBck.Image = Properties.Resources.buttonback_on;
        }

        private void PBBtnBck_MouseUp(object sender, MouseEventArgs e)
        {
            if (PBBtnBck.Image != Properties.Resources.buttonback) PBBtnBck.Image = Properties.Resources.buttonback;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            BtnDelete.Image = Properties.Resources.delete_on;
            DeleteRecord();
        }

        private void BtnDelete_GotFocus(object sender, EventArgs e)
        {
            BtnDelete.Image = Properties.Resources.delete_on;
        }

        private void BtnDelete_LostFocus(object sender, EventArgs e)
        {
            BtnDelete.Image = Properties.Resources.delete;
        }

        private void BtnDelete_MouseDown(object sender, MouseEventArgs e)
        {
            BtnDelete.Image = Properties.Resources.delete_on;
        }

        private void BtnDelete_MouseUp(object sender, MouseEventArgs e)
        {
            BtnDelete.Image = Properties.Resources.delete;
        }

        

        private void BtnSaveFake_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void BtnSaveFake_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && iQtyEntered)
            {
                SaveChanges();
                BtnSaveFake.Enabled = false;
                if (AppGeneralSettings.UseLot)
                    TBLotCode.Focus();
                else
                    TBItemCode.Focus();
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

        

    }
}