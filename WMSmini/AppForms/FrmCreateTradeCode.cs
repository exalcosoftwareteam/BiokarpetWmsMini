using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WMSMobileClient;
using WMSMobileClient.Components;
using WMSMobileClient.WMSSyncService;


namespace WMSMobileClient
{
    public partial class FrmCreateTradeCode : Form
    {   
        PackingListDetailHandler packhandler = new PackingListDetailHandler();
        PackingListHeaderHandler packheaderhandler = new PackingListHeaderHandler();
        DB db = new DB();
        public FrmCreateTradeCode()
        {
            InitializeComponent();
        }


        private void FrmCreateTradeCode_Load(object sender, EventArgs e)
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
            AppGeneralSettings.TransType = 0;
            AppGeneralSettings.CustomerCode = null;


           if (MessageBox.Show("Η διακίνηση θα αποσταλεί στο Atlantis ,θέλετε να συνεχίσετε;", "Ερώτηση", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {

                ExportData();

                }
               
                        

        }

        
        
        #endregion

    

        protected void CheckInvHdrID()
        {
            if (!(Program.iInvHeader.InvHdrID > 0))          
                GoBack();  
        }

        protected void ExportData()
        {
            long exprows = 0;
            long PackingListHeaderID=0;
            ExportData exportdata = new ExportData();
            PackingListHeaderHandler packheader = new PackingListHeaderHandler();
            Cursor.Current = Cursors.WaitCursor;

            if (packheader.CheckPackingListStatus(Program.iPackHeader.PackingListHeaderID) > 0) 
            {
                if (MessageBox.Show("Η διακίνηση έχει ήδη αποσταλεί , θέλετε να το ξαναστείλετe;", "Ερώτηση", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No) 
                {
                    Cursor.Current = Cursors.Default;
                    return;
                                    
                }
   
            }

            //PackingListHeaderID = exportdata.ExportPackingListHeader(Program.iPackHeader.PackingListHeaderID);

            //if (!(PackingListHeaderID > 0))
            //{
            //    if (PackingListHeaderID == -10)
            //        MessageBox.Show("Πρόβλημα Επικοινωνίας με την υπηρεσία!");
            //    else
            //        MessageBox.Show("Πρόβλημα με την εξαγωγή Δεδομένων!");

            //    ImgExportInventory.Visible = false;
            //    LBExpInvRows.Visible = false;

            //    Cursor.Current = Cursors.Default;

            //    return;
            //}


           exprows = exportdata.ExportPackingList(Program.iPackHeader.PackingListHeaderID, PackingListHeaderID);

            //ImgExportInventory.Visible = true;
            //LBExpInvRows.Visible = true;
            //LBExportInventory.Visible = true;

            if (exprows > 0)
            {
                ImgExportInventory.Image = Properties.Resources.ok;

                LBExpInvRows.Text = "Εξαγωγή (" + exprows.ToString() + " ) εγγραφές";
                packheader.SetPackingListHeaderStatus(Program.iPackHeader.PackingListHeaderID,1);
                
            }
            else
            {
                ImgExportInventory.Image = Properties.Resources.error;
                LBExpInvRows.Text = "Εξαγωγή ( 0 ) εγγραφές";
            }

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
            WMSForms.FrmCreateTradeCode = new FrmCreateTradeCode();
            WMSForms.FrmCreateTradeCode.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Θα δημιουργθεί παραστατικό στο Atlantis , συνέχεια ?", "Ερώτηση", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
    
                PackingListHeader thispackheader = new PackingListHeader();
                PackingHeader localpackheader = new PackingHeader();

                localpackheader = packheaderhandler.Parse(db.DBFillDataTable("SELECT * FROM TWMSPackingListsHeader WHERE PackingListHeaderID=" + Program.iPackHeader.PackingListHeaderID, "Tpack").Rows[0]);
                thispackheader.Branchid = localpackheader.Branchid;
                thispackheader.Compid = localpackheader.Compid;
                thispackheader.CustomerCode = localpackheader.CustomerCode;
                thispackheader.CustomerTitle = localpackheader.CustomerTitle;
                thispackheader.Kindid = localpackheader.Kindid;
                thispackheader.Packinglistcomments = localpackheader.Packinglistcomments;
                thispackheader.PackingListDate = localpackheader.PackingListDate;
                thispackheader.PackingListHeaderID = localpackheader.PackingListHeaderID;
                thispackheader.Packinglistserverid = localpackheader.Packinglistserverid;
                thispackheader.PackingListStatus = localpackheader.PackingListStatus;
                thispackheader.StoreID = localpackheader.StoreID;
                thispackheader.StoreName = localpackheader.StoreName;
                thispackheader.TransCode = localpackheader.TransCode;
                thispackheader.TransType = localpackheader.TransType;

                try
                {
                    AppGeneralSettings.WebSyncServiceProvider.CreateTradeCodeOnTheFly(thispackheader, packhandler.GetPackingListByHeader(Program.iPackHeader.PackingListHeaderID),
                    AppGeneralSettings.CompID, AppGeneralSettings.BranchID, AppGeneralSettings.StoreID, int.Parse(TBdsrid.Text), 23);
                }
                catch(Exception ex) { }


                //SYNEXEIA APO EDO
                
                //ExportData();

            }
        }


    }
}