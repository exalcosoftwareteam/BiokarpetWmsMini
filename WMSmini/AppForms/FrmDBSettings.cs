using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WMSMobileClient.Components;


namespace WMSMobileClient
{
    public partial class FrmDBSettings : Form
    {
        CompactDB localDB = new CompactDB();
 
        public FrmDBSettings()
        {
            InitializeComponent();

            FixResolutionIssues();

            GetSettings();
        }

        

        private void FrmDBSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                GoBack();
            }            
        }
        
        private void BtnCreateDB_Click(object sender, EventArgs e)
        {
            CreateDB();
        }

        private void BtnDeleteDB_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            
            DeleteDB();
            Cursor.Current = Cursors.Default;
        }

        private void BtnDeleteDB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                GoBack();
        }

        private void BtnCreateDB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                GoBack();
        }

        private void PBoxEsc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                GoBack();
        }

       

        protected void GetSettings()
        {
           
            if (localDB.FileExists())
            {
                LBPathInfo.Text = localDB.DBFilePAth;
                LBDBSize.Text = localDB.DBSizeInKB().ToString() + " KB";
                BtnDeleteDB.Visible = true;
                BtnCreateDB.Visible = false;
            }
            else
            {
                BtnDeleteDB.Visible = false;
                BtnCreateDB.Visible = true;
            }

        }

        protected void GoToSettings()
        {
            Cursor.Current = Cursors.WaitCursor;

            WMSForms.FrmSettings = new FrmSettings();
            WMSForms.FrmSettings.Show();

            this.Close();

            Cursor.Current = Cursors.Default;
        }

        protected void GoBack()
        {
            GoToSettings();
        }

        protected void CreateDB()
        {
            
            localDB.CreateDB();
            localDB.CreateTables();
            GetSettings();
        }

        protected void DeleteDB()
        {
            if (MessageBox.Show("Είστε Βέβαιοι για την Διαγραφή;", "Ερώτηση", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {

                localDB.db.DBDisConnect();
 
                //SQLDBConnection.Close();
                if (localDB.DeleteDB())
                {
                    BtnCreateDB.Visible = true;
                    BtnDeleteDB.Visible = false;

                    LBPathInfo.Text = "";
                    LBDBSize.Text = "";
                }
            }
            
        }


        protected void FixResolutionIssues()
        {

            string oldpos = PBMenuBar.Location.X.ToString() + "X" + PBMenuBar.Location.Y.ToString();
            int oldbtny = PBBtnBck.Location.Y - PBMenuBar.Location.Y;

            if (Screen.PrimaryScreen.Bounds.Width > 240)
            {
                

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

        private void FrmDBSettings_Load(object sender, EventArgs e)
        {

        }


       
                                               
    }
}