using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WMSMobileClient;
using WMSMobileClient.Components;
using WMSMobileClient.WMSservice;


namespace WMSMobileClient
{
    public partial class FrmExportInventory : Form
    {
        public FrmExportInventory()
        {
            InitializeComponent();
        }


        private void FrmExportInventory_Load(object sender, EventArgs e)
        {
            FixResolutionIssues();
        }

     



        #region Form Events
        private void PBBtnBck_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private void PBBtnBck_GotFocus(object sender, EventArgs e)
        {
            PBBtnBck.Image = Properties.Resources.buttonback_on;
        }

        private void PBBtnBck_LostFocus(object sender, EventArgs e)
        {
            PBBtnBck.Image = Properties.Resources.buttonback;
        }

        private void PBBtnBck_MouseDown(object sender, MouseEventArgs e)
        {
            PBBtnBck.Image = Properties.Resources.buttonback_on;
        }

        private void PBBtnBck_MouseUp(object sender, MouseEventArgs e)
        {
            PBBtnBck.Image = Properties.Resources.buttonback;
        }

        private void FrmExportInventory_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Escape))
            {
                GoBack();
            }


        }

        private void BtnSyncData_Click(object sender, EventArgs e)
        {
            ExportData();
        }
        #endregion

    

        protected void CheckInvHdrID()
        {
            if (!(Program.iInvHeader.InvHdrID > 0))          
                GoBack();  
        }



            
            
            //
            //RE-INSERT INVENTORYHEADER TO DESKTOP DB
            //GET ID
            //UPDATE EXISTING TABLES WITH VALID INVHDRID
            //RESET SYNC IDS
            

        protected void ExportData()
        {
            ResultWithMessage rsmsg = new ResultWithMessage();
            long exprows = 0;
            long invhdrsrvr=0;
            ExportData exportdata = new ExportData();
            BtnSyncData.Text = "Παρακαλώ περιμένετε...";

            Cursor.Current = Cursors.WaitCursor;
            rsmsg = exportdata.ExportInventoryHeader(Program.iInvHeader.InvHdrID);
            if (!rsmsg.posresult) 
            {
                MessageBox.Show(rsmsg.errormessage);
                Cursor.Current = Cursors.Default;
                BtnSyncData.Text = "Εξαγωγή Απογραφής";
                ImgExportInventory.Visible = false;
                LBExpInvRows.Visible = false;
                return;
            }

            invhdrsrvr = rsmsg.resultno;

    //        if (invhdrsrvr == -5) MigrateInventory(Program.iInvHeader.InvHdrID);

            if (!(invhdrsrvr > 0) && !(invhdrsrvr == -5))
            {
                if (invhdrsrvr == -10)
                    MessageBox.Show("Πρόβλημα Επικοινωνίας με την υπηρεσία!");

                else
                    MessageBox.Show("Πρόβλημα με την εξαγωγή Δεδομένων!");

                BtnSyncData.Text = "Εξαγωγή Απογραφής";
                ImgExportInventory.Visible = false;
                LBExpInvRows.Visible = false;

                Cursor.Current = Cursors.Default;

                return;
            }
           

            exprows = ExportInventory(Program.iInvHeader.InvHdrID, invhdrsrvr);

            ImgExportInventory.Visible = true;
            LBExpInvRows.Visible = true;
            LBExportInventory.Visible = true;

            if (exprows > 0)
            {
                ImgExportInventory.Image = Properties.Resources.ok;
                LBExpInvRows.Text = "Εξαγωγή (" + exprows.ToString() + " ) εγγραφές";
                
            }
            else
            {
                ImgExportInventory.Image = Properties.Resources.error;
                LBExpInvRows.Text = "Εξαγωγή ( 0 ) εγγραφές";
            }

            BtnSyncData.Text="Εξαγωγή Απογραφής";

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


                PBMenuBar.SizeMode = PictureBoxSizeMode.StretchImage;
                PBBtnBck.SizeMode = PictureBoxSizeMode.StretchImage;

                PBMenuBar.Width = Screen.PrimaryScreen.Bounds.Width;


            }
            else {

                PBMenuBar.Location = new Point(PBMenuBar.Location.X, Screen.PrimaryScreen.Bounds.Height - PBMenuBar.Height - 25);

                PBBtnBck.Location = new Point(PBBtnBck.Location.X, PBMenuBar.Location.Y + oldbtny);
            
            
            
            
            }
        }

        protected void GoBack()
        {
            WMSForms.FrmInventoryView = new FrmInventoryView();
            WMSForms.FrmInventoryView.Show();
            this.Close();
        }

        public long ExportInventory(long invhdrid, long ServerInvHdrID)
        {
            DB db = new DB();
            ItemHandler itemhandler = new ItemHandler();
            LotHandler lothandler = new LotHandler();
            WMSservice.ERPItem newitem = new WMSservice.ERPItem();
            WMSservice.ERPLot newlot = new WMSservice.ERPLot();
            long minid = 0, maxid = 0;
            long rset = 0;
            long raffected = 0;
            int invrows = 0;
            bool clearprevious = true;

            DataTable DT = new DataTable();
            DataSet Ds = new DataSet();

            TInventory[] SOAInv;

            string sqlstr = null;

            LBExpInvRows.Visible = true;






            if (AppGeneralSettings.ALTERINVEXPORT) 
            {
                invrows = 0;
                sqlstr = "SELECT * FROM TInventory WHERE InvHdrID=" + invhdrid.ToString();
                Ds = db.DBFillDataset(sqlstr, "DSINVENTORY");

                

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    
                    for (int j = 0; j < Ds.Tables[0].Rows.Count; j++)
                    {

                        TInventory i = new TInventory();

                        i.InvHdrID = invhdrid;
                        i.InvHdrIDServer = ServerInvHdrID;

                        if (Ds.Tables[0].Rows[j]["InvID"] != DBNull.Value) { i.InvID = long.Parse(Ds.Tables[0].Rows[j]["InvID"].ToString()); }
                        if (Ds.Tables[0].Rows[j]["ItemID"] != DBNull.Value) { i.ItemID = long.Parse(Ds.Tables[0].Rows[j]["ItemID"].ToString()); }
                        if (Ds.Tables[0].Rows[j]["LotID"] != DBNull.Value) { i.LotID = long.Parse(Ds.Tables[0].Rows[j]["LotID"].ToString()); }
                        if (Ds.Tables[0].Rows[j]["LotCode"] != DBNull.Value) { i.LotCode = Ds.Tables[0].Rows[j]["LotCode"].ToString(); }
                        //i.CompID = AppGeneralSettings.CompID;
                        //if (Ds.Tables[0].Rows[j]["BranchID"] != DBNull.Value) { i.BranchID = short.Parse(Ds.Tables[0].Rows[j]["BranchID"].ToString()); }
                        if (Ds.Tables[0].Rows[j]["ItemCode"] != DBNull.Value) { i.ItemCode = Ds.Tables[0].Rows[j]["ItemCode"].ToString(); }
                        if (Ds.Tables[0].Rows[j]["InvQtyPrimary"] != DBNull.Value) { i.InvQty = decimal.Parse(Ds.Tables[0].Rows[j]["InvQtyPrimary"].ToString()); }
                        if (Ds.Tables[0].Rows[j]["InvMunitPrimary"] != DBNull.Value) { i.MUnitPrimary = short.Parse(Ds.Tables[0].Rows[j]["InvMunitPrimary"].ToString()); }
                        if (Ds.Tables[0].Rows[j]["InvQtySecondary"] != DBNull.Value) { i.InvQtySecondary = decimal.Parse(Ds.Tables[0].Rows[j]["InvQtySecondary"].ToString()); }
                        if (Ds.Tables[0].Rows[j]["InvMunitSecondary"] != DBNull.Value) { i.MUnitSecondary = short.Parse(Ds.Tables[0].Rows[j]["InvMunitSecondary"].ToString()); }
         
                        i.StoreID = AppGeneralSettings.StoreID;
                        try
                        {
                            ServiceCalls.ImportInventory(i, clearprevious);
                            invrows++;
                            LBExpInvRows.Text = "Εξαγωγή " + invrows.ToString() + " από " + Ds.Tables[0].Rows.Count.ToString();
                            clearprevious = false;
                            Application.DoEvents();

                        }
                        catch { }

                    }
                }

                return invrows;
            }


            LBExpInvRows.Text = "Προετοιμασία δεδομένων απογραφής , παρακαλώ περιμένετε ...";
            Application.DoEvents();

            sqlstr = "SELECT invid FROM TInventory WHERE InvHdrID=" + invhdrid.ToString() + " ORDER BY invid asc ";
            DT = db.DBFillDataTable(sqlstr, "inv");

            db.DBConnect();
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                if (db.DBExecuteSQLCmdManual("UPDATE TInventory SET invno = " + (i + 1).ToString() + "  WHERE invid = " + DT.Rows[i][0].ToString() + " AND InvHdrID=" + invhdrid.ToString()) < 0) i = i - 1;
                LBExpInvRows.Text = "Προετοιμασία δεδομένων " + i.ToString() + " από " + DT.Rows.Count.ToString() + " παρακαλώ περιμένετε ...";
                Application.DoEvents();
            }
            db.DBDisConnect();



            try { minid = db.DBGetNumResultFromSQLSelect("SELECT MIN(invno)  FROM TInventory WHERE InvHdrID=" + invhdrid.ToString()); }
            catch
            {
            }
            try { maxid = db.DBGetNumResultFromSQLSelect("SELECT MAX(invno)  FROM TInventory WHERE InvHdrID=" + invhdrid.ToString()); }
            catch
            {
            }


            try
            {
                LBExpInvRows.Text = "Εξαγωγή 0 από " + maxid.ToString();
                invrows = int.Parse(maxid.ToString());
                Application.DoEvents();
            }
            catch { }
            
            for (long i = minid; i <= maxid; i += 50)
            {

                rset = i + 50;

               sqlstr = "SELECT * FROM TInventory WHERE InvHdrID=" + invhdrid.ToString() + " AND invno >= " + i.ToString() + " AND invno < " + rset.ToString();
               Ds = db.DBFillDataset(sqlstr, "DSINVENTORY");

               if (rset == maxid) 
               {
                   sqlstr = "SELECT * FROM TInventory WHERE InvHdrID=" + invhdrid.ToString() + " AND invno >= " + i.ToString() + " AND invno <= " + rset.ToString();
                   Ds = db.DBFillDataset(sqlstr, "DSINVENTORY");
                   i++;
               }

 
                if (Ds.Tables[0].Rows.Count > 0)
                {

                    SOAInv = new TInventory[Ds.Tables[0].Rows.Count];
                    List<TInventory> linv = new List<TInventory>();
                    for (int j = 0; j < Ds.Tables[0].Rows.Count; j++)
                    {
                        SOAInv[j] = new TInventory();
                        SOAInv[j].InvHdrID = invhdrid;
                        SOAInv[j].InvHdrIDServer = ServerInvHdrID;
                        try { 
                            SOAInv[j].InvID = long.Parse(Ds.Tables[0].Rows[j]["InvID"].ToString());
                        }
                        catch { }
                        try { 
                            SOAInv[j].ItemID = long.Parse(Ds.Tables[0].Rows[j]["ItemID"].ToString());
                        }
                        catch { }
                        try { 
                            SOAInv[j].LotID = long.Parse(Ds.Tables[0].Rows[j]["LotID"].ToString());
                        }
                        catch { }
                        try { 
                            SOAInv[j].LotCode = Ds.Tables[0].Rows[j]["LotCode"].ToString();
                        }
                        catch { }
                        //try { 
                        //    SOAInv[j].CompID = short.Parse(Ds.Tables[0].Rows[j]["CompID"].ToString());
                        //}
                        //catch { }
                        //try { 
                        //    SOAInv[j].BranchID = short.Parse(Ds.Tables[0].Rows[j]["BranchID"].ToString());
                        //}
                        //catch { }
                        try { 
                            SOAInv[j].ItemCode = Ds.Tables[0].Rows[j]["ItemCode"].ToString();
                        }
                        catch { }
                        try { 
                            SOAInv[j].InvQty = decimal.Parse(Ds.Tables[0].Rows[j]["InvQtyPrimary"].ToString());
                        }
                        catch { }
                        try { 
                            SOAInv[j].MUnitPrimary = short.Parse(Ds.Tables[0].Rows[j]["InvMunitPrimary"].ToString());
                        }
                        catch { }
                        try { 
                            SOAInv[j].InvQtySecondary = decimal.Parse(Ds.Tables[0].Rows[j]["InvQtySecondary"].ToString());
                        }
                        catch { }
                        try { 
                            SOAInv[j].MUnitSecondary = short.Parse(Ds.Tables[0].Rows[j]["InvMunitSecondary"].ToString());
                        }
                        catch { }
                        SOAInv[j].StoreID = AppGeneralSettings.StoreID;
                    }




                    try 
                    {
                        raffected += AppGeneralSettings.webServiceProvider.ImportInventoryCType(SOAInv, clearprevious);
                    }
                    catch (Exception ex) 
                    {
                        MessageBox.Show("ImportInventoryCType"+ex.Message);
                    
                    }

                    

                    if ((rset - 1) > invrows) 
                    {

                        LBExpInvRows.Text = "Εξαγωγή " + invrows.ToString() + " από " + invrows.ToString();
                    }
                    else
                    {
                        LBExpInvRows.Text = "Εξαγωγή " + (rset - 1).ToString() + " από " + invrows.ToString();
                    }
                    Application.DoEvents();
                    clearprevious = false;
                }
                else
                    Logger.Flog("ExportData.ExportInventory>>" + sqlstr);

            }

            if (raffected > 0) return invrows;

            return raffected;
        }
    }
}