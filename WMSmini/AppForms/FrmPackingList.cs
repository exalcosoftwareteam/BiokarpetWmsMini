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
    public partial class FrmPackingList : Form
    {
        decimal tempqty = 0;
        bool iLotCodeEntered = false;
        bool iItemCodeEntered = false;
        bool iQtyEntered = false;
        bool iLotChecked = false;
        short caller = 0;
        long OrderID;
        Item item = new Item();
        Lot lot = new Lot();
        PackingListHeaderHandler packheader = new PackingListHeaderHandler();
        PackingList packlistdtl = new PackingList();
        LotHandler lothandler = new LotHandler();
        PackingListDetailHandler PackHandler = new PackingListDetailHandler();
        ItemHandler itemhandler = new ItemHandler();

        MUnits PackMunitPrimary = new MUnits();
        MUnits PackMunitSecondary = new MUnits();


        public FrmPackingList(long packdtlid)
        {
            InitializeComponent();
            lbcounter.Text = "Εγγραφές : " + packheader.GetPackingListCounter(Program.iPackHeader.PackingListHeaderID);
            if (packdtlid > 0){
            GetPackingListRecord(packdtlid);
            caller = 1;
                 }

        }

        public FrmPackingList()
        {
            InitializeComponent();
            lbcounter.Text = "Εγγραφές : " + packheader.GetPackingListCounter(Program.iPackHeader.PackingListHeaderID);
        }

        public FrmPackingList(long packdtlid,long myorderid)
        {
            OrderID = myorderid;
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
            HideMessageBox();
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
                iQtyEntered = true;
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
            if (e.KeyCode == Keys.Enter || e.KeyCode==Keys.LineFeed)
            {
                if (!iLotChecked)
                {
                    CheckLotCode();
                }
            }
            if (e.KeyCode == Keys.Escape)
                GoBack();
        }

        private void TBLotCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (!iLotChecked)
                {
                    CheckLotCode();
                    TBItemCode.Focus();
                }              
            }

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


        protected void GetPackingListRecord(long packingdtlid)
        {
            LBMunitQty.Visible = true;
            LBAlterMunit.Visible = true;
            LBDimensions.Visible = true;


            packlistdtl = PackHandler.PackingListRecord(packingdtlid);

            if (packlistdtl.PackingListDTLID > 0)
            {
                BtnDelete.Visible = true;
                TBLotCode.Text = packlistdtl.LotCode;
                TBItemCode.Text = packlistdtl.ItemCode;
                LBColor.Text = "Χρώμα :" + packlistdtl.Color;
                LBDraft.Text = "Σχέδιο :" + packlistdtl.Draft;
                if (packlistdtl.ItemID > 0)
                {
                    item = itemhandler.ItemByID(packlistdtl.ItemID);
                    LBItemDesc.Visible = true;
                    LBItemDesc.Text = item.ItemDesc;
                    
                }
                if (packlistdtl.LotID > 0)
                {
                    lot = lothandler.LotByID(packlistdtl.LotID);
                   
                    LBDimensions.Text = Math.Round(lot.Width, 2).ToString() + " X " + Math.Round(lot.Length, 2).ToString();
                }

               if ((packlistdtl.Width > 0)&& (item.MUnitPrimary == 12))
                {

                    TBQty.Text = Math.Round(packlistdtl.ItemQtySecondary, 0).ToString();
                    tempqty = packlistdtl.ItemQtySecondary * (lot.Width * lot.Length);
                    LBAlterQty.Text = tempqty.ToString();

                }
                else {

                    TBQty.Text = Math.Round(packlistdtl.ItemQtyPrimary,0).ToString();
                
                }
                
                 
            }
            TBQty.Focus();
        }

        protected void GetInvMunitSettings()
        {
            AppSettings appsettings = new AppSettings();
            MUnitHandler munithandler = new MUnitHandler();

            if (appsettings.PackMunit > 0) PackMunitPrimary = munithandler.MunitInfo(appsettings.PackMunit);
            if (appsettings.M2Uunit > 0) PackMunitSecondary = munithandler.MunitInfo(appsettings.M2Uunit);

        }

        protected void CheckInventoryHeaderID()
        {
            if (!(Program.iPackHeader.PackingListHeaderID > 0))
            {
                GoSelectPackingListTask();
            }
            else
            {
                this.Text += "-" + PackHandler.PackingListHeaderTitle(Program.iPackHeader.PackingListHeaderID);
                packlistdtl.PackingListHeaderID = Program.iPackHeader.PackingListHeaderID;
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
                WMSForms.FrmSelectPackingList = new FrmSelectPackingList();
                WMSForms.FrmSelectPackingList.Show();
                this.Close();
            }
            else {

                WMSForms.FrmPackingListView = new FrmPackingListView();
                WMSForms.FrmPackingListView.Show();
                this.Close();
             }

        
        
        }

        protected void GoSelectPackingListTask()
        {
            WMSForms.FrmSelectPackingList = new FrmSelectPackingList();
            WMSForms.FrmSelectPackingList.Show();
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
            LBColor.Text = "";
            LBDraft.Text = "";  

            TBLotCode.Text = "";
            TBItemCode.Text = "";
            LBItemDesc.Text = "";
            LBDimensions.Text = "";
            lbcounter.Text = "Εγγραφές : " + packheader.GetPackingListCounter(Program.iPackHeader.PackingListHeaderID);
            BtnDelete.Visible = false;
          
            TBQty.Text = "";
            lot = null;
            item = null;
            packlistdtl = null;

            packlistdtl = new PackingList();

            lot = new Lot();
            item = new Item();

            
            DisableSave();

            packlistdtl.PackingListHeaderID = Program.iPackHeader.PackingListHeaderID;
        }

        protected void CheckLotCode()
        {
            if (TBLotCode.Text.Length > 0 && iLotCodeEntered)
            {
                iLotCodeEntered = false;
                lot = lothandler.LotByCode(TBLotCode.Text.Trim(),0);
                if (lot.LotID > 0)
                {
                    //PARSE LOT TO PACKING LIST
                    packlistdtl.LotID = lot.LotID;
                    packlistdtl.LotCode = lot.LotCode;
                    packlistdtl.Color = lot.Color;
                    packlistdtl.Draft = lot.Draft;
                    packlistdtl.Width = lot.Width;
                    packlistdtl.Length = lot.Length;
                    //

                    if (lot.Width > 0)
                    {
                        LBDimensions.Visible = true;
                        LBDimensions.Text = Math.Round(lot.Width,2).ToString() + " X " + Math.Round(lot.Length,2).ToString();
                    }
                    else
                        LBDimensions.Visible = false;

                    if (lot.ItemID > 0)
                    {
                        LBColor.Text = "Χρώμα : " + packlistdtl.Color;
                        LBDraft.Text = "Σχέδιο : " + packlistdtl.Draft;
                        packlistdtl.ItemID = lot.ItemID;
                        item = itemhandler.ItemByID(lot.ItemID);
                        if (item.ItemID > 0)
                        {
                            TBItemCode.Text = item.ItemCode;
                            LBItemDesc.Text = item.ItemDesc;
                            packlistdtl.ItemCode = TBItemCode.Text;
                            packlistdtl.ItemMunitPrimary = (short)item.MUnitPrimary;
                            packlistdtl.ItemMunitSecondary = (short)item.MUnitSecondary;


                            if (PackMunitPrimary.MunitID > 0)
                                LBMunitQty.Text = PackMunitPrimary.MUnit;
                            else
                                if (!string.IsNullOrEmpty(item.MUnitDesc1)) LBMunitQty.Text = item.MUnitDesc1;

                            if (PackMunitSecondary.MunitID > 0)
                                LBAlterMunit.Text = PackMunitSecondary.MUnit;
                            else
                                LBAlterMunit.Text = item.MUnitDesc2;

                            if ((lot.Width > 0) && (item.MUnitPrimary == 12))
                            {
                                LBAlterMunit.Text = item.MUnitDesc1;
                            }
                            else
                            {
                                LBDimensions.Visible = false;
                                LBAlterMunit.Text = item.MUnitDesc2;
                            }


                            TBQty.Text = "1";
                            TBQty.SelectAll();
                            TBQty.Focus();
                        }
                    }
                }
                else
                {

                    packlistdtl.LotCode = TBLotCode.Text.Trim();
                    ShowMessageBox("H Παρτίδα δεν βρέθηκε!",true);
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
                    packlistdtl.ItemID = item.ItemID;
                    packlistdtl.ItemCode = item.ItemCode;
                    LBItemDesc.Text = item.ItemDesc;


                    packlistdtl.ItemMunitPrimary = (short)item.MUnitPrimary;
                    packlistdtl.ItemMunitSecondary = (short)item.MUnitSecondary;

                    if (PackMunitPrimary.MunitID > 0)
                        LBMunitQty.Text = PackMunitPrimary.MUnit;
                    else
                        if (!string.IsNullOrEmpty(item.MUnitDesc1)) LBMunitQty.Text = item.MUnitDesc1;

                    if (PackMunitSecondary.MunitID > 0)
                        LBAlterMunit.Text = PackMunitSecondary.MUnit;
                    else
                        LBAlterMunit.Text = item.MUnitDesc2;

                    return true;
                }
                else
                {
                    packlistdtl.ItemCode = TBItemCode.Text.Trim();
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
                try { packlistdtl.ItemQtyPrimary = decimal.Parse(TBQty.Text); }
                catch { packlistdtl.ItemQtyPrimary = 0; }

                if (!(packlistdtl.ItemQtyPrimary > 0))
                {
                    ShowMessageBox("Η ποσότητα δεν είναι έγκυρη!",true);
                    return false;
                }

                if ((-lot.Width > 0)&& (item.MUnitPrimary == 12))
                {
                    LBAlterMunit.Visible = true;
                    LBAlterQty.Visible = true;
                    LBAlterQty.Text = Math.Round((packlistdtl.ItemQtyPrimary * (lot.Width * lot.Length)), 2).ToString();
                }
                else
                {
                    LBAlterMunit.Visible = false;
                    LBAlterQty.Visible = false;
                }


                if ((lot.Width > 0) && (item.MUnitPrimary == 12))
                {

                    packlistdtl.ItemQtySecondary = packlistdtl.ItemQtyPrimary;
                    packlistdtl.ItemQtyPrimary = packlistdtl.ItemQtyPrimary * (lot.Width * lot.Length);
                
                }


                //if (PackMunitPrimary.MunitID > 0)
                //{
                //    if (PackMunitPrimary.MunitID == item.MUnitPrimary && (lot.Width > 0) & item.MUnitSecondary > 0)
                //    {
                //        packlistdtl.ItemMunitSecondary = (short)item.MUnitSecondary;
                //        packlistdtl.ItemQtySecondary = packlistdtl.ItemQtySecondary * (lot.Width * lot.Length);
                //    }
                //    else if (PackMunitPrimary.MunitID == item.MUnitSecondary && (lot.Width > 0))
                //    {
                //        packlistdtl.ItemMunitPrimary = (short)item.MUnitPrimary;
                //        packlistdtl.ItemQtyPrimary = packlistdtl.ItemQtyPrimary * (lot.Width * lot.Length);
                //    }
                //}

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
            if (PackHandler.UpdatePackingListDTL(packlistdtl) > 0)
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
            PackHandler.DeleteRecord(packlistdtl.PackingListDTLID);
            InitEntry();
        }

        private void PBBtnBck_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (CheckQty())
            {
                BtnSaveFake.Enabled = true;
                BtnSaveFake.Focus();
                iQtyEntered = true;
            }
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

        private void LBScanned_ParentChanged(object sender, EventArgs e)
        {

        }


       
    
    }
}