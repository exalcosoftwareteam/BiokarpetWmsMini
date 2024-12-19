using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WMSMobileClient;
using WMSMobileClient.Components;
using WMSDBCollections;



namespace WMSMobileClient
{

    public  struct MenuOptions
    {
        public const string receiving = "receiving";
        public const string putaway = "putaway";
        public const string picking = "picking";
        public const string exports = "exports";
        public const string inventory = "inventory";
        public const string shipping = "shipping";
        public const string settings = "settings";        
    }

    public struct MenuOptionsEnabled
    {
        public static bool receiving = false;
        public static bool putaway = false;
        public static bool picking = false;
        public static bool exports = false;
        public static bool inventory = false;
        public static bool shipping = false;
        public static bool settings = false;
       

    }

    public partial class FrmMenu : Form
    {
             
        public FrmMenu()
        {
            InitializeComponent();

            FixREsolutionIssues();


        }


        


        private void FrmMenu_Load(object sender, EventArgs e)
        {
            PBReceiving.Image = Properties.Resources.receiving_big_off;
     //       MenuOptionsEnabled.receiving = true;
             
           
        }

        protected void FixREsolutionIssues()
        {
            if (Screen.PrimaryScreen.Bounds.Width <= 240)
            {

                PBReceiving.Width = 70;
                PBReceiving.Height = 70;
                PBReceiving.Location = new Point(30, 110);
                PBReceiving.SizeMode = PictureBoxSizeMode.StretchImage;


                PBpicking.Width = 70;
                PBpicking.Height = 70;
                PBpicking.Location = new Point(30, 30);
                PBpicking.SizeMode = PictureBoxSizeMode.StretchImage;


                PBinventory.Width = 70;
                PBinventory.Height = 70;
                PBinventory.Location = new Point(130, 30);
                PBinventory.SizeMode = PictureBoxSizeMode.StretchImage;

                lb_online.Location = new Point(130, 100);


                //PBPutaway.Width = 70;
                //PBPutaway.Height = 70;
                //PBPutaway.Location = new Point(130, 30);
                //PBPutaway.SizeMode = PictureBoxSizeMode.StretchImage;




                PBexports.Width = 70;
                PBexports.Height = 70;
                PBexports.Location = new Point(130, 130);
                PBexports.SizeMode = PictureBoxSizeMode.StretchImage;




                PBMenuSettings.Width = 70;
                PBMenuSettings.Height = 70;
                PBMenuSettings.Location = new Point(130, 230);
                PBMenuSettings.SizeMode = PictureBoxSizeMode.StretchImage;

            }
        }

        #region menu behaviour

        private void FrmMenu_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.NumPad1 || e.KeyCode == Keys.D1)
                EnableMenuChoice(MenuOptions.receiving);
            else if (e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.D2)
                EnableMenuChoice(MenuOptions.putaway);
            else if (e.KeyCode == Keys.NumPad3 || e.KeyCode == Keys.D3)
                EnableMenuChoice(MenuOptions.picking);
            else if (e.KeyCode == Keys.NumPad4 || e.KeyCode == Keys.D4)
                EnableMenuChoice(MenuOptions.exports);
            else if (e.KeyCode == Keys.NumPad5 || e.KeyCode == Keys.D5)
                EnableMenuChoice(MenuOptions.inventory);
            else if (e.KeyCode == Keys.NumPad6 || e.KeyCode == Keys.D6)
                EnableMenuChoice(MenuOptions.settings);
            else if (e.KeyCode == Keys.Down)
            {
                if (MenuOptionsEnabled.receiving)
                    EnableMenuChoice(MenuOptions.putaway);
                else if (MenuOptionsEnabled.putaway)
                    EnableMenuChoice(MenuOptions.picking);
                else if (MenuOptionsEnabled.picking)
                    EnableMenuChoice(MenuOptions.exports);
                else if (MenuOptionsEnabled.exports)
                    EnableMenuChoice(MenuOptions.inventory);
                else if (MenuOptionsEnabled.inventory)
                    EnableMenuChoice(MenuOptions.settings);
                else if (MenuOptionsEnabled.settings)
                    EnableMenuChoice(MenuOptions.receiving);
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (MenuOptionsEnabled.settings)
                    EnableMenuChoice(MenuOptions.inventory);
                else if (MenuOptionsEnabled.inventory)
                    EnableMenuChoice(MenuOptions.exports);
                else if (MenuOptionsEnabled.exports)
                    EnableMenuChoice(MenuOptions.picking);
                else if (MenuOptionsEnabled.picking)
                    EnableMenuChoice(MenuOptions.putaway);
                else if (MenuOptionsEnabled.putaway)
                    EnableMenuChoice(MenuOptions.receiving);
                else if (MenuOptionsEnabled.receiving)
                    EnableMenuChoice(MenuOptions.settings);
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (MenuOptionsEnabled.receiving)
                    EnableMenuChoice(MenuOptions.putaway);
                else if (MenuOptionsEnabled.putaway)
                    EnableMenuChoice(MenuOptions.picking);
                else if (MenuOptionsEnabled.picking)
                    EnableMenuChoice(MenuOptions.exports);
                else if (MenuOptionsEnabled.exports)
                    EnableMenuChoice(MenuOptions.inventory);
                else if (MenuOptionsEnabled.inventory)
                    EnableMenuChoice(MenuOptions.settings);
                else if (MenuOptionsEnabled.settings)
                    EnableMenuChoice(MenuOptions.receiving);
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (MenuOptionsEnabled.settings)
                    EnableMenuChoice(MenuOptions.inventory);
                else if (MenuOptionsEnabled.inventory)
                    EnableMenuChoice(MenuOptions.exports);
                else if (MenuOptionsEnabled.exports)
                    EnableMenuChoice(MenuOptions.picking);
                else if (MenuOptionsEnabled.picking)
                    EnableMenuChoice(MenuOptions.putaway);
                else if (MenuOptionsEnabled.putaway)
                    EnableMenuChoice(MenuOptions.receiving);
                else if (MenuOptionsEnabled.receiving)
                    EnableMenuChoice(MenuOptions.settings);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (MenuOptionsEnabled.receiving)
                    MessageBox.Show("Not implemented yet!");//Goto Receiving                    
                else if (MenuOptionsEnabled.putaway)
                    GotoPutaway();
                else if (MenuOptionsEnabled.picking)
                    GotoPicking();
                else if (MenuOptionsEnabled.exports)
                    GotoExports();
                else if (MenuOptionsEnabled.inventory)
                    GotoInventory();
                //else if (MenuOptionsEnabled.shipping)
                //    GoToShipping();
                else if (MenuOptionsEnabled.settings)
                    GoToSettings();

            }
            else if (e.KeyCode == Keys.NumPad0 || e.KeyCode == Keys.D0)
                TerminateApplication();
            
        }


        protected void EnableMenuChoice(string menuoption)
        {

            PBReceiving.Image = Properties.Resources.receiving_big_off;
            PBPutaway.Image = Properties.Resources.putaway_big_off;

          //  PBpicking.Image = Properties.Resources.transfer;
            PBpicking.Image = Properties.Resources.picking_big_off;

            PBexports.Image = Properties.Resources.exports_big_off;
            PBinventory.Image = Properties.Resources.inventory_big_off;
            PBMenuSettings.Image = Properties.Resources.settings_big_off;


            MenuOptionsEnabled.receiving = false;
            MenuOptionsEnabled.putaway = false;
            MenuOptionsEnabled.picking = false;
            MenuOptionsEnabled.exports = false;
            MenuOptionsEnabled.inventory = false;
            //MenuOptionsEnabled.shipping = false;
            MenuOptionsEnabled.settings = false;

            switch (menuoption)
            {
                case MenuOptions.receiving:
                    {
                        PBReceiving.Image = Properties.Resources.receiving_big;
                        MenuOptionsEnabled.receiving = true;
                        break;
                    }
                case MenuOptions.putaway:
                    {
                        PBPutaway.Image = Properties.Resources.putaway_big;
                        MenuOptionsEnabled.putaway = true;
                        break;
                    }
                case MenuOptions.picking:
                    {
                        PBpicking.Image = Properties.Resources.picking_big;
                        //PBpicking.Image = Properties.Resources.transfer_on;
                        MenuOptionsEnabled.picking = true;
                        break;
                    }
                case MenuOptions.exports:
                    {

                        PBexports.Image = Properties.Resources.exports_big;
                        MenuOptionsEnabled.exports = true;
                        break;
                    }
                case MenuOptions.inventory:
                    {

                        PBinventory.Image = Properties.Resources.inventory_big;
                        MenuOptionsEnabled.inventory = true;
                        break;
                    }
                //case MenuOptions.shipping:
                //    {

                //        PBinventory.Image = Properties.Resources.shippment_big;
                //        MenuOptionsEnabled.shipping = true;
                //        break;
                //    }
                case MenuOptions.settings:
                    {

                        PBMenuSettings.Image = Properties.Resources.settings_big;
                        MenuOptionsEnabled.settings = true;
                        break;
                    }
                default:
                     break;
            }
        }

        #endregion

        #region Click events

        private void PBReceiving_Click(object sender, EventArgs e)
        {
            EnableMenuChoice(MenuOptions.receiving);
            GotoReceive();
        }

        private void PBPutaway_Click(object sender, EventArgs e)
        {
            EnableMenuChoice(MenuOptions.putaway);
            GotoPutaway();
        }

        private void PBpicking_Click(object sender, EventArgs e)
        {            
            EnableMenuChoice(MenuOptions.picking);
            GotoPicking();          
        }

        private void PBMenuSettings_Click(object sender, EventArgs e)
        {
            EnableMenuChoice(MenuOptions.settings);
            GoToSettings();
        }

        #endregion
       
        #region otherevents


        private void PBMenuSettings_MouseDown(object sender, MouseEventArgs e)
        {
            EnableMenuChoice(MenuOptions.settings);
        }

        private void PBPutaway_MouseDown(object sender, MouseEventArgs e)
        {
            EnableMenuChoice(MenuOptions.putaway);
        }        

        private void PBpicking_MouseDown(object sender, MouseEventArgs e)
        {
            EnableMenuChoice(MenuOptions.picking);
        }

        private void PBReceiving_MouseDown(object sender, MouseEventArgs e)
        {
            EnableMenuChoice(MenuOptions.receiving);
        }

        private void PBReceiving_GotFocus(object sender, EventArgs e)
        {
            EnableMenuChoice(MenuOptions.receiving);
        }

        private void PBinventory_Click(object sender, EventArgs e)
        {
            EnableMenuChoice(MenuOptions.inventory);

            GotoInventory();
        }

        private void PBinventory_GotFocus(object sender, EventArgs e)
        {
            EnableMenuChoice(MenuOptions.inventory);
        }

        private void PBinventory_MouseDown(object sender, MouseEventArgs e)
        {
            EnableMenuChoice(MenuOptions.inventory);
        }

        private void PBexports_Click(object sender, EventArgs e)
        {
            EnableMenuChoice(MenuOptions.exports);
            GotoExports();

        }

        private void PBexports_GotFocus(object sender, EventArgs e)
        {
            EnableMenuChoice(MenuOptions.exports);
        }

        private void PBexports_MouseDown(object sender, MouseEventArgs e)
        {
            EnableMenuChoice(MenuOptions.exports);
        }

        private void PBpicking_GotFocus(object sender, EventArgs e)
        {
            EnableMenuChoice(MenuOptions.picking);
        }


        #endregion


        #region Commands

        protected void GotoPutaway()
        {
            //WMSForms.FrmBinTrans = new FrmBinTrans(TransTypesCollection.TransTypePutawayInsert);

            //Cursor.Current = Cursors.WaitCursor;
            //WMSForms.FrmBinTrans.Show();
            //Cursor.Current = Cursors.Default;

        }

        protected void GotoPicking()
        {

            if (!MenuOptionsEnabled.picking)
                EnableMenuChoice(MenuOptions.picking);

            Cursor.Current = Cursors.WaitCursor;

          //  WMSForms.FrmPackingList = new FrmPackingList(0);
        //   WMSForms.FrmPackingList.Show();

            WMSForms.FrmSelectPackingList = new FrmSelectPackingList();
            WMSForms.FrmSelectPackingList.Show();

           // WMSForms.FrmSalesOrderTrans = new FrmSalesOrderTrans(0);
           // WMSForms.FrmSalesOrderTrans.Show();


            Cursor.Current = Cursors.Default;                        
        }

        protected void GotoExports()
        {
            Cursor.Current = Cursors.WaitCursor;

            //WMSForms.FrmBinTrans = new FrmBinTrans(TransTypesCollection.TransTypeRemove);
            //WMSForms.FrmBinTrans.Show();

            Cursor.Current = Cursors.Default;
        }

        protected void GotoReceive()
        {
            Cursor.Current = Cursors.WaitCursor;

            WMSForms.FrmReceivesHeader = new FrmReceivesHeader();
            WMSForms.FrmReceivesHeader.Show();

            Cursor.Current = Cursors.Default;
        }



        

        protected void GoToShipping()
        {
           

        }

        protected void GoToSettings()
        {
            Cursor.Current = Cursors.WaitCursor;

            WMSForms.FrmSettings = new FrmSettings();
            WMSForms.FrmSettings.Show();

            Cursor.Current = Cursors.Default;
        }

        protected void GotoInventory()
        {
            Cursor.Current = Cursors.WaitCursor;

            if (AppGeneralSettings.OnlineMode)
            {
                WMSForms.FrmSelectInventoryHeaderOnline = new FrmSelectInventoryHeaderOnline();
                WMSForms.FrmSelectInventoryHeaderOnline.Show();


            }
            else {
                WMSForms.FrmInventory = new FrmInventory(0);
                WMSForms.FrmInventory.Show();        
            }

            Cursor.Current = Cursors.Default;
        }


        protected void TerminateApplication()
        {
            if (MessageBox.Show("Είστε Βέβαιοι για την Έξοδο από την εφαρμογή;", "Ερώτηση", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        #endregion

        private void PBMenuSettings_MouseMove(object sender, MouseEventArgs e)
        {        
            EnableMenuChoice(MenuOptions.settings);
        }
       
        private void PBinventory_MouseMove(object sender, MouseEventArgs e)
        {
            EnableMenuChoice(MenuOptions.inventory);
        }

        private void PBPutaway_MouseMove(object sender, MouseEventArgs e)
        {
            EnableMenuChoice(MenuOptions.putaway);
        }

        private void PBpicking_MouseMove(object sender, MouseEventArgs e)
        {
            EnableMenuChoice(MenuOptions.picking);
        }

        private void PBexports_MouseMove(object sender, MouseEventArgs e)
        {
            EnableMenuChoice(MenuOptions.exports);
        }

        private void PBReceiving_MouseMove(object sender, MouseEventArgs e)
        {
            EnableMenuChoice(MenuOptions.receiving);
        }

        private void FrmMenu_GotFocus(object sender, EventArgs e)
        {
            lb_online.Visible = AppGeneralSettings.OnlineMode;
        }        
    }
}